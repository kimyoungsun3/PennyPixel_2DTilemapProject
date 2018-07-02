using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolManager5;

public class EnemySpawner : MonoBehaviour {
    List<Transform> spawnPoint = new List<Transform>();
    public float WAIT_TIME = 2f;
    float waitTime;

    void Awake()
    {
        for(int i = 0, iMax = transform.childCount; i < iMax; i++)
        {
            spawnPoint.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > waitTime)
        {
            waitTime = Time.time + WAIT_TIME;
            int _idx = Random.Range(0, spawnPoint.Count);
            PoolManager.ins.Instantiate("Enemy", spawnPoint[_idx].position, spawnPoint[_idx].rotation);
        }
    }
}
