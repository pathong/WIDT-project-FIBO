using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class wavespawnerInfinite : MonoBehaviour
{
    public static int EnemiesisAlive = 0;
    public Transform enemyPrefab;
    public Transform bossPrefab;
    public Transform speedSter;
    public Transform[] spawnPoints;
    public float timebetweenwaves = 10f;
    private float countdown = 10f;
    public Text countdownText;


    private int waveNumber = 0;
    public GameObject _par_spawn;


    private void Update()
    {
        if(EnemiesisAlive > 0)
        {
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(spawnWave());
            countdown = timebetweenwaves;
 

        }

        countdown -= Time.deltaTime;

        countdownText.text = Mathf.Floor(countdown).ToString();

        countdownText.text = string.Format("{0:00}", countdown);
    }

    IEnumerator spawnWave()
    {




        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {

            Transform spawnPoint = spawnPoints[(int)(Random.Range(0, spawnPoints.Length-1))];

            spawnEnemy(spawnPoint);
            yield return new WaitForSeconds(1f);

        }
        Debug.Log("Wave Incoming");


        if (waveNumber % 5 == 0)
        {
            Transform spawnPoint = spawnPoints[(int)(Random.Range(0, spawnPoints.Length-1))];

            spawnBoss(spawnPoint);
        }


        if (waveNumber % 3 == 0)
        {
            Transform spawnPoint = spawnPoints[(int)(Random.Range(0, spawnPoints.Length-1))];

            spawnSpeed(spawnPoint);
        }

    }

    void spawnEnemy(Transform spawnPoint)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesisAlive++;

        Debug.Log("Spawning enemy: ");
        Instantiate(_par_spawn, spawnPoint.position, Quaternion.identity);
        FindObjectOfType<SoundManager>().Play("sound_spawn_virus");
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void spawnBoss(Transform spawnPoint)
    {
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Boss Incoming!!");
        EnemiesisAlive++;


        Debug.Log("Spawning enemy: ");
        Instantiate(_par_spawn, spawnPoint.position, Quaternion.identity);
        FindObjectOfType<SoundManager>().Play("sound_spawn_virus");
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void spawnSpeed(Transform spawnPoint)
    {
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Speedster Incoming!!");
        EnemiesisAlive++;


        Debug.Log("Spawning enemy: ");
        Instantiate(_par_spawn, spawnPoint.position, Quaternion.identity);
        FindObjectOfType<SoundManager>().Play("sound_spawn_virus");
        Instantiate(speedSter, spawnPoint.position, spawnPoint.rotation);
    }
}