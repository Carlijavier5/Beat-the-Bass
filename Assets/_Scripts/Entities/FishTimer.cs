using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    private FishEntity fish;
    private float flopTime;

    void Start() {
        fish = this.GetComponent<FishEntity>();
    }
}
