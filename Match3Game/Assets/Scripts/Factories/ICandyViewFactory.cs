using UnityEngine;

public interface ICandyViewFactory : IFactory
{
    public CandyView CreateItem(Vector3 position);
}
