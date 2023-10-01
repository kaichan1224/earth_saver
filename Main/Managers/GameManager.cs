using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private RectTransform countDownRectTransform;
    [SerializeField] private TMP_Text waveClearText;
    [SerializeField] private MasterParam masterParam;
    void Start()
    {
        soundManager.PlayBattleBGM();
        masterParam.Init();
        GameStart();
    }

    void GameStart()
    {
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() => countDownRectTransform.localScale = new Vector3(0,0,0))
            .AppendCallback(() => countDownText.text = "3")
            .Append(countDownRectTransform.DOScale(new Vector3(3,3,1),1))
            .AppendCallback(() => countDownRectTransform.localScale = new Vector3(0, 0, 0))
            .AppendCallback(() => countDownText.text = "2")
            .Append(countDownRectTransform.DOScale(new Vector3(3, 3, 1), 1))
            .AppendCallback(() => countDownRectTransform.localScale = new Vector3(0, 0, 0))
            .AppendCallback(() => countDownText.text = "1")
            .Append(countDownRectTransform.DOScale(new Vector3(3, 3, 1), 1))
            .AppendCallback(() => countDownRectTransform.localScale = new Vector3(0, 0, 0))
            .AppendCallback(() => countDownText.text = "Start")
            .Append(countDownRectTransform.DOScale(new Vector3(3, 3, 1), 0.75f))
            .AppendCallback(() => countDownRectTransform.localScale = new Vector3(0, 0, 0))
            .OnComplete(() =>
            {
                waveManager.StartWave();
            });
    }

    public void NextWave()
    {
        waveManager.NextWave();
        waveManager.StartWave();
    }
}
