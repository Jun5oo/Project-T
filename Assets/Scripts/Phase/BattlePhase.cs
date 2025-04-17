using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : IPhase
{
    private CardManager cardManager;

    public string PhaseName { get { return "Battle"; } }

    public void Init(CardManager cardManager)
    {
        this.cardManager = cardManager;
    }

    public void EnterPhase()
    {
        
    }

    public void UpdatePhase()
    {
        Debug.Log(" Battle Phase"); 
    }
    public void ExitPhase()
    {
     
    }

}
