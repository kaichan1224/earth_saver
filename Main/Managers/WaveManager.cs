using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using UnityEditor;

public class WaveManager : MonoBehaviour
{
    private int minX = -9, maxX = 9, minY = -9, maxY = 9;
    private Vector3[] spawnPositions;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private WaveData waveData;
    [SerializeField] private EnemyData enemyData;
    public ReactiveProperty<int> currentWave = new(0);
    public ReactiveProperty<bool> isRunCurrentWave = new(true);
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private void Start()
    {
        spawnPositions = new Vector3[]{
            new Vector3(0, maxY, 0),
            new Vector3(maxX, maxY, 0),
            new Vector3(maxX, 0, 0),
            new Vector3(maxX, minY, 0),
            new Vector3(0, minY, 0),
            new Vector3(minX, minY, 0),
            new Vector3(minX, 0, 0),
            new Vector3(minX, maxY, 0)
        };
    }

    public void StartWave()
    {
        Time.timeScale = 1;
        isRunCurrentWave.Value = true;
        StartCoroutine(DoWave());
    }

    IEnumerator DoWave()
    {
        if (waveData.WaveList[currentWave.Value].IsAuto)
        {
            var genCnt = currentWave.Value;
            for (int i = 0; i < genCnt; i++)
            {
                EnemyType enemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                GenPlace genPlace = (GenPlace)Random.Range(0, System.Enum.GetValues(typeof(GenPlace)).Length);
                GameObject spawnedObj = Instantiate(SearchGameObjectToEnemyDict(enemyType), spawnPositions[(int)genPlace], Quaternion.identity);
                var spwaned = spawnedObj.GetComponent<IEnemy>();
                if (spwaned != null)
                {
                    spwaned.Init(soundManager);
                }
                spawnedObjects.Add(spawnedObj);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f));
            }
            while (spawnedObjects.Count > 0)
            {
                spawnedObjects.RemoveAll(obj => obj == null); // Destroyされたオブジェクトをリストから削除
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            EndCurrentWave();
        }
        else
        {
            var nowWaveObjs = waveData.WaveList[currentWave.Value].generateObjDatas;
            foreach (var nowWaveObj in nowWaveObjs)
            {
                GameObject spawnedObj = Instantiate(SearchGameObjectToEnemyDict(nowWaveObj.enemyType), spawnPositions[(int)nowWaveObj.place], Quaternion.identity);
                var spwaned = spawnedObj.GetComponent<IEnemy>();
                if (spwaned != null)
                {
                    spwaned.Init(soundManager);
                }
                spawnedObjects.Add(spawnedObj);
                yield return new WaitForSeconds(nowWaveObj.plusTime);
            }
            while (spawnedObjects.Count > 0)
            {
                spawnedObjects.RemoveAll(obj => obj == null); // Destroyされたオブジェクトをリストから削除
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            EndCurrentWave();
        }
    }

    public GameObject SearchGameObjectToEnemyDict(EnemyType enemyType)
    {
        foreach (var dictItem in enemyData.enemyTypeDict)
        {
            if (dictItem.IsEquelKey(enemyType))
            {
                return dictItem.Value;
            }
        }
        return null;
    }

    public void EndCurrentWave()
    {
        isRunCurrentWave.Value = false;
    }

    public void NextWave()
    {
        currentWave.Value++;
    }

}