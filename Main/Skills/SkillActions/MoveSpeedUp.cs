using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUp : MonoBehaviour,ISkillAction
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private float incrementPercent;
    public void Action()
    {
        masterParam.playerSpeed.Value -= masterParam.playerSpeed.Value * (incrementPercent / 100);
    }
}
