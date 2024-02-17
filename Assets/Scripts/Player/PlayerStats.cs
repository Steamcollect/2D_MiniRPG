using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text vitalityText;
    public Text manaText;
    public Text strengeText;
    public Text powerText;
    public Text habilityText;

    public static PlayerStats instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Movement")]
    public float movementSpeed; // %

    [Header("Health")]
    public int vitality;

    [Header("Combat")]
    public int strenge;
    public int power;
    public float hability;
    public int mana;

    public int critiquePourcentage; //%
    public float bulletSpeed; // %

    public void SetVisual()
    {
        vitalityText.text = vitality.ToString();
        manaText.text = mana.ToString();
        strengeText.text = strenge.ToString();
        powerText.text = power.ToString();
        habilityText.text = hability.ToString();
    }
}
