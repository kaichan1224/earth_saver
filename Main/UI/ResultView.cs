using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class ResultView : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;
    public IObservable<Unit> OnContinue => continueButton.OnClickAsObservable();
    public IObservable<Unit> OnRetry => retryButton.OnClickAsObservable();
    public IObservable<Unit> OnTitle => titleButton.OnClickAsObservable();
}
