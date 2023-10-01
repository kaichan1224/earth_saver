using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using unityroom.Api;

public class UIPresenter : MonoBehaviour
{
    //model
    [SerializeField] private Player player;
    [SerializeField] private EarthManager earthManager;
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private SoundManager soundManager;
    //UI
    [SerializeField] private PlayingView playingView;
    [SerializeField] private PowerUpView powerUpView;
    [SerializeField] private ResultView resultView;
    [SerializeField] private TMP_Text waveClearText;

    public bool isContinue = false;
    void Start()
    {
        isContinue = false;
        //継続ページ
        resultView.OnContinue
            .Subscribe(x =>
            {
                isContinue = true;
                masterParam.Init2();
                soundManager.PlayBattleBGM();
                resultView.gameObject.SetActive(false);
                player.isGameOver.Value = false;
                earthManager.isGameOver.Value = false;
                Time.timeScale = 1f;
            })
            .AddTo(this);

        resultView.OnRetry
            .Subscribe(x =>
            {
                if(isContinue==false)
                    UnityroomApiClient.Instance.SendScore(1,waveManager.currentWave.Value+1, ScoreboardWriteMode.HighScoreAsc);
                Time.timeScale = 1f;
                SceneManager.LoadSceneAsync("Main");
            })
            .AddTo(this);

        resultView.OnTitle
            .Subscribe(x =>
            {
                if(isContinue==false)
                    UnityroomApiClient.Instance.SendScore(1, waveManager.currentWave.Value+1, ScoreboardWriteMode.HighScoreAsc);
                SceneManager.LoadSceneAsync("Title");
            })
            .AddTo(this);

        //ゲームオーバー時の処理
        Observable.Merge(player.isGameOver, earthManager.isGameOver)
            .Where(result => result == true)
            .Delay(TimeSpan.FromMilliseconds(200))
            .Subscribe(_ =>
            {
                soundManager.PlayGameOverBGM();
                Time.timeScale = 0;
                resultView.gameObject.SetActive(true);
            })
            .AddTo(this);

        masterParam.playerLife
            .Subscribe(x => playingView.SetPlayerLife(x))
            .AddTo(this);

        waveManager.currentWave
            .Subscribe(x => playingView.SetWave(x))
            .AddTo(this);

        masterParam.earthLife
            .Subscribe(x => playingView.SetEarthLifeSlider(x,masterParam.maxEarthLife))
            .AddTo(this);

        waveManager.isRunCurrentWave
            .Skip(1) // 初回のイベントを無視
            .Subscribe(x =>
            {
                if (x)
                {
                    //次のウェーブ開始時の処理
                    powerUpView.gameObject.SetActive(false);
                }
                else
                {
                    WaveClear();
                }
            });
            
        powerUpView.OnSkill1
            .Subscribe(x =>
            {
                WaveStart(AtSkill1);
            })
            .AddTo(this);

        powerUpView.OnSkill2
            .Subscribe(x =>
            {
                WaveStart(AtSkill2);
            })
            .AddTo(this);

        powerUpView.OnSkill3
            .Subscribe(x =>
            {
                WaveStart(AtSkill3);
            })
            .AddTo(this);
    }

    void WaveClear()
    {
        RectTransform v = waveClearText.rectTransform;
        v.anchoredPosition = new Vector2(-550,0);
        waveClearText.text = $"wave{waveManager.currentWave.Value + 1} clear";
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(v.DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.InOutQuart))
            .AppendInterval(0.5f)
            .Append(v.DOAnchorPos(new Vector2(900, 0), 1f).SetEase(Ease.InOutQuart))
            .OnComplete(() =>
            {
                Time.timeScale = 0;
                powerUpView.gameObject.SetActive(true);
                //スキル生成
                powerUpView.InitSkills(skillManager.RandomGenerate(), skillManager.RandomGenerate(), skillManager.RandomGenerate());
            });
    }

    void WaveStart(Action atSkill)
    {
        Time.timeScale = 1f;
        powerUpView.gameObject.SetActive(false);
        atSkill.Invoke();
    }

    void AtSkill1()
    {
        var GetSkill = powerUpView.GetSkill1();
        skillManager.DoSkillAction(GetSkill);
        gameManager.NextWave();
    }

    void AtSkill2()
    {
        var GetSkill = powerUpView.GetSkill2();
        skillManager.DoSkillAction(GetSkill);
        gameManager.NextWave();
    }

    void AtSkill3()
    {
        var GetSkill = powerUpView.GetSkill3();
        skillManager.DoSkillAction(GetSkill);
        gameManager.NextWave();
    }
}
