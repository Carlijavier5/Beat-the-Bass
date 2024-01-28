using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour {

    [SerializeField] private Vector2 maxDistance;
    [SerializeField] private float angularStabilitySpeed;
    [SerializeField] private Vector2 rotationEndpoints;
    [SerializeField] private Vector3 stabilityPoint;

    private Vector3 angularSpeed;
   /// private float rotationSpeed = 100f;
    ///public Vector3 targetRotation;
    /// Vector3 originalEulers;

    //private float clampRotationAroundZ = 30f;
    //private float clampRotationAroundX = 30f;

    [SerializeField] private List<Entity> objectsOnBoat = new();

    public AnimationCurve moveCurve;
    //private float animationTimePosition;
    /*
    void Start() {
        originalEulers = transform.eulerAngles;
    }*/

    void FixedUpdate() {
        RotateBoat();
    }

    private void OnTriggerEnter(Collider other) {
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity != null) objectsOnBoat.Add(entity);
    }

    private void OnTriggerExit(Collider other) {
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity != null) objectsOnBoat.Remove(entity);
    }
    
    private void RotateBoat() {
        Vector2 weightedDist = Vector2.zero;
        foreach (Entity entity in objectsOnBoat) {
            weightedDist.x += (entity.transform.localPosition.z / maxDistance.y) * entity.Data.weight;
            weightedDist.y += (entity.transform.localPosition.x / maxDistance.x) * entity.Data.weight;
        } Vector2 rotationSpeed = ComputeTiltTolerance(weightedDist);
        rotationSpeed = new Vector2(Mathf.Abs(rotationSpeed.x) * Mathf.Sign(weightedDist.x), Mathf.Abs(rotationSpeed.y) * Mathf.Sign(weightedDist.y));
        stabilityPoint = new Vector3(Mathf.MoveTowards(transform.rotation.x, rotationEndpoints.x * Mathf.Sign(rotationSpeed.x), rotationSpeed.x),
                                     transform.eulerAngles.y,
                                     Mathf.MoveTowards(transform.rotation.z, rotationEndpoints.y * Mathf.Sign(rotationSpeed.y), rotationSpeed.y));
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, stabilityPoint, ref angularSpeed, 1);
    }

    private Vector2 ComputeTiltTolerance(Vector2 weightedDist) {
        return new Vector2(Mathf.Abs(weightedDist.x) * angularStabilitySpeed * Mathf.Max(0.1f, moveCurve.Evaluate(Mathf.Abs(stabilityPoint.x / rotationEndpoints.x))),
                           Mathf.Abs(weightedDist.y) * angularStabilitySpeed * Mathf.Max(0.1f, moveCurve.Evaluate(Mathf.Abs(stabilityPoint.z / rotationEndpoints.y))));
    }

    /*
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
                individualWeightX = objectPosition.x * objectsOnBoat[i].Data.weight;
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
    }*/
}
