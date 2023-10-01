using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class PowerUpView : MonoBehaviour
{
    [SerializeField] private SkillButton skill1Button;
    [SerializeField] private SkillButton skill2Button;
    [SerializeField] private SkillButton skill3Button;
    public IObservable<Unit> OnSkill1 => skill1Button.button.OnClickAsObservable();
    public IObservable<Unit> OnSkill2 => skill2Button.button.OnClickAsObservable();
    public IObservable<Unit> OnSkill3 => skill3Button.button.OnClickAsObservable();
    public void InitSkills(Skill skill1,Skill skill2,Skill skill3)
    {
        skill1Button.Init(skill1);
        skill2Button.Init(skill2);
        skill3Button.Init(skill3);
    }

    public Skill GetSkill1()
    {
        return skill1Button.skill;
    }

    public Skill GetSkill2()
    {
        return skill2Button.skill;
    }

    public Skill GetSkill3()
    {
        return skill3Button.skill;
    }
}
