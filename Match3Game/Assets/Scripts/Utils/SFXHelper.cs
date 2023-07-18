using UnityEngine;

public class SFXHelper
{
    public static float Pitch { get; set; }

    public static void PlayOneShot(AudioClip clip, float volume)
    {
        var sourceObject = new GameObject($"[SFX] {clip.name}");
        var source = sourceObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.pitch = Pitch;
        source.PlayOneShot(clip);
        GameObject.Destroy(sourceObject, clip.length);
    }

    public static void PlayOnShotDontDestroy(AudioClip clip, float volume)
    {
        var sourceObject = new GameObject($"[SFX] {clip.name}");
        MonoBehaviour.DontDestroyOnLoad(sourceObject);
        var source = sourceObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.pitch = Pitch;
        source.PlayOneShot(clip);
        GameObject.Destroy(sourceObject, clip.length);
    }

    public static void PlayOneShot(AudioClip clip, float volume, float pitch)
    {
        var sourceObject = new GameObject($"[SFX] {clip.name}");
        var source = sourceObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.pitch = pitch;
        source.PlayOneShot(clip);
        GameObject.Destroy(sourceObject, clip.length);
    }

    public static void PlayForSeconds(AudioClip clip, float volume, float time)
    {
        var sourceObject = new GameObject($"[SFX] {clip.name}");
        var source = sourceObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.PlayOneShot(clip);
        GameObject.Destroy(sourceObject, time);
    }
}
