using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime;
    private ReactiveProperty<float> currentTime = new();
    public IReadOnlyReactiveProperty<float> CurrentTime => currentTime;
    void Start()
    {
        currentTime.Value = startTime;
    }

    void Update()
    {
        if (currentTime.Value > 0)
            currentTime.Value -= Time.deltaTime;

    }
}
