using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour {
    [SerializeField] private float dragInstance = 0f;
    public float getDragInstance() { return dragInstance; }

    private float rotationSpeed = 100f;
    public Vector3 targetRotation;
    private Vector3 originalEulers;

    private float clampRotationAroundZ = 30f;
    private float clampRotationAroundX = 30f;

    private List<Entity> objectsOnBoat = new();

    public AnimationCurve moveCurve;
    private float animationTimePosition;

    void Start() {
        originalEulers = transform.eulerAngles;
    }

    void FixedUpdate() {
        RotateBoat();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Entity>() != null) {
            objectsOnBoat.Add(other.gameObject.GetComponent<Entity>());
        }
        Debug.Log("added object");
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Entity>() != null) {
            objectsOnBoat.Remove(other.gameObject.GetComponent<Entity>());
        }
        Debug.Log("object removed");
    }

    // based on child coords rotate the boat
    private void RotateBoat() {
        // check if there are children
        if (objectsOnBoat.Count > 0) {
            // calc the avg X-Z position of all children
            float totalPositionX = 0f;
            float totalPositionZ = 0f;
            float individualWeightX = 0f;
            float individualWeightZ = 0f;
            for (int i = 0; i < objectsOnBoat.Count; ++i) {
                Vector3 objectPosition = objectsOnBoat[i].transform.localPosition; 
                //calc individual weights (how much to rotate)
                individualWeightX = objectPosition.x * objectsOnBoat[i].data.weight;
                //individualWeightZ = objectPosition.z * objectsOnBoat[i].data.weight;

                totalPositionX += individualWeightX;
                //totalPositionZ += individualWeightZ;
            }
            targetRotation = targetRotation + new Vector3(totalPositionZ, 0f, -totalPositionX) * Time.deltaTime * rotationSpeed;    //
            targetRotation = new Vector3(targetRotation.x % 360, targetRotation.y % 360, targetRotation.z % 360);

            //overall rotation
            targetRotation.z = Mathf.Clamp(targetRotation.z, -clampRotationAroundZ, clampRotationAroundZ);    // rotation around the Z is based on x coords and vice versa
           // targetRotation.x = Mathf.Clamp(targetRotation.x, -clampRotationAroundZ, clampRotationAroundZ);

            // if the tilt value is within a certain range then don't keep rotating the boat
            float currentZTilt = CalculateBoatTilt(targetRotation.z);
            if (currentZTilt >= -0.1f && currentZTilt <= 0.1f) {
                // keep boat steady
                Debug.Log("steady");
                transform.eulerAngles = originalEulers; //
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, originalEulers, Time.deltaTime);
                //targetRotation = new Vector3(0f, 0f, 0f);   // movetowards transform
            }
            else {

                //transform.eulerAngles = targetRotation;
                //RotateBetweenPoints(transform.eulerAngles, targetRotation);
            }
        }
    }

    private void RotateBetweenPoints(Vector3 startRot, Vector3 targetRot) {
        if (startRot != targetRot) {
            animationTimePosition += Time.deltaTime;
            float angle = Mathf.LerpAngle(startRot.x, targetRot.x, moveCurve.Evaluate(animationTimePosition));
            Debug.Log(angle);
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        } else {
            animationTimePosition = 0f;
        }
    }

    private float CalculateBoatTilt(float currentRotAxis) {
        float normalizedTilt = currentRotAxis / clampRotationAroundZ;
        return Mathf.Clamp(normalizedTilt, -1f, 1f);
    }

    public Vector3 GetCurrentEulers() {
        return transform.eulerAngles;
    }
}
