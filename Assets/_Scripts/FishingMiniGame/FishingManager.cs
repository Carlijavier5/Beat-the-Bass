using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    private int level = 1;
    [SerializeField] private float movementSpeed = 0f;
    private RectTransform parentRectTransform;
    private float halfParentHeight;

    [SerializeField] private Image container;
    [SerializeField] private Image bobber;
    [SerializeField] private Image greenArea;

    private int rectSize;
    private float bottomCord;
    private float topCord;

    private SpawnFish spawnFishScript;

    void Start()
    {
        spawnFishScript = GetComponent<SpawnFish>();

        GameManager.Instance.Input.OnBeat += OnMouseClick;
        parentRectTransform = container.GetComponentInParent<RectTransform>();
        halfParentHeight = parentRectTransform.rect.height / 2.0f;
        rectSize = 0;

        container.gameObject.SetActive(false);
    }

    public void StartFishing() {
        level = 1;
        container.gameObject.SetActive(true);
        SpawnGreenArea();
    }

    private void OnMouseClick() {
        Debug.Log("clicked");
        if (bobber.rectTransform.localPosition.y >= bottomCord && bobber.rectTransform.localPosition.y <= topCord) {
            level++;
            SpawnGreenArea();
        } else {
            level--;
            if (level < 1) { level = 1; }
            SpawnGreenArea();
        }

        if (level > 3) {
            Debug.Log("Game Won");
            // spawn fish n shit
            spawnFishScript.SpawnAFish();
            container.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        MoveBobber();
    }

    private void MoveBobber() {
        float newY = Mathf.PingPong(Time.time * movementSpeed, halfParentHeight * 2) - halfParentHeight;

        RectTransform rectTransform = bobber.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, newY, rectTransform.localPosition.z);
    }

    private void SpawnGreenArea() {
        Debug.Log("spawned green");
        switch (level) {
            case 1:
                rectSize = 70;
                break;
            case 2:
                rectSize = 50;
                break;
            case 3:
                rectSize = 30;
                break;
            default:
                Debug.Log("uh oh");
                break;
        }
        greenArea.rectTransform.sizeDelta = new Vector2(greenArea.rectTransform.sizeDelta.x, rectSize);

        float randomY = Random.Range(-parentRectTransform.rect.height / 2f + rectSize / 2f, parentRectTransform.rect.height / 2f - rectSize / 2f);
        greenArea.rectTransform.localPosition = new Vector2(0, randomY);

        bottomCord = randomY - rectSize / 2;
        topCord = randomY + rectSize / 2;

    }
}
