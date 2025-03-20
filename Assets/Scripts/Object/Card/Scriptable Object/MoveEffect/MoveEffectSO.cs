using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveEffectSO", menuName = "ScriptableObject/MoveEffectSO")]
public class MoveEffectSO : EffectSO
{
    [SerializeField] PatternSO movePattern; 

    public override void Perform()
    {
        Debug.Log(movePattern.pattern[0] + " 만큼 이동한다."); 
    }


}
