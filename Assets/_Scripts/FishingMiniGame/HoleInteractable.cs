using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInteractable : MonoBehaviour
{
    void Start() {
        GameManager.Instance.Input.OnInteraction += DropFish;
    }

    private void DropFish() {
        // if player has a fish (check with PlayerEntityScript)

        // do animation for dropping fish in the hole and delete the prefab in the players hands

        // update score
    }
}
