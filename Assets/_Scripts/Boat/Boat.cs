using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float dragInstance = 0f;
    private float rotationSpeed = 100f;
    public Vector3 currentRotation;

    private float clampRotationZ = 30f;

    public float getDragInstance() { return dragInstance; }

    void Start() {

    }

    void Update() {
        RotateBoat();
    }

    void FixedUpdate() {
        // call chrys method
    }

    // based on child coords rotate the boat
    private void RotateBoat() { 
        // check if there are children
        if (transform.childCount > 0) {
            // calc the avg X-Z position of all children
            float totalPositionX = 0f;
            float totalPositionZ = 0f;
            float testWeight = 1.5f;
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
            currentRotation.z = Mathf.Clamp(currentRotation.z, -clampRotationZ, clampRotationZ);    // rotation around the Z is based on x coords and vice versa
            currentRotation.x = Mathf.Clamp(currentRotation.x, -clampRotationZ, clampRotationZ);
            transform.eulerAngles = currentRotation;
        }
    }
    // lerp animation curve
}
