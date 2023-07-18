using UnityEngine.UI;

public interface ILevelEndView : IView
{
    public Button ExitButton { get; set; }
    public Button ReplayLevelButton { get; set; }
    public Button NextLevelButton { get; set; }

    public void DisplayResultInfo(string info, float score);
    public void ShowPanel(bool show);
}