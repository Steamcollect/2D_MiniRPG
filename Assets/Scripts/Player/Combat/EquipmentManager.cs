using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipmentManager : MonoBehaviour
{
    public LayerMask targetLayer;
    public LayerMask parentLayer;

    [Header("Slot references")]
    public Slot weaponSlot;
    public Slot helmetSlot;
    public Slot plateSlot;
    public Slot feetSlot;

    [Header("Slot visual references")]
    public Image weaponSlotVisual;
    public Image helmetSlotVisual;
    public Image plateSlotVisual;
    public Image feetSlotVisual;

    [Header("Necessary references")]
    public Transform weaponContent;    
    public Sprite invisiblePixel;
    public ItemData handWeapon;

    [HideInInspector] public GameObject currentWeaponGO;
    [HideInInspector] public ItemData currentWeaponData;
    [HideInInspector] public Slot currentWeaponSlot;
    [HideInInspector] public bool haveWeapon = false;

    [HideInInspector] public ItemData currentHelmetData;
    [HideInInspector] public Slot currentHelmetSlot;
    [HideInInspector] public bool haveHelmet = false;

    [HideInInspector] public ItemData currentPlateData;
    [HideInInspector] public Slot currentPlateSlot;
    [HideInInspector] public bool havePlate = false;

    [HideInInspector] public ItemData currentFeetData;
    [HideInInspector] public Slot currentFeetSlot;
    [HideInInspector] public bool haveFeet = false;

    [Header("Empty equipment references")]
    public ItemData emptyHelmet;
    public ItemData emptyPlate;
    public ItemData emptyFeet;

    [HideInInspector] public WeaponType currentWeaponType = WeaponType.Magique;

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameObject newWeapon = Instantiate(handWeapon.weaponPrefabs, weaponContent);
        newWeapon.transform.position = weaponContent.position;
        currentWeaponGO = newWeapon;

        // Set equipment and weapon to empty
        ChangeWeapon(true, true, null, null);
        ChangeHelmet(true, true, null, null);
        ChangePlate(true, true, null, null);
        ChangeFeet(false, true, null, null);
    }

    public void ChangeWeapon(bool isFirstTime, bool isHand, ItemData currentItemData, Slot currentSlot)
    {
        if (isHand)
        {
            currentWeaponData = handWeapon;
            currentWeaponSlot = null;
            weaponSlot.currentItem = null;
            weaponSlotVisual.sprite = invisiblePixel;

            haveWeapon = false;
            weaponSlot.SetSelected(false);
        }
        else
        {
            currentWeaponData = currentItemData;
            currentWeaponSlot = currentSlot;
            weaponSlot.currentItem = currentItemData;
            weaponSlotVisual.sprite = currentItemData.itemIcone;

            haveWeapon = true;
            weaponSlot.SetSelected(true);
        }

        //Supprimer l'arme actuelle
        Destroy(currentWeaponGO);

        //Instancier la nouvelle arme
        GameObject newWeapon = Instantiate(currentWeaponData.weaponPrefabs, weaponContent);
        newWeapon.transform.position = weaponContent.position;
        currentWeaponGO = newWeapon;

        // change the current weapon type
        currentWeaponType = currentWeaponData.weaponType;

        // reference weaponParent with current weapon
        WeaponParent.instance.currentWeaponGraphics = currentWeaponGO.GetComponent<SpriteRenderer>();
        WeaponParent.instance.currentWeaponScript = currentWeaponGO.GetComponent<WeaponScript>();

        // set the weapon to attack the good target
        WeaponParent.instance.currentWeaponScript.parentType = characterType.Player;
        WeaponParent.instance.currentWeaponScript.targetLayer = targetLayer;

        // reference playerCombat with current weapon
        PlayerCombat.instance.currentWeaponData = currentWeaponData;
        PlayerCombat.instance.currentWeapon = currentWeaponGO.GetComponent<WeaponScript>();
        PlayerCombat.instance.currentWeaponAnimator = currentWeaponGO.GetComponent<Animator>();

        // set the visual of the inventory
        weaponSlot.SetVisual();
        if(!isFirstTime) EquipmentTip.instance.SetVisual(currentWeaponData, currentHelmetData, currentPlateData, currentFeetData);
    }

    public void ChangeHelmet(bool isFirstTime, bool desequip, ItemData currentEquipmentData, Slot currentSlot)
    {
        if (desequip)
        {
            currentHelmetData = emptyHelmet;
            currentHelmetSlot = null;
            helmetSlot.currentItem = null;
            helmetSlotVisual.sprite = invisiblePixel;

            haveHelmet = false;
            helmetSlot.SetSelected(false);
        }
        else
        {
            currentHelmetData = currentEquipmentData;
            currentHelmetSlot = currentSlot;
            helmetSlot.currentItem = currentEquipmentData;
            helmetSlotVisual.sprite = currentEquipmentData.itemIcone;

            haveHelmet = true;
            helmetSlot.SetSelected(true);
        }

        helmetSlot.SetVisual();
        if (!isFirstTime) EquipmentTip.instance.SetVisual(currentWeaponData, currentHelmetData, currentPlateData, currentFeetData);
    }
    public void ChangePlate(bool isFirstTime, bool desequip, ItemData currentEquipmentData, Slot currentSlot)
    {
        if (desequip)
        {
            currentPlateData = emptyPlate;
            currentPlateSlot = null;
            plateSlot.currentItem = null;
            plateSlotVisual.sprite = invisiblePixel;

            havePlate = false;
            plateSlot.SetSelected(false);
        }
        else
        {
            currentPlateData = currentEquipmentData;
            currentPlateSlot = currentSlot;
            plateSlot.currentItem = currentEquipmentData;
            plateSlotVisual.sprite = currentEquipmentData.itemIcone;

            havePlate = true;
            plateSlot.SetSelected(true);
        }

        plateSlot.SetVisual();
        if (!isFirstTime) EquipmentTip.instance.SetVisual(currentWeaponData, currentHelmetData, currentPlateData, currentFeetData);
    }
    public void ChangeFeet(bool isFirstTime, bool desequip, ItemData currentEquipmentData, Slot currentSlot)
    {
        if (desequip)
        {
            currentFeetData = emptyFeet;
            currentFeetSlot = null;
            feetSlot.currentItem = null;
            feetSlotVisual.sprite = invisiblePixel;

            haveFeet = false;
            feetSlot.SetSelected(false);
        }
        else
        {
            currentFeetData = currentEquipmentData;
            currentFeetSlot = currentSlot;
            feetSlot.currentItem = currentEquipmentData;
            feetSlotVisual.sprite = currentEquipmentData.itemIcone;

            haveFeet = true;
            feetSlot.SetSelected(true);
        }

        feetSlot.SetVisual();
        if (!isFirstTime) EquipmentTip.instance.SetVisual(currentWeaponData, currentHelmetData, currentPlateData, currentFeetData);
    }
}
