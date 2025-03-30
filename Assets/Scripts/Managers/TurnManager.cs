using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static EnumScript;

public class TurnManager : MonoBehaviour
{
    [SerializeField] Turn currentTurn;

    private void Awake()
    {
        Init(); 
    }

    void Init()
    {
        currentTurn = Turn.Selection;
    }

    public void ChangeTurn()
    {
        switch(currentTurn)
        {
            case Turn.Selection:
                currentTurn = Turn.Battle;
                break; 
            case Turn.Battle:
                currentTurn = Turn.Selection;
                break; 
        }
    }

    void StartSelectionPhase()
    {

    }

    void EndSelectionPhase()
    {

    }

    void StartBattlePhase()
    {

    }

    void EndBattlePhase()
    {

    }

    public Turn GetCurrentTurn() => currentTurn; 

}
