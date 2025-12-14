using Member.JYG._Code;
using Unity.Cinemachine;
using UnityEngine;

public class StopFollowPlayer : MonoBehaviour
{
    private CinemachineCamera cam;
    private CinemachinePositionComposer positionComposer;
    private float stopYValue;

    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
        positionComposer = GetComponent<CinemachinePositionComposer>();

        stopYValue = GameManager.Instance.StageSO.MapDistance - cam.Lens.OrthographicSize - 2;
    }

    private void Update()
    {
        if (transform.position.y >= stopYValue)
        {
            positionComposer.enabled = false;
        }
    }
}
