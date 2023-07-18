public class TaskPanelController : ITaskPanelContoller
{
    private ITaskPanelModel _model;
    private ITaskPanelView _view;

    public TaskPanelController(ITaskPanelView view, ITaskPanelModel model)
    {
        _view = view;
        _model = model;
    }

    public void TaskInit(CandyData data, int count)
    {
        _model.InitTask(data, count);
    }

    public void TaskUpdate()
    {
        _model.UpdateTask();
    }
}
