using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity Data/Base Entity")]
public class EntityData : ScriptableObject
{
    [Header("Entity Attributes")]
    public string displayName = "";

    [Range(0f, 3f)] public float weight;
}
