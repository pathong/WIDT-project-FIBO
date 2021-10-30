using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wavespawnernew : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }


    public Wave[] waves;
    private int nextwaveindex = 0;
    public float timebetweenwave = 5f;
    public float waveCountDown;
    public Text countdownText;
    public Transform SpawnPoint;
    private float searchCountDown = 1f; 
    private SpawnState state = SpawnState.counting;

    public GameObject _par_spawn;

    void Start()
    {
        waveCountDown = timebetweenwave;
        
    }

    void Update()
    {
        if (state == SpawnState.waiting) 
        {
            //check if enemies are still alive 
            if (!EnemyIsAlive())
            {
                //Begin a new round
                waveComplete();
            } else
            {
                return;
            }
        }

        if(waveCountDown <= 0f)
        {
            if(state != SpawnState.spawning)
            {
                StartCoroutine(spawnWave(waves[nextwaveindex]));


            }

        }
        else
        {
            waveCountDown -= Time.deltaTime;

            countdownText.text = Mathf.Floor(waveCountDown).ToString();

            countdownText.text = string.Format("{0:00}", waveCountDown);
        }




    }
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;

            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return false;
            }
        }

        return true; 
    }

    void waveComplete()
    {
        Debug.Log("Wave complete");
        state = SpawnState.counting;
        waveCountDown = timebetweenwave;
  
        if (nextwaveindex + 1 > waves.Length - 1)
        {
            Debug.Log("Completed all waves!");
            state = SpawnState.waiting;

          
        }
        nextwaveindex++;
    }

    IEnumerator spawnWave(Wave _wave)
    {
        Debug.Log("Spawning enemy");

        state = SpawnState.spawning;
        for (int i = 0; i < _wave.count; i++)
        {
            spawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);

        }
        //spawn

        state = SpawnState.waiting;
        yield break;

    }

    void spawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Instantiate(_par_spawn, SpawnPoint.position, Quaternion.identity);
        FindObjectOfType<SoundManager>().Play("sound_spawn_virus");
        Instantiate(_enemy, SpawnPoint.position, SpawnPoint.rotation);


    }

}
