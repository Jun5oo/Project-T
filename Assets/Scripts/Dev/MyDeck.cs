using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyDeck : MonoBehaviour
{
    List<CardSO> deckList;
    TextMeshProUGUI deckCount; 

    void AddCardsToDeck()
    {

    }

    void RemoveCardFromDeck()
    {

    }

    void Shuffle()
    {

    }

    void UpdateDeckCount()
    {
        deckCount.text = deckList.Count.ToString(); 
    }
}
