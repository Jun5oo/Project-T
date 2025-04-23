using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    void OnDragEnter(Vector2 mousePosition);
    void OnDrag(Vector2 mousePosition); 
    void OnDragExit(Vector2 mousePosition); 
}
