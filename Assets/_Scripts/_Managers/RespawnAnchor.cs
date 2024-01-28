using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAnchor : MonoBehaviour {

    private static RespawnAnchor instance;
    public static RespawnAnchor Instance => instance;
    public Transform spawnLoc;
}
