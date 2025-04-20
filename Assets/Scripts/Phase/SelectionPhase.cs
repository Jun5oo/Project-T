using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPhase : IReadyablePhase
{
    PhaseType phaseType => PhaseType.Selection; 
    public PhaseType PhaseType
    {
        get { return phaseType; }
    }

    CardManager cardManager;
    PhaseManager phaseManager;
    UIManager uiManager; 

    Dictionary<string, bool> playerReady;

    public void Init(CardManager cardManager, PhaseManager phaseManager, UIManager uiManager)
    {
        this.cardManager = cardManager;
        this.phaseManager = phaseManager;
        this.uiManager = uiManager;

        uiManager.OnSelectionUIOnScreenComplete += ExecuteDrawCard; 
    }

    public void EnterPhase()
    {
        playerReady = new Dictionary<string, bool>()
        {
            { "Player1", false }, 
            { "Player2", true }
        };

        uiManager.ShowSelectionPhaseUI();    
    }
    public void UpdatePhase()
    {

    }
    public void ExitPhase()
    {
        cardManager.DiscardAll(); 
        // If last card is discarded, then Move UI; 
        // After UI is all disappeared from screen, Enable Phase UI; 
    }

    public void ExecuteDrawCard()
    {
        cardManager.DrawCards(5); 
    }

    public void PlayerReady(string playerID)
    {
        if(playerReady.ContainsKey(playerID))
            playerReady[playerID] = true;
        
        else
        {
            Debug.Log("playerID doesn't exits"); 
        }
    }
    public bool IsAllPlayerReady()
    {
        foreach(var isReady in playerReady.Values)
        {
            if (!isReady)
                return false; 
        }

        return true; 
    }
}
