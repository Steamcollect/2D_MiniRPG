using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Animator bookAnimator;

    public GameObject cursorVisual;
    public GameObject itemActionPanel;
    public GameObject pickUpTextGo;

    public Sprite bookMarkSelected;
    public Sprite bookMarkUnselected;

    public Image[] bookMarkReferences;
    public GameObject[] panelGO;

    int currentPage = 0;

    bool isOpen = false;
    bool isInAnimation = false;

    GameState newGameState;

    private void Start()
    {
        bookAnimator.SetBool("IsOpen", isOpen);

        for (int i = 0; i < bookMarkReferences.Length; i++)
        {
            if (i == 0)
            {
                bookMarkReferences[i].sprite = bookMarkSelected;
                panelGO[i].SetActive(true);
            }
            else
            {
                bookMarkReferences[i].sprite = bookMarkUnselected;
                panelGO[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInAnimation)
        {
            if (GameStateManager.instance.CurrentGameState == GameState.Paused && !isInAnimation) StartCoroutine(Resume());
            else if (GameStateManager.instance.CurrentGameState == GameState.Gameplay && !isInAnimation) StartCoroutine(Pause());
        }

        if (Input.GetKeyDown(KeyCode.I) && !isInAnimation)
        {
            if (GameStateManager.instance.CurrentGameState == GameState.Gameplay) StartCoroutine(OpenInventory());
            else if (GameStateManager.instance.CurrentGameState == GameState.Paused) StartCoroutine(Resume());
        }
    }

    IEnumerator Pause()
    {
        isOpen = true;
        isInAnimation = true;

        pickUpTextGo.SetActive(false);

        cursorVisual.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        bookAnimator.SetBool("IsOpen", isOpen);

        newGameState = GameState.Paused;
        GameStateManager.instance.SetState(newGameState);

        yield return new WaitForSeconds(1);
        isInAnimation = false;
    }
    IEnumerator OpenInventory()
    {
        isOpen = true;
        isInAnimation = true;

        pickUpTextGo.SetActive(false);

        cursorVisual.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        bookAnimator.SetBool("IsOpen", isOpen);

        newGameState = GameState.Paused;
        GameStateManager.instance.SetState(newGameState);

        currentPage = 4;

        for (int i = 0; i < bookMarkReferences.Length; i++)
        {
            if (i == currentPage)
            {
                bookMarkReferences[i].sprite = bookMarkSelected;
                panelGO[i].SetActive(true);
            }
            else
            {
                bookMarkReferences[i].sprite = bookMarkUnselected;
                panelGO[i].SetActive(false);
            }

        }

        PlayerStats.instance.SetVisual();

        yield return new WaitForSeconds(1);
        isInAnimation = false;
    }

    public void ResumeGame()
    {
        if(!isInAnimation) StartCoroutine(Resume());
        ItemActionSystem.instance.CloseActionPanel();
    }
    IEnumerator Resume()
    {
        isOpen = false;
        isInAnimation = true;

        pickUpTextGo.SetActive(true);

        itemActionPanel.SetActive(false);

        cursorVisual.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        bookAnimator.SetBool("IsOpen", isOpen);

        yield return new WaitForSeconds(.3f);

        newGameState = GameState.Gameplay;
        GameStateManager.instance.SetState(newGameState);

        yield return new WaitForSeconds(.4f);
        isInAnimation = false;
    }

    public void PauseMenuButton()
    {
        if(currentPage != 0 && !isInAnimation) StartCoroutine(TurnPage(0));
    }
    public void StatisticsButton()
    {
        if (currentPage != 3 && !isInAnimation) StartCoroutine(TurnPage(3));
        PlayerStats.instance.SetVisual();
    }
    public void InventoryButton()
    {
        if (currentPage != 4 && !isInAnimation) StartCoroutine(TurnPage(4));
    }

    IEnumerator TurnPage(int pagekSelected)
    {
        isInAnimation = true;

        if(pagekSelected > currentPage) bookAnimator.SetTrigger("TurnPageLeft");
        else if (pagekSelected < currentPage) bookAnimator.SetTrigger("TurnPageRight");

        currentPage = pagekSelected;

        for (int i = 0; i < bookMarkReferences.Length; i++)
        {
            if (i == currentPage)
            {
                bookMarkReferences[i].sprite = bookMarkSelected;
                panelGO[i].SetActive(true);
            }
            else
            {
                bookMarkReferences[i].sprite = bookMarkUnselected;
                panelGO[i].SetActive(false);
            }

        }

        yield return new WaitForSeconds (1);

        isInAnimation = false;
    }
}
