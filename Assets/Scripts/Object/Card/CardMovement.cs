using DG.Tweening;
using System;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public PRS originPRS;
    public bool isMoving; 

    public void Init(PRS prs)
    {
        originPRS = prs;

        this.transform.position = prs.position; 
        this.transform.rotation = prs.rotation;
        this.transform.localScale = prs.scale; 
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0, Action callback = null)
    {
        if (useDotween)
        {
            originPRS = prs;

            isMoving = true; 

            Sequence sequence = DOTween.Sequence();
            sequence.Join(transform.DOMove(prs.position, dotweenTime)); 
            sequence.Join(transform.DORotateQuaternion(prs.rotation, dotweenTime));
            sequence.Join(transform.DOScale(prs.scale, dotweenTime));
            sequence.OnComplete(() =>
            {
                callback?.Invoke();
                isMoving = false; 
            }); 
        }
        else
        {
            originPRS = prs; 

            transform.position = prs.position;
            transform.rotation = prs.rotation;
            transform.localScale = prs.scale;
        }
    }
}
