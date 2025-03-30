using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject phaseUI;
    [SerializeField] GameObject waitingUI;
    [SerializeField] GameObject countDownUI;

    [SerializeField] GameObject deckUI;
    [SerializeField] GameObject discardPileUI; 

    void OnActivePhaseUI() => phaseUI.SetActive(true);
    void OnActiveWaitingUI() => waitingUI.SetActive(true);
    void OnActivePhaseTimerUI() => countDownUI.SetActive(true); 


}
