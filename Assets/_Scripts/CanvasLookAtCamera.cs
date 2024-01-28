using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour {

    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
    }
}