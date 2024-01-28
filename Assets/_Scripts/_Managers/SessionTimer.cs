using UnityEngine;

public class SessionTimer : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI text;

    void Update() {
        int mins = (int) SessionTracker.Instance.CurrDuration / 60;
        int seconds = ((int) SessionTracker.Instance.CurrDuration - mins * 60);
        text.text = (mins > 9 ? mins : "0" + mins.ToString()) + ":" + (seconds > 9 ? seconds : "0" + seconds);
    }
}