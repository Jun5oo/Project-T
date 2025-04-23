using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] SpriteRenderer sr;

    Color onSelected;
    Color nonSelected; 

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        
        onSelected = Color.red;
        nonSelected = Color.red; 

        onSelected.a = 1f;
        nonSelected.a = 0f; 
    }

    void OnMouseEnter()
    {
        sr.color = onSelected; 
    }

    void OnMouseExit()
    {
        sr.color = nonSelected; 
    }
}
