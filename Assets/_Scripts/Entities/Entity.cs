using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Netcode;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Entity : NetworkBehaviour {
    [Range(0f, 1f)] [SerializeField] private float dragConstant;
    
    public EntityData data;

    [SerializeField] private Vector3 floatAdditive;
    
    private Vector3 totalForce = Vector3.zero;
    private const float ForceClampApproximation = 0.0001f;
    
    private void Awake() {
        //dragConstant = GetComponentInParent<Boat>();
        //TODO: Set drag constant
    }

    public void AddForce(Vector3 direction) {
        totalForce += direction;
    }

    private void FixedUpdate() {
        AddForce(floatAdditive);
        totalForce *= dragConstant;
        ApproximateForce();
        Debug.Log(totalForce);
        Translate();
    }

    private void Translate() {
        transform.position += totalForce;
    }

    private void ApproximateForce() {
        if (totalForce.magnitude < ForceClampApproximation) {
            totalForce = Vector3.zero;
        }
    }
}
