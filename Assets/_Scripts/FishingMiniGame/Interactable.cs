using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool canFish = false;
    [SerializeField] private FishingManager fishingManager;  

    void Start() {
        GameManager.Instance.Input.OnInteraction += StartFishing;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            canFish = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            canFish = false;
        }
    }

    private void StartFishing() {
        if (canFish) {
            fishingManager.StartFishing();
        }
    }
}
