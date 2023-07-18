using TMPro;
using UnityEngine;

public class ScorePanelView : MonoBehaviour, IScorePanelView
{
    [field: SerializeField] private TextMeshProUGUI _scoreText;

    public void DisplayScore(float score)
    {
        _scoreText.text = $"{GameConstants.KeyWords.SCORE} {score}";
    }
}
