public interface ICandyController : IController
{
    public IGridSelectable GetModelInfo();
    public void Select();
    public void Deselect();
    public void Move(GridPosition position);
    public void Collapse();
    public bool Contains(ICandyView view);
}
