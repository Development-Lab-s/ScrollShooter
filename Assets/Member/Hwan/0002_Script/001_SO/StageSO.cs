using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Objects/StageSO")]
public class StageSO : ScriptableObject
{
    [field: SerializeField] public float MapDistance { get; private set; }
    [field: SerializeField] public string StageBGM { get; private set; }
}
