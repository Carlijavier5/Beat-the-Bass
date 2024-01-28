using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NetworkManagerUI : MonoBehaviour {
    [SerializeField] private Button hostBtn;
    [SerializeField] private TMPro.TMP_InputField ipField;
    [SerializeField] private Button clientBtn;
    [SerializeField] private Button startGame;
    [SerializeField] private SessionManager seshManager;

    void Awake() {
        hostBtn.transform.DOScale(0, 0);
        ipField.transform.DOScale(0, 0);
        clientBtn.transform.DOScale(0, 0);
        startGame.transform.DOScale(0, 0);
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipField.text;
            NetworkManager.Singleton.StartClient();
        });
        startGame.onClick.AddListener(() => {
            seshManager.StartGameClientRpc();
        });
        seshManager.OnLobbyToggle += ToggleLobby;
    }

    private void ToggleLobby(bool toggle) => StartCoroutine(_ToggleLobby(toggle));
        
    private IEnumerator _ToggleLobby(bool toggle) {
        Ease ease = toggle ? Ease.OutBounce : Ease.OutQuad;
        float duration = toggle ? 0.75f : 0.35f;
        clientBtn.transform.DOScale(toggle ? 1 : 0, duration).SetEase(ease);
        ipField.transform.DOScale(toggle ? 1 : 0, duration).SetEase(ease);
        yield return new WaitForSeconds(0.2f);
        hostBtn.transform.DOScale(toggle ? 1 : 0, duration).SetEase(ease);
        yield return new WaitForSeconds(0.2f);
        startGame.transform.DOScale(toggle ? 1 : 0, duration).SetEase(ease);
    }
}
