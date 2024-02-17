using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiTextManager : MonoBehaviour
{
    public static UiTextManager instance;


    private void Awake()
    {
        instance = this;
    }

    public void SetText(TMP_Text uiText, string text)
    {
        StartCoroutine(SetTextVisual(uiText, text));
    }

    IEnumerator SetTextVisual(TMP_Text uiText, string text)
    {
        uiText.text = "";

        foreach (char letter in text)
        {
            uiText.text += letter;

            yield return new WaitForSeconds(0.005f);
        }
    }

    public void ResetText()
    {
        StopAllCoroutines();
    }
}