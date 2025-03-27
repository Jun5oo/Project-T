using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    [SerializeField] SelectionManager selectionManager;
    [SerializeField] TurnManager turnManager; 
    public void OnClickEvent()
    {
        turnManager.ChangeTurn(); 
    }
}
