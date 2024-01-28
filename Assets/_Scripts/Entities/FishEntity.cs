using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEntity : Entity
{
    private FishData fishData;
    private float timer = 0.0f;

    private bool fishIsMoving = true;

    private bool isPickedUp = false;

    void Awake() {
        fishData = (FishData) data;
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= fishData.moveInterval && fishIsMoving) {
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

    public int getFlopTime() {
        return fishData.flopTime;
    }

    public void StopFlop() {
        fishIsMoving = false;
    }

    public float getSpawnProbability() {
        if (fishData == null) fishData = (FishData) data;
        return fishData.spawnChance;
    }

    public void PickUpFish(GameObject player) {
        isPickedUp = true;

        this.transform.SetParent(player.transform);
    }

    public void DropFish() {
        isPickedUp = false;
        this.transform.SetParent(null);
    }
}
