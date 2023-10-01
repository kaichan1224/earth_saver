using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

/// <summary>
/// スキルに関するマスタデータ
/// ISkillActionを継承したプレハブをインスペクターから指定することができる
/// </summary>
[CreateAssetMenu(fileName = "SkillData", menuName ="Data/SkillData")]
public class SkillData : ScriptableObject
{
    public List<DictPair<Skill, SerializableInterface<ISkillAction>>> skills;
}
