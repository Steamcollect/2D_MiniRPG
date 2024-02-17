using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [HideInInspector]public float angle;

    public Transform cursorVisual;
    public Transform weaponContent;

    [HideInInspector] public SpriteRenderer currentWeaponGraphics;
    [HideInInspector] public WeaponScript currentWeaponScript;

    public PlayerCombat playerCombat;

    Rigidbody2D rb;
    Camera cam;

    Vector2 mousePos;
    Vector2 lookDir;

    public static WeaponParent instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        instance = this;
    }

    private void Update()
    {
        if (GameStateManager.instance.CurrentGameState == GameState.Gameplay) Rotate();
    }

    void Rotate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - rb.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        cursorVisual.rotation = Quaternion.Euler(0, 0, angle);

        // set the flipX of weapon in the correct direction
        currentWeaponGraphics.flipY = angle < 90 && angle > -90 ? false : true;

        // set the order in layer of weapon in the correct order
        currentWeaponGraphics.sortingOrder = angle < 180 && angle > 0 ? -1 : 1;
    }
}