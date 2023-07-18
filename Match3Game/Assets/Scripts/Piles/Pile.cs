using UnityEngine;

public class Pile : MonoBehaviour, IPile
{
    private GridPosition _pos;
    private CandyType _type;

    public GridPosition Pos => _pos;
    public CandyType Type => _type;
    public Vector3 WorldPosition => transform.position;

    public void SetPos(GridPosition p)
    {
        _pos = p;
    }

    public void SetValue(CandyType t)
    {
        _type = t;
    }

    public void Init(Vector3 position)
    {
        transform.localPosition = position;
    }
}
