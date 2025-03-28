using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    CardSO cardSO;

    public PRS originPRS;

    // Card Data 
    public string cardName;
    public SpriteRenderer sp;
    public int cardCost; 
    public string cardDescription; 
    public List<EffectSO> cardEffects; 

    public void Initialize(CardSO cardData)
    {
        cardSO = cardData;
        cardName = cardData.cardName;
        sp.sprite = cardData.cardIcon;
        cardCost = cardData.cost;
        cardEffects = cardData.effects; 
    }
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        Debug.Log("Card MoveTransform"); 

        if(useDotween)
        {
            transform.DOMove(prs.position, dotweenTime);
            transform.DORotateQuaternion(prs.rotation, dotweenTime); 
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.position; 
            transform.rotation = prs.rotation;
            transform.localScale = prs.scale; 
        }
    }

    public CardSO GetCardSO() => cardSO; 
}
