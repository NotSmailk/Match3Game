using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelView : MonoBehaviour, ITaskPanelView
{
    [field: SerializeField] private Image _candySprite;
    [field: SerializeField] private TextMeshProUGUI _countText;
    [field: SerializeField] private TextMeshProUGUI _taskInfoText;

    public void DisplayTaskText(int count, int totalCount)
    {
        _taskInfoText.text = GameConstants.KeyWords.CRUCH_CANDIES;
        _countText.text = $"{count}/{totalCount}";
    }

    public void DisplayTaskValues(Sprite sprite, int count)
    {
        _candySprite.sprite = sprite;
        _countText.text = $"{0}/{count}";
    }
}
