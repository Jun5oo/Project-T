using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager; 
    [SerializeField] List<CardUI> selectedCard;
    int maxCardNum = 3; 

    void Awake()
    {
        ClearList(); 
    }

    public void AddToList(CardUI card)
    {
        selectedCard.Add(card);
        uiManager.UpdateSelectionOrderUI();
    }
    public void RemoveFromList(CardUI card)
    {
        foreach(CardUI selected in selectedCard)
        {
            if (selected.cardName == card.cardName)
            {
                selectedCard.Remove(selected);
                break; 
            }
        }
        uiManager.UpdateSelectionOrderUI(); 
    }
    public void ClearList() => selectedCard.Clear(); 
    public bool IsCardFull() => selectedCard.Count >= maxCardNum? true : false;
    public int GetSelectedOrder(CardUI card)
    {
        int idx = 0; 

        for (int i = 0; i < selectedCard.Count; i++)
        {
            if (selectedCard[i].cardName == card.cardName)
            {
                idx = i + 1;
                break;
            }
        }

        return idx;
    }
    public List<CardUI> GetAllSelectedCard() => selectedCard; 
}
