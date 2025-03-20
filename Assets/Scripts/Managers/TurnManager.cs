using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Turn
    {
        Selection,
        Battle, 
    }

    [SerializeField] SelectionManager selectionManager;
    [SerializeField] Turn currentTurn;

    private void Awake()
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

    public Turn GetCurrentTurn() => currentTurn; 

}
