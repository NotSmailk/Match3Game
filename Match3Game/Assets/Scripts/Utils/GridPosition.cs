[System.Serializable]
public class GridPosition
{
    public int Row { get; set; }
    public int Column { get; set; }
    public UnityEngine.Vector3 Position { get; set; }

    public GridPosition(int r, int c)
    {
        Row = r;
        Column = c;
    }

    public GridPosition(int r, int c ,UnityEngine.Vector3 pos)
    {
        Row = r;
        Column = c;
        Position = pos;
    }

    public GridPosition(GridPosition old)
    {
        Row = old.Row;
        Column = old.Column;
        Position = old.Position;
    }

    public static bool operator == (GridPosition p1, GridPosition p2)
    {
        return p1.Row == p2.Row && p1.Column == p2.Column;
    }

    public static bool operator !=(GridPosition p1, GridPosition p2)
    {
        return p1.Row != p2.Row || p1.Column != p2.Column;
    }

    public override bool Equals(object obj)
    {
        bool isPos = obj is GridPosition;

        if (!isPos)
            return false;

        return this == (GridPosition)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Row}, {Column}";
    }
}
