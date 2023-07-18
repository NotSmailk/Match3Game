using System;
using UnityEngine;

public class CandyViewFactory : MonoBehaviour, ICandyViewFactory
{
    [field: SerializeField] private CandyView _viewPrefab;

    public CandyView CreateItem(Vector3 position)
    {
        var view = Instantiate(_viewPrefab);
        view.transform.SetParent(transform);
        view.transform.position = position;
        view.Init();

        return view;
    }

    public IFactoryItem CreateItem(Type type, object[] parametres) => default;
}
