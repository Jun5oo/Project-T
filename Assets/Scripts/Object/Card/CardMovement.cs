using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public PRS originPRS;

    public void Init(PRS prs)
    {
        originPRS = prs;

        this.transform.position = prs.position; 
        this.transform.rotation = prs.rotation;
        this.transform.localScale = prs.scale; 
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0, Action callback = null)
    {
        if(useDotween)
        {
            transform.DOMove(prs.position, dotweenTime).OnComplete(()=> callback?.Invoke());
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
}
