using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LobbyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

    private Button button;

    void Awake() => button = GetComponent<Button>();

    public void OnPointerEnter(PointerEventData data) {
        if (!button.interactable) return;
        transform.DOScale(1.1f, 0.25f);
    }
    public void OnPointerClick(PointerEventData data) {
        if (!button.interactable) return;
        transform.DOScale(1.2f, 0.25f);
    }
    public void OnPointerExit(PointerEventData data) {
        if (!button.interactable) return;
        transform.DOScale(1, 0.25f);
    }
}