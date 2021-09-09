using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    // Start is called before the first frame update
    public void Awake()
    {
        if (SoundManager.instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        bgVolume = ASMusic.volume;
        CheckMusic();
    }
    float bgVolume=0.5f;
    public AudioSource ASMusic;
    public AudioClip musicFront, musicBack;
    public AudioSource ASClick, ASCoins;
    public void CheckMusic()
    {
        if (!GameData.IsSwitchOn("Sound")) { ASMusic.volume = 0; }
        else
        {
            ASMusic.volume = bgVolume;
        }
    }
    public void PlayBg(eClipType clipType)
    {
        if (!GameData.IsSwitchOn("Sound")) { ASMusic.volume = 0; }
        switch (clipType)
        {
            case eClipType.musicFront:
                if (ASMusic.clip == musicFront && ASMusic.isPlaying) return;
                ASMusic.clip = musicFront;
                break;
            case eClipType.musicBack:
                if (ASMusic.clip == musicBack && ASMusic.isPlaying) return;
                ASMusic.clip = musicBack;
                break;
        }
        ASMusic.Play();
       
    }
    public void StopBG()
    {
        ASMusic.Stop();
    }
    public void PlayClip(eClipType clipType)
    {
        if (!GameData.IsSwitchOn("Sound")) { return; }
        switch (clipType)
        {
            case eClipType.click:
                ASClick.Play();
                break;
            case eClipType.coins:
                ASCoins.Play();
                break;
        }
    }

}
public enum eClipType
{
    click,coins,musicFront,musicBack
}
