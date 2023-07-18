using TMPro;
using UnityEngine;

public class MovesPanelView : MonoBehaviour, IMovesPanelView
{
    [field: SerializeField] private TextMeshProUGUI _movesText;

    public void DisplayMoves(int moves, int totalMoves)
    {
        _movesText.text = $"{GameConstants.KeyWords.MOVES} {moves}/{totalMoves}";
    }
}
