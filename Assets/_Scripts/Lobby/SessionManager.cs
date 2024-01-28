using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SessionManager : NetworkBehaviour {
    public event System.Action<bool> OnLobbyToggle;

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMPro.TextMeshProUGUI logo;

    [SerializeField] private PlayerSpawn[] spawns;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera lobbyCam;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera boatCam;
    [SerializeField] private GameObject docks;
    bool await;

    void Awake() {
        startButton.onClick.AddListener(Go2Lobby);
    }

    private void Go2Lobby() => StartCoroutine(_Go2Lobby());
    private IEnumerator _Go2Lobby() {
        lobbyCam.Priority = 20;
        yield return new WaitForSeconds(0.5f);
        startButton.transform.DOMove(new Vector2(startButton.transform.position.x, -1000), 2);
        startButton.transform.DORotate(new Vector3(0, 0, 140), 3);
        yield return new WaitForSeconds(0.5f);
        exitButton.transform.DOMove(new Vector2(startButton.transform.position.x, -1000), 2);
        exitButton.transform.DORotate(new Vector3(0, 0, -140), 3);
        yield return new WaitForSeconds(1.5f);
        logo.DOFade(0, 0.85f);
        yield return new WaitForSeconds(0.5f);
        OnLobbyToggle?.Invoke(true);
    }

    public override void OnNetworkSpawn() {
        if (!IsHost) return;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
    }

    private void NetworkManager_OnClientConnectedCallback(ulong clientID) {
        for (int i = 0; i < spawns.Length; i++) {
            if (spawns[i].PlayerNO == null) {
                spawns[i].NetworkObject.ChangeOwnership(clientID);
                spawns[i].AdmitServerRPC(clientID);
                break;
            }
        }
    }

    private void Singleton_OnClientDisconnectCallback(ulong clientID) {
        for (int i = 0; i < spawns.Length; i++) {
            if (spawns[i].OwnerClientId == clientID) {
                spawns[i].NetworkObject.ChangeOwnership(OwnerClientId);
                spawns[i].Dispose();
            }
        }
    }

    [ClientRpc]
    public void StartGameClientRpc() => StartCoroutine(_StartGame());

    private IEnumerator _StartGame() {
        await = true;
        GameManager.Instance.OnFadeEnd += () => await = false;
        GameManager.Instance.Fade(1);
        OnLobbyToggle?.Invoke(false);
        while (await) yield return null;
        boatCam.Priority = 50;
        SetupPlayers();
        Destroy(docks);
        GameManager.Instance.Fade(0);
    }

    private void SetupPlayers() {
        foreach (PlayerSpawn spawn in spawns) {
            spawn.InitializePlayer();
        }
    }
}