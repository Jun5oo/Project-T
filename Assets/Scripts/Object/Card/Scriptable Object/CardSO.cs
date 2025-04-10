using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "ScriptableObject/CardSO")]
public class CardSO : ScriptableObject 
{
    public string name;
    public string description;
    public int mana;

    public Sprite image;
    public List<EffectSO> effects; 
}
