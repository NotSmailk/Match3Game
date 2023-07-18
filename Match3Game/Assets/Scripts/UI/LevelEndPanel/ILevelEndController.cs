using UnityEngine.Events;

public interface ILevelEndController : IController
{
    public void ShowLevelEndPanel(string info, float score);
    public void SetListeners(UnityAction exit, UnityAction replay, UnityAction next);
    public void ShowPanel(bool show);
}
