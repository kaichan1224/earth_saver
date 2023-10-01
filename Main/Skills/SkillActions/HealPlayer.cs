using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour,ISkillAction
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private int incrementLife;
    public void Action()
    {
        Debug.Log("HealPlayer");
        masterParam.playerLife.Value += incrementLife;
    }
}
