using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject deckPile;
    [SerializeField] GameObject discardPile;
    
    [SerializeField] Transform handLeft;
    [SerializeField] Transform handRight;

    // Card on Hand 
    [SerializeField] List<GameObject> handCardList;
    [SerializeField] List<CardSO> deckList;

    public Action OnDrawComplete; 
    public Action OnDiscardComplete; 

    public GameObject CreateCard()
    {
        Vector3 deckPos = Camera.main.ScreenToWorldPoint(deckPile.transform.position);
        
        PRS prs = new PRS(deckPos, Quaternion.identity, Vector3.zero); 

        var cardObject = Instantiate(cardPrefab);
        cardObject.GetComponent<Card>()?.Init(deckList[0], prs);

        return cardObject; 
    }
    public void AddCard(GameObject card)
    {
        handCardList.Add(card);
    }
    public void DrawCard()
    {
        var card = CreateCard();
        AddCard(card); 
        CardAlignment();
        SortOrder();
    }
    public void DrawCards(int drawNum)
    {
        StartCoroutine(coDrawCards(drawNum)); 
    }
    public void Discard(GameObject card, bool isLastCard = false)
    {
        Vector3 discardPilePos = Camera.main.ScreenToWorldPoint(discardPile.transform.position);
    
        PRS targetPrs = new PRS(discardPilePos, Quaternion.identity, Vector3.zero);

        if (!isLastCard)
            card.GetComponent<CardMovement>().MoveTransform(targetPrs, true, 0.7f);
        else
            card.GetComponent<CardMovement>().MoveTransform(targetPrs, true, 0.7f, OnDiscardComplete); 

        handCardList.Remove(card); 
    }
    public void DiscardAll()
    {
        StartCoroutine(coDiscardAll()); 
    }
    public void SortOrder()
    {
        int idx = 0; 

        foreach (var card in handCardList)
            card.GetComponent<SortingGroup>().sortingOrder = idx++;  
    }
    public void CardAlignment()
    {
        var targetCards = handCardList;
       
        List<PRS> originCardPRS = new List<PRS>();
        
        originCardPRS = RoundAlignment(handLeft, handRight, targetCards.Count, 0.5f, Vector3.one); 
        
        for(int i=0; i<targetCards.Count; i++)
        {
            var movement = targetCards[i].GetComponent<CardMovement>();
            movement.MoveTransform(originCardPRS[i], true, 0.7f); 
        }
    }
    public List<PRS> RoundAlignment(Transform left, Transform right, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break; 
            case 2: objLerps = new float[] { 0.35f, 0.65f }; break;
            case 3: objLerps = new float[] { 0.2f, 0.5f, 0.8f }; break;
            
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
                // objLerps[i] (bounds of objLerps = 0 ~ 1) 
                // height = radius of the half-round
                float curveY = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                targetPos.y += curveY;

                float rotZ = Mathf.LerpAngle(left.eulerAngles.z, right.eulerAngles.z, objLerps[i]); 
                targetRot = Quaternion.Euler(0, 0, rotZ); 
            }

            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results; 
    }

    IEnumerator coDrawCards(int drawNum)
    {
        // Selection Phase 시작 때 UI가 준비 된 후, 0.5초 후 드로우 
        yield return new WaitForSeconds(0.5f); 

        for(int i=0; i<drawNum; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(0.1f); 
        }
    }
    IEnumerator coDiscardAll()
    {
        bool isLastCard = false; 

        while(handCardList.Count > 0)
        {
            if (handCardList.Count == 1)
                isLastCard = true; 
            
            Discard(handCardList[0], isLastCard);
            
            yield return new WaitForSeconds(0.1f); 
        }
    }
}
