using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PanelType panelType;

    public bool isPointing;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Slot currentSlot = GetComponent<Slot>();
        RectTransform currentTransform = GetComponent<RectTransform>();

        isPointing = true;

        switch (panelType)
        {
            case PanelType.Slot:
                if (GetComponent<Slot>().currentItem == null) return;
                ToolTipSystem.instance.Show(currentSlot.currentItem, currentTransform.position);
                break;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointing = false;

        switch (panelType)
        {
            case PanelType.Slot:
                ToolTipSystem.instance.Hide();
                break;
        }
    }
}

public enum PanelType
{
    Slot,
    ItemActionPanel
}