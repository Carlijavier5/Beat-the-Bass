using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity/Data")]
public class EntityData : ScriptableObject {
    [Header("Entity Attributes")]
    public string displayName = "";
    [Range(0f, 10f)] public float weight;
}
