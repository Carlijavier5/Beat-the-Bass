using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SessionManager : NetworkBehaviour {

    [SerializeField] private PlayerSpawn[] spawns;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera boatCam;
    [SerializeField] private GameObject docks;
    bool await;

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
        while (await) yield return null;
        boatCam.Priority = 15;
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