using UnityEngine;

public class SessionScore : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI text;

    void Update() {
        text.text = SessionTracker.Instance.Score.Value.ToString();
    }
}
