public class CandyModel : ICandyModel
{
    public CandyType CandyType { get; set; }
    public GridPosition GridPosition { get; set; }
    
    private ICandyView _view;

    public CandyModel(ICandyView view, CandyData candyData, GridPosition position)
    {
        CandyType = candyData.CandyType;
        GridPosition = position;
        _view = view;
    }

    public void SetSelectedState(bool select)
    {
        if (select)
            _view.DisplaySelectedCandy();
        else
            _view.DisplayDeselectedCandy();
    }

    public void SetPositionState(GridPosition position)
    {
        GridPosition = position;
        _view.DisplayMoveOfCandy(position);
    }
}
