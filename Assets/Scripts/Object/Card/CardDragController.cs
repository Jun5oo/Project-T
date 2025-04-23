using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDragController : MonoBehaviour, IDraggable
{
    CardMovement cardMovement;

    void Awake()
    {
        cardMovement = GetComponent<CardMovement>();
    }

    public void OnDragEnter(Vector2 mousePosition)
    {
        this.transform.rotation = Quaternion.identity;
        this.GetComponent<SortingGroup>().sortingOrder = 99; 
        this.transform.position = mousePosition; 
    }

    public void OnDrag(Vector2 mousePosition)
    {
        this.transform.position = mousePosition;
    }

    public void OnDragExit(Vector2 mousePosition)
    {
        // Raycast (if its on the Drop Zone, Destroy) 
        cardMovement.MoveTransform(cardMovement.originPRS, true, 0.7f);
    }
}
