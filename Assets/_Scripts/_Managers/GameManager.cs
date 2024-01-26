using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public partial class GameManager : NetworkBehaviour {

    private static GameManager instance;
    public static GameManager Instance => instance;

    public override void OnNetworkSpawn() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(gameObject);
    }
}
