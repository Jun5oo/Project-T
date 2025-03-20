using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector2 origin; 

    Dictionary<Vector2Int, GridObject> gridDictionary = new Dictionary<Vector2Int, GridObject>();

    [SerializeField] GameObject gridPrefab;
    [SerializeField] int cellSize;

    void Awake()
    {
        CreateGrid(); 
    }

    void CreateGrid()
    {
        origin = Vector2.zero;
        GameObject gridParent = new GameObject("gridParent");
        
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                Vector2 worldPos = GetWorldPosition(x, y); 

                GridObject grid = new GridObject(gridPos, worldPos, true);
                gridDictionary.Add(gridPos, grid);

                GameObject go = Instantiate(gridPrefab, worldPos, Quaternion.identity);
                go.name = $"{x},{y}"; 
                go.transform.parent = gridParent.transform; 
            }
        }
    }

    Vector2 GetWorldPosition(int x, int y)
    {
        float offsetX = (width - 1) * cellSize * 0.5f;
        float offsetY = (height - 1) * cellSize * 0.5f;

        return new Vector2(origin.x + (x * cellSize) - offsetX, origin.y + (y * cellSize) - offsetY);  
    }

    public Vector2 GetRandomStartPosition()
    {
        int rand = Random.Range(0, height - 1);
        Vector2 pos = gridDictionary[new Vector2Int(0, rand)].worldPos;
        return pos; 
    }
}
