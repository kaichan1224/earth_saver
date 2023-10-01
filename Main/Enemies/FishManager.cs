using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fishを生成するマネージャ
/// </summary>
public class FishManager : MonoBehaviour,IEnemy
{
    [SerializeField] private Fish fishPrefab;
    [SerializeField] private int generateNum = 10;
    private List<Fish> activeFishes = new List<Fish>();
    [SerializeField] GameObject fishLeaderPrefab;
    public GameObject fishLeader;
    private SoundManager soundManager;
    //群れの各個体の向きの平均値
    public Vector3 averageAlignment;
    //群れの各個体の座標の平均値
    public Vector3 averageSeparation;
    public void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }
    void Start()
    {
        fishLeader = Instantiate(fishLeaderPrefab, new Vector3(0,5,0), Quaternion.identity);
        fishLeader.GetComponent<FishLeader>().Init(this,soundManager);
        for (int i = 0; i < generateNum; i++)
        {
            Vector3 position = new Vector3(Random.Range(-1,1),Random.Range(4,6),0);
            var fish = Instantiate(fishPrefab,position,Quaternion.identity);
            fish.Init(this,soundManager);
            activeFishes.Add(fish);
        }
    }

    private void Update()
    {
        averageAlignment = GetAverageAlignment();
        averageSeparation = GetAverageSeparation();
    }

    Vector3 GetAverageAlignment()
    {
        Vector3 alignment = Vector3.zero;
        foreach (Fish fish in activeFishes)
        {
            alignment += fish.Velocity;
        }
        alignment /= activeFishes.Count;
        return alignment;
    }

    Vector3 GetAverageSeparation()
    {
        Vector3 separation = Vector3.zero;
        foreach (Fish fish in activeFishes)
        {
            separation += fish.thisTransform.position;
        }
        separation /= activeFishes.Count;
        return separation;
    }

    public void RemoveFish(Fish fish)
    {
        if (activeFishes.Contains(fish))
        {
            activeFishes.Remove(fish);
        }
        if (activeFishes.Count == 0)
        {
            fishLeader.GetComponent<FishLeader>().BariaOff();
        }
    }
}
