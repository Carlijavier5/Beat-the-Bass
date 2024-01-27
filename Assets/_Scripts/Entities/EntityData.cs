using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : ScriptableObject
{
    [Header("Entity Details")]
    [SerializeField] private string displayName = "";

    [SerializeField] private float weight;
}
