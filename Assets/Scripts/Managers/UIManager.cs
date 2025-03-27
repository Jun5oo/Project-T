using System.Collections;
using System.Collections.Generic;
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
        Init(); 
    }

    void Init()
    {
        CreateCardUI();
        turnManager.OnTurnChanged += UpdatePhaseUI; 
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
    }

    public void OnDisplaySelectionEndButton()
    {

    }
    public void UpdatePhaseUI() => currentPhase.text = turnManager.GetCurrentTurn().ToString();
}
