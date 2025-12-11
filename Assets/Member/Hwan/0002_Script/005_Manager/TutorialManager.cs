using Member.JYG._Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YGPacks;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private int LastPhase; 
    public event Action<TutorialInfoSO> OnPlayerNearObstacle;
    public event Action OnSkipPhaze;

    private int currentPhase = 0;
    private InteractiveType[] currentNeedInputs;
    private List<TutoTypeHolder> usedBlocks = new();

    private bool getInput = false;

    protected override void Awake()
    {
        base.Awake();
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        if (GameManager.Instance.StageSO.StageNumber == 0)
        {
            col.enabled = true;
            StartTuto();
        }
    }

    private void Update()
    {
        transform.position = new Vector3(0, GameManager.Instance.Player.transform.position.y,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TutoTypeHolder block))
        {
            usedBlocks.ForEach((holder) =>
            {
                if (holder == block) return;
            });

            usedBlocks.Add(block);
            PlayTuto(block.tutoSO);
        }
    }

    private void PlayTuto(TutorialInfoSO tutoInfoSO)
    {
        TutorialInfoSO currentTutoInfo = tutoInfoSO;

        getInput = true;
        OnPlayerNearObstacle?.Invoke(currentTutoInfo);
        currentNeedInputs = currentTutoInfo.NeedInput; 
        foreach (InteractiveType type in currentNeedInputs)
        {
            InputControlManager.Instance.ChangePlayerInputActiveType(type, true);
        }
        currentPhase++;
    }

    public void GetInput(InteractiveType type)
    {
        if (getInput == false || currentNeedInputs.Contains(type) == false) return;

        getInput = false;
        OnSkipPhaze?.Invoke();
        if (currentPhase == LastPhase) EndTuto();
    }

    private void StartTuto()
    {
        InputControlManager.Instance.ChangeAllPlayerActiveType(false);
        InputControlManager.Instance.ChangePlayerInputActiveType(InteractiveType.Middle, true);
    }

    private void EndTuto()
    {
        InputControlManager.Instance.ChangeAllPlayerActiveType(true);
    }
}
