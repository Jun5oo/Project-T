using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseUI : MonoBehaviour, IUIElement
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI phaseUIText;

    Action fadeInEnd;
    Action fadeOutEnd; 

    void OnEnable()
    {
        this.canvasGroup.alpha = 0f;

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
    public void OnUpdateText(IPhase currentPhase)
    {
        if (currentPhase == null)
        {
            Debug.LogWarning("Phase is null");
            return;
        }
        phaseUIText.text = currentPhase.PhaseName; 
    }

    public void Show()
    {
        this.gameObject.SetActive(true); 
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
