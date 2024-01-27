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
        Vector2 input = GameManager.Instance.Input.MoveVector;
        Vector3 direction = new Vector3(input.x, 0f, input.y) * playerData.moveSpeed;
        Debug.Log(direction.ToString());
        rigidbody.AddRelativeForce(direction);
    }
}
