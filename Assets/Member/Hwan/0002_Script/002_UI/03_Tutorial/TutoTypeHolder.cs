using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TutoTypeHolder : MonoBehaviour
{
    [field: SerializeField] public int TutoNumber { get; private set; }
}