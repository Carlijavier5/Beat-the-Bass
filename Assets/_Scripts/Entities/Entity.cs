using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Netcode;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class Entity : NetworkBehaviour {
    [SerializeField] protected GlobalEntityData globalData;
    [SerializeField] protected EntityData data;
    public EntityData Data => data;
    protected Rigidbody rb;

    public override void OnNetworkSpawn() {
        rb = GetComponent<Rigidbody>();
        rb.drag = globalData.Drag * data.weight;
    }

    public void Translate(Vector3 direction) {
        ///rb.AddRelativeForce(direction);
    }
}