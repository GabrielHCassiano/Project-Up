using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider playerLife;
    [SerializeField] private Slider playerStamina;

    public void SetHUD(PlayerStatus playerStatus)
    {
        playerLife.value = (float)playerStatus.Life / playerStatus.MaxLife;
        playerStamina.value = (float)playerStatus.Stamina / playerStatus.MaxStamina;
    }
}
