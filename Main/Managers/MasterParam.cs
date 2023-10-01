using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// スキルで変わるパラメータを全て格納したScriptalObject
/// </summary>
[CreateAssetMenu(fileName = "MasterParam", menuName = "Data/MasterParam")]
public class MasterParam : ScriptableObject
{
    public int defaultPlayerLife;
    public int defaultEarthLife;
    public float defaultBulletSpeed;
    public float defaultShotInterval;
    public float defaultPlayerSpeed;
    public ReactiveProperty<int> playerLife;
    public ReactiveProperty<int> earthLife;
    public ReactiveProperty<float> bulletSpeed;
    public ReactiveProperty<float> shotInterval;
    public ReactiveProperty<float> playerSpeed;
    public int maxEarthLife;
    public void Init()
    {
        playerLife.Value = defaultPlayerLife;
        earthLife.Value = defaultEarthLife;
        maxEarthLife = defaultEarthLife;
        bulletSpeed.Value = defaultBulletSpeed;
        shotInterval.Value = defaultShotInterval;
        playerSpeed.Value = defaultPlayerSpeed;
    }

    public void Init2()
    {
        playerLife.Value = defaultPlayerLife;
        earthLife.Value = defaultEarthLife;
        maxEarthLife = defaultEarthLife;
    }
}
