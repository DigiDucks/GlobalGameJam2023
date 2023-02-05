using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : Singleton<WaveSpawner>
{
    //spawner state locks
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public int roundCounter = 0;

    //wave define
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

    //spawn locations
    public GameObject[] spawnLocations;
    private GameObject currentPoint;
    private int index;

    //timers
    public float timeBetweenWaves = 5f;

    public float waveCountdown;
    private float searchCountdown = 1f;
    public TextMeshProUGUI waveCountdownTimer;

    //state machine
    public SpawnState state = SpawnState.COUNTING;

    public static List<Enemy> EnemiesInWave = new List<Enemy>();

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
                //begin a new round
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
            waveCountdownTimer.text = $"{waveCountdown:00.00}";
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

    /// <summary>
    /// Query the status of aliveness [Li]
    /// </summary>
    /// <returns> non zeroness of the enemy count</returns>
    bool EnemyIsAlive() => EnemiesInWave.Count > 0;
    

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
       EnemiesInWave.Add(Instantiate(_enemy, RandomSpawnLocation().position, transform.rotation).GetComponent<Enemy>());
    }

    public void EnemyKilled(Enemy enemy)
    {
        EnemiesInWave.Remove(enemy);
    }


    //random spawn point from the array
    private Transform RandomSpawnLocation()
    {
        index = Random.Range(0, spawnLocations.Length);
        currentPoint = spawnLocations[index];

        return currentPoint.transform;
    }
}
