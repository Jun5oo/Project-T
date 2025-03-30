using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EnumScript;

public class PhaseUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI phaseUIText;

    Turn currentTurn; 

    string selectionPhase = "Selection Phase";
    string battlePhase = "Battle Phase";

    Action fadeInEnd;
    Action fadeOutEnd; 

    void OnEnable()
    {
        this.canvasGroup.alpha = 0f;

        // 1. Get Current Phase; 
        currentTurn = Turn.Selection;
        // 2. Change text into current phase;
        OnUpdateCurrentPhaseText(); 

        fadeInEnd += StartFadeOut;
        fadeOutEnd += InActiveSelf; 

        StartFadeIn(); 
    }

    void OnDisable()
    {
        this.canvasGroup.alpha = 0f; 

        fadeInEnd -= StartFadeOut;
        fadeOutEnd -= InActiveSelf;
    }

    IEnumerator FadeInCoroutine()
    {
        while(canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += 0.05f;
            yield return new WaitForSeconds(0.01f); 
        }

        fadeInEnd?.Invoke(); 

        yield return null; 
    } 
    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(2f); 

        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= 0.05f;
            yield return new WaitForSeconds(0.01f);
        }

        fadeOutEnd?.Invoke(); 
    }

    void StartFadeIn() => StartCoroutine(FadeInCoroutine()); 
    void StartFadeOut() => StartCoroutine(FadeOutCoroutine());
    void InActiveSelf() => this.gameObject.SetActive(false); 
    string OnUpdateCurrentPhaseText()
    {
        string result = "";

        switch (currentTurn)
        {
            case Turn.Selection:
                result = selectionPhase;
                break;
            case Turn.Battle:
                result = battlePhase;
                break;
        }

        return result;
    }
}
