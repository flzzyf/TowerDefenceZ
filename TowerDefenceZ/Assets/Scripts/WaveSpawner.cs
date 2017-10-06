using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Wave[] wave;

    public float timeBetweenWaves = 3f;

    int currentWaveIndex = 0;
    float spawnCountDown = 0;

    bool spawningWave = false;

	void Start ()
    {
	}
	
	void Update ()
    {
        //仍在波次中
        if (spawningWave == true)
            return;

        //完成全部波数
        if(currentWaveIndex == wave.Length)
        {
            //胜利
            this.enabled = false;
        }

		if(spawnCountDown <= 0)
        {
            spawnCountDown = timeBetweenWaves;
            StartCoroutine(SpawnWave());
            currentWaveIndex++;

        }
        else
        {
            spawnCountDown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;

        Wave currentWave = wave[currentWaveIndex];

        Vector3 spawnDir = WayPointManager.wayPoints[1].position - WayPointManager.wayPoints[0].position;

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
        {
            Instantiate(currentWave.waveUnits[i], WayPointManager.wayPoints[0].position, Quaternion.LookRotation(spawnDir));
            yield return new WaitForSeconds(currentWave.spawnRate);
        }

        spawningWave = false;

    }

}
