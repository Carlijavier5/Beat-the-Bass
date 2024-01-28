using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    [SerializeField] private Transform modelTransform;
    private Vector3 prevPos;

    private PlayerData playerData;
    [SerializeField] private GameObject hitCollider;
    [SerializeField] private float hitDelay = 1f;
    private IEnumerator activeHit = null;
    private bool canMove = true;
    void Awake() {
        playerData = (PlayerData) Data;
        rb = GetComponent<Rigidbody>();
        GameManager.Instance.Input.OnBeat += HandleAttack;
    }

    void FixedUpdate() {
        HandleInput();
    }

    public void CanMove(bool enableMove) {
        this.canMove = enableMove;
    }

    private void HandleInput() {
        if (!IsOwner) return;
        Vector2 input = GameManager.Instance.Input.MoveVector;
        Vector3 direction = new Vector3(input.x, 0f, input.y) * playerData.moveSpeed;
        if (canMove) Translate(direction);
        if (rb.velocity.magnitude > 0) modelTransform.transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
    }

    private void HandleAttack() {
        if (activeHit == null) {
            activeHit = AttackAction();
            StartCoroutine(activeHit);
        }
    }

    private IEnumerator AttackAction() {
        GameObject attack = Instantiate(hitCollider, transform.position + transform.forward * 2, transform.rotation);
        Destroy(attack, 0.5f);
    }
}
