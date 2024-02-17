using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ItemActionSystem : MonoBehaviour
{
    int countItemToDrop;

    public GameObject actionPanel;
    public GameObject dropPanel;

    ItemData currentItemData;
    Slot currentSlot;

    public Button consomableButton;
    public Button equipButton;
    public Button desequipButton;
    public Button dropButton;

    public TMP_Text dropCountText;
    public Slider dropSlider;

    Vector2 posOffset = new Vector2(30, 0);

    EquipmentManager equipmentManager;

    public static ItemActionSystem instance;

    private void Awake()
    {
        equipmentManager = GetComponent<EquipmentManager>();

        instance = this;
    }

    public void SetButton(Slot slotSelected)
    {
        if (slotSelected.currentItem == null) return;

        currentItemData = slotSelected.currentItem;
        currentSlot = slotSelected;

        Vector2 slotPos = slotSelected.transform.position;
        ItemType currentItemType = slotSelected.currentItem.itemType;

        if (!currentSlot.isAlreadyUse)
        {
            switch (currentItemType)
            {
                case ItemType.Consomable:
                    consomableButton.gameObject.SetActive(true);
                    equipButton.gameObject.SetActive(false);
                    desequipButton.gameObject.SetActive(false);
                    dropButton.gameObject.SetActive(true);
                    break;

                case ItemType.Weapon:
                    consomableButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(true);
                    desequipButton.gameObject.SetActive(false);
                    dropButton.gameObject.SetActive(true);
                    break;
                
                case ItemType.Equipment:
                    consomableButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(true);
                    desequipButton.gameObject.SetActive(false);
                    dropButton.gameObject.SetActive(true);
                    break;

                case ItemType.Artifact:
                    consomableButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(false);
                    desequipButton.gameObject.SetActive(false);
                    dropButton.gameObject.SetActive(true);
                    break;
            }
        }
        else if (currentSlot.isEquipmentSlot)
        {
            consomableButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            desequipButton.gameObject.SetActive(true);
            dropButton.gameObject.SetActive(false);
        }
        else
        {
            consomableButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            desequipButton.gameObject.SetActive(true);
            dropButton.gameObject.SetActive(false);
        }

        actionPanel.transform.position = slotPos + posOffset;
        dropPanel.transform.position = slotPos + posOffset;
        actionPanel.SetActive(true);
    }

    public void EquipButton()
    {       
        if(currentItemData.itemType == ItemType.Weapon)
        {
            if (equipmentManager.haveWeapon) equipmentManager.currentWeaponSlot.SetSelected(false);
            equipmentManager.ChangeWeapon(false, false, currentItemData, currentSlot);
        }
        else if(currentItemData.itemType == ItemType.Equipment)
        {
            switch (currentItemData.equipmentType)
            {
                case EquipmentType.Helmet:
                    if (equipmentManager.haveHelmet) equipmentManager.currentHelmetSlot.SetSelected(false);
                    equipmentManager.ChangeHelmet(false, false, currentItemData, currentSlot);
                    break;
                case EquipmentType.Plate:
                    if (equipmentManager.havePlate) equipmentManager.currentPlateSlot.SetSelected(false);
                    equipmentManager.ChangePlate(false, false, currentItemData, currentSlot);
                    break;
                case EquipmentType.Feet:
                    if (equipmentManager.haveFeet) equipmentManager.currentFeetSlot.SetSelected(false);
                    equipmentManager.ChangeFeet(false, false, currentItemData, currentSlot);
                    break;
            }
        }
        currentSlot.SetSelected(true);

        CloseActionPanel();
    }
    public void DesequipButton()
    {
        currentSlot.SetSelected(false);

        if (currentItemData.itemType == ItemType.Weapon)
        {
            if (equipmentManager.haveWeapon) equipmentManager.currentWeaponSlot.SetSelected(false);
            equipmentManager.ChangeWeapon(false, true, currentItemData, currentSlot);
        }
        else if (currentItemData.itemType == ItemType.Equipment)
        {
            switch (currentItemData.equipmentType)
            {
                case EquipmentType.Helmet:
                    if (equipmentManager.haveHelmet) equipmentManager.currentHelmetSlot.SetSelected(false);
                    equipmentManager.ChangeHelmet(false, true, currentItemData, currentSlot);
                    break;
                case EquipmentType.Plate:
                    if (equipmentManager.havePlate) equipmentManager.currentPlateSlot.SetSelected(false);
                    equipmentManager.ChangePlate(false, true, currentItemData, currentSlot);
                    break;
                case EquipmentType.Feet:
                    if (equipmentManager.haveFeet) equipmentManager.currentFeetSlot.SetSelected(false);
                    equipmentManager.ChangeFeet(false, true, currentItemData, currentSlot);
                    break;
            }
        }

        CloseActionPanel();
    }

    public void ConsumeButton()
    {
        for (int i = 0; i < currentItemData.consomableType.Length; i++)
        {
            switch (currentItemData.consomableType[i])
            {
                case ConsomableType.Health:
                    PlayerHealth.instance.TakeHealth(currentItemData.healthGiven);
                    break;
                case ConsomableType.AttackDamage:
                    PlayerStats.instance.strenge += currentItemData.damageGiven;
                    break;
            }
        }

        InventoryManager.instance.RemoveItem(currentItemData, 1);  
        CloseActionPanel();
    }

    public void DropButton()
    {
        // recuperer l'element concerner dans l'inventaire
        ItemsInInventory itemsInInventory = InventoryManager.instance.inventoryContent.Where(elem => elem.currentItem == currentItemData).FirstOrDefault();

        if(itemsInInventory.itemCount > 1) // si plus de 1 item, ouvrir le panel compteur
        {
            actionPanel.SetActive(false);
            dropPanel.SetActive(true);

            dropCountText.text = "1";

            dropSlider.minValue = 1;
            dropSlider.value = 1;
            dropSlider.maxValue = itemsInInventory.itemCount;
        }
        else
        {
            countItemToDrop = 1;
            DropItem();
        }        
    }
    public void DropPanelSlider(float sliderValue)
    {
        countItemToDrop = (int)sliderValue;
        dropCountText.text = countItemToDrop.ToString();
    }
    public void DropItem()
    {
        GameObject playerPos = GameObject.FindGameObjectWithTag(TagReferences.instance.playerMain);
        GameObject itemParent = GameObject.FindGameObjectWithTag(TagReferences.instance.itemParent);

        InventoryManager.instance.RemoveItem(currentItemData, countItemToDrop);

        GameObject current = Instantiate(currentItemData.itemPrefabs, itemParent.transform);
        current.transform.transform.position = playerPos.transform.position;
        current.GetComponent<ItemInInventory>().itemCount = countItemToDrop;

        CloseActionPanel();
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        dropPanel.SetActive(false);
        currentItemData = null;
    }
}