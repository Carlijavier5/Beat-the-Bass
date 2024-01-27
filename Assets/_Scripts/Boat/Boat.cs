using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
    [SerializeField] private float dragInstance = 0f;
    public float getDragInstance() { return dragInstance; }

    private float rotationSpeed = 100f;
    public Vector3 currentRotation;
    private Vector3 originalEulers;

    private float clampRotationAroundZ = 30f;
    private float clampRotationAroundX = 30f;

    void Start() {
        originalEulers = transform.eulerAngles;
    }

    void FixedUpdate() {
        RotateBoat();
    }

    // based on child coords rotate the boat
    private void RotateBoat() {
        // check if there are children
        if (transform.childCount > 0) {
            // calc the avg X-Z position of all children
            float totalPositionX = 0f;
            float totalPositionZ = 0f;
            float testWeight = 1f;
            float individualWeightX = 0f;
            float individualWeightZ = 0f;
            for (int i = 0; i < transform.childCount; ++i) {
                Vector3 childPosition = transform.GetChild(i).localPosition; 
                //calc individual weights (how much to rotate)
                individualWeightX = childPosition.x * testWeight;
                individualWeightZ = childPosition.z * testWeight;

                totalPositionX += individualWeightX;
                totalPositionZ += individualWeightZ;
            }
            currentRotation = currentRotation + new Vector3(totalPositionZ, 0f, -totalPositionX) * Time.deltaTime * rotationSpeed;
            currentRotation = new Vector3(currentRotation.x % 360, currentRotation.y % 360, currentRotation.z % 360);

            //overall rotation
            currentRotation.z = Mathf.Clamp(currentRotation.z, -clampRotationAroundZ, clampRotationAroundZ);    // rotation around the Z is based on x coords and vice versa
            currentRotation.x = Mathf.Clamp(currentRotation.x, -clampRotationAroundZ, clampRotationAroundZ);

            // if the tilt value is within a certain range then don't keep rotating the boat
            float currentZTilt = CalculateBoatTilt(currentRotation.z);
            Debug.Log(currentZTilt);
            if (currentZTilt >= -0.1f && currentZTilt <= 0.1f) {
                // keep boat steady
                Debug.Log("steady");
                transform.eulerAngles = originalEulers;
                currentRotation = new Vector3(0f, 0f, 0f);
            }
            else {
                transform.eulerAngles = currentRotation;
            }
        }
    }

    private float CalculateBoatTilt(float currentRotAxis) {
        float normalizedTilt = currentRotAxis / clampRotationAroundZ;
        return Mathf.Clamp(normalizedTilt, -1f, 1f);
    }
    // lerp animation curve

    public Vector3 GetCurrentEulers() {
        return transform.eulerAngles;
    }
}
