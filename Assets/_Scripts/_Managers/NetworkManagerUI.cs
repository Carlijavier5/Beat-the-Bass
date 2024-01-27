using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour {
    [SerializeField] private Button hostBtn;

    [SerializeField] private TMPro.TMP_InputField ipField;
    [SerializeField] private Button clientBtn;

    void Awake() {
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipField.text;
            NetworkManager.Singleton.StartClient();
            Debug.LogWarning(ipField.text);
        });
    }


}
