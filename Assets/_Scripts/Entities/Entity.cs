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
    #region Physics
    [Range(0f, 10f)] [SerializeField] private float dragConstant; //TODO: REPLACE WITH BOAT DRAG CONSTANT
    
    private float drag;
    protected Rigidbody rigidbody;
    #endregion Physics
    
    public EntityData data;

    [SerializeField] private Vector3 floatAdditive; //TODO: REPLACE WITH BOAT TRANSLATE
    
    protected virtual void Awake() {
        drag = dragConstant * data.weight;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.drag = drag;
        //dragConstant = GetComponentInParent<Boat>();
        //TODO: Set drag constant
    }

    protected virtual void FixedUpdate() {
        Translate(floatAdditive);
    }

    private void Translate(Vector3 direction) {
        rigidbody.AddRelativeForce(direction);
    }
}
