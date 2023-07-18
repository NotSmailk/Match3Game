using UnityEngine.Events;

public class LevelStatusSystem : ILevelStatusSystem
{
    public CandyType TargetType { get; set; }
    public int TargetDestroyed { get; set; }
    public int CurrentMoves { get; set; }
    public float CurrentScore { get; set; }

    public UnityEvent OnVictory { get; set; } = new UnityEvent();
    public UnityEvent OnDefeat { get; set; } = new UnityEvent();
    public UnityEvent<float> OnGetScore { get; set; } = new UnityEvent<float>();
    public UnityEvent OnMoveUpdate { get; set; } = new UnityEvent();
    public UnityEvent OnTaskUpdate { get; set; } = new UnityEvent();

    private GameParametres _parametres;

    public LevelStatusSystem(GameParametres parametres)
    {
        _parametres = parametres;
    }

    public void MovesUpdate()
    {
        CurrentMoves++;
    }

    public void ScoreUpdate(float score)
    {
        CurrentScore += score;
    }

    public void TaskUpdate()
    {
        TargetDestroyed++;
    }

    public void CheckGameStatus(GameState state)
    {
        if (state.Equals(GameState.Updating))
        {
            if (TargetDestroyed >= _parametres.CandiesCount)
            {
                OnVictory.Invoke();
                return;
            }

            if (CurrentMoves >= _parametres.Moves)
            {
                OnDefeat.Invoke();
                return;
            }
        }
    }
}
