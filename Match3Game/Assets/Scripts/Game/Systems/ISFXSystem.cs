public interface ISFXSystem
{
    public void ResetPitch();
    public void AddToPitch(float value);
    public void CandyMoveDone();
    public void CandyMoveCancel();
    public void CandyTouch();
    public void CandyBreak();
    public void CandyBreak(float pitch);
    public void ButtonClick();
    public void Victory();
    public void Defeat();
}
