using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnEndUI : MonoBehaviour
{
    const int TIME = 60;

    [Header("Reference")]
    [SerializeField] PhaseManager phaseManager;
    [SerializeField] TextMeshProUGUI timerUI;

    float timer;

    void OnEnable()
    {
        timerUI.text = TIME.ToString(); 
        StartCountDown(); 
    }

    void OnDisable()
    {
        StopCountDown(); 
    }

    public void OnClick()
    {
        if (phaseManager.GetCurrentPhase().PhaseType != PhaseType.Selection)
            return;

        StopCountDown(); 

        if(phaseManager.GetCurrentPhase() is IReadyablePhase readyablePhase)
        {
            readyablePhase.PlayerReady("Player1");
            
            if(readyablePhase.IsAllPlayerReady())
                phaseManager.ChangePhase(); 
        }
    }

    void StartCountDown()
    {
        StartCoroutine(CountDownCoroutine()); 
    }

    void StopCountDown()
    {
        StopAllCoroutines(); 
    }

    IEnumerator CountDownCoroutine()
    {
        timer = TIME;
        timerUI.text = timer.ToString(); 

        yield return new WaitForSeconds(2f);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerUI.text = Mathf.Ceil(timer).ToString();
            yield return null;
        }

        timer = 0;

        OnClick(); 
    }
}
