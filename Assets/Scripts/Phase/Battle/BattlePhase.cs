using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : IPhase
{
    CardManager cardManager;
    PhaseManager phaseManager;
    UIManager uiManager; 

    PhaseType phaseType => PhaseType.Battle; 
    public PhaseType PhaseType
    {
        get { return phaseType; }
    }

    public void Init(CardManager cardManager, PhaseManager phaseManager, UIManager uiManager)
    {
        this.cardManager = cardManager;
        this.phaseManager = phaseManager;
        this.uiManager = uiManager;
    }

    public void EnterPhase()
    {
        
    }
    public void UpdatePhase()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            phaseManager.ChangePhase();
        }
    }
    public void ExitPhase()
    {
     
    }

}
