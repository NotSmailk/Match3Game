using UnityEngine;

public interface IPilesFactory<IPile> : IFactory
{
    IPile CreateItem(Vector3 position);
}
