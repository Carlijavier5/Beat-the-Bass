using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPingPong : MonoBehaviour {

    [SerializeField] private float pingPongSpeed;
    private TextMeshProUGUI text;

    void Awake() {
        text = GetComponent<TextMeshProUGUI>();
        text.alpha = 0;
    }

    void Update() {
        text.alpha = Mathf.PingPong(Time.time, 1 / pingPongSpeed);
    }
}
