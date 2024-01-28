using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HitCollider : MonoBehaviour {
    private BoxCollider collider;
    [SerializeField] private float hitPower = 2f;

    public void Awake() {
        collider = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider other) {
        Transform collided = other.transform;
        Vector3 offset = (collided.position - transform.position).normalized;
        offset = new Vector3(offset.x, 0f, offset.z);
        Rigidbody rb = other.transform.GetComponent<Rigidbody>();
        if (rb == null) rb.AddRelativeForce(offset * hitPower);
    }
}
