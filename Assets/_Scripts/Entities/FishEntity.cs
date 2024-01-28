using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEntity : Entity
{
    private FishData fishData;
    private float timer = 0.0f;

    void Awake() {
        fishData = (FishData) data;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= fishData.moveInterval) {
            MoveFish();
            timer = 0.0f;
        }
    }

    private void MoveFish() {
        Vector3 randDirection = Random.onUnitSphere;
        randDirection.y = 0;

        Vector3 localDirection = transform.TransformDirection(randDirection);
        rb.AddRelativeForce(localDirection * fishData.flopMagnitude, ForceMode.Impulse);
    }

    public float getFlopTime() {
        return fishData.flopTime;
    }

    // get direction between two points
}
