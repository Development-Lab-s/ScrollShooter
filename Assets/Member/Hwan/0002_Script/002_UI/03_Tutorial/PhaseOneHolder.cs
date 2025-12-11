using Member.JYG._Code;
using UnityEngine;

public class PhaseOneHolder : TutoTypeHolder
{
    private Transform playerTrn;

    private void Awake()
    {
        playerTrn = GameManager.Instance.Player.transform;
    }
    private void Update()
    {
        transform.position = new Vector3(0, playerTrn.transform.position.x, 0);
    }
}
