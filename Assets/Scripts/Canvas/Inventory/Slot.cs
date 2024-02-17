using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public bool isEquipmentSlot;

    [HideInInspector]  public ItemData currentItem;
    [HideInInspector] public int itemCount;

    [HideInInspector] public bool isAlreadyUse = false;
    public Sprite unselectedSprite;
    public Sprite selectedSprite;

    public Sprite invisiblePixel;
    public Image itemVisual;
    public TMP_Text countText;

    Image currentImage;

    private void Awake()
    {
        currentImage = GetComponent<Image>();
    }

    public void SetVisual()
    {
        if (currentItem == null) itemVisual.sprite = invisiblePixel;
        else
        {
            itemVisual.sprite = currentItem.itemIcone;
            if (itemCount > 1) countText.text = itemCount.ToString();
            else countText.text = "";
        }
    }

    public void SetSelected(bool isSelected)
    {
        if (selectedSprite == null) return;

        if (isSelected)
        {
            currentImage.sprite = selectedSprite;
            isAlreadyUse = true;
        }
        else
        {
            currentImage.sprite = unselectedSprite;
            isAlreadyUse = false;
        }
    }

    public void ClickOnSlot()
    {
        ItemActionSystem.instance.SetButton(this);
    }
}