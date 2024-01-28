using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    private PlayerData playerData;
    [SerializeField] private GameObject hitCollider;
    [SerializeField] private float hitDelay = 1f;
    private IEnumerator activeHit = null;
    private bool canMove = true;
    void Awake() {
        playerData = (PlayerData) Data;
        rb = GetComponent<Rigidbody>();
        GameManager.Instance.Input.OnBeat += HandleAttack;
        //GameManager.Instance.Input.OnMove += HandleInput;
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
        Debug.Log(direction);
        if (canMove) rb.AddRelativeForce(direction);
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
