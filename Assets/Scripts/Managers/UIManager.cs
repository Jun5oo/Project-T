using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PhaseManager phaseManager; 

    [SerializeField] PhaseUI phaseUI;
    [SerializeField] WaitingUI waitingUI;
    [SerializeField] TurnEnd countDownUI;

    [SerializeField] GameObject deckUI;
    [SerializeField] GameObject discardPileUI;

    void Awake()
    {
        phaseManager.OnPhaseChanged += OnUpdatePhaseUI;
    }
    
    public void OnEnableUI(IUIElement ui)
    {
        ui.Show(); 
    }

    public void OnDisableUI(IUIElement ui)
    {
        ui.Hide(); 
    }

    void OnUpdatePhaseUI(IPhase currentPhase)
    {
        phaseUI.OnUpdateText(currentPhase);
        OnEnableUI(phaseUI); 
    }
}
