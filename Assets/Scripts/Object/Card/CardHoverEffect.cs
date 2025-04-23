using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardHoverEffect : MonoBehaviour
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

    void OnMouseEnter()
    {
        if (cardMovement.isMoving)
            return;

        if (isHover)
            return; 

        originPRS = cardMovement.originPRS; 
        hoverPRS = new PRS(originPRS.position + hoverPosition, hoverRotation, hoverScale);

        originOrder = sortGroup.sortingOrder;
        sortGroup.sortingOrder = hoverOrder;

        cardMovement.MoveTransform(hoverPRS, true);
        
        isHover = true; 
    }

    void OnMouseExit()
    {
        if (!isHover)
            return; 

        sortGroup.sortingOrder = originOrder;

        cardMovement.MoveTransform(originPRS, true);

        isHover = false; 
    }

    void OnMouseDown()
    {
        sortGroup.sortingOrder = hoverOrder;
        cardMovement.MoveTransform(originPRS, true);
    }

    void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mousePos;
    }

    private void OnMouseUp()
    {
        cardMovement.MoveTransform(originPRS, true, 0.7f);
    }

}
