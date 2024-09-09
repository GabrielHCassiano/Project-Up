using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class CharSelect : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject buttonStart;

    [SerializeField] private GameObject[] pressStartObject;
    [SerializeField] private GameObject[] selectObject;
    [SerializeField] private GameObject[] select;
    [SerializeField] private GameObject[] selectCharIcon;

    [SerializeField] private TextMeshProUGUI[] charName;
    [SerializeField] private Animator[] charAnimator;

    [SerializeField] private string[] charsNames;
    [SerializeField] private Sprite[] charsIcons;
    [SerializeField] private RuntimeAnimatorController[] charsAnimators;

    [SerializeField] private int[] idChar;

    [SerializeField] private bool[] playerConfirmed;
    [SerializeField] private bool[] charConfirmed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.InputsPlayers[0].Button3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        BackMenu();
        SelectChar();
        StartFase();
    }

    public void BackMenu()
    {
        if (gameManager.InputsPlayers[0].ButtonSelect && !playerConfirmed[0])
        {
            gameManager.InputsPlayers[0].ButtonSelect = false;

            if (gameManager.InputsPlayers.Count > 1)
            {
                for (int i = 1; i < gameManager.InputsPlayers.Count + 1; i++)
                {
                    idChar[gameManager.InputsPlayers.Count - 1] = 0;
                    playerConfirmed[gameManager.InputsPlayers.Count - 1] = false;
                    charConfirmed[gameManager.InputsPlayers.Count - 1] = false;
                    pressStartObject[gameManager.InputsPlayers.Count - 1].SetActive(true);
                    selectObject[gameManager.InputsPlayers.Count - 1].SetActive(false);
                    select[gameManager.InputsPlayers.Count - 1].SetActive(false);
                    var inputObject = gameManager.InputsPlayers[gameManager.InputsPlayers.Count - 1];
                    gameManager.InputsPlayers.Remove(inputObject);
                    Destroy(inputObject.gameObject);
                }
            }

            idChar[0] = 0;
            playerConfirmed[0] = false;
            charConfirmed[0] = false;

            eventSystem.SetSelectedGameObject(buttonStart);
            mainMenu.SetActive(true);
            selectMenu.SetActive(false);
        }
    }

    public void SelectChar()
    {
        for (int i = 0; i < gameManager.InputsPlayers.Count; i++)
        {
            pressStartObject[i].SetActive(!gameManager.InputsPlayers[i].gameObject.activeSelf);
            selectObject[i].SetActive(gameManager.InputsPlayers[i].gameObject.activeSelf);
            select[i].SetActive(gameManager.InputsPlayers[i].gameObject.activeSelf);

            charName[i].text = charsNames[idChar[i]];
            select[i].transform.parent = selectCharIcon[idChar[i]].transform;
            select[i].transform.position = selectCharIcon[idChar[i]].transform.position;
            charAnimator[i].runtimeAnimatorController = charsAnimators[idChar[i]];

            if (gameManager.InputsPlayers[i].MoveDirection.x < 0 && !playerConfirmed[i])
            {
                gameManager.InputsPlayers[i].MoveDirection = new Vector2(0, 0);
                if (idChar[i] - 1 < 0)
                    idChar[i] = charsNames.Length - 1;
                else
                    idChar[i]--;
            }
            else if (gameManager.InputsPlayers[i].MoveDirection.x > 0 && !playerConfirmed[i])
            {
                gameManager.InputsPlayers[i].MoveDirection = new Vector2(0, 0);
                if (idChar[i] + 1 > charsNames.Length - 1)
                    idChar[i] = 0;
                else
                    idChar[i]++;
            }

            if (gameManager.InputsPlayers[i].Button3 && !playerConfirmed[i] && !charConfirmed[idChar[i]])
            {
                charConfirmed[idChar[i]] = true;
                playerConfirmed[i] = true;
                charAnimator[i].SetBool("Intro", true);
                gameManager.InputsPlayers[i].Button3 = false;
                gameManager.PlayersDatas[i].CharName = charsNames[idChar[i]];
                gameManager.PlayersDatas[i].IconSprite = charsIcons[idChar[i]];
                gameManager.PlayersDatas[i].AnimatorController = charsAnimators[idChar[i]];

            }
            else if ((gameManager.InputsPlayers[i].Button2 || gameManager.InputsPlayers[i].ButtonSelect) && playerConfirmed[i])
            {
                charConfirmed[idChar[i]] = false;
                playerConfirmed[i] = false;
                charAnimator[i].SetBool("Intro", false);
                gameManager.InputsPlayers[i].Button2 = false;
                gameManager.InputsPlayers[i].ButtonSelect = false;
                gameManager.PlayersDatas[i].CharName = null;
                gameManager.PlayersDatas[i].IconSprite = null;
                gameManager.PlayersDatas[i].AnimatorController = null;
            }
        }
    }

    public void StartFase()
    {
        if (playerConfirmed[0] && gameManager.InputsPlayers.Count == 1)
            StartLevel();
        else if (playerConfirmed[0] && playerConfirmed[1] && gameManager.InputsPlayers.Count == 2)
            StartLevel();
        else if (playerConfirmed[0] && playerConfirmed[1] && playerConfirmed[2] && gameManager.InputsPlayers.Count == 3)
            StartLevel();
        else if (playerConfirmed[0] && playerConfirmed[1] && playerConfirmed[2] && playerConfirmed[3] && gameManager.InputsPlayers.Count == 4)
            StartLevel();
    }

    public void StartLevel()
    {
        if (gameManager.InputsPlayers[0].Button3)
        {
            gameManager.InputsPlayers[0].Button3 = false;
            gameManager.StartLevel("Fase1");
        }
    }
}
