using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity Data/Player Entity")]
public class PlayerData : EntityData
{
    [Header("Player Attributes")]
    
    [Range(0f, 100f)] public float moveSpeed = 5f;
}
