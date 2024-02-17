using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponents : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameStateManager.instance.CurrentGameState == GameState.Paused || GameStateManager.instance.CurrentGameState == GameState.LevelUp)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }
    }
}
