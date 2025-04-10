using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    IPhase currentPhase;

    SelectionPhase selectionPhase;
    BattlePhase battlePhase; 

    public Action<IPhase> OnPhaseChanged;

    void Start()
    {
        selectionPhase = new SelectionPhase();
        selectionPhase.Init(FindObjectOfType<CardManager>());

        battlePhase = new BattlePhase();
        battlePhase.Init(FindObjectOfType<CardManager>()); 

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
        OnPhaseChanged?.Invoke(currentPhase);
    }

    public void ChangePhase(IPhase nextPhase)
    {
        currentPhase?.ExitPhase();
        currentPhase = nextPhase;

        OnPhaseChanged?.Invoke(currentPhase);
        currentPhase.EnterPhase();
    }

    public IPhase GetCurrentPhase() => currentPhase;
    public IPhase GetSelectionPhase() => selectionPhase;
    public IPhase GetBattlePhase() => battlePhase;
}
