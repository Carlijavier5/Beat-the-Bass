using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform.GetComponent<PlayerEntity>() != null) {
            other.transform.GetComponent<PlayerEntity>().Ragdoll();
        }
    }
}
