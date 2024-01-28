using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinEntity : Entity
{
    private PenguinData penguinData;
    private float timer = 0.0f;

    void Awake() {
        penguinData = (PenguinData)data;
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= penguinData.moveInterval) {
            MovePengu();
            timer = 0.0f;
        }
    }

    private void MovePengu() {

    }
}
