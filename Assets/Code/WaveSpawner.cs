using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    //spawner state locks
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public int roundCounter = 0;

    //wave defign
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    //wave array and count and spawn location
    public Wave[] waves;
    private int nextWave = 0;
    public GameObject spawnLocation;

    //timers
    public float timeBetweenWaves = 5f;

    public float waveCountdown;
    private float searchCountdown = 1f;
    public TextMeshProUGUI waveCountdownTimer;

    //state machine
    public SpawnState state = SpawnState.COUNTING;

    //start 
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    //countdowns
    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //check if enemies are still alive
            if (!EnemyIsAlive())
            {
                //begine a new round
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //start wave spawning
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            waveCountdown = Mathf.Clamp(waveCountdown, 0f, Mathf.Infinity);
            waveCountdownTimer.text = string.Format("{0:00.00}", waveCountdown);
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! LOOPING...");
            //winscreen or endless mode or???
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        //search countdown timer
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            //Debug.Log("search counting");
            searchCountdown = 1f;
            //check every gameobject for tag enemy HEAVY LAG, so increase countdown to delay time between searches
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                //Debug.Log("search trigger");
                return false;
            }
        }

        //else true
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        //Debug.Log("Spawning Wave: " + _wave.name);

        roundCounter++;

        //for each enemy we are spawning itterate them
        state = SpawnState.SPAWNING;

        //delay after each spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    //enemy creation
    void SpawnEnemy(Transform _enemy)
    {
        //spawn enemy
        //Debug.Log("Spawning Enemy: " + _enemy.name);
        Instantiate(_enemy, spawnLocation.transform.position, transform.rotation);
    }
}
