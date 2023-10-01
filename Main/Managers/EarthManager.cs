using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EarthManager : MonoBehaviour
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private SoundManager soundManager;        
    public ReactiveProperty<bool> isGameOver = new(false);
    public void Init()
    {
        isGameOver.Value = true;
    }
    public void GetDamage()
    {
        soundManager.EarthDamageSE();
        masterParam.earthLife.Value--;
        if (masterParam.earthLife.Value <= 0)
        {
            isGameOver.Value = true;
        }
    }
}
