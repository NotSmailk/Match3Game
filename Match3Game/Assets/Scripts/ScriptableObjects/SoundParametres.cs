using UnityEngine;

[CreateAssetMenu(menuName = "Parametres/Sound", fileName = "DefaultSoundParametres")]
public class SoundParametres : ScriptableObject
{

    [field: SerializeField] private AudioClip _pickCandyClip;
    [field: SerializeField] private AudioClip _moveDoneCandyClip;
    [field: SerializeField] private AudioClip _moveCancelCandyClip;
    [field: SerializeField] private AudioClip _candyCrushClip;
    [field: SerializeField] private AudioClip _victoryClip;
    [field: SerializeField] private AudioClip _defeatClip;
    [field: SerializeField] private AudioClip _buttonClickClip;
    [field: SerializeField] private float _volume = 0.1f;

    public AudioClip PickCandyClip => _pickCandyClip;
    public AudioClip MoveDoneCandyClip => _moveDoneCandyClip;
    public AudioClip MoveCancelCandyClip => _moveCancelCandyClip;
    public AudioClip CandyCrushClip => _candyCrushClip;
    public AudioClip VictoryClip => _victoryClip;
    public AudioClip DefeatClip => _defeatClip;
    public AudioClip ButtonClickClip => _buttonClickClip;
    public float Volume => _volume;
}
