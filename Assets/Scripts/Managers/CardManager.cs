using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // ���� ���� �� �÷��̾ ������ ī�� 
    [SerializeField] List<CardSO> cardList;

    public List<CardSO> GetCardList() => cardList; 
}
