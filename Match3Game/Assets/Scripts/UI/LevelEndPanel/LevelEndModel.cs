public class LevelEndModel : ILevelEndModel
{
    private bool _activity;
    private ILevelEndView _view;

    public LevelEndModel(ILevelEndView view)
    {
        _view = view;
    }

    public void SetActivity(bool activity)
    {
        _activity = activity;
        _view.ShowPanel(activity);
    }

    public void SetInfo(string info, float score)
    {
        _view.DisplayResultInfo(info, score);
    }
}
