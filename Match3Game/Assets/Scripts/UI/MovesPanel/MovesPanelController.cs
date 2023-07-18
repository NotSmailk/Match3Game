public class MovesPanelController : IMovesPanelController
{
    private IMovesPanelModel _model;
    private IMovesPanelView _view;

    public MovesPanelController(IMovesPanelView view, IMovesPanelModel model)
    {
        _model = model;
        _view = view;
    }

    public void MoveUpdate()
    {
        _model.AddMove();
    }

    public void PanelInit(int moves)
    {
        _model.InitMoves(moves);
    }
}
