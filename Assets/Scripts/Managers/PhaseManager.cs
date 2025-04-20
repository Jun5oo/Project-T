using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    IPhase currentPhase;

    SelectionPhase selectionPhase;
    BattlePhase battlePhase; 

    public Action OnPhaseChanged;

    void Start()
    {
        selectionPhase = new SelectionPhase();
        selectionPhase.Init(FindObjectOfType<CardManager>(), FindObjectOfType<PhaseManager>(), FindObjectOfType<UIManager>());

        battlePhase = new BattlePhase();
        battlePhase.Init(FindObjectOfType<CardManager>(), FindObjectOfType<PhaseManager>(), FindObjectOfType<UIManager>()); 

        currentPhase = selectionPhase;
        InitPhase(); 
    }

    public void Update()
    {
        currentPhase?.UpdatePhase();
    }

    void InitPhase()
    {
        currentPhase.EnterPhase();
        OnPhaseChanged?.Invoke();
    }

    public void ChangePhase()
    {
        currentPhase?.ExitPhase();
        
        switch (currentPhase.PhaseType)
        {
            case PhaseType.Selection:
                currentPhase = battlePhase; 
                break;
            case PhaseType.Battle:
                currentPhase = selectionPhase; 
                break;
            default:
                Debug.LogError("No such PhaseType exits");
                break; 
        }

        OnPhaseChanged?.Invoke();
        currentPhase.EnterPhase();
    }

    public IPhase GetCurrentPhase() => currentPhase;
}
