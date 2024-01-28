using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    [SerializeField] private Transform modelTransform;
    private Vector3 prevPos;

    private PlayerData playerData;
    void Awake() {
        playerData = (PlayerData) Data;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        HandleInput();
    }

    private void HandleInput() {
        if (!IsOwner) return;
        Vector2 input = GameManager.Instance.Input.MoveVector;
        Vector3 direction = new Vector3(input.x, 0f, input.y) * playerData.moveSpeed;
        Translate(direction);
        if (rb.velocity.magnitude > 0) modelTransform.transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
    }
}
