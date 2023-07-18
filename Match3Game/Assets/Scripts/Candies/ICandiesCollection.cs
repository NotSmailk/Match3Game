using System.Collections.Generic;
using UnityEngine;

public interface ICandiesCollection
{
    public List<ICandyView> Views { get; }
    public List<ICandyController> Controllers { get; }

    public ICandyController GetControllerFromGridPos(GridPosition pos);
    public ICandyController GetControllerFromView(ICandyView view);
    public ICandyController CreateCandy(CandyData data, Vector3 worldPos, GridPosition gridPosition);
    public void Remove(ICandyController candy);
}
