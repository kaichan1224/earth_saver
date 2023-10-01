using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ひとつのwaveに含まれる要素に関するクラス
/// </summary>
[Serializable]
public class WaveParam
{
    public bool IsAuto;
    public List<GenerateObjData> generateObjDatas;
}
