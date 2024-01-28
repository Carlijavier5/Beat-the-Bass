using UnityEngine;

[CreateAssetMenu(menuName = "Entity/GlobalData")]
public class GlobalEntityData : ScriptableObject {
    [SerializeField] private float drag;
    public float Drag => drag;
}