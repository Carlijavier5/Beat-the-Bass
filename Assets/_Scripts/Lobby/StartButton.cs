using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.UI;

public class StartButton : NetworkBehaviour {

    private Button button;

    void Awake() {
        button = GetComponent<Button>();
    }

    void Update() {
        button.interactable = NetworkManager.Singleton.IsHost
                              && NetworkManager.Singleton.ConnectedClients.Count > 0;
    }
}
