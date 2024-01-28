using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;

    private FishEntity fish;

    private int remainingTime;

    void Start() {
        fish = this.GetComponent<FishEntity>();
        remainingTime = fish.getFlopTime();

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer() {
        while (remainingTime >= 0) {
            uiText.text = remainingTime.ToString();
            uiFill.fillAmount = Mathf.InverseLerp(0, fish.getFlopTime(), remainingTime);
            remainingTime--;
            yield return new WaitForSeconds(1f);
        }

        fish.StopFlop();
        uiFill.fillAmount = 1;
        uiFill.color = Color.gray;
        uiText.text = "";
    }
}
