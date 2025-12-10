using Member.JYG._Code;
using Member.JYG.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YGPacks;

[DefaultExecutionOrder(-1)]
public class InputControlManager : Singleton<InputControlManager>
{
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private PlayerInputSO uiInputSO;
    private GoButtonUI goButtonUI;

    protected override void Awake()
    {
        UIController uiContoroller = GetComponent<UIController>();
        uiContoroller.OnUIChange += OnUIChange;

        goButtonUI = GetComponentInChildren<GoButtonUI>();
    }

    private void OnUIChange(List<UIType> uiList)
    {
        goButtonUI.ButtonMove(uiList.Count != 0);
        if (uiList.Count == 0) return;
        goButtonUI.IconChange(uiList.Last());
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

    public void ChangeAllPlayerActiveType(bool active)
    {
        foreach (InteractiveType type in Enum.GetValues(typeof(InteractiveType)))
        {
            ChangePlayerInputActiveType(type, active);
        }
    }
}
