using Unity.Netcode;
using UnityEngine;

public class PlayerSpawn : NetworkBehaviour {

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LobbySelector selector;
    //[SerializeField] private ColorPanel colorPanel;
    [SerializeField] private Transform shipSpawn;

    public NetworkObject PlayerNO { get; private set; }

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

    public void InitializePlayer() {
        if (PlayerNO == null) return;
        PlayerNO.transform.position = shipSpawn.position;
        PlayerNO.transform.SetParent(shipSpawn.GetComponentInParent<Boat>().transform);
        GameManager.Instance.Input.ToggleMovement(true);
        GameManager.Instance.Input.ToggleInteraction(true);
        Destroy(gameObject);
    }

    public override void OnGainedOwnership() {
        if (!IsOwner) return;
        selector.gameObject.SetActive(true);
    }
    public override void OnLostOwnership() {
        selector.gameObject.SetActive(false);
    }

    public void Dispose() {
        Destroy(PlayerNO);
        NetworkObject.RemoveOwnership();
    }
}