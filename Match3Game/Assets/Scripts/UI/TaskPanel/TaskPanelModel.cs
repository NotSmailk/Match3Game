public class TaskPanelModel : ITaskPanelModel
{
    private ITaskPanelView _view;
    private int _count;
    private int _totalCount;

    public TaskPanelModel(ITaskPanelView view)
    {
        _view = view;
    }

    public void InitTask(CandyData data, int count)
    {
        _totalCount = count;
        _view.DisplayTaskValues(data.Sprite, count);
    }

    public void UpdateTask()
    {
        _count++;
        _view.DisplayTaskText(_count, _totalCount);
    }
}
