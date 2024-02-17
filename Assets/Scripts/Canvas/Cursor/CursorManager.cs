using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Transform cursorPosition;
    public Texture2D cursorTexture;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.Auto);
    }

    private void Update()
    {
        cursorPosition.position = Input.mousePosition;
    }
}