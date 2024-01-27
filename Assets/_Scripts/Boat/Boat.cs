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
            for (int i = 0; i < transform.childCount; ++i) {
                Vector3 childPosition = transform.GetChild(i).localPosition;
                float testWeight = 1.5f;
                //calc individual weights (how much to rotate)
                float individualWeight = childPosition.x * testWeight;

                totalPositionX += individualWeight;
            }
            currentRotation = currentRotation - new Vector3(0f, 0f, totalPositionX) * Time.deltaTime * rotationSpeed;
            currentRotation = new Vector3(currentRotation.x % 360, currentRotation.y % 360, currentRotation.z % 360);
            Debug.Log(currentRotation);

            //overall rotation
            currentRotation.z = Mathf.Clamp(currentRotation.z, -clampRotationZ, clampRotationZ);
            transform.eulerAngles = currentRotation;

            //PositionX /= transform.childCount;

            //// calc rotation angle based on the avg
            //float angle = Mathf.Atan2(averagePositionXZ.y, averagePositionXZ.x) * Mathf.Rad2Deg;
        }

        // transform .Euler angle rotation
        // set completely tilted/not completely tiled?
        // interpolate using vector 3 to rotate --> move towards and set Euler angle to that value
    }
}
