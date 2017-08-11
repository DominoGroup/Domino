using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 基础的骨牌脚本
/// </summary>
public class Domino : MapItem
{
    private Rigidbody rigidTarget;
    protected override void Init()
    {
        rigidTarget = GetComponent<Rigidbody>();
    }
    // 将物理时间转回渲染时间
    private void Update()
    {
        if (!rigidTarget.IsSleeping())
            state = MapItemState.acting;
        else if (state != MapItemState.wait)
            state = MapItemState.acted;
    }
}