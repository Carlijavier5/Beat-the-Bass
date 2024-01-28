using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity Data/Penguin Entity")]
public class PenguinData : EntityData
{
    public float moveInterval = 1f;
    public float flopMagnitude = 5f;

    public float spawnChance = 0f;
}
