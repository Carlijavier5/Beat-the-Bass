using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEntity : Entity
{
    private FishData fishData;
    private float timer = 0.0f;

    protected override void Awake() {
        base.Awake();
        fishData = (FishData) data;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
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
        rigidbody.AddRelativeForce(localDirection * fishData.flopMagnitude, ForceMode.Impulse);
    }

    // get direction between two points
}
