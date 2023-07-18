public class ScorePanelModel : IScorePanelModel
{
    private float _score = 0;
    private IScorePanelView _view;

    public ScorePanelModel(IScorePanelView view)
    {
        _view = view;
    }

    public void AddScore(float score)
    {
        _score += score;
        _view.DisplayScore(_score);
    }
}
