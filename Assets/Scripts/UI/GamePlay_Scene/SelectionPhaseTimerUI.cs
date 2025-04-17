using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionPhaseTimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerUIText; 
    [SerializeField] float timer; 

    void OnEnable()
    {
        StartCountDown(); 
        timer = 60f;     
    }

    void OnDisable()
    {
        StopCountDown(); 
        timer = 60f; 
    }

    void StartCountDown() => StartCoroutine(CountDownCoroutine());
    void StopCountDown() => StopCoroutine(CountDownCoroutine()); 

    IEnumerator CountDownCoroutine()
    {
        yield return new WaitForSeconds(2f);

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            timerUIText.text = Mathf.Ceil(timer).ToString();
            yield return null; 
        }

        timer = 0; 
    }
}
