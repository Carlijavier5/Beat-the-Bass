using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundFish : MonoBehaviour {
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 rotation;
    private float speed;
    
    private void Awake() {
        speed = Random.Range(1f, 3f);
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void Update() {
        transform.position += (speed * direction);
    }
}
