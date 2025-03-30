using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform cardDespawnPoint; 

    [SerializeField] Transform handLeft;
    [SerializeField] Transform handRight;

    // Temp Card List 
    [SerializeField] List<CardSO> cardList; 
    // Card on Hand 
    [SerializeField] List<Card> handCardList;
    // Card on Deck 
    [SerializeField] Queue<CardSO> deckList;
    // Discard Pile 
    [SerializeField] List<CardSO> discardList;

    [SerializeField] TextMeshProUGUI discardPileNum;
    [SerializeField] TextMeshProUGUI deckNum; 

    void Awake()
    {
        Init(); 
    }

    void Init()
    {
        deckList = new Queue<CardSO>();

        for (int i = 0; i < cardList.Count; i++)
            deckList.Enqueue(cardList[i]);

        UpdateDeckNum();
        UpdateDiscardPileNum(); 
    }

    public void DrawCard()
    {
        CardSO cardData = null;

        if (deckList.Count <= 0)
            RecycleCard(); 

        cardData = deckList.Dequeue();

        UpdateDeckNum(); 

        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
        
        Card card = cardObject.GetComponent<Card>();
        card.Initialize(cardData);
        handCardList.Add(card); 
        CardAlignment(); 
    }

    public void CardAlignment()
    {
        var targetCards = handCardList;
       
        List<PRS> originCardPRS = new List<PRS>();
        
        originCardPRS = RoundAlignment(handLeft, handRight, targetCards.Count, 0.5f, Vector3.one); 
        
        for(int i=0; i<targetCards.Count; i++)
        {
            var targetCard = targetCards[i];
            targetCard.originPRS = originCardPRS[i]; 
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f); 
        }
    }
    public List<PRS> RoundAlignment(Transform left, Transform right, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break; 
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            
            default:
                float interval = 1f / (objCount - 1); 
                for(int i=0; i<objCount; i++)
                    objLerps[i] = interval * i;
                break; 
        }

        for(int i=0; i<objCount; i++)
        {
            var targetPos = Vector3.Lerp(left.position, right.position, objLerps[i]);
            var targetRot = Quaternion.identity; 
            if(objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(left.rotation, right.rotation, objLerps[i]); 
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results; 
    }

    public void DiscardAllCards()
    {
        for(int i=0; i<handCardList.Count; i++)
        {
            discardList.Add(handCardList[i].GetCardSO());
            handCardList[i].MoveTransform(new PRS(cardDespawnPoint.position, Quaternion.identity, Vector3.one), true, 0.7f); 
        }

        handCardList.Clear(); 

        UpdateDiscardPileNum(); 
    }
    public void RecycleCard()
    {
        Shuffle(discardList); 

        for(int i=0; i<discardList.Count; i++)
            deckList.Enqueue(discardList[i]);

        discardList.Clear();

        UpdateDeckNum();
        UpdateDiscardPileNum(); 
    }
    public void Shuffle(List<CardSO> cardList)
    {
        for(int i=0; i<cardList.Count; i++)
        {
            int rand = Random.Range(i, cardList.Count);
            CardSO first = cardList[i];
            CardSO second = cardList[rand];
            cardList[i] = second; 
            cardList[rand] = first; 
        }
    }
    public void UpdateDeckNum()
    {
        deckNum.text = deckList.Count.ToString(); 
    }
    public void UpdateDiscardPileNum()
    {
        discardPileNum.text = discardList.Count.ToString();
    }
}
