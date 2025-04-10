using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPhase : IPhase
{
    private CardManager cardManager;

    public string PhaseName { get { return "Selection"; } }

    public void Init(CardManager cardManager)
    {
        this.cardManager = cardManager; 
    }

    public void EnterPhase()
    {
        // 2. Draw card 
        // 3. Select enabled  
        // 4. Timer start 
    }

    public void UpdatePhase()
    {
        // Allowed to select card 
        // Timer update 
        // If turn end button is pressed or timer is 0 
            // call exit phase(); 
    }

    public void ExitPhase()
    {
        // Check is all the card is selected; 
        // If not, select random cards on the hand 

        // Discard all the card that is not selected; 
    }

}
