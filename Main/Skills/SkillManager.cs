using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillData skillData;
    public Skill RandomGenerate()
    {
        var skill = skillData.skills[Random.Range(0,skillData.skills.Count)];
        return skill.Key;
    }

    public void DoSkillAction(Skill skill)
    {
        foreach (var skilldict in skillData.skills)
        {
            if (skilldict.IsEquelKey(skill))
            {
                skilldict.Value.Value.Action();
                return;
            }
        }
    }
}
