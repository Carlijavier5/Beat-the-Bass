using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    private PlayerData playerData;
    void Awake() {
        playerData = (PlayerData) Data;
        rb = GetComponent<Rigidbody>();
        //GameManager.Instance.Input.OnMove += HandleInput;
    }

    void FixedUpdate() {
        HandleInput();
    }

    private void HandleInput() {
        if (!IsOwner) return;
        Vector2 input = GameManager.Instance.Input.MoveVector;
        Vector3 direction = new Vector3(input.x, 0f, input.y) * playerData.moveSpeed;
        Debug.Log(direction);
        rb.AddRelativeForce(direction);
    }
}
