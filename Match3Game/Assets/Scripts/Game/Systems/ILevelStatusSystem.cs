using UnityEngine.Events;

public interface ILevelStatusSystem
{
    public CandyType TargetType { get; set; }
    public int TargetDestroyed { get; set; }
    public int CurrentMoves { get; set; }
    public float CurrentScore { get; set; }
    public UnityEvent OnVictory { get; set; }
    public UnityEvent OnDefeat { get; set; }
    public UnityEvent<float> OnGetScore { get; set; }
    public UnityEvent OnMoveUpdate { get; set; }
    public UnityEvent OnTaskUpdate { get; set; }

    public void ScoreUpdate(float score);
    public void MovesUpdate();
    public void TaskUpdate();
    public void CheckGameStatus(GameState state);
}
