using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave;
    public float waveRatio;
    public float waveDuration;

    public float timer;
    
    public List<GameObject> enemies;
    public List<Transform> spawnLocations;

    
    

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        
        for (int i = 0; i < enemies.Count; ++i)
        {
            for (int j = 0; j < Math.Ceiling(currentWave * waveRatio)+1; ++j)
            {
                Instantiate(enemies[i],spawnLocations[i].position,Quaternion.identity,this.transform);
                yield return new WaitForSeconds(0.2f);
            }
        }

        currentWave++;

        yield return WaveDuration();
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else timer = 0;
    }

    IEnumerator WaveDuration()
    {
        timer = waveDuration;
        yield return new WaitForSeconds(waveDuration);
        
        yield return SpawnEnemies();
    }

    





}