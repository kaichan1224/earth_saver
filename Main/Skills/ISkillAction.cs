using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillAction
{
    /// <summary>
    /// スキルを獲得した際に実行されるメソッド
    /// </summary>
    public void Action();
}
