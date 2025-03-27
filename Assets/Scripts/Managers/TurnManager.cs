using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static EnumScript;

public class TurnManager : MonoBehaviour
{
    [SerializeField] Turn currentTurn;
    public Action OnTurnChanged; 

    private void Awake()
    {
        Init(); 
    }

    void Init()
    {
        currentTurn = Turn.Selection;
        OnTurnChanged?.Invoke(); 
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

        OnTurnChanged?.Invoke(); 
    }

    public Turn GetCurrentTurn() => currentTurn; 

}
