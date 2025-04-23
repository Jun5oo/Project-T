using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardHoverEffect : MonoBehaviour, IHoverable 
{
    CardMovement cardMovement; 

    PRS originPRS;
    PRS hoverPRS;

    Vector3 hoverPosition;
    Quaternion hoverRotation;
    Vector3 hoverScale;

    SortingGroup sortGroup;
    int originOrder;
    int hoverOrder;

    bool isHover; 

    void Awake()
    {
        cardMovement = this.GetComponent<CardMovement>();

        originPRS = cardMovement.originPRS;

        hoverPosition = Vector3.up; 
        hoverRotation = Quaternion.identity;
        hoverScale = Vector3.one * 2f; 

        sortGroup = this.GetComponent <SortingGroup>();
        hoverOrder = 99;

        isHover = false; 
    }

    public void OnHoverEnter()
    {
        if (isHover || cardMovement.isMoving)
            return; 

        originPRS = cardMovement.originPRS;

        hoverPRS = new PRS(originPRS.position + hoverPosition, hoverRotation, hoverScale);
        sortGroup.sortingOrder = hoverOrder;

        cardMovement.MoveTransform(hoverPRS, true); 
        isHover = true; 
    }

    public void OnHoverExit()
    {
        if (!isHover)
            return; 

        sortGroup.sortingOrder = originOrder;
        
        cardMovement.MoveTransform(originPRS, true);
        isHover = false; 
    }

}
