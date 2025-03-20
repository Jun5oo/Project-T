using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType
{
    Movement, 
    Attack, 
    Util
}

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{ 
    CardSO cardSO;
    [SerializeField] SelectionManager selectionManager; 
    
    public string cardName; 
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI cost;

    public bool isSelected;
    public GameObject selectionMask;
    public TextMeshProUGUI selectedOrder; 

    void Awake()
    {
        selectionManager = GameObject.FindObjectOfType<SelectionManager>(); 
    }

    public void Initialize(CardSO cardData)
    {
        cardSO = cardData;
        cardName = cardData.cardName; 
        img.sprite = cardSO.cardIcon;
        cost.text = cardSO.cost.ToString(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(cardSO.cardName + " Selected");
        
        if (!isSelected && !selectionManager.IsCardFull())
        {
            isSelected = true;
            selectionManager.AddToList(this);
            DisplaySelectionMask(); 
        }
        else
        {
            isSelected = false;
            selectionManager.RemoveFromList(this);
            HideSelectionMask(); 
        }
    }

    public void OnPointerEnter(PointerEventData eventData) => this.transform.localScale = Vector3.one * 1.1f; 

    public void OnPointerExit(PointerEventData eventData) => this.transform.localScale = Vector3.one; 

    public void PeformEffect()
    {
        foreach(var effect in cardSO.effects)
        {
            effect.Perform(); 
        }
    }

    public void DisplaySelectionMask() => selectionMask.SetActive(true);
    public void HideSelectionMask() => selectionMask.SetActive(false); 
    public void UpdateSelectedOrder(int order) => selectedOrder.text = order.ToString();
}
