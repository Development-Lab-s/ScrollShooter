using Member.JYG._Code;
using System.Collections;
using UnityEngine;

public class PhaseOneHolder : TutoTypeHolder
{
    private Transform playerTrn;
    private bool doMove;

    private void Awake()
    {
        playerTrn = GameManager.Instance.Player.transform;
        doMove = true;
    }
    private void Update()
    {
        if (doMove == false) return;
        transform.position = new Vector3(playerTrn.transform.position.x, transform.position.y,  0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doMove == true && collision.TryGetComponent<TutorialManager>(out TutorialManager tutorialManager))
        {
            StartCoroutine(WaitForStopFollow());
        }
    }

    private IEnumerator WaitForStopFollow()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        doMove = false;
    }
}
