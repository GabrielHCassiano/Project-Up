using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.SceneManagement;

public class InputsPlayers : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool buttonDashLeft, buttonDashRight;
    [SerializeField] private bool button1, button2, button3, button4Hold, button4;
    [SerializeField] private bool buttonR1, buttonR2, buttonL1, buttonL2;
    [SerializeField] private bool buttonStart, buttonSelect;

    [Header("Buttons IDs Declarations")]
    [SerializeField] private List<string> keyboardIDs;
    [SerializeField] private List<string> playstationIDs;
    [SerializeField] private List<string> xboxIDs;
    [SerializeField] private List<string> nintendoIDs;
    [SerializeField] private List<string> genericIDs;

    private string inputName,idButton1, idButton2, idButton3, idButton4, idUp, idDown, idLeft, idRight, idDash;

    [SerializeField] private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            if (GameManager.Instance != null)
            {
                playerInput = GetComponent<PlayerInput>();
                playerData = ScriptableObject.CreateInstance<PlayerData>();

                switch (playerInput.playerIndex + 1)
                {
                    case 1:
                        playerData.PlayerName = "<color=red>Player 1</color>";
                        break;
                    case 2:
                        playerData.PlayerName = "<color=blue>Player 2</color>";
                        break;
                    case 3:
                        playerData.PlayerName = "<color=green>Player 3</color>";
                        break;
                    case 4:
                        playerData.PlayerName = "<color=yellow>Player 4</color>";
                        break;
                }

                transform.parent = GameManager.Instance.transform;

                GameManager.Instance.InputsPlayers.Add(this);
                GameManager.Instance.PlayersDatas.Add(playerData);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        TyperInput();
    }

    #region TyperInput

    public void TyperInput()
    {
        if (playerInput.devices[0] is Keyboard)
        {
            inputName = "Keyboard";
            idButton1 = keyboardIDs[0];
            idButton2 = keyboardIDs[1];
            idButton3 = keyboardIDs[2];
            idButton4 = keyboardIDs[3];
            idUp = keyboardIDs[4];
            idDown = keyboardIDs[5];
            idLeft = keyboardIDs[6];
            idRight = keyboardIDs[7];
            idDash = keyboardIDs[8];
        }
        else if (playerInput.devices[0].description.manufacturer != "")
        {
            switch (playerInput.devices[0].description.manufacturer)
            {
                case "Sony Interactive Entertainment":
                    inputName = "Playstation";
                    idButton1 = playstationIDs[0];
                    idButton2 = playstationIDs[1];
                    idButton3 = playstationIDs[2];
                    idButton4 = playstationIDs[3];
                    idUp = playstationIDs[4];
                    idDown = playstationIDs[5];
                    idLeft = playstationIDs[6];
                    idRight = playstationIDs[7];
                    idDash = playstationIDs[8];
                    break;
                case "Nintendo":
                    inputName = "Nintendo";
                    idButton1 = nintendoIDs[0];
                    idButton2 = nintendoIDs[1];
                    idButton3 = nintendoIDs[2];
                    idButton4 = nintendoIDs[3];
                    idUp = nintendoIDs[4];
                    idDown = nintendoIDs[5];
                    idLeft = nintendoIDs[6];
                    idRight = nintendoIDs[7];
                    idDash = nintendoIDs[8];
                    break;
                default:
                    inputName = "Generic";
                    idButton1 = genericIDs[0];
                    idButton2 = genericIDs[1];
                    idButton3 = genericIDs[2];
                    idButton4 = genericIDs[3];
                    idUp = genericIDs[4];
                    idDown = genericIDs[5];
                    idLeft = genericIDs[6];
                    idRight = genericIDs[7];
                    idDash = genericIDs[8];
                    break;
            }
        }
        else
        {
            if (playerInput.devices[0] is XInputController)
            {
                inputName = "Xbox";
                idButton1 = xboxIDs[0];
                idButton2 = xboxIDs[1];
                idButton3 = xboxIDs[2];
                idButton4 = xboxIDs[3];
                idUp = xboxIDs[4];
                idDown = xboxIDs[5];
                idLeft = xboxIDs[6];
                idRight = xboxIDs[7];
                idDash = xboxIDs[8];
            }
            else
            {
                inputName = "Generic";
                idButton1 = genericIDs[0];
                idButton2 = genericIDs[1];
                idButton3 = genericIDs[2];
                idButton4 = genericIDs[3];
                idUp = genericIDs[4];
                idDown = genericIDs[5];
                idLeft = genericIDs[6];
                idRight = genericIDs[7];
                idDash = genericIDs[8];
            }
        }
    }

    #endregion

    #region GetAndSet

    public Vector2 MoveDirection 
    { 
        get { return moveDirection; } 
        set {  moveDirection = value; }
    }

    public bool ButtonDashLeft
    {
        get { return buttonDashLeft; }
        set { buttonDashLeft = value; }
    }

    public bool ButtonDashRight
    {
        get { return buttonDashRight; }
        set { buttonDashRight = value; }
    }

    public bool Button1
    {
        get { return button1; }
        set { button1 = value; }
    }

    public bool Button2
    {
        get { return button2; }
        set { button2 = value; }
    }

    public bool Button3
    {
        get { return button3; }
        set { button3 = value; }
    }

    public bool Button4Hold
    {
        get { return button4Hold; }
        set { button4Hold = value; }
    }

    public bool Button4
    {
        get { return button4; }
        set { button4 = value; }
    }

    public bool ButtonR1
    {
        get { return buttonR1; }
        set { buttonR1 = value; }
    }

    public bool ButtonR2
    {
        get { return buttonR2; }
        set { buttonR2 = value; }
    }

    public bool ButtonL1
    {
        get { return buttonL1; }
        set { buttonL1 = value; }
    }

    public bool ButtonL2
    {
        get { return buttonL2; }
        set { buttonL2 = value; }
    }

    public bool ButtonStart
    {
        get { return buttonStart; }
        set { buttonStart = value; }
    }

    public bool ButtonSelect
    {
        get { return buttonSelect; }
        set { buttonSelect = value; }
    }

    public string IdButton1
    {
        get { return idButton1; }
        set { idButton1 = value; }
    }

    public string IdButton2
    {
        get { return idButton2; }
        set { idButton2 = value; }
    }

    public string IdButton3
    {
        get { return idButton3; }
        set { idButton3 = value; }
    }

    public string IdButton4
    {
        get { return idButton4; }
        set { idButton4 = value; }
    }

    public string IdUp
    {
        get { return idUp; }
        set { idUp = value; }
    }

    public string IdDown
    {
        get { return idDown; }
        set { idDown = value; }
    }

    public string IdLeft
    {
        get { return idLeft; }
        set { idLeft = value; }
    }
    public string IdRight
    {
        get { return idRight; }
        set { idRight = value; }
    }

    public string IdDash
    {
        get { return idDash; }
        set { idDash = value; }
    }

    public PlayerData PlayerData
    {
        get { return playerData; }
        set { playerData = value; }
    }

    #endregion

    #region ButtonsSet

    public void MoveSet(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void ButtonDashLeftSet(InputAction.CallbackContext context)
    {
        buttonDashLeft = context.action.triggered;
    }

    public void ButtonDashRightSet(InputAction.CallbackContext context)
    {
        buttonDashRight = context.action.triggered;
    }

    public void Button1Set(InputAction.CallbackContext context)
    {
        button1 = context.action.triggered;
    }

    public void Button2Set(InputAction.CallbackContext context)
    {
        button2 = context.action.triggered;
    }

    public void Button3Set(InputAction.CallbackContext context)
    {
        button3 = context.action.triggered;
    }

    public void Button4HoldSet(InputAction.CallbackContext context)
    {
        button4Hold = context.action.triggered;
    }

    public void Button4Set(InputAction.CallbackContext context)
    {
        button4 = context.action.triggered;
    }

    public void ButtonR1Set(InputAction.CallbackContext context)
    {
        buttonR1 = context.action.triggered;
    }

    public void ButtonR2Set(InputAction.CallbackContext context)
    {
        buttonR2 = context.action.triggered;
    }

    public void ButtonL1Set(InputAction.CallbackContext context)
    {
        buttonL1 = context.action.triggered;
    }

    public void ButtonL2Set(InputAction.CallbackContext context)
    {
        buttonL2 = context.action.triggered;
    }

    public void ButtonStartSet(InputAction.CallbackContext context)
    {
        buttonStart = context.action.triggered;
    }

    public void ButtonSelectSet(InputAction.CallbackContext context)
    {
        buttonSelect = context.action.triggered;
    }

    #endregion

    public void ResetAllInputs()
    {
        moveDirection = Vector2.zero;
        buttonDashLeft = false;
        buttonDashRight = false;
        button1 = false;
        button2 = false;
        button3 = false;
        button4Hold = false;
        button4 = false;
        buttonR1 = false;
        buttonR2 = false;
        buttonL1 = false;
        buttonL2 = false;
        buttonStart = false;
        buttonSelect = false;
    }
}
