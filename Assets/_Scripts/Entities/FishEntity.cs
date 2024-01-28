using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEntity : Entity
{
    private FishData fishData;

    protected override void Awake() {
        base.Awake();
        fishData = (FishData) data;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    private void MoveFish() {

    }
}
