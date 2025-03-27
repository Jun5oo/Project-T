using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        selectionManager.OnSelectionChanged += UpdateSelectionMask; 
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
        }
        else
        {
            isSelected = false;
            selectionManager.RemoveFromList(this);
        }
    }
    public void OnPointerEnter(PointerEventData eventData) => this.transform.localScale = Vector3.one * 1.1f; 
    public void OnPointerExit(PointerEventData eventData) => this.transform.localScale = Vector3.one; 

    public void DisplaySelectionMask() => selectionMask.SetActive(true);
    public void HideSelectionMask() => selectionMask.SetActive(false); 
    public void UpdateSelectionMask(List<CardUI> selectedCards)
    {
        int idx = selectedCards.IndexOf(this); 

        if(idx >= 0)
        {
            selectedOrder.text = (idx + 1).ToString();
            DisplaySelectionMask(); 
        }

        else
        {
            selectedOrder.text = ""; 
            HideSelectionMask(); 
        }
    }
}
