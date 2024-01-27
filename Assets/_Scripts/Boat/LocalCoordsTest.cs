using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCoordsTest : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("LogEverySecond", 0f, 1f);
    }

    private void LogEverySecond() {
        // Assuming this script is attached to the parent object

        // Get the child object's local coordinates
        Transform childTransform = transform.GetChild(0); // Adjust the index as needed
        Vector3 localCoordinates = childTransform.localPosition;

        // Print the local coordinates
        Debug.Log("Local Coordinates of Child: " + localCoordinates);
    }
}
