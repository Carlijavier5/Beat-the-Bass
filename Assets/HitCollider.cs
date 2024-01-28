using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {
    private BoxCollider collider;
    [SerializeField] private float hitPower = 2f;

    public void Awake() {
        collider = GetComponent<BoxCollider>();
    }

    public void OnCollisionEnter(Collision other) {
        Transform collided = other.transform;
        Vector3 offset = (collided.position - transform.position).normalized;
        other.transform.GetComponent<Rigidbody>().AddRelativeForce(offset * hitPower);
        Debug.Log(offset.ToString());
    }
}
