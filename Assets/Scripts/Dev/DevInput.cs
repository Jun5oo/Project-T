using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevInput : MonoBehaviour
{
    [SerializeField] CardManager cardManager;

    void Update()
    {
#if UNITY_EDITOR
        Dev_Input();
#endif
    }

    public void Dev_Input()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            cardManager.DrawCard();

        if (Input.GetKeyDown(KeyCode.D))
            cardManager.DiscardAllCards();
    }
}
