using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] TextMeshPro title;
    [SerializeField] TextMeshPro description;
    [SerializeField] TextMeshPro mana;
    
    [SerializeField] SpriteRenderer imageSR;
    [SerializeField] GameObject wrapper;

    public Card Card { get; private set; }

    public void Init(Card card)
    {
        Card = card;

        title.text = card.CardName;
        description.text = card.CardDescription;
        mana.text = card.CardMana.ToString();
        
        imageSR.sprite = card.CardImage; 
    }
}
