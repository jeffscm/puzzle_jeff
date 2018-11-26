using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SOUNDTYPE {CLICK_UI, CLICK_CARD, WIN, LOSE, START_LEVEL, FLIPCARD, ADDSCORE, NONE };

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    public AudioSource source;

    [SerializeField]
    AudioClip[] clips;

    bool isMuted = false;
    public bool IsMuted
    {
        get
        {
            return isMuted;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TestMuted();    
    }

    public void Play(SOUNDTYPE soundtype)
    {
        if ((int)soundtype < clips.Length)
        {
            source.Stop();
            source.PlayOneShot(clips[(int)soundtype]);
        }
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetMute(bool muted)
    {        
        PlayerPrefs.SetInt("MUTED", ((muted) ? 1 : 0));
        TestMuted();
    }

    void  TestMuted()
    {
        var temp = PlayerPrefs.GetInt("MUTED", 0);
        AudioListener.volume = (temp == 0) ? 1f : 0f;
        isMuted = (temp != 0);
    }
}
