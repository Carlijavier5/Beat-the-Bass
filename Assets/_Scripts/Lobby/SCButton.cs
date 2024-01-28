using Unity.Netcode;
using UnityEngine.UI;

public class SCButton : NetworkBehaviour {

    private Button button;

    void Awake() {
        button = GetComponent<Button>();
    }

    void Update() {
        button.interactable = !NetworkManager.Singleton.IsConnectedClient;
    }
}