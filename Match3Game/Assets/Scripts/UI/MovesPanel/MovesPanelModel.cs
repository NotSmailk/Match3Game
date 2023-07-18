public class MovesPanelModel : IMovesPanelModel
{
    private int _moves = 0;
    private int _totalMoves = 0;
    private IMovesPanelView _view;

    public MovesPanelModel(IMovesPanelView view)
    {
        _view = view;
    }

    public void InitMoves(int moves)
    {
        _totalMoves = moves;
        _view.DisplayMoves(_moves, _totalMoves);
    }

    public void AddMove()
    {
        _moves++;
        _view.DisplayMoves(_moves, _totalMoves);
    }
}
