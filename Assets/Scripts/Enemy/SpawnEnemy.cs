using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] players;

    private int difficult;
    private bool canSpawn = false;

    private EnemyControl[] enemies;

    [SerializeField] private GameObject enemyBase;

    [SerializeField] private CamCollider camCollider;
    [SerializeField] private GameObject levelCamCollider;

    [SerializeField] private float minPosX, maxPosX, minPosY, maxPosY;
    private float posX, posY;

    [SerializeField] private int[] enemyCount;

    [SerializeField] private int waveCount;

    [SerializeField] private GameObject nextWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        StartWave();
        EndLevel();
    }

    public void StartWave()
    {
        enemies = FindObjectsOfType<EnemyControl>();

        if (waveCount == 0 && enemies.Length == 0 && nextWave != null)
        {
            Destroy(levelCamCollider.gameObject);
            camCollider.NextLevel = false;
            nextWave.SetActive(true);
            gameObject.SetActive(false);
        }

        if (enemies.Length <= 0 && waveCount > 0 && canSpawn)
        {
            SpawnEnemyLogic();
        }
    }

    public void SpawnEnemyLogic()
    {
        waveCount--;
        for (int i = 0; i < enemyCount[waveCount]; i++)
        {
            posX = Random.Range(minPosX, maxPosX);
            posY = Random.Range(minPosY, maxPosY);
            Vector3 enemyPos = new Vector2(posX, posY);
            print((enemyPos + transform.position));
            Instantiate(enemyBase, enemyPos + transform.position, Quaternion.identity, transform);
        }
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        players = GameObject.FindGameObjectsWithTag("Player");

        print(players.Length);

        if (players.Length == 1 || players.Length == 2)
            difficult = 1;
        else if (players.Length == 3 || players.Length == 4)
            difficult = 2;

        for (int i = 0; i < enemyCount.Length; i++)
            enemyCount[i] = enemyCount[i] * difficult;

        canSpawn = true;
    }

    public void EndLevel()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ResetGame();

        if (enemies.Length == 0 && waveCount == 0 && canSpawn && nextWave == null)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }

}
