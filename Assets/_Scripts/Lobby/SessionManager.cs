using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SessionManager : NetworkBehaviour {

    [SerializeField] private PlayerSpawn[] spawns;

    public override void OnNetworkSpawn() {
        if (!IsHost) {
            Destroy(gameObject);
            return;
        }
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
}