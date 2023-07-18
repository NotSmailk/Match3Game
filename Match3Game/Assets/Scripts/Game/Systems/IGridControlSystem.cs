using System.Collections.Generic;

public interface IGridControlSystem
{
    public bool HasMatches(ICandyController g1, ICandyController g2);
    public void SwapGridElements(ICandyController g1, ICandyController g2);
    public bool IsNeighboor(ICandyController selectable, ICandyController selected);
    public List<ICandyController> GetColumn(ICandyController candy, List<ICandyController> controllers);
}
