using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenu : MonoBehaviour {

    [SerializeField] private float pullUpTime;
    [SerializeField] private RectTransform anchor;
    private RectTransform rectTransform;

    void Start() {
        GameManager.Instance.Input.InputMap.MenuInput.StartGame.performed += GameManager_StartGame;
        rectTransform = GetComponent<RectTransform>();
    }

    private void GameManager_StartGame(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        StartCoroutine(PullUp());
    }

    private IEnumerator PullUp() {
        anchor.DOAnchorPos(anchor.anchoredPosition + new Vector2(0, rectTransform.sizeDelta.y), pullUpTime).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(pullUpTime * 1.1f);
        Destroy(gameObject);
    }

    void OnDisable() {
        GameManager.Instance.Input.InputMap.MenuInput.StartGame.performed -= GameManager_StartGame;
    }
}
