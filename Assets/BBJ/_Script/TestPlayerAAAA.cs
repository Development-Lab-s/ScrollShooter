using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class TestPlayerAAAA : MonoBehaviour
{
    
}
[CreateAssetMenu(fileName = "MoveDataSO", menuName = "SO/MoveDataSO")]
public class MoveDataSO : ScriptableObject
{
    [Range(0, 100)]
    public float acceleration, deacceleration;

    [Range(0.1f, 10)]
    public float maxSpeed;

    [Range(10f, 20)]
    public float dashSpeed;
}
