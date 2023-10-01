using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEarth : MonoBehaviour,ISkillAction
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private int incrementLife;
    public void Action()
    {
        var tmpLife = masterParam.earthLife.Value + incrementLife;
        if (tmpLife >= masterParam.defaultEarthLife)
        {
            masterParam.earthLife.Value = masterParam.defaultEarthLife;
        }
        else
        {
            masterParam.earthLife.Value += incrementLife;
        }
    }
}
