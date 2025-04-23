using System;
using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    IPhase currentPhase;

    CardManager cardManager;
    UIManager uiManager;

    SelectionPhase selectionPhase;
    BattlePhase battlePhase;

    public Action<IPhase> OnPhaseChanged; 

    public void Init(CardManager cardManager, UIManager uiManager)
    {
        this.cardManager = cardManager; 
        this.uiManager = uiManager;

        selectionPhase = new SelectionPhase();
        selectionPhase.Init(cardManager, this, uiManager);
        battlePhase = new BattlePhase();
        battlePhase.Init(cardManager, this, uiManager);

        currentPhase = selectionPhase; 
    }
    public void Update()
    {
        currentPhase?.UpdatePhase();
    }
    public void StartPhase()
    {
        currentPhase?.EnterPhase();
        OnPhaseChanged?.Invoke(currentPhase); 
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
        
        currentPhase.EnterPhase();
        OnPhaseChanged?.Invoke(currentPhase); 
    }
    public void Execute(IEnumerator coroutine)
    {
        StartCoroutine(coroutine); 
    }
    public IPhase GetCurrentPhase() => currentPhase;
}
