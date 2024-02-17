using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public ToolTip toolTip;

    public static ToolTipSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Hide();
    }

    public void Show(ItemData currentItem, Vector2 slotPos)
    {
        toolTip.gameObject.SetActive(true);

        toolTip.SetVisual(currentItem, slotPos);
    }
    public void Hide()
    {
        toolTip.gameObject.SetActive(false);
    }
}
