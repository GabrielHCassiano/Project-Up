using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private Image charImage;
    [SerializeField] private Slider playerLife;
    [SerializeField] private Slider playerStamina;
    [SerializeField] private TextMeshProUGUI extralifeText;
    [SerializeField] private TextMeshProUGUI hitComboText;
    [SerializeField] private Color hitBaseColor;
    [SerializeField] private Color hitLevel2Color;
    [SerializeField] private Color hitLevel3Color;
    [SerializeField] private Color hitLevel4Color;
    [SerializeField] private Color hitLevel5Color;
    [SerializeField] private Color alphaColor;

    private Color currentColor;

    [SerializeField] private SpriteRenderer spriteHeavy;

    private int hitCombo;

    public int HitCombo
    {
        get { return hitCombo; }
        set { hitCombo = value; }
    }

    public void SetPlayerUI(PlayerData playerData)
    {
        playerUI.SetActive(true);
        charName.text = playerData.CharName;
        charImage.sprite = playerData.IconSprite;
    }

    public void SetHUD(PlayerStatus playerStatus)
    {
        playerLife.value = (float)playerStatus.Life / playerStatus.MaxLife;
        playerStamina.value = (float)playerStatus.Stamina / playerStatus.MaxStamina;
        extralifeText.text = playerStatus.ExtraLife.ToString();
    }

    public void HeavyAnim(PlayerCombat playerCombat, SpriteRenderer sprite)
    {
        if (playerCombat.HoldInput >= 1 && playerCombat.HoldInput <= 1.15f)
        {
            spriteHeavy.sprite = sprite.sprite;
            sprite.enabled = false;
            spriteHeavy.gameObject.SetActive(true);
        }
        else
        {
            sprite.enabled = true;
            spriteHeavy.gameObject.SetActive(false);
        }
    }



    public void AddHit()
    {
        StopAllCoroutines();
        hitCombo++;
        if (hitComboText.fontSize + 0.25f <= 50)
            hitComboText.fontSize += 0.25f;
        HitLevel();
        StartCoroutine(AddHitCooldown());
        hitComboText.gameObject.SetActive(true);
        hitComboText.text =  hitCombo.ToString() + "Hit";
        StartCoroutine(HitCooldown());
    }

    public void EndHit()
    {
        StopAllCoroutines();
        hitComboText.gameObject.SetActive(true);
        StartCoroutine(HitEndCooldown());
    }

    public void CancelHit()
    {
        StopAllCoroutines();
        hitComboText.gameObject.SetActive(true);
        StartCoroutine(CancelHitEndCooldown());
    }

    public void HitLevel()
    {
        if (hitCombo < 25)
            hitComboText.color = hitBaseColor;
        else if (hitCombo >= 25 && hitCombo < 50)
            hitComboText.color = hitLevel2Color;
        else if (hitCombo >= 50 && hitCombo < 75)
            hitComboText.color = hitLevel3Color;
        else if (hitCombo >= 75 && hitCombo < 100)
            hitComboText.color = hitLevel4Color;
        else if (hitCombo >= 100)
            hitComboText.color = hitLevel5Color;

        currentColor = hitComboText.color;
    }

    IEnumerator HitEndCooldown()
    {
        int endHit = hitCombo;
        hitCombo = 0;
        if (endHit == 0)
            hitComboText.text = "Bad";
            //hitComboText.text = "Interrupted\n" + endHit.ToString() + "Hit";
        else if (endHit > 0 && endHit < 25)
            hitComboText.text = "Ok\n" + endHit.ToString() + "Hit";
        else if (endHit >= 25 && endHit < 50)
            hitComboText.text = "Good\n" + endHit.ToString() + "Hit";
        else if (endHit >= 50 && endHit < 75)
            hitComboText.text = "Very Good\n" + endHit.ToString() + "Hit";
        else if (endHit >= 75 && endHit < 100)
            hitComboText.text = "Great\n" + endHit.ToString() + "Hit";
        else if (endHit >= 100)
            hitComboText.text = "Excellent\n" + endHit.ToString() + "Hit";
        yield return new WaitForSeconds(0.3f);
        hitComboText.fontSize = 25f;
        hitComboText.color = hitBaseColor;
        hitComboText.gameObject.SetActive(false);
    }

    IEnumerator CancelHitEndCooldown()
    {
        int endHit = hitCombo;
        hitCombo = 0;
        if (endHit == 0)
            hitComboText.text = "Bad";
        else
            hitComboText.text = "Interrupted\n" + endHit.ToString() + "Hit";
        yield return new WaitForSeconds(0.3f);
        hitComboText.fontSize = 25f;
        hitComboText.color = hitBaseColor;
        hitComboText.gameObject.SetActive(false);
    }

    IEnumerator AddHitCooldown()
    {
        hitComboText.color = currentColor;
        yield return new WaitForSeconds(0.0625f);
        hitComboText.color = Color.white;
        yield return new WaitForSeconds(0.0625f);
        hitComboText.color = currentColor;
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 1; i++)
        {
            hitComboText.color = alphaColor;
            yield return new WaitForSeconds(0.25f);
            hitComboText.color = currentColor;
            yield return new WaitForSeconds(0.25f);
        }
        for (int i = 0; i < 4; i++)
        {
            hitComboText.color = alphaColor;
            yield return new WaitForSeconds(0.125f);
            hitComboText.color = currentColor;
            yield return new WaitForSeconds(0.125f);
        }
        EndHit();
    }

}
