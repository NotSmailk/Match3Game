using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    public void CollapsePile(GridPosition position);
    public void Add(Pile pile);
    public bool HasMatches(GridPosition position);
    public List<GridPosition> GetMatches(GridPosition position, CandyType type);
    public Vector3 GetPos(GridPosition pos);
    public Pile GetPile(GridPosition pos);
    public List<Pile> GetCollumn(GridPosition pos);
    public void SetPileType(GridPosition position, CandyType type);
}
