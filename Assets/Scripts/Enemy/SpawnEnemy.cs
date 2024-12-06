using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
//using Unity.AI.Navigation;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] players;

    private int difficult;
    private bool canSpawn = false;

    private EnemyControl[] enemies;

    [SerializeField] private CamCollider camCollider;
    [SerializeField] private GameObject levelCamCollider;

    [SerializeField] private int waveCount;
    [SerializeField] private GameObject[] subWavesL1;
    [SerializeField] private GameObject[] subWavesL2;

    [SerializeField] private GameObject nextWave;
    [SerializeField] private GameObject nextWavePoint;

    [SerializeField] private NavMeshSurface navMeshSurface;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        StartWave();
    }

    public void StartWave()
    {
        navMeshSurface.BuildNavMesh();

        enemies = FindObjectsOfType<EnemyControl>();

        if (waveCount == 0 && enemies.Length == 0 && nextWave != null)
        {
            nextWavePoint.SetActive(true);
            Destroy(levelCamCollider.gameObject);
            camCollider.NextLevel = false;
        }
        else if (waveCount == 0 && enemies.Length == 0 && nextWave == null)
        {
            GameManager.Instance.WinGame = true;
        }

        if (enemies.Length <= 0 && waveCount > 0 && canSpawn)
        {
            SpawnEnemyLogic();
        }
    }

    public void SpawnEnemyLogic()
    {
        waveCount--;

        switch (difficult)
        {
            case 1:
                subWavesL1[waveCount].SetActive(true);
                break;
            case 2:
                subWavesL2[waveCount].SetActive(true);
                break;
        }
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 1 || players.Length == 2)
            difficult = 1;
        else if (players.Length == 3 || players.Length == 4)
            difficult = 2;

        canSpawn = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundCollisionPlayer") && nextWavePoint.activeSelf)
        {
            nextWave.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
