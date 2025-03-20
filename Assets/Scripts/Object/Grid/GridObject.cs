using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject 
{
    public Vector2Int gridPos;
    public Vector2 worldPos;
    public bool isMoveable; 

    public GridObject(Vector2Int gridPos, Vector2 worldPos, bool isMoveable)
    {
        this.gridPos = gridPos;
        this.worldPos = worldPos;
        this.isMoveable = isMoveable;
    }
}
