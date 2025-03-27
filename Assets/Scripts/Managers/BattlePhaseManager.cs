using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhaseManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;

    void Awake()
    {
        turnManager.OnTurnChanged += OnBattleStateMachine;
    }

    void OnBattleStateMachine()
    {
        OnBattleStart();
        OnBattleEnd();
    }

    void OnBattleStart()
    {
        Debug.Log("On Battle Start!"); 
    }

    void OnBattleEnd()
    {
        Debug.Log("On Battle End!"); 
    }
}
