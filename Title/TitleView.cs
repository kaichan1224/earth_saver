using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class TitleView : MonoBehaviour
{
    [SerializeField] private Button lv1Button;
    [SerializeField] private Button lv2Button;
    [SerializeField] private Button lv3Button;
    public IObservable<Unit> OnLV1 => lv1Button.OnClickAsObservable();
    public IObservable<Unit> OnLV2 => lv2Button.OnClickAsObservable();
    public IObservable<Unit> OnLV3 => lv3Button.OnClickAsObservable();
}
