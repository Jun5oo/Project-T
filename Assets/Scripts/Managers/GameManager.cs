using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] CardManager cardManager;
    [SerializeField] PhaseManager phaseManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] GridManager gridManager;

    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;

    void Awake()
    {
        uiManager.Init(cardManager);
        phaseManager.Init(cardManager, uiManager);

        phaseManager.OnPhaseChanged += uiManager.OnUpdatePhaseUI; 
    }

    void Start()
    {
        phaseManager.StartPhase(); 
    }
}
