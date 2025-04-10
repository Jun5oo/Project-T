using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    [SerializeField] PhaseManager phaseManager;
    public void OnClick()
    {
        if (phaseManager.GetCurrentPhase().PhaseName != "Selection")
            return;

        phaseManager.ChangePhase(phaseManager.GetBattlePhase()); 
    }
}
