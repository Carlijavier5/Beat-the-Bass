using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager {

    [Header("Transition")]
    [SerializeField] private float transitionRate;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;

    public event System.Action OnFadeEnd;

    private void InitTransition() {
        canvasGroup.alpha = 1;
        Fade(0);
    }

    public void Fade(float goTo) => Fade(goTo, Color.black);
    public void Fade(float goTo, Color? color = null) {
        Color colorRes = color ?? Color.black;
        StopAllCoroutines();
        StartCoroutine(_Fade(goTo, colorRes));
    }

    IEnumerator _Fade(float goTo, Color color) {
        image.color = color;
        while (canvasGroup.alpha != goTo) {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, goTo, transitionRate * Mathf.Min(0.1f, Time.unscaledDeltaTime));
            yield return null;
        } yield return new WaitForSeconds(0.5f);
        canvasGroup.interactable = goTo > 0 ? true : false;
        canvasGroup.blocksRaycasts = goTo > 0 ? true : false;
        OnFadeEnd?.Invoke();
    }
}