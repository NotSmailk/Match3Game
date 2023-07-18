using System;
using UnityEngine;

public class PilesFactory : MonoBehaviour, IPilesFactory<Pile>
{
    [field: SerializeField] private Pile _pilePrefab;

    public Pile CreateItem(Vector3 position)
    {
        var pile = Instantiate(_pilePrefab);
        pile.transform.SetParent(transform);
        pile.Init(position);
        return pile;
    }

    public IFactoryItem CreateItem(Type type, object[] parametres) => default;
}