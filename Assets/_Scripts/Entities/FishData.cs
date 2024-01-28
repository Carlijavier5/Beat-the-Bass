using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity Data/Fish Entity")]
public class FishData : EntityData
{
    public float moveInterval = 1f;
    public float flopMagnitude = 5f;
    public float flopTime = 10f;
}
