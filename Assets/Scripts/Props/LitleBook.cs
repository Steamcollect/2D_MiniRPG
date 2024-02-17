using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitleBook : MonoBehaviour
{
    Transform playerPos;

    public GameObject litleBookGO;
    SpriteRenderer litleBookGraphics;

    Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("PlayerCenter").transform;
        litleBookGraphics = litleBookGO.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, playerPos.position, ref velocity, .2f);

        if (playerPos.position.x > litleBookGO.transform.position.x) litleBookGraphics.flipX = true;
        else litleBookGraphics.flipX = false;
    }
}
