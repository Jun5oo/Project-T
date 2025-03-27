using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager; 

    [SerializeField] List<CardUI> selectedCard = new List<CardUI>();

    public Action<List<CardUI>> OnSelectionChanged;

    int maxCardNum = 3;

    void Awake()
    {
        Init();     
    }

    void Init()
    {
        turnManager.OnTurnChanged += StartSelectionPhase; 
    }

    void StartSelectionPhase()
    {
        Debug.Log("Start Selection Phase"); 
    }

    public void AddToList(CardUI card)
    {
        if (selectedCard.Count >= maxCardNum)
            return; 

        selectedCard.Add(card);
        OnSelectionChanged?.Invoke(selectedCard);
    }
    public void RemoveFromList(CardUI card)
    {
        if (!selectedCard.Remove(card))
        {
            Debug.LogError($"No {card.name} is found at selectedCard List");
            return;
        }

        OnSelectionChanged?.Invoke(selectedCard);
    }
    public void ClearList()
    {
        selectedCard.Clear();
        OnSelectionChanged?.Invoke(selectedCard); 
    }
    public bool IsCardFull() => selectedCard.Count >= maxCardNum;
}
