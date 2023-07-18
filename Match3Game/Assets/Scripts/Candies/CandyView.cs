using DG.Tweening;
using UnityEngine;

public class CandyView : MonoBehaviour, ICandyView
{
    private SpriteRenderer _renderer;

    public void Init()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void DisplaySprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void DisplayMoveOfCandy(GridPosition position)
    {
        transform.DOMove(position.Position, 0.25f);
    }

    public void DisplaySelectedCandy()
    {
        var scale = transform.localScale;
        var newScale = new Vector3(scale.x + 0.2f, scale.y + 0.2f, scale.z);
        transform.DOScale(newScale, 0.1f);
    }

    public void DisplayDeselectedCandy()
    {
        var scale = transform.localScale;
        var newScale = new Vector3(scale.x - 0.2f, scale.y - 0.2f, scale.z);
        transform.DOScale(newScale, 0.1f);
    }

    public void DisplayDestroyedCandy()
    {
        var newScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(newScale, 0.1f);
        GameObject.Destroy(gameObject, 0.15f);
    }
}
