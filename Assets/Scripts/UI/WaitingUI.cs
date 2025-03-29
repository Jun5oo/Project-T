using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WaitingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waitingText;

    string displayText = "Waiting for other player";

    int dotCount;
    int mod; 
    float delay;

    WaitForSeconds waitSecond; 

    void OnEnable()
    {
        dotCount = 0;
        mod = 4; 
        delay = 0.5f;

        waitSecond = new WaitForSeconds(delay);

        StartCoroutine(UpdateWaitingText());
    }

    void OnDisable()
    {
        StopAllCoroutines(); 
    }

    IEnumerator UpdateWaitingText()
    {
        while (true)
        {
            dotCount = (dotCount + 1) % mod;
            waitingText.text = displayText + new string('.', dotCount);
            yield return new WaitForSeconds(delay);
        }
    }
}
