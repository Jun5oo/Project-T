using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PhaseManager phaseManager; 

    [SerializeField] PhaseUI phaseUI;
    [SerializeField] GameObject waitingUI;
    [SerializeField] GameObject countDownUI;

    [SerializeField] GameObject deckUI;
    [SerializeField] GameObject discardPileUI;

    void Awake()
    {
        phaseManager.OnPhaseChanged += OnUpdatePhaseUI;
    }

    void OnUpdatePhaseUI(IPhase currentPhase)
    {
        phaseUI.OnUpdateText(currentPhase); 
        OnActivePhaseUI(); 
    }

    void OnActivePhaseUI() => phaseUI.gameObject.SetActive(true);
    void OnActiveWaitingUI() => waitingUI.SetActive(true);
    void OnActivePhaseTimerUI() => countDownUI.SetActive(true); 


}
