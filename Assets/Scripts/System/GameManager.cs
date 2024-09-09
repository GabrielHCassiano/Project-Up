using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] private PlayerControl[] players;
    private bool canSetPlayers = true;

    // Start is called before the first frame update
    void Awake()
    {
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

            players = FindObjectsByType<PlayerControl>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            canSetPlayers = false;

            for (int i = 0; i < inputsPlayers.Count; i++)
            {
                var player = players[i];
                players[i] = players[player.IdPlayer - 1];
                players[player.IdPlayer - 1] = player;

                inputsPlayers[i].transform.parent = players[i].gameObject.transform;
                players[i].gameObject.SetActive(true);
            }
        }
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
}
