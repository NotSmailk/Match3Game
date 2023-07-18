using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndView : MonoBehaviour, ILevelEndView
{
    [field: SerializeField] private GameObject _panel;
    [field: SerializeField] private TextMeshProUGUI _endResultInfoText;
    [field: SerializeField] private TextMeshProUGUI _endResultScoreText;
    [field: SerializeField] private Button _exitButton;
    [field: SerializeField] private Button _replayLevelButton;
    [field: SerializeField] private Button _nextLevelButton;

    public Button ExitButton { get => _exitButton; set { } }
    public Button ReplayLevelButton { get => _replayLevelButton; set { } }
    public Button NextLevelButton { get => _nextLevelButton; set { } }

    public void DisplayResultInfo(string info, float score)
    {
        _endResultInfoText.text = info;
        _endResultScoreText.text = $"{GameConstants.KeyWords.LEVEL_END_MESSAGE} {score}";
    }

    public void ShowPanel(bool show)
    {
        _panel.SetActive(show);
    }
}
