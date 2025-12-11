using UnityEngine;

public class GizmoRenderer : MonoBehaviour
{
    [SerializeField] private StageSO stageSO;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float camRange = Camera.main.orthographicSize * 16 / 9;
        Gizmos.DrawLine(new Vector3(-camRange, 0), new Vector3(-camRange, stageSO.MapDistance));
        Gizmos.DrawLine(new Vector3(camRange, 0), new Vector3(camRange, stageSO.MapDistance));
    }
}