using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject obstaclePrefab;
    public GameObject[] enemyPrefabs = new GameObject[2];
    private PlayerController playerControllerScript;

    private float xRange = 25.0f;
    private float yRange = 8.0f;
    private float startDelay = 2.0f;
    private float spawnInterval = 10.0f;

    public int waveNumber = 1;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, spawnInterval);
        SpawnPowerup();
        SpawnEnemyWave(waveNumber);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;

            //starts the new wave with an additional enemy
            if (enemyCount == 0)
            {
                waveNumber++;
                SpawnPowerup();
                SpawnEnemyWave(waveNumber);
            }
        }
        
    }

    //creates the spawn position for the enemies and obstacle
    private Vector3 GenerateRightSpawnPosition()
    {
        float spawnPosY = Random.Range(-yRange, yRange);
        Vector3 randomPos = new Vector3(36, spawnPosY, -2);

        return randomPos;
    }

    //creates the spawn position for the powerup
    private Vector3 GeneratePowerupSpawnPosition()
    {
        float spawnPosX = Random.Range(-xRange, xRange);
        float spawnPosY = Random.Range(-yRange, yRange);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -2);

        return randomPos;
    }

    //spawns a random enemy from the 2 options
    void SpawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], GenerateRightSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
    }

    //spawns the powerup
    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GeneratePowerupSpawnPosition(), powerupPrefab.transform.rotation);
    }

    //spawns the obstacle while the game is active
    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, GenerateRightSpawnPosition(), obstaclePrefab.transform.rotation);
        }
    }

    //increases the enemies in each wave
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnRandomEnemy();
        }
    }
}
