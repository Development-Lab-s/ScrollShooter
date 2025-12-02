using Member.JYG._Code;
using UnityEngine;

public class TitlePlayerRenderer : MonoBehaviour
{
    private TitleMovement _player;
    [field: SerializeField] public float Offset { get; private set; }
    [field: SerializeField] public float MaxDegree { get; private set; }

    private void Awake()
    {
        _player = GetComponent<TitleMovement>();
    }

    private Vector3 _currentPosition;
    private void Update()
    {
        SetRotation(gameObject, _player.XVelocity); //이동속도를 받아와서 나를 돌린다
    }

    private void SetRotation(GameObject target, float velocity)
    {
        float zValue = Mathf.Lerp(-MaxDegree, MaxDegree, 0.5f + velocity / _player.MaxSpeed * 0.5f) * -1;
        zValue = Mathf.MoveTowardsAngle(transform.eulerAngles.z, zValue, Time.deltaTime * 450f);
        transform.rotation = Quaternion.Euler(0, 0, zValue);
    }
}

