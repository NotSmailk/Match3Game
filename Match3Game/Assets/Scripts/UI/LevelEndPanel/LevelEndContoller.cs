using UnityEngine.Events;

public class LevelEndContoller : ILevelEndController
{
    private ILevelEndView _view;
    private ILevelEndModel _model;

    public LevelEndContoller(ILevelEndView view, ILevelEndModel model)
    {
        _view = view;
        _model = model;
    }

    public void SetListeners(UnityAction exit, UnityAction replay, UnityAction next)
    {
        _view.ExitButton.onClick.AddListener(exit);
        _view.ReplayLevelButton.onClick.AddListener(replay);
        _view.NextLevelButton.onClick.AddListener(next);
    }

    public void ShowLevelEndPanel(string info, float score)
    {
        _model.SetActivity(true);
        _model.SetInfo(info, score);
    }

    public void ShowPanel(bool show)
    {
        _model.SetActivity(show);
    }
}
