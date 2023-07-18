using UnityEngine;
using UnityEngine.Events;

public class GameUI : MonoBehaviour, IGameUI
{
    [field: SerializeField] private ScorePanelView _scorePanelView;
    [field: SerializeField] private MovesPanelView _movesPanelView;
    [field: SerializeField] private TaskPanelView _taskPanelView;
    [field: SerializeField] private LevelEndView _levelEndView;

    private IScorePanelController _scorePanelController;
    private IMovesPanelController _movesPanelController;
    private ITaskPanelContoller _taskPanelContoller;
    private ILevelEndController _levelEndController;

    public void Init(CandyData data, int count, int moves)
    {
        _scorePanelController = new ScorePanelController(_scorePanelView, new ScorePanelModel(_scorePanelView));
        _movesPanelController = new MovesPanelController(_movesPanelView, new MovesPanelModel(_movesPanelView));
        _taskPanelContoller = new TaskPanelController(_taskPanelView, new TaskPanelModel(_taskPanelView));
        _levelEndController = new LevelEndContoller(_levelEndView, new LevelEndModel(_levelEndView));

        _levelEndController.ShowPanel(false);
        _scorePanelController.ScoreUpdate(0f);
        _movesPanelController.PanelInit(moves);
        _taskPanelContoller.TaskInit(data, count);
    }

    public void AddScore(float score)
    {
        _scorePanelController.ScoreUpdate(score);
    }

    public void MovesUpdate()
    {
        _movesPanelController.MoveUpdate();
    }

    public void TaskUpdate()
    {
        _taskPanelContoller.TaskUpdate();
    }

    public void EndLevel(string info, float score)
    {
        _levelEndController.ShowLevelEndPanel(info, score);
        gameObject.SetActive(false);
    }

    public void InitLevelEndEvents(UnityAction exit, UnityAction replay, UnityAction next)
    {
        _levelEndController.SetListeners(exit, replay, next);
    }
}
