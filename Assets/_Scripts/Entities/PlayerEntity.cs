using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerEntity : Entity {

    [SerializeField] private Transform modelTransform;

    private PlayerData playerData;
    [SerializeField] private GameObject hitCollider;
    [SerializeField] private float hitDelay = 1f;
    [SerializeField] private float hitDuration = 0.8f;
    [SerializeField] private Animator anim;
    private bool isHolding;
    public bool isFishing;
    private float attackTimer;
    
    private bool canMove = true;

    private bool dead = false;
    private float respawnTimer = 3f;

    void Awake() {
        attackTimer = hitDelay;
        playerData = (PlayerData) Data;
        rb = GetComponent<Rigidbody>();
        GameManager.Instance.Input.OnBeat += HandleAttack;
    }

    void FixedUpdate() {
        HandleInput();
    }

    private void Update() {
        float diff = Time.deltaTime;
        attackTimer -= diff;
        if (dead) respawnTimer -= diff;
        HandleRespawn();
    }

    private void HandleRespawn() {
        if (dead && respawnTimer <= 0) {
            dead = false;
            respawnTimer = 3f;
            transform.localPosition = RespawnAnchor.Instance.spawnLoc.position;
            Debug.Log("RESPAWNED");
        }
    }

    public void CanMove(bool enableMove) {
        this.canMove = enableMove;
    }

    public override void Ragdoll() {
        if (!dead) {
            base.Ragdoll();
            CanMove(false);
            dead = true;
        }
    }

    private void HandleInput() {
        if (!IsOwner) return;
        Vector2 input = GameManager.Instance.Input.MoveVector;
        Vector3 direction = new Vector3(input.x, 0f, input.y) * playerData.moveSpeed;
        if (canMove) Translate(direction);
        if (rb.velocity.magnitude > 0) modelTransform.transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
        
        
        anim.SetBool("IsMoving", rb.velocity.magnitude > .2f);
        anim.SetBool("IsHolding", isHolding);
        anim.SetBool("IsFishing", isFishing);
    }

    public void HandleAttack() {
        if (attackTimer <= 0 ) {
            AttackAction();
        }
    }

    private void AttackAction() {
        if (!IsHost) return;
        anim.SetTrigger("Attack");
        Vector3 colliderTransform = modelTransform.position + modelTransform.forward * 1.5f;
        colliderTransform = new Vector3(colliderTransform.x, colliderTransform.y + 1, colliderTransform.z);
        GameObject attack = Instantiate(hitCollider, colliderTransform, modelTransform.rotation);
        attack.GetComponent<Unity.Netcode.NetworkObject>().Spawn(true);
        attack.transform.SetParent(transform);
        Destroy(attack, hitDuration);
        attackTimer = hitDelay;
    }
}
