using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2; 

    [SerializeField] GridManager gridManager;

    void Start()
    {
        Player1.transform.position = gridManager.GetRandomStartPosition(); 
    }

}
