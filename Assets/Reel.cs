using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour {
    public float rotationSpeed = 0.5f;
    private void Update() {
        transform.Rotate(new Vector3(0f, 0f, -rotationSpeed));
    }
}
