using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] // Debuging
public class Grid : IGrid
{
    private List<Pile> _piles = new List<Pile>();
    private int columns = 0;
    private int rows = 0;

    public Grid(int r, int c)
    {
        columns = c;
        rows = r;   
    }

    public void Add(Pile pile)
    {
        _piles.Add(pile);
    }

    public Vector3 GetPos(GridPosition pos)
    {
        Vector3 outPos = Vector3.zero;

        foreach (var pile in _piles)
        {
            if (pile.Pos == pos)
                outPos = pile.WorldPosition;
        }

        return outPos;
    }

    public void CollapsePile(GridPosition position)
    {
        var pile = GetPile(position.Row, position.Column);
        _piles[_piles.IndexOf(pile)].SetValue(CandyType.None);
    }

    public bool HasMatches(GridPosition position)
    {
        return VerticalMatches(position) | HorizontalMatches(position);
    }

    private bool HorizontalMatches(GridPosition position)
    {
        int matches = 0;
        var type = GetPile(position).Type;

        for (int r = position.Row - 1; r >= 0; r--)
        {
            var pile = GetPile(r, position.Column);

            if (pile.Type.Equals(type))
                matches++;
            else
                break;
        }

        for (int r = position.Row + 1; r < rows; r++)
        {
            var pile = GetPile(r, position.Column);

            if (pile.Type.Equals(type))
                matches++;
            else
                break;
        }

        return matches >= 2;
    }

    private List<Pile> GetHorizontalMatches(GridPosition position, CandyType type)
    {
        var piles = new List<Pile>();

        for (int r = position.Row - 1; r >= 0; r--)
        {
            var pile = GetPile(r, position.Column);

            if (pile.Type.Equals(type))
                piles.Add(pile);
            else
                break;
        }

        for (int r = position.Row + 1; r < rows; r++)
        {
            var pile = GetPile(r, position.Column);

            if (pile.Type.Equals(type))
                piles.Add(pile);
            else
                break;
        }

        if (piles.Count < 2)
            piles.Clear();

        return piles;
    }

    private bool VerticalMatches(GridPosition position)
    {
        int matches = 0;
        var type = GetPile(position).Type;

        for (int c = position.Column - 1; c >= 0; c--)
        {
            var pile = GetPile(position.Row, c);

            if (pile.Type.Equals(type))
                matches++;
            else
                break;
        }

        for (int c = position.Column + 1; c < columns; c++)
        {
            var pile = GetPile(position.Row, c);

            if (pile.Type.Equals(type))
                matches++;
            else
                break;
        }

        return matches >= 2;
    }

    private List<Pile> GetVerticalMatches(GridPosition position, CandyType type)
    {
        var piles = new List<Pile>();

        for (int c = position.Column - 1; c >= 0; c--)
        {
            var pile = GetPile(position.Row, c);

            if (pile.Type.Equals(type))
                piles.Add(pile);
            else
                break;
        }

        for (int c = position.Column + 1; c < columns; c++)
        {
            var pile = GetPile(position.Row, c);

            if (pile.Type.Equals(type))
                piles.Add(pile);
            else
                break;
        }

        if (piles.Count < 2)
            piles.Clear();

        return piles;
    }

    public List<GridPosition> GetMatches(GridPosition position, CandyType type)
    {
        var matches = new List<GridPosition>();

        matches.Add(position);

        foreach (var match in GetHorizontalMatches(position, type))
            matches.Add(match.Pos);

        foreach (var match in GetVerticalMatches(position, type))
            matches.Add(match.Pos);

        if (matches.Count < 3)
            matches.Clear();

        var sortedC = from m in matches orderby m.Column select m;
        var sortedR = from m in matches orderby m.Row select m;

        return sortedR.ToList();
    }

    public void SetPileType(GridPosition position, CandyType type)
    {
        foreach (var pile in _piles)
        {
            if (pile.Pos == position)
            {
                pile.SetValue(type);
            }
        }
    }

    private Pile GetPile(int r, int c)
    {
        foreach(var pile in _piles)
        {
            if (pile.Pos.Row == r && pile.Pos.Column == c)
                return pile;
        }

        return null;
    }

    public Pile GetPile(GridPosition pos)
    {
        Pile outPile = null;

        foreach (var pile in _piles)
        {
            if (pile.Pos == pos)
                outPile = pile;
        }

        return outPile;
    }

    public List<Pile> GetCollumn(GridPosition pos)
    {
        var lastPile = GetPile(pos);
        var column = new List<Pile>();

        foreach (var pile in _piles)
        {
            if (pile.Pos.Row <= lastPile.Pos.Row && pile.Pos.Column == lastPile.Pos.Column)
                column.Add(pile);
        }

        var sortedColumn = from c in column orderby c.Pos.Column select c;
        return sortedColumn.ToList();
    }
}
