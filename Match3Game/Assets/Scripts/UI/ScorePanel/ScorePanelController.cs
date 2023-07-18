public class ScorePanelController : IScorePanelController
{
    private IScorePanelModel _model;
    private IScorePanelView _view;

    public ScorePanelController(IScorePanelView view, IScorePanelModel model)
    {
        _model = model;
        _view = view;
    }

    public void ScoreUpdate(float score)
    {
        _model.AddScore(score);
    }
}
