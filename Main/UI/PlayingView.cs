using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class PlayingView : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private Slider earthLifeSlider;
    [SerializeField] private RectTransform lifeGagePanel;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private TMP_Text lifeText;
    public void SetWave(int wave)
    {
        waveText.text = $"WAVE{wave+1}";
    }

    public void SetPlayerLife(int life)
    {
        for (int i = 0; i < lifeGagePanel.childCount; i++)
        {
            Destroy(lifeGagePanel.GetChild(i).gameObject);
        }
        for (int i = 0; i < life; i++)
        {
            Instantiate(lifePrefab,lifeGagePanel);
        }
    }

    public void SetEarthLifeSlider(int life,int maxLife)
    {
        earthLifeSlider.value = (float)life / (float)maxLife;
    }
}
