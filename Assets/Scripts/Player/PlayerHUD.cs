using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider playerLife;
    [SerializeField] private Slider playerStamina;
    [SerializeField] private TextMeshProUGUI hitComboText;

    private int hitCombo;

    public int HitCombo
    {
        get { return hitCombo; }
        set { hitCombo = value; }
    }

    public void SetHUD(PlayerStatus playerStatus)
    {
        playerLife.value = (float)playerStatus.Life / playerStatus.MaxLife;
        playerStamina.value = (float)playerStatus.Stamina / playerStatus.MaxStamina;
    }

    public void AddHit()
    {
        StopAllCoroutines();
        hitComboText.gameObject.SetActive(false);
        hitCombo++;
        hitComboText.gameObject.SetActive(true);
        hitComboText.text =  hitCombo.ToString() + "Hit";
        StartCoroutine(HitCooldown());
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(3f);
        hitCombo = 0;
        hitComboText.gameObject.SetActive(false);
    }

}
