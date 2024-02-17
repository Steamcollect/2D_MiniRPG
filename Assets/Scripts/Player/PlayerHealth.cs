using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;

    public GameObject[] hearts;

    public GameObject hitEffect;

    public static PlayerHealth instance;

    Rigidbody2D rb;
    PlayerMovement movement;
    PlayerStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();

        instance = this;
    }

    private void Start()
    {
        currentHealth = stats.vitality;

        for (int i = 0; i < hearts.Length; i++)
        {
            // Limit max health visual
            if (i < stats.vitality / 4) hearts[i].SetActive(true);
            else hearts[i].SetActive(false);
        }

        StartCoroutine(HeartFlashTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) TakeDamage(1, new Vector2(12, -20), 2000) ;
        if (Input.GetKeyDown(KeyCode.L)) TakeHealth(1);
    }

    public void TakeDamage(int damageTaken, Vector2 attackPos, int knowbackForce)
    {
        currentHealth -= damageTaken;

        CameraController.ShakeCam(4, .3f);
        StartCoroutine(SlowTime());

        //transforme position attaquant en angle
        Vector2 direction = attackPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        StartCoroutine(CantMoveCouldown());

        // appliquer le knowback
        rb.AddForce((direction.normalized * -1) * knowbackForce, ForceMode2D.Force);

        //applique l'angle aux particules
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.Euler( 0, 0, angle + 180));
        Destroy(effect, 5); 

        SetHealthVisual();
    }
    public void TakeHealth(int healthTaken)
    {
        currentHealth += healthTaken;

        SetHealthVisual();
    }

    public void TakeMaxHealth(int maxHealthTaken)
    {
        stats.vitality += maxHealthTaken;
        currentHealth += maxHealthTaken;

        for (int i = 0; i < hearts.Length; i++)
        {
            // Limit max health visual
            if (i < stats.vitality / 4) hearts[i].SetActive(true);
            else hearts[i].SetActive(false);
        }

        SetHealthVisual();
    }

    void SetHealthVisual()
    {
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > stats.vitality) currentHealth = stats.vitality;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (currentHealth <= i * 4) hearts[i].GetComponent<Image>().fillAmount = 0;
            if (currentHealth == i * 4 + 1) hearts[i].GetComponent<Image>().fillAmount = .25f;
            if (currentHealth == i * 4 + 2) hearts[i].GetComponent<Image>().fillAmount = .5f;
            if (currentHealth == i * 4 + 3) hearts[i].GetComponent<Image>().fillAmount = .75f;
            if (currentHealth >= i * 4 + 4) hearts[i].GetComponent<Image>().fillAmount = 1;
        }
    }

    IEnumerator SlowTime()
    {
        Time.timeScale = .05f;

        yield return new WaitForSeconds(.012f);

        Time.timeScale = 1;
    }

    IEnumerator HeartFlash()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].GetComponent<Animator>().SetTrigger("Flash");
            yield return new WaitForSeconds(.12f);
        }
    }
    IEnumerator HeartFlashTimer()
    {
        StartCoroutine(HeartFlash());
        yield return new WaitForSeconds(30);
        StartCoroutine(HeartFlashTimer());
    }

    IEnumerator CantMoveCouldown()
    {
        movement.enabled = false;

        yield return new WaitForSeconds(.2f);

        movement.enabled = true;
    }
}