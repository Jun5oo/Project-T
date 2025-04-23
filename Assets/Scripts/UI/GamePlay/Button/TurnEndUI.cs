using System.Collections;
using TMPro;
using UnityEngine;

public class TurnEndUI : MonoBehaviour
{
    const int TIME = 60;

    [Header("Reference")]
    [SerializeField] PhaseManager phaseManager;
    [SerializeField] SelectionPhase selectionPhase; 

    [SerializeField] TextMeshProUGUI timerUI;

    float timer;
    void Start()
    {
        selectionPhase = (SelectionPhase)phaseManager.GetCurrentPhase();
        selectionPhase.OnPhaseStarted += StartCountDown; 
    }

    void OnDestroy()
    {
        selectionPhase.OnPhaseStarted -= StartCountDown;    
    }

    public void OnClick()
    {
        if (phaseManager.GetCurrentPhase().PhaseType != PhaseType.Selection)
            return;

        StopCountDown();
        
        selectionPhase.SelectionState = SelectionState.Ready; 
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
