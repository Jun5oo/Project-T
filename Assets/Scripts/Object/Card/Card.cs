using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Card 
{
    CardSO cardData;

    public PRS originPRS;

    // Card Data 
    public string cardName;
    public string cardDescription;
    public int cardMana;

    public Sprite cardImage;
    public List<EffectSO> cardEffects; 

    public Card(CardSO cardData)
    {
        cardName = cardData.name;
        cardDescription = cardData.description;
        cardMana = cardData.mana; 

        cardImage = cardData.image;
        cardEffects = new List<EffectSO>();
    }

    /*
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
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
    */ 
}
