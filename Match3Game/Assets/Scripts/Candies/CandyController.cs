public class CandyController : ICandyController
{
    private ICandyModel _model;
    private ICandyView _view;

    public CandyController(ICandyModel model, ICandyView view)
    {
        _model = model;
        _view = view;
    }

    public void Collapse()
    {
        _view.DisplayDestroyedCandy();
    }

    public bool Contains(ICandyView view)
    {
        return _view == view;
    }

    public void Deselect()
    {
        _model.SetSelectedState(false);
    }

    public IGridSelectable GetModelInfo()
    {
        return _model;
    }

    public void Move(GridPosition position)
    {
        _model.SetPositionState(position);
    }

    public void Select()
    {
        _model.SetSelectedState(true);
    }
}
