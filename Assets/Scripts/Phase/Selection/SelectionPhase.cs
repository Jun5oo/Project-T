using System;
using System.Collections;
using UnityEngine;
public class SelectionPhase : IPhase
{
    PhaseType phaseType => PhaseType.Selection; 
    public PhaseType PhaseType { get { return phaseType; } }

    public SelectionState selectionState;
    public SelectionState SelectionState 
    { 
        get { return selectionState; }
        set 
        {
            selectionState = value; 
            OnStateChanged(); 
        }
    }

    CardManager cardManager;
    PhaseManager phaseManager;
    UIManager uiManager;

    public Action OnPhaseStarted; 

    public void Init(CardManager cardManager, PhaseManager phaseManager, UIManager uiManager)
    {
        this.cardManager = cardManager;
        this.phaseManager = phaseManager;
        this.uiManager = uiManager;
    }

    public void EnterPhase()
    {
        SelectionState = SelectionState.Initialize; 
    }
    public void UpdatePhase()
    {

    }
    public void ExitPhase()
    {

    }

    public void OnStateChanged()
    {
        switch (SelectionState)
        {
            case SelectionState.Initialize:
                SelectionState = SelectionState.Displaying; 
                break;
            case SelectionState.Displaying:
                phaseManager.Execute(DisplayUIRoutine());
                break; 
            case SelectionState.Drawing:
                phaseManager.Execute(DrawCardRoutine()); 
                break;
            case SelectionState.Main:
                OnPhaseStarted?.Invoke(); 
                break;
            case SelectionState.Ready:
                SelectionState = SelectionState.Discarding; 
                break;
            case SelectionState.Discarding:
                phaseManager.Execute(DiscardRoutine()); 
                break; 
            case SelectionState.Hiding:
                phaseManager.Execute(HideDisplayUIRoutine()); 
                break;
            case SelectionState.Complete:
                phaseManager.ChangePhase();
                break; 
            default:
                Debug.LogError("No such Selection State exist");
                break;
        }
    }

    IEnumerator DisplayUIRoutine()
    {
        bool uiDone = false;
        uiManager.ShowSelectionPhaseUI(()=> uiDone = true);
        yield return new WaitUntil(()=> uiDone);

        SelectionState = SelectionState.Drawing; 
    }
    IEnumerator DrawCardRoutine()
    {
        bool drawDone = false;
        cardManager.DrawCards(5, ()=> drawDone = true);
        yield return new WaitUntil(()=> drawDone);

        SelectionState = SelectionState.Main; 
    }
    IEnumerator DiscardRoutine()
    {
        bool discardDone = false;
        cardManager.DiscardAll(()=> discardDone = true);
        yield return new WaitUntil(()=> discardDone);

        SelectionState = SelectionState.Hiding; 
    }
    IEnumerator HideDisplayUIRoutine()
    {
        bool offDone = false; 
        uiManager.HideSelectionPhaseUI(() => offDone = true);   
        yield return new WaitUntil(() => offDone);

        SelectionState = SelectionState.Complete; 
    }
}
