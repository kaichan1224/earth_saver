using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour
{
    public ReactiveProperty<bool> isGameOver = new(false);
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform shotPos;
    private int direction = 1;
    // 中心点
    [SerializeField] private Vector3 _center = Vector3.zero;
    void Start()
    {
        StartCoroutine(Shot());
    }

    void Update()
    {
        transform.RotateAround(
              _center,
              new Vector3(0,0,direction),
              360 / masterParam.playerSpeed.Value * Time.deltaTime
        );
        if (Input.GetMouseButtonDown(0))
        {
            direction *= -1;
        }
    }

    IEnumerator Shot()
    {
        while (true)
        {
            soundManager.ShotSE();
            var tmp = Instantiate(bullet,shotPos.position,Quaternion.identity);;
            tmp.Setup(this.gameObject);
            yield return new WaitForSeconds(masterParam.shotInterval.Value);
        }
    }

    public void GetDamage()
    {
        soundManager.PlayerDamageSE();
        masterParam.playerLife.Value--;
        if (masterParam.playerLife.Value <= 0)
        {
            DestroyShip();
        }
    }

    /// <summary>
    /// 機体破壊エフェクト
    /// </summary>
    public void DestroyShip()
    {
        isGameOver.Value = true;
    }
}
