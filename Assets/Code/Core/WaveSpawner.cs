using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGJ2023;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class WaveSpawner : Singleton<WaveSpawner>
{
    //spawner state locks
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public int waveCounter = 0;
    [SerializeField] private int RoundInterval = 5;


    //wave array and count and spawn location
    public List<Wave> waves = new List<Wave>();
    private int nextWave = 0;

    //spawn locations
    public GameObject[] spawnLocations;
    private GameObject currentPoint;
    private int index;

    //timers
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public TextMeshProUGUI waveCountdownTimer;

    //state machine
    public SpawnState state = SpawnState.COUNTING;

    public static readonly List<Enemy> EnemiesInWave = new List<Enemy>();

    //start 
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        EventManager.ArenaChange.Invoke();
    }

    //countdowns
    private void Update()
    {
        //Cheat Codes
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
            state = SpawnState.COUNTING;
        
        
        switch(state)
        {
            case SpawnState.SPAWNING:
                //Wait for coroutine to finish
                break;
            case SpawnState.WAITING:
                //check if enemies are still alive
                if (!EnemyIsAlive())
                    WaveCompleted();//begin a new round
                break;
            case SpawnState.COUNTING:
                waveCountdown -= Time.deltaTime;
                waveCountdown = Mathf.Clamp(waveCountdown, 0f, Mathf.Infinity);
                waveCountdownTimer.text = $"{waveCountdown:00.00}";
                

                if (waveCountdown <= 0)
                {
                    state = SpawnState.SPAWNING;
                    StartCoroutine(SpawnWave(waves[Random.Range(0,waves.Count)]));
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        nextWave++;
        
        if (nextWave % RoundInterval == 0)
        {
            EventManager.ArenaChange.Invoke();
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
        waveCounter++;

        foreach (var element in _wave.enemies)
        {
            for (int i = 0; i < element.count; i++)
            {
                SpawnEnemy(element.Enemy);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }
        
        //delay after each spawn
   

        state = SpawnState.WAITING;
        yield break;
    }

    //enemy creation
    void SpawnEnemy(GameObject _enemy)
    {
        //spawn enemy
        //Debug.Log("Spawning Enemy: " + _enemy.name);
       EnemiesInWave.Add(Instantiate(_enemy, RandomSpawnLocation().position, transform.rotation).GetComponent<Enemy>());
    }


    //random spawn point from the array
    private Transform RandomSpawnLocation()
    {
        index = Random.Range(0, spawnLocations.Length);
        currentPoint = spawnLocations[index];

        return currentPoint.transform;
    }
}
