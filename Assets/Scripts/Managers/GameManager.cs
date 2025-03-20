using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] GridManager gridManager;

    void Start()
    {
        player.transform.position = gridManager.GetRandomStartPosition(); 
    }

}
