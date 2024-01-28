using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SessionTracker : NetworkBehaviour {
    
    [SerializeField] private float sessionDuration;
    public float CurrDuration { get; private set; }

    public static SessionTracker Instance { get; private set; }
    public NetworkVariable<int> Score = new NetworkVariable<int>(0);
    bool sessionOn;

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update() {
        if (sessionOn) {
            CurrDuration -= Time.deltaTime;
            if (CurrDuration <= 0) ;
        }
    }

    [ServerRpc]
    public void StartSessionServerRpc() {
        CurrDuration = sessionDuration;
        sessionOn = true;
    }

    [ServerRpc]
    public void IncreasePointsServerRpc(int value) {
        Score.Value += value;
    }
}

public class SessionScore : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI text;

    void Update() {
        text.text = SessionTracker.Instance.Score.Value.ToString();
    }
}

public class SessionTimer : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI text;

    void Update() {
        int mins = (int) SessionTracker.Instance.CurrDuration / 60;
        int seconds = ((int) SessionTracker.Instance.CurrDuration - mins);
        text.text = (mins > 9 ? mins : "0" + mins.ToString()) + ":" + (seconds > 9 ? seconds : "0" + seconds);
    }
}