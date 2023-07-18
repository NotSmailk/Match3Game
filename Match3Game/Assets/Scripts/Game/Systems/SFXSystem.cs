public class SFXSystem : ISFXSystem
{
    private SoundParametres _soundParametres;

    public SFXSystem(SoundParametres soundParametres)
    {
        _soundParametres = soundParametres;
        SFXHelper.Pitch = 1f;
    }

    public void ResetPitch()
    {
        SFXHelper.Pitch = 1f;
    }

    public void AddToPitch(float value)
    {
        SFXHelper.Pitch += value;
    }

    public void CandyMoveDone()
    {
        SFXHelper.PlayOneShot(_soundParametres.MoveDoneCandyClip, _soundParametres.Volume);
    }

    public void CandyMoveCancel()
    {
        SFXHelper.PlayOneShot(_soundParametres.MoveCancelCandyClip, _soundParametres.Volume);
    }

    public void CandyTouch()
    {
        SFXHelper.PlayOneShot(_soundParametres.PickCandyClip, _soundParametres.Volume);
    }

    public void CandyBreak()
    {
        SFXHelper.PlayOneShot(_soundParametres.CandyCrushClip, _soundParametres.Volume);
    }

    public void CandyBreak(float pitch)
    {
        SFXHelper.PlayOneShot(_soundParametres.CandyCrushClip, _soundParametres.Volume, SFXHelper.Pitch + pitch);
    }

    public void ButtonClick()
    {
        SFXHelper.PlayOnShotDontDestroy(_soundParametres.ButtonClickClip, _soundParametres.Volume);
    }

    public void Victory()
    {
        SFXHelper.PlayOnShotDontDestroy(_soundParametres.VictoryClip, _soundParametres.Volume);
    }

    public void Defeat()
    {
        SFXHelper.PlayOnShotDontDestroy(_soundParametres.DefeatClip, _soundParametres.Volume);
    }
}
