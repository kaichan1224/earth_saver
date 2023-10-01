using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] private TitleView titleView;
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        StartCoroutine(StartMusic());
        titleView.OnLV1
            .Subscribe(_ => SceneManager.LoadScene("Main"))
            .AddTo(this);
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(2f);
        audioSource.Play();
    }
}
