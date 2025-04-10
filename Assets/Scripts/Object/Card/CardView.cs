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

    public void SetUp(Card card)
    {
        Card = card;

        title.text = card.cardName;
        description.text = card.cardDescription;
        mana.text = card.cardMana.ToString();
        
        imageSR.sprite = card.cardImage; 
    }
}
