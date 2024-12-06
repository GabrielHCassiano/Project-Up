using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerData : ScriptableObject
{
    [SerializeField] private string playerName;
    [SerializeField] private string charName;
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private RuntimeAnimatorController animatorController;
    private int comboScore = 0;

    public string PlayerName
    { 
        get { return playerName; } 
        set { playerName = value; }
    }

    public string CharName
    {
        get { return charName; }
        set { charName = value; }
    }

    public Sprite IconSprite
    {
        get { return iconSprite; }
        set { iconSprite = value; }
    }

    public RuntimeAnimatorController AnimatorController
    {
        get { return animatorController; }
        set { animatorController = value; }
    }

    public int ComboScore
    {
        get { return comboScore; }
        set { comboScore = value; }
    }
}
