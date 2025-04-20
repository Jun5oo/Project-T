using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReadyablePhase : IPhase
{
    void PlayerReady(string playerID);
    bool IsAllPlayerReady(); 
}
