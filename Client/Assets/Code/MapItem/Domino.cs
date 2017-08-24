using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 基础的骨牌脚本
/// </summary>
public class Domino : MapItem
{
    private Rigidbody rigidBody;
    private BoxCollider boxCollider;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    protected override void SetTypeData(ItemTypeData typeData)
    {
        base.SetTypeData(typeData);
        rigidBody.mass = typeData.mass;
        rigidBody.drag = typeData.drag;
        rigidBody.angularDrag = typeData.angularDrag;
        boxCollider.sharedMaterial = AssetHub.instance.GetAsset<PhysicMaterial>(PathConst.physicsBundle, typeData.physics);
    }
}