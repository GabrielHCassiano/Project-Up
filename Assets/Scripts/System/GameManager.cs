using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using NavMeshPlus.Components;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerInputManager playerInputManager;

    [SerializeField] private GameObject pressStartMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject buttonStart;
    private bool startGame = true;

    [SerializeField] private List<InputsPlayers> inputsPlayers;
    [SerializeField] private List<PlayerData> playersDatas;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] players;

    [SerializeField] private GameObject canvas;
    private bool canSetPlayers = true;

    private bool inPause = false;
    [SerializeField] private GameObject pausePanel;

    private bool winGame;
    [SerializeField] private TextMeshProUGUI nameTextEndGame;

    private bool canEndGame = true;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private List<TextMeshProUGUI> scores;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        playerInputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StartMenu();
        SetPlayer();
        PauseGame();
        BackMenu();
        EndGame();
    }

    public void StartLevel(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
    }

    public void StartMenu()
    {
        if (pressStartMenu != null && inputsPlayers.Count > 0 && startGame)
        {
            startGame = false;
            pressStartMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        if (pressStartMenu != null && inputsPlayers.Count > 0 && inputsPlayers[0].ButtonSelect && mainMenu.activeSelf && !startGame)
        {
            inputsPlayers[0].ButtonSelect = false;
            for (int i = 0; i < inputsPlayers.Count + 1; i++)
            {
                var inputObject = inputsPlayers[inputsPlayers.Count - 1];
                playersDatas.Remove(inputObject.PlayerData);
                inputsPlayers.Remove(inputObject);
                Destroy(inputObject.gameObject);
            }
            startGame = true;
            eventSystem.SetSelectedGameObject(buttonStart);
            pressStartMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void SetPlayer()
    {
        if (SceneManager.GetActiveScene().name == "Fase1" && canSetPlayers)
        {
            canSetPlayers = false;
            canvas.SetActive(true);
            //canvas.SetActive(true);
            //canvas.transform.parent = null;

            for (int i = 0; i < inputsPlayers.Count; i++)
            {
                players[i].GetComponent<PlayerControl>().InputsPlayers = inputsPlayers[i];
                players[i].SetActive(true);
                inputsPlayers[i].transform.parent = players[i].transform;
                inputsPlayers[i].ResetAllInputs();
            }
            
            /*players = FindObjectsByType<PlayerControl>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            canSetPlayers = false;

            for (int i = 0; i < players.Length; i++)
            {
                var player = players[i];
                players[i] = players[player.IdPlayer - 1];
                players[player.IdPlayer - 1] = player;
            }

            for (int i = 0; i < inputsPlayers.Count; i++)
            {
                players[i].InputsPlayers = inputsPlayers[i];
                players[i].gameObject.SetActive(true);
                inputsPlayers[i].transform.parent = players[i].gameObject.transform;
                inputsPlayers[i].ResetAllInputs();
            }*/
        }
    }

    public void BackMenu()
    {
        /*if (SceneManager.GetActiveScene().name == "Fase1" && !endPanel.activeSelf)
        {
            for (int i = 0; i < inputsPlayers.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha0))
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene(0);
                }
            }
        }*/
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey(KeyCode.Alpha0) == true)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(0);
            }
        }
    }

    public void PauseGame()
    {
        if (SceneManager.GetActiveScene().name == "Fase1" && !endPanel.activeSelf)
        {

            for (int i = 0; i < inputsPlayers.Count; i++)
            {
                if (inputsPlayers[i].ButtonSelect)
                {
                    inputsPlayers[i].ButtonSelect = false;
                    inPause = !inPause;
                }
            }

            if (inPause)
            {
                for (int i = 0; i < inputsPlayers.Count; i++)
                {
                    inputsPlayers[i].ResetAllInputs();
                }
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pausePanel.SetActive(false);
                FindObjectOfType<NavMeshSurface>().BuildNavMesh();
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume()
    {
        inPause = false;
    }

    public void EndGame()
    {
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            if (winGame)
            {
                nameTextEndGame.text = "<color=green> VITÓRIA";
            }
            else
                nameTextEndGame.text = "<color=red> GAME OVER";

            if (inputsPlayers.Count == 1 && players[0].GetComponent<PlayerControl>().PlayerStatus.TrueDeath || winGame)
            {
                endPanel.SetActive(true);
                scores[0].gameObject.SetActive(true);
                scores[0].text = playersDatas[0].PlayerName + " (" + playersDatas[0].CharName + ") Total Hit Score: " + playersDatas[0].ComboScore.ToString();
            }
            if (inputsPlayers.Count == 2 && ((players[0].GetComponent<PlayerControl>().PlayerStatus.TrueDeath
                                         && players[1].GetComponent<PlayerControl>().PlayerStatus.TrueDeath) || winGame))
            {
                playersDatas.Sort((p1, p2) => p1.ComboScore.CompareTo(p2.ComboScore));
                playersDatas.Reverse();
                endPanel.SetActive(true);
                scores[0].gameObject.SetActive(true);
                scores[1].gameObject.SetActive(true);
                scores[0].text = playersDatas[0].PlayerName + " (" + playersDatas[0].CharName + ") Total Hit Score: " + playersDatas[0].ComboScore.ToString();
                scores[1].text = playersDatas[1].PlayerName + " (" + playersDatas[1].CharName + ") Total Hit Score: " + playersDatas[1].ComboScore.ToString();
            }
            if (inputsPlayers.Count == 3 && ((players[0].GetComponent<PlayerControl>().PlayerStatus.TrueDeath
                                         && players[1].GetComponent<PlayerControl>().PlayerStatus.TrueDeath
                                         && players[2].GetComponent<PlayerControl>().PlayerStatus.TrueDeath) || winGame))
            {
                playersDatas.Sort((p1, p2) => p1.ComboScore.CompareTo(p2.ComboScore));
                playersDatas.Reverse();
                endPanel.SetActive(true);
                scores[0].gameObject.SetActive(true);
                scores[1].gameObject.SetActive(true);
                scores[2].gameObject.SetActive(true);
                scores[0].text = playersDatas[0].PlayerName + " (" + playersDatas[0].CharName + ") Total Hit Score: " + playersDatas[0].ComboScore.ToString();
                scores[1].text = playersDatas[1].PlayerName + " (" + playersDatas[1].CharName + ") Total Hit Score: " + playersDatas[1].ComboScore.ToString();
                scores[2].text = playersDatas[2].PlayerName + " (" + playersDatas[2].CharName + ") Total Hit Score: " + playersDatas[2].ComboScore.ToString();
            }
            if (inputsPlayers.Count == 4 && ((players[0].GetComponent<PlayerControl>().PlayerStatus.TrueDeath
                                         && players[1].GetComponent<PlayerControl>().PlayerStatus.TrueDeath
                                         && players[2].GetComponent<PlayerControl>().PlayerStatus.TrueDeath
                                         && players[3].GetComponent<PlayerControl>().PlayerStatus.TrueDeath) || winGame))
            {
                playersDatas.Sort((p1, p2) => p1.ComboScore.CompareTo(p2.ComboScore));
                playersDatas.Reverse();
                endPanel.SetActive(true);
                scores[0].gameObject.SetActive(true);
                scores[1].gameObject.SetActive(true);
                scores[2].gameObject.SetActive(true);
                scores[3].gameObject.SetActive(true);
                scores[0].text = playersDatas[0].PlayerName + " (" + playersDatas[0].CharName + ") Total Hit Score: " + playersDatas[0].ComboScore.ToString();
                scores[1].text = playersDatas[1].PlayerName + " (" + playersDatas[1].CharName + ") Total Hit Score: " + playersDatas[1].ComboScore.ToString();
                scores[2].text = playersDatas[2].PlayerName + " (" + playersDatas[2].CharName + ") Total Hit Score: " + playersDatas[2].ComboScore.ToString();
                scores[3].text = playersDatas[3].PlayerName + " (" + playersDatas[3].CharName + ") Total Hit Score: " + playersDatas[3].ComboScore.ToString();

            }

            if (endPanel.activeSelf && canEndGame)
            {
                StartCoroutine(CooldownEndGame());
            }
        }
    }

    private IEnumerator CooldownEndGame()
    {
        canEndGame = false;
        for (int i = 0; i < inputsPlayers.Count; i++)
        {
            inputsPlayers[i].ResetAllInputs();
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public List<InputsPlayers> InputsPlayers
    {
        get { return inputsPlayers; }
        set { inputsPlayers = value; }
    }

    public List<PlayerData> PlayersDatas
    {
        get { return playersDatas; }
        set { playersDatas = value; }
    }

    public bool WinGame
    {
        get { return winGame; }
        set { winGame = value; }
    }
}
