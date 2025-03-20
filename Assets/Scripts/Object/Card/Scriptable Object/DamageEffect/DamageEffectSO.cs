using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffectSO", menuName = "ScriptableObject/DamageEffectSO")]
public class DamageEffectSO : EffectSO
{
    public PatternSO attackPattern; 
    public override void Perform()
    {
        foreach(Vector2Int pos in attackPattern.pattern)
        {
            Debug.Log(pos); 
        }
        Debug.Log("를 공격한다"); 
    }
}
