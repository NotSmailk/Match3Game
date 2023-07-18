using System.Collections.Generic;
using System.Linq;

public class GridControlSystem : IGridControlSystem
{
    private IGrid _grid;

    public GridControlSystem(IGrid grid)
    {
        _grid = grid;
    }

    public bool HasMatches(ICandyController g1, ICandyController g2)
    {
        return _grid.HasMatches(g1.GetModelInfo().GridPosition)
            || _grid.HasMatches(g2.GetModelInfo().GridPosition);
    }

    public void SwapGridElements(ICandyController g1, ICandyController g2)
    {
        _grid.SetPileType(g1.GetModelInfo().GridPosition, g2.GetModelInfo().CandyType);
        _grid.SetPileType(g2.GetModelInfo().GridPosition, g1.GetModelInfo().CandyType);

        var pos = new GridPosition(g1.GetModelInfo().GridPosition);
        g1.Move(g2.GetModelInfo().GridPosition);
        g2.Move(pos);
    }

    public bool IsNeighboor(ICandyController selectable, ICandyController selected)
    {
        var g1 = selectable.GetModelInfo().GridPosition;
        var g2 = selected.GetModelInfo().GridPosition;

        bool fromUp = (g1.Row - 1) == g2.Row && g1.Column == g2.Column;
        bool fromBottom = (g1.Row + 1) == g2.Row && g1.Column == g2.Column;
        bool fromLeft = (g1.Column + 1) == g2.Column && g1.Row == g2.Row;
        bool fromRight = (g1.Column - 1) == g2.Column && g1.Row == g2.Row;

        return fromBottom || fromLeft || fromRight || fromUp;
    }

    public List<ICandyController> GetColumn(ICandyController candy, List<ICandyController> controllers)
    {
        var column = new List<ICandyController>();
        var pos = candy.GetModelInfo().GridPosition;

        foreach (var controller in controllers)
        {
            if (controller.GetModelInfo().GridPosition.Row < pos.Row && controller.GetModelInfo().GridPosition.Column == pos.Column)
                column.Add(controller);
        }

        var sortedColumn = from c in column orderby c.GetModelInfo().GridPosition.Row select c;
        return sortedColumn.ToList();
    }
}
