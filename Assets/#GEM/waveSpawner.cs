using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class waveSpawner : MonoBehaviour
{

    public static int EnemiesisAlive = 0;
    public Transform virusCancer;

    public Transform SpawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text countdownText;
    private int waveIndex = 0;


    void Update()
    {
       
        if(EnemiesisAlive > 0 )
        {
            return;
        }
        
        if(countdown <= 0f)
        {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdownText.text = Mathf.Floor(countdown).ToString();

        countdownText.text = string.Format("{0:00.00}", countdown);

    }

    IEnumerator spawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            Debug.Log("Wave Incoming!");
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
         
        }


   


    }
    void SpawnEnemy()
    {

        Instantiate(virusCancer, SpawnPoint.position,SpawnPoint.rotation);
        EnemiesisAlive++;

    }
}
