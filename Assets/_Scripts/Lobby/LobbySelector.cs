using UnityEngine;

public class LobbySelector : MonoBehaviour {

    [SerializeField] private float amplitude;
    private Vector3 startPos;

    void Awake() {
        startPos = transform.position;
    }

    void Update() {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time) * amplitude, startPos.z);
    }
}