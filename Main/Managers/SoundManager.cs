using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //AudioSource
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource SEAudio;
    //BGM
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip battleBGM;
    [SerializeField] private AudioClip gameOverBGM;
    //SE
    [SerializeField] private AudioClip playerDamage;
    [SerializeField] private AudioClip earthDamage;
    [SerializeField] private AudioClip shotSE;
    [SerializeField] private AudioClip meteoSE;
    [SerializeField] private AudioClip WARNSE;

    public void PlayTitleBGM()
    {
        PlayBGM(titleBGM);
    }

    public void PlayBattleBGM()
    {
        PlayBGM(battleBGM);
    }


    public void PlayGameOverBGM()
    {
        PlayBGM(gameOverBGM);
    }

    public void PlayerDamageSE()
    {
        PlaySE(playerDamage);
    }

    public void EarthDamageSE()
    {
        PlaySE(earthDamage);
    }

    public void ShotSE()
    {
        PlaySE(shotSE);
    }

    public void WarnSE()
    {
        PlaySE(WARNSE);
    }

    public void EnemyDestroySE()
    {
        PlaySE(meteoSE);
    }

    void PlayBGM(AudioClip clip)
    {
        bgmAudio.clip = clip;
        bgmAudio.Play();
    }

    void PlaySE(AudioClip clip)
    {
        SEAudio.PlayOneShot(clip);
    }
}
