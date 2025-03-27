using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumScript : MonoBehaviour
{
    public enum CardType
    {
        Movement,
        Attack,
        Util
    }

    public enum Turn
    {
        Selection,
        Battle,
    }
}
