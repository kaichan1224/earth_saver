using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedUp : MonoBehaviour,ISkillAction
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private float incrementPercent;
    public void Action()
    {
        masterParam.bulletSpeed.Value += masterParam.bulletSpeed.Value * (incrementPercent / 100);
    }
}
