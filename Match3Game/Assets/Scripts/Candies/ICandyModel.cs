using UnityEngine;

public interface ICandyModel : IModel, IGridSelectable
{
    public void SetSelectedState(bool select);
    public void SetPositionState(GridPosition position);
}
