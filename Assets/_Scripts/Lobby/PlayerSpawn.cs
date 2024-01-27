using Unity.Netcode;
using UnityEngine;

public class PlayerSpawn : NetworkBehaviour {
    public event System.Action<bool> OnSpawnToggle;

    [SerializeField] private GameObject playerPrefab;
    //[SerializeField] private ColorPanel colorPanel;
    public NetworkObject PlayerNO { get; private set; }
    private bool isHost;

    public override void OnNetworkSpawn() {
        NetworkObject.CheckObjectVisibility = (clientID) => true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void AdmitServerRPC(ulong clientID) {
        GameObject playerGO = Instantiate(playerPrefab, transform.position, transform.rotation);
        PlayerNO = playerGO.GetComponent<NetworkObject>();
        PlayerNO.Spawn(true);
        PlayerNO.ChangeOwnership(clientID);
    }

    public void Init() => OnSpawnToggle?.Invoke(true);

    public void Dispose() {
        Destroy(PlayerNO);
        NetworkObject.RemoveOwnership();
        OnSpawnToggle?.Invoke(false);
    }
}