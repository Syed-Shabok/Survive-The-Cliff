using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    public List<GameObject> enemyPrefabs;
    public List<GameObject> powerupPrefabs;
    private float spawnRange = 9.0f;
    public int enemyCount;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {   
        SpawnPowerup();
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {   
            ++waveNumber;
            SpawnPowerup();
            SpawnEnemyWave(waveNumber);
        }
    }

    // Spawns a wave of enemies.
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; ++i)
        {   
            int randIndex = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[randIndex], GenerateSpawnPosition(), enemyPrefabs[randIndex].transform.rotation);
        }
    }
    // Spaens a powerup for the player.    
    private void SpawnPowerup()
    {   
        int randIndex = Random.Range(0, powerupPrefabs.Count);
        Instantiate(powerupPrefabs[randIndex], GenerateSpawnPosition(), powerupPrefabs[randIndex].transform.rotation);
    }

    // Returns a random position for the enemy to spawn.
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
