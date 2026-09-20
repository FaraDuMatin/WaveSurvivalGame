using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    public AudioClip hit, death, pickup, levelUp, hurt, click, gameOver, music;
    [SerializeField] float musicVol = 0.4f;
    [SerializeField] int voices = 12;

    public static Sfx I;
    AudioSource[] src; int next;
    readonly Dictionary<AudioClip, float> last = new();

    void Awake()
    {
        I = this;
        src = new AudioSource[voices];
        for (int i = 0; i < voices; i++) src[i] = gameObject.AddComponent<AudioSource>();
        var m = gameObject.AddComponent<AudioSource>(); m.clip = music; m.loop = true; m.volume = musicVol; m.Play();
    }

    // minInterval caps repeats of the same clip (300 enemies hit at once = one sound, not 300)
    public static void Play(AudioClip clip, float vol = 1f, float minInterval = 0.05f)
    {
        if (clip == null || I == null) return;
        if (I.last.TryGetValue(clip, out float t) && Time.unscaledTime - t < minInterval) return;
        I.last[clip] = Time.unscaledTime;
        var s = I.src[I.next++ % I.src.Length];
        s.pitch = Random.Range(0.9f, 1.1f);
        s.PlayOneShot(clip, vol);
    }
}
