using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsPlayers : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool button1, button2, button3, button3Hold, button4;
    [SerializeField] private bool buttonR1, buttonR2, buttonL1, buttonL2;
    [SerializeField] private bool buttonStart, buttonSelect;

    [SerializeField] private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            playerInput = GetComponent<PlayerInput>();
            playerData = ScriptableObject.CreateInstance<PlayerData>();
            playerData.PlayerName = "Player" + (playerInput.playerIndex + 1);

            transform.parent = GameManager.Instance.transform;

            GameManager.Instance.InputsPlayers.Add(this);
            GameManager.Instance.PlayersDatas.Add(playerData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region GetAndSet

    public Vector2 MoveDirection 
    { 
        get { return moveDirection; } 
        set {  moveDirection = value; }
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

    public bool Button3Hold
    {
        get { return button3Hold; }
        set { button3Hold = value; }
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

    public void Button3HoldSet(InputAction.CallbackContext context)
    {
        button3Hold = context.action.triggered;
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
}
