using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private PlayerControl[] players;

    [SerializeField] private TextMeshProUGUI[] tutorialText;
    [SerializeField] private GameObject[] panelTutorial;


    private bool[] moveFrontConfirm = new bool[4];
    private bool[] canMoveFrontConfirm = new bool[4];

    private bool[] moveBackConfirm = new bool[4];
    private bool[] canMoveBackConfirm = new bool[4];

    private bool[] moveUpConfirm = new bool[4];
    private bool[] canMoveUpConfirm = new bool[4];

    private bool[] moveDownConfirm = new bool[4];
    private bool[] canMoveDownConfirm = new bool[4];

    private bool[] dashConfirm = new bool[4];
    private bool[] canDashConfirm = new bool[4];

    private bool[] attackConfirm = new bool[4];
    private bool[] canAttackConfirm = new bool[4];

    private bool[] attackComboConfirm = new bool[4];
    private bool[] canAttackComboConfirm = new bool[4];

    private bool[] attackHeavyConfirm = new bool[4];
    private bool[] canAttackHeavyConfirm = new bool[4];

    private bool[] attackSpecialConfirm = new bool[4];
    private bool[] canAttackSpecialConfirm = new bool[4];

    private bool[] getItenConfirm = new bool[4];
    private bool[] canGetItenConfirm = new bool[4];

    private bool endTutorial = false;

    private bool canSkip;

    [SerializeField] private GameObject[] iconBox;
    [SerializeField] private GameObject[] iconCandy;

    [SerializeField] private GameObject candy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CooldownSkip());
    }

    // Update is called once per frame
    void Update()
    {
        CountTutorial();
        CandyIten();
        EndTutorialLogic();
    }

    public bool EndTutorial
    {
        get { return endTutorial; }
        set { endTutorial = value; }
    }

    public IEnumerator CooldownSkip()
    {
        yield return new WaitForSeconds(1);
        canSkip = true;
    }

    public void CountTutorial()
    {
        if (!endTutorial)
        {
            for (int i = 0; i < GameManager.Instance.InputsPlayers.Count; i++)
            {
                panelTutorial[i].SetActive(true);

                if (players[i].InputsPlayers.ButtonStart && canSkip)
                {
                    players[i].InputsPlayers.ButtonStart = false;
                    getItenConfirm[i] = true;
                }

                if (!getItenConfirm[i])
                {
                    if (!moveFrontConfirm[i] && !canMoveFrontConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdRight + "> para move para frente <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].InputsPlayers.MoveDirection.x > 0)
                        {
                            canMoveFrontConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdRight + "> para move para frente <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", moveFrontConfirm));
                        }
                    }
                    if (moveFrontConfirm[i] && !moveBackConfirm[i] && !canMoveBackConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdLeft + "> para move para trás <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].InputsPlayers.MoveDirection.x < 0)
                        {
                            canMoveBackConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdLeft + "> para move para trás <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", moveBackConfirm));
                        }
                    }
                    if (moveBackConfirm[i] && !moveUpConfirm[i] && !canMoveUpConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdUp + "> para move para cima <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].InputsPlayers.MoveDirection.y > 0)
                        {
                            canMoveUpConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdUp + "> para move para cima <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", moveUpConfirm));
                        }
                    }
                    if (moveUpConfirm[i] && !moveDownConfirm[i] && !canMoveDownConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdDown + "> para move para baixo <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].InputsPlayers.MoveDirection.y < 0)
                        {
                            canMoveDownConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdDown + "> para move para baixo <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", moveDownConfirm));
                        }
                    }
                    if (moveDownConfirm[i] && !dashConfirm[i] && !canDashConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdDash + "> para usar dash <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].PlayerMovement.InDash)
                        {
                            canDashConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdDash + "> para usar dash <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", dashConfirm));
                        }
                    }
                    if (dashConfirm[i] && !attackConfirm[i] && !canAttackConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdButton4 + "> para Atacar <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].PlayerCombat.InCombo > 0)
                        {
                            canAttackConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdButton4 + "> para Atacar <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", attackConfirm));
                        }
                    }
                    if (attackConfirm[i] && !attackComboConfirm[i] && !canAttackComboConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdButton4 + "> <sprite=" + players[i].InputsPlayers.IdButton4 + "> <sprite=" + players[i].InputsPlayers.IdButton4 + "> para forma um combo na caixa    <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        iconBox[i].SetActive(true);
                        if (players[i].PlayerCombat.Combo == 3)
                        {
                            canAttackComboConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdButton4 + "> <sprite=" + players[i].InputsPlayers.IdButton4 + "> <sprite=" + players[i].InputsPlayers.IdButton4 + "> para forma um combo na caixa    <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", attackComboConfirm));
                        }
                    }

                    if (attackComboConfirm[i] && !attackHeavyConfirm[i] && !canAttackHeavyConfirm[i])
                    {
                        tutorialText[i].text = "Segure <sprite=" + players[i].InputsPlayers.IdButton4 + "> para usar o ataque pesado <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].PlayerCombat.HeavyAttack)
                        {
                            canAttackHeavyConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Segure <sprite=" + players[i].InputsPlayers.IdButton4 + "> para usar o ataque pesado <color=green>[1 / 1]</color>\n <color=red>Termine o Tutorial</color>", attackHeavyConfirm));
                        }
                    }
                    if (attackHeavyConfirm[i] && !attackSpecialConfirm[i] && !canAttackSpecialConfirm[i])
                    {
                        players[i].PlayerStatus.Stamina = 25;
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdButton2 + "> para usar o ataque especial <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        if (players[i].PlayerCombat.SpecialAttack)
                        {
                            canAttackSpecialConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdButton2 + "> para usar o ataque especial <color=green>[1 / 1]</color>\n <color=red>Termine o Tutorial</color>", attackSpecialConfirm));
                        }
                    }
                    if (attackSpecialConfirm[i] && !getItenConfirm[i] && !canGetItenConfirm[i])
                    {
                        tutorialText[i].text = "Aperte <sprite=" + players[i].InputsPlayers.IdButton1 + "> para pegar o item     <color=red>[0 / 1]</color> \n <color=red>Termine o Tutorial</color>";
                        iconCandy[i].SetActive(true);
                        if (players[i].PlayerStatus.GetIten)
                        {
                            canGetItenConfirm[i] = true;
                            StartCoroutine(TutorialCooldown(i, "Aperte <sprite=" + players[i].InputsPlayers.IdButton1 + "> para pegar o item     <color=green>[1 / 1]</color> \n <color=red>Termine o Tutorial</color>", getItenConfirm));
                        }
                    }
                }

                if (getItenConfirm[i])
                {
                    players[i].PlayerStatus.Stamina = 25;
                    tutorialText[i].text = "\n <color=green>Termine o Tutorial<color=green>";
                }

            }
        }
    }

    IEnumerator TutorialCooldown(int id, string text, bool[] confirm)
    {
        tutorialText[id].text = text;
        yield return new WaitForSeconds(0.5f);
        confirm[id] = true;
        iconBox[id].SetActive(false);
        iconCandy[id].SetActive(false);
    }

    public void CandyIten()
    {
        if (!candy.activeSelf && !endTutorial && (attackHeavyConfirm[0] && !getItenConfirm[0] || attackHeavyConfirm[1] && !getItenConfirm[1] || attackHeavyConfirm[2] && !getItenConfirm[2] || attackHeavyConfirm[3] && !getItenConfirm[3]))
        {
            candy.SetActive(true);
        }
    }

    public void EndTutorialLogic()
    {
        if (getItenConfirm[0] && GameManager.Instance.InputsPlayers.Count == 1 ||
            getItenConfirm[0] && getItenConfirm[1] && GameManager.Instance.InputsPlayers.Count == 2 ||
            getItenConfirm[0] && getItenConfirm[1] && getItenConfirm[2] && GameManager.Instance.InputsPlayers.Count == 3 ||
            getItenConfirm[0] && getItenConfirm[1] && getItenConfirm[2] && getItenConfirm[3] && GameManager.Instance.InputsPlayers.Count == 4)
        {
            panelTutorial[0].SetActive(false);
            panelTutorial[1].SetActive(false);
            panelTutorial[2].SetActive(false);
            panelTutorial[3].SetActive(false);

            candy.SetActive(false);
            endTutorial = true;
        }
    }
}
