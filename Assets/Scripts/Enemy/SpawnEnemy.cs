using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    private EnemyControl[] enemies;

    [SerializeField] private GameObject enemyBase;
    [SerializeField] private GameObject enemiesSpawn;

    [SerializeField] private float minPosX, maxPosX, minPosY, maxPosY;
    private float posX, posY;
    [SerializeField] private int enemyCount;

    [SerializeField] private int waveCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartWave();
    }

    public void StartWave()
    {
        enemies = FindObjectsOfType<EnemyControl>();

        if (enemies.Length <= 0 && waveCount > 0)
        {
            SpawnEnemyLogic();
        }
    }

    public void SpawnEnemyLogic()
    {
        waveCount--;
        for (int i = 0; i < enemyCount; i++)
        {
            posX = Random.Range(minPosX, maxPosX);
            posY = Random.Range(minPosY, maxPosY);
            Vector3 enemyPos = new Vector2(posX, posY);
            Instantiate(enemyBase, enemyPos + transform.position, Quaternion.identity, enemiesSpawn.transform);
        }
    }

}
