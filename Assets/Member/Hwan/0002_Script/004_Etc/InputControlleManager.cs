using Member.JYG._Code;
using Member.JYG.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YGPacks;

public class InputControlleManager : Singleton<InputControlleManager>
{
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private PlayerInputSO uiInputSO;
    private GoButtonUI goButtonUI;

    protected override void Awake()
    {
        uiInputSO.SetInputActive(true);
        playerInputSO.SetInputActive(true);

        UIController uiContoroller = GetComponent<UIController>();
        uiContoroller.OnUIChange += OnUIChange;

        goButtonUI = GetComponentInChildren<GoButtonUI>();
    }

    private void OnUIChange(List<UIType> uiList)
    {
        GameManager.Instance.SetCursorActive(uiList.Count == 0);
        if (uiList.Count == 0) return;
        goButtonUI.ButtonMove(uiList.Last(), true);
    }

    public void ChangeUIInputActive(bool active)
    {
        uiInputSO.SetInputActive(active);
    }

    public void ChangePlayerInputActive(bool active)
    {
        playerInputSO.SetInputActive(active);
    }

    public void ChangePlayerInputActiveType(InteractiveType type, bool active)
    {
        playerInputSO.SetInputTypeActive(type, active);
    }

    public void ChangeUIActiveType(InteractiveType type, bool active)
    {
        uiInputSO.SetInputTypeActive(type, active);
    }

    public void ChangeAllPlayerActiveType(bool active)
    {
        foreach (InteractiveType type in Enum.GetValues(typeof(InteractiveType)))
        {
            playerInputSO.SetInputTypeActive(type, active);
        }
    }

    public void ChangeAllUIActiveType(bool active)
    {
        foreach (InteractiveType type in Enum.GetValues(typeof(InteractiveType)))
        {
            uiInputSO.SetInputTypeActive(type, active);
        }
    }
}
