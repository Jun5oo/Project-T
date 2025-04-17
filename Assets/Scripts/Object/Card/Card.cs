using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    CardSO cardData;

    // Card Data 
    public string CardName { get; private set; }
    public string CardDescription { get; private set; }
    public int CardMana { get; private set; }

    public Sprite CardImage { get; private set; }
    public List<EffectSO> CardEffects { get; private set; } 

    public void Init(CardSO card, PRS prs)
    {
        // ī�� ������ 
        cardData = card; 

        CardName = cardData.name;
        CardDescription = cardData.description;
        CardMana = cardData.mana; 

        CardImage = cardData.image;
        CardEffects = new List<EffectSO>();

        // ī�� �� 
        GetComponent<CardView>()?.Init(this);
        // ī�� PRS 
        GetComponent<CardMovement>()?.Init(prs); 
    }
}
