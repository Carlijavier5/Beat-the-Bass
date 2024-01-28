using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using DG.Tweening;

public class SessionTracker : NetworkBehaviour {
    
    [SerializeField] private float sessionDuration;
    [SerializeField] private Transform[] sessionTrackers;
    [SerializeField] private Transform[] finalButtons;
    [SerializeField] private Canvas finalCanvas;
    public float CurrDuration { get; private set; }

    public static SessionTracker Instance { get; private set; }
    public NetworkVariable<int> Score = new NetworkVariable<int>(0);
    bool sessionOn;

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        foreach (Transform tracker in sessionTrackers) tracker.DOScale(0, 0);
        foreach (Transform finalBtn in finalButtons) finalBtn.DOScale(0, 0);
    }

    void Update() {
        if (sessionOn) {
            CurrDuration -= Time.deltaTime;
            if (CurrDuration <= 0) {
                sessionOn = false;
                GameManager.Instance.Fade(0.5f);
                foreach (Transform finalBtn in finalButtons) finalBtn.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
                finalCanvas.sortingOrder = 5;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartSessionServerRpc() {
        CurrDuration = sessionDuration;
        sessionOn = true;
        foreach (Transform tracker in sessionTrackers) tracker.DOScale(1, 0.75f).SetEase(Ease.OutBounce);
    }

    [ServerRpc]
    public void IncreasePointsServerRpc(int value) {
        Score.Value += value;
        Debug.Log(Score.Value + " is score");
    }
}