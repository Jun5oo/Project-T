using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "ScriptableObject/CardSO")]
public class CardSO : ScriptableObject 
{
    public string cardName;
    public Sprite cardIcon;
    public string description;
    public int cost;
    public List<EffectSO> effects; 
}
