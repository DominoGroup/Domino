using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoSingleton<GameFlowManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}