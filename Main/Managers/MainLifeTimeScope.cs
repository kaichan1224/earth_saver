using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifeTimeScope : LifetimeScope
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private EarthManager earthManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private MasterParam masterParam;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(soundManager);
    }
}
