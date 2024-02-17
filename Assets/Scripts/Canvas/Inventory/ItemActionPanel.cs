using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionPanel : MonoBehaviour
{
    PointOverUI pointOverUI;

    private void Awake()
    {
        pointOverUI = GetComponent<PointOverUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !pointOverUI.isPointing) ItemActionSystem.instance.CloseActionPanel();
    }
}
