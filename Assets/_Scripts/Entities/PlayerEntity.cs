using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {
    private PlayerData playerData;
    protected override void Awake() {
        base.Awake();
        playerData = (PlayerData) data;
        //GameManager.Instance.Input.OnMove += HandleInput;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        HandleInput();
        
    }

    private void HandleInput(/*Vector2 input*/) {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * playerData.moveSpeed;
        rigidbody.AddRelativeForce(direction);
    }
}
