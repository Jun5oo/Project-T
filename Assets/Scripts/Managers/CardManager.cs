using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // 게임 시작 전 플레이어가 선택한 카드 
    [SerializeField] List<CardSO> cardList;

    public List<CardSO> GetCardList() => cardList; 
}
