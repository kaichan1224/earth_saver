using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
using System;

/// <summary>
/// 一つのSkillButton
/// </summary>
public class SkillButton: MonoBehaviour
{
    [SerializeField] private TMP_Text skillTitleText;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text explainText;
    [SerializeField] private Button _button;
    public Button button => _button;
    private Skill _skill;
    public Skill skill => _skill;
    /// <summary>
    /// ボタンの初期化
    /// </summary>
    /// <param name="skill"></param>
    public void Init(Skill skill)
    {
        _skill = skill;
        skillTitleText.text = skill.skillName;
        icon.sprite = skill.icon;
        explainText.text = skill.skillExplain;
    }
}
