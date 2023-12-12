using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static WaveSpawner;

public partial class WaveSpawner : MonoBehaviour
{

    public Wave[] Waves;
    private int nextWave = 0;
    private int enemiesAlive = 0;
    public Transform[] SpawnPoints;
    public float timeBetweenWaves = 5f;
    private float waveCountDown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    //public PlayerScreenUI ScreenUI;

    //[SerializeField]
    //private Transform bossEnemyPrefab;
    public GameObject transformVessel;
    private bool vesselTriggered = false;

    void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.FINISHED || !vesselTriggered)
            return;

        //ScreenUI.SetWave(nextWave+1, enemiesAlive, waveCountDown);
        if(state== SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
                if (state == SpawnState.FINISHED || !vesselTriggered)
                {
                    //ScreenUI.SetWave(1, 1, -1f);
                    return; 
                }
            }
            else
            {
                return;
            }
        }
        if(waveCountDown <= 0 && vesselTriggered)
        {
            if(state!= SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(Waves[nextWave]));
            }
        }
        else
        {
            waveCountDown-=Time.deltaTime;
        }

        // Cheat
        // if (Input.GetKeyDown(KeyCode.F10))
        // {
        //     SpawnBossEnemy();
        // }
    }

    public void TriggerWaveSpawn()
    {
        if (!vesselTriggered && transformVessel != null)
        {
            vesselTriggered = true;
            //StartCoroutine(SpawnWave(Waves[nextWave])); // Initial spawn
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");
        //DataPersistenceManager.Instance.SaveGame();
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if ((nextWave + 1) % 5 == 0)
        {
            // Spawn a boss enemy
            //SpawnBossEnemy();
        }

        if (nextWave + 1 > Waves.Length - 1)
        {
            Debug.Log("All waves completed");
            state = SpawnState.FINISHED;
            SceneManager.LoadScene("VR-game-win");
            //GetComponent<GameManager>().GameComplete();
        }
        else
        {
            nextWave++;
            transformVessel.gameObject.SetActive(true);
            vesselTriggered = false;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown<= 0)
        {
            searchCountdown = 1f;
            enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemiesAlive==0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: "+nextWave);
        state = SpawnState.SPAWNING;
        for(int i=0; i<_wave.counts.Length; i++)
        {
            for (int j = 0; j < _wave.counts[i]; j++)
            {
                SpawnEnemy(_wave.enemies[i]);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Transform _sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, _sp.position, Quaternion.identity);
        enemiesAlive++;
    }

    // void SpawnBossEnemy()
    // {
    //     Debug.Log("Spawning boss enemy");
    //     Transform _sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
    //     Transform bossEnemy = Instantiate(bossEnemyPrefab, _sp.position, Quaternion.identity);
    //     bossEnemy.localScale = new Vector3(3f, 3f, 3f);

    //     bossEnemy.GetComponent<Enemy>().maxHealth = 300f;
    //     bossEnemy.GetComponent<Enemy>().attackDamage = 40;

    //     float bossEnemyHealth = bossEnemy.GetComponent<Enemy>().maxHealth;
    //     ScreenUI.UpdateBossEnemyHealth(bossEnemyHealth);
    //     enemiesAlive++;
    // }
}
