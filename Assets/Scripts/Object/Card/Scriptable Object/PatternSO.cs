using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternSO", menuName = "ScriptableObject/PatternSO")]
public class PatternSO : ScriptableObject
{
    public List<Vector2Int> pattern; 
}
