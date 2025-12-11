using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TutoTypeHolder : MonoBehaviour
{
    [field: SerializeField] public TutorialInfoSO tutoSO { get; private set; }
}