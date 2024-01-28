using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingInteractable : MonoBehaviour
{
    private bool canFish = false;
    [SerializeField] private FishingManager fishingManager;
    public PlayerEntity playerInArea = null;

    void Start() {
        GameManager.Instance.Input.OnInteraction += StartFishing;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            playerInArea = other.GetComponent<PlayerEntity>();
            canFish = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerInArea = null;
            canFish = false;
        }
    }

    private void StartFishing() {
        if (canFish) {
            playerInArea.CanMove(false);
            fishingManager.StartFishing(this);
        }
    }

    public void StopFishing() {
        playerInArea.CanMove(true);
    }
}
