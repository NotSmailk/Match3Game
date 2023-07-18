using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Game : MonoBehaviour
{
    [field: SerializeField] private GameParametres _gameParametres;
    [field: SerializeField] private SoundParametres _soundParametres;
    [field: SerializeField] private CandiesData _candiesData;
    [field: SerializeField] private PilesFactory _pilesFactory;
    [field: SerializeField] private CandyViewFactory _candyViewFactory;
    [field: SerializeField] private Transform _tablePoint;
    [field: SerializeField] private GameUI _gameUI;

    private IGrid _grid;
    private IGridControlSystem _gridControlSystem;
    private ILevelStatusSystem _levelStatusSystem;
    private ISceneControlSystem _sceneControlSystem;
    private ISFXSystem _sfxSystem;
    private ICandiesCollection _candiesCollection;
    private ICandyController _selectedObject;
    private GameState _state = GameState.Updating;

    private void Awake()
    {
        InitGame();
    }

    private void Update()
    {
        UpdateGame();
    }

    public void InitGame()
    {
        _grid = new Grid(_gameParametres.Rows, _gameParametres.Columns);
        _gridControlSystem = new GridControlSystem(_grid);
        _levelStatusSystem = new LevelStatusSystem(_gameParametres);
        _sfxSystem = new SFXSystem(_soundParametres);
        _candiesCollection = new CandiesCollection(_candyViewFactory);
        _sceneControlSystem = new SceneControlSystem();

        var targetData = _candiesData.GetRandom();
        _levelStatusSystem.TargetType = targetData.CandyType;
        _gameUI.Init(targetData, _gameParametres.CandiesCount, _gameParametres.Moves);
        _gameUI.InitLevelEndEvents(ExitLevel, ReplayLevel, NextLevel);

        InitEvents();
        InitTable();
    }

    private void InitEvents()
    {
        _levelStatusSystem.OnGetScore.AddListener(_levelStatusSystem.ScoreUpdate);
        _levelStatusSystem.OnGetScore.AddListener(_gameUI.AddScore);

        _levelStatusSystem.OnMoveUpdate.AddListener(_levelStatusSystem.MovesUpdate);
        _levelStatusSystem.OnMoveUpdate.AddListener(_gameUI.MovesUpdate);
        _levelStatusSystem.OnMoveUpdate.AddListener(_sfxSystem.CandyMoveDone);

        _levelStatusSystem.OnTaskUpdate.AddListener(_levelStatusSystem.TaskUpdate);
        _levelStatusSystem.OnTaskUpdate.AddListener(_gameUI.TaskUpdate);

        _levelStatusSystem.OnVictory.AddListener(Victory);
        _levelStatusSystem.OnDefeat.AddListener(Defeat);
    }

    private void InitTable()
    {
        var rOffset = _gameParametres.Rows % 2f == 0 ? 0.5f : 0f;
        var cOffset = _gameParametres.Columns % 2f == 0 ? 0.5f : 0f;

        for (int c = 0; c < _gameParametres.Columns; c++)
        {
            for (int r = 0; r < _gameParametres.Rows; r++)
            {
                var gridPos = new GridPosition(r, c);
                var x = ((c - _gameParametres.Columns / 2 + cOffset) * _gameParametres.OffsetCoef) + _tablePoint.position.x;
                var y = -((r - _gameParametres.Rows / 2 + rOffset) * _gameParametres.OffsetCoef) + _tablePoint.position.y;
                Vector3 pos = new Vector3(x, y, 0f);
                gridPos.Position = pos;

                var pile = _pilesFactory.CreateItem(pos);
                pile.SetPos(gridPos);
                _grid.Add(pile);

                var data = _candiesData.GetRandom();
                pile.SetValue(data.CandyType);
                _candiesCollection.CreateCandy(data, pile.WorldPosition, gridPos);
            }
        }

        _tablePoint.localScale = new Vector3(_gameParametres.Columns, _gameParametres.Rows, 1f);
    }

    public async void UpdateGame()
    {
        await InputProcessing();
        await StateUpdate();
        _levelStatusSystem.CheckGameStatus(_state);
    }

    private async Task InputProcessing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _state.Equals(GameState.Updating))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit && hit.transform.TryGetComponent(out ICandyView view))
            {
                var selectable = _candiesCollection.GetControllerFromView(view);
                _selectedObject?.Deselect();
                await CandyProcessing(selectable);
            }
        }
    }

    private async Task StateUpdate()
    {
        if (_state.Equals(GameState.Busy))
        {
            _state = await ResetPileValues() ? GameState.Busy : GameState.Updating;

            if (_state.Equals(GameState.Updating))
                _sfxSystem.ResetPitch();
        }
    }

    private async Task CandyProcessing(ICandyController selectable)
    {
        if (_selectedObject != selectable)
        {
            if (_selectedObject != null && _gridControlSystem.IsNeighboor(selectable, _selectedObject))
            {
                _gridControlSystem.SwapGridElements(_selectedObject, selectable);

                if (_gridControlSystem.HasMatches(selectable, _selectedObject))
                {
                    _levelStatusSystem.OnMoveUpdate.Invoke();
                    await TryToCollapse(selectable, _selectedObject);
                }
                else
                {
                    _gridControlSystem.SwapGridElements(_selectedObject, selectable);
                    _sfxSystem.CandyTouch();
                }

                _selectedObject = null;
            }
            else
            {
                _sfxSystem.CandyTouch();
                selectable.Select();
                _selectedObject = selectable;
            }
        }
        else
        {
            _sfxSystem.CandyTouch();
            _selectedObject = null;
        }
    }

    private async Task TryToCollapse(ICandyController s1, ICandyController s2)
    {
        _state = GameState.Collapsing;
        await Task.Delay(200);

        List<GridPosition> matches1 = _grid.GetMatches(s1.GetModelInfo().GridPosition, s1.GetModelInfo().CandyType);
        List<GridPosition> matches2 = new List<GridPosition>();

        if (!matches1.Contains(s2.GetModelInfo().GridPosition))
            matches2 = _grid.GetMatches(s2.GetModelInfo().GridPosition, s2.GetModelInfo().CandyType);

        await CollapseAll(matches1);
        await CollapseAll(matches2);
        _state = GameState.Busy;
    }
    
    private void Collapse(ICandyController candy)
    {
        _grid.CollapsePile(candy.GetModelInfo().GridPosition);
        _candiesCollection.Remove(candy);

        if (candy.GetModelInfo().CandyType.Equals(_levelStatusSystem.TargetType))
            _levelStatusSystem.OnTaskUpdate.Invoke();

        var lastPos = candy.GetModelInfo().GridPosition;
        var candies = _gridControlSystem.GetColumn(candy, _candiesCollection.Controllers);
        var pileCol = _grid.GetCollumn(lastPos);
        var firstPos = new GridPosition(pileCol[0].Pos);
        _gameParametres.InstantiateFX(candy, lastPos);
        candy.Collapse();

        if (candies.Count > 0)
        {
            for (int i = 0; i < candies.Count; i++)
            {
                var pos = new GridPosition(lastPos);
                pos.Position = lastPos.Position;
                lastPos = pileCol[^(i + 2)].Pos;
                candies[^(i + 1)].Move(pos);
                _grid.SetPileType(candies[^(i + 1)].GetModelInfo().GridPosition, candies[^(i + 1)].GetModelInfo().CandyType);
            }
        }

        for (int i = pileCol.Count - 1 - candies.Count; i >= 0; i--)
        {
            var data = _candiesData.GetRandom();
            var worldPosition = _grid.GetPos(firstPos);
            worldPosition.y += 1f;
            var controller = _candiesCollection.CreateCandy(data, worldPosition, pileCol[i].Pos);
            _grid.SetPileType(controller.GetModelInfo().GridPosition, data.CandyType);
            controller.Move(pileCol[i].Pos);
        }
    }

    private async Task CollapseAll(List<GridPosition> matches)
    {
        if (matches.Count >= 3)
        {
            float addPitch = matches.Count > 3 ? 0.1f : 0f;
            float score = 50 * matches.Count;

            _levelStatusSystem.OnGetScore.Invoke(score);
            _sfxSystem.CandyBreak(addPitch);
            _state = GameState.Collapsing;
            foreach (var match in matches)
                Collapse(_candiesCollection.GetControllerFromGridPos(match));
        }        

        await Task.Delay(200);
    }

    private async Task<bool> ResetPileValues()
    {
        bool hasMatches = false;

        for (int c = 0; c < _gameParametres.Columns; c++)
        {
            for (int r = 0; r < _gameParametres.Rows; r++)
            {
                var pos = new GridPosition(r, c);
                if (_grid.HasMatches(pos))
                {
                    var matches = _grid.GetMatches(pos, _grid.GetPile(pos).Type);
                    await CollapseAll(matches);
                    _sfxSystem.AddToPitch(0.1f);
                    hasMatches = true;
                }
            }
        }

        _state = GameState.Busy;
        return hasMatches;
    }

    private void HideGameTable()
    {
        _candyViewFactory.gameObject.SetActive(false);
        _pilesFactory.gameObject.SetActive(false);
        _tablePoint.gameObject.SetActive(false);
    }

    private void Victory()
    {
        _state = GameState.Finished;
        HideGameTable();
        _sfxSystem.Victory();
        _gameUI.EndLevel(GameConstants.KeyWords.LEVEL_CLEARED, _levelStatusSystem.CurrentScore);
    }

    private void Defeat()
    {
        _state = GameState.Finished;
        HideGameTable();
        _sfxSystem.Defeat();
        _gameUI.EndLevel(GameConstants.KeyWords.DEFEAT, _levelStatusSystem.CurrentScore);
    }

    private void ExitLevel()
    {
        _sfxSystem.ButtonClick();
        _sceneControlSystem.QuitApplication();
    }

    private void ReplayLevel()
    {
        _sfxSystem.ButtonClick();
        _sceneControlSystem.LoadScene(GameConstants.Scenes.TEST_SCENE);
    }

    private void NextLevel()
    {
        _sfxSystem.ButtonClick();
        _sceneControlSystem.LoadScene(GameConstants.Scenes.TEST_SCENE);
    }
}
