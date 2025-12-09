using UnityEngine;

[CreateAssetMenu(fileName = "OverlapDataSO", menuName = "Scriptable Objects/OverlapDataSO")]
public class OverlapDataSO : ScriptableObject
{
    [field: SerializeField] public float size;
    [field: SerializeField] public LayerMask whatIsTarget { get; private set; }
}
