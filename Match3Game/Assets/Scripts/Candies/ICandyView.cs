using UnityEngine;
using UnityEngine.Events;

public interface ICandyView : IView
{
    public void DisplaySprite(Sprite sprite);
    public void DisplayDeselectedCandy();
    public void DisplaySelectedCandy();
    public void DisplayDestroyedCandy();
    public void DisplayMoveOfCandy(GridPosition position);
}
