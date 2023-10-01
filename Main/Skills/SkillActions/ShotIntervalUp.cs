using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotIntervalUp : MonoBehaviour,ISkillAction
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private float decrementPercent;
    public void Action()
    {
        masterParam.shotInterval.Value -= masterParam.shotInterval.Value * (decrementPercent/100);
    }
}
