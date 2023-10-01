using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData:ScriptableObject
{
    //辞書作成
    public List<DictPair<EnemyType, GameObject>> enemyTypeDict;
}
