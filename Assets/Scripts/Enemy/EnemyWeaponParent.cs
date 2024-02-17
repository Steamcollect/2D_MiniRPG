using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponParent : MonoBehaviour
{
    float angle;
    float finalAngle;

    [HideInInspector] public SpriteRenderer currentWeaponGraphics;

    Transform target;
    Rigidbody2D rb;

    Vector2 lookDir;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        lookDir = (Vector2)target.position - rb.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        finalAngle = Mathf.Lerp(finalAngle, angle, .05f);
        transform.rotation = Quaternion.Euler(0, 0, finalAngle);

        // set the flipX of weapon in the correct direction
        if(currentWeaponGraphics != null) currentWeaponGraphics.flipY = angle < 90 && angle > -90 ? false : true;

        // set the order in layer of weapon in the correct order
        if (currentWeaponGraphics != null) currentWeaponGraphics.sortingOrder = angle < 180 && angle > 0 ? -1 : 1;
    }
}
