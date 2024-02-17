using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentTip : MonoBehaviour
{
    public Image parentVisual;
    public Sprite invisibleSprite;
    public Sprite panelSprite;

    public TMP_Text weaponField;
    public TMP_Text helmetField;
    public TMP_Text plateField;
    public TMP_Text feetField;

    public TMP_Text damageField;
    public TMP_Text healthField;

    ItemData currentWeapon;
    ItemData currentHelmet;
    ItemData currentPlate;
    ItemData currentFeet;

    int totalDamage;
    int totalHealth;

    public static EquipmentTip instance;
    private void Awake()
    {
        instance = this;
    }

    // from ToolTipSystem
    public void SetVisual(ItemData weapon, ItemData helmet, ItemData plate, ItemData feet)
    {
        UiTextManager.instance.ResetText();

        // if their is no equipment currently equiped
        if (weapon == EquipmentManager.instance.handWeapon && helmet == EquipmentManager.instance.emptyHelmet && plate == EquipmentManager.instance.emptyPlate && feet == EquipmentManager.instance.emptyFeet) parentVisual.sprite = invisibleSprite;
        else parentVisual.sprite = panelSprite;

        // set text for weapon infos
        if (weapon != EquipmentManager.instance.handWeapon)
        {
            if(weapon != currentWeapon) UiTextManager.instance.SetText(weaponField, "-" + weapon.itemName);
        }
        else weaponField.text = "";

        // set text for helmet infos
        if (helmet != EquipmentManager.instance.emptyHelmet)
        {
            if(helmet != currentHelmet) UiTextManager.instance.SetText(helmetField, "-" + helmet.itemName);
        }
        else helmetField.text = "";

        // set text for plate infos
        if (plate != EquipmentManager.instance.emptyPlate)
        {
            if(plate != currentPlate) UiTextManager.instance.SetText(plateField, "-" + plate.itemName);
        }
        else plateField.text = "";

        // set text for feet infos
        if (feet != EquipmentManager.instance.emptyFeet)
        {
            if(feet != currentFeet) UiTextManager.instance.SetText(feetField, "-" + feet.itemName);
        }
        else feetField.text = "";

        // set the stats total the  equipment give and apply it to the text
        totalDamage = weapon.damageGiven + helmet.damageGiven + plate.damageGiven + feet.damageGiven;
        totalHealth = weapon.healthGiven + helmet.healthGiven + plate.healthGiven + feet.healthGiven;

        if (totalDamage != 0) UiTextManager.instance.SetText(damageField, "+ " + totalDamage + " degats");
        else damageField.text = "";
        if (totalHealth != 0) UiTextManager.instance.SetText(healthField, "+ " + totalHealth + " vies");
        else healthField.text = "";

        currentWeapon = weapon;
        currentHelmet = helmet;
        currentPlate = plate;
        currentFeet = feet;
    }
}