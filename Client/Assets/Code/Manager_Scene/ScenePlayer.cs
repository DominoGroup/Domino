﻿using UnityEngine;
using UnityEditor;
public class ScenePlayer : MonoSingleton<ScenePlayer>
{
    public SceneObjectHub objectHub { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        objectHub = new SceneObjectHub();
    }
}