using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public partial class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance => instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(gameObject);
        Input = new(new PlayerInput());
        InitTransition();
    }
}
