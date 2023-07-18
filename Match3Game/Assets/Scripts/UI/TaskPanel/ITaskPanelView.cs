using UnityEngine;

public interface ITaskPanelView : IView
{
    public void DisplayTaskValues(Sprite sprite, int count);
    public void DisplayTaskText(int count, int totalCount);
}
