using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CardManager cardManager; 
    [SerializeField] TurnManager turnManager;
    [SerializeField] SelectionManager selectionManager;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject cardParent;

    [SerializeField] TextMeshProUGUI currentPhase;

    void Awake()
    {
        CreateCardUI();
        UpdatePhaseUI(); 
    }

    public void CreateCardUI()
    {
        List<CardSO> cardList = cardManager.GetCardList(); 
        
        foreach (var card in cardList)
        {
            GameObject cu = Instantiate(cardPrefab);
            cu.transform.parent = cardParent.transform;
            CardUI cardScript = cu.GetComponent<CardUI>();
            cardScript.Initialize(card);
        }

        // CloseCardUI(); 
    }

    public void DisplayCardUI() => cardParent.SetActive(true);
    public void CloseCardUI() => cardParent.SetActive(false);
    public void UpdatePhaseUI() => currentPhase.text = turnManager.GetCurrentTurn().ToString();
    public void UpdateSelectionOrderUI()
    {
        List<CardUI> selected = selectionManager.GetAllSelectedCard(); 
        
        foreach (var card in selected)
        {
            int idx = selectionManager.GetSelectedOrder(card);
            card.UpdateSelectedOrder(idx); 
        }
    }
}
