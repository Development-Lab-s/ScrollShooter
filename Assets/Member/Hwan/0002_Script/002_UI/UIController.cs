using Member.JYG._Code;
using Member.JYG.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private List<UIType> openUIList = new();
    private GoButtonUI goButtonUI;
    private Dictionary<UIType, IUI> uiDictionary = new();
    [field: SerializeField] private PlayerInputSO inputSO;
    public event Action<List<UIType>> OnUIChange;

    private void Awake()
    {
        uiDictionary = UIManager.Instance.UIDictionary;

        goButtonUI = GetComponentInChildren<GoButtonUI>();
        goButtonUI.Initialize(GetInputForward, GetInputBack);

        foreach (IUI ui in uiDictionary.Values)
        {
            ui.OnOpen += AddInputUI;
            ui.OnClose += RemoveInputUI;
            ui.Initialize();
        }

        inputSO.OnBrakePressed += GetInputForward;
        inputSO.OnDashPressed += GetInputBack;
        inputSO.OnLeftClicked += GetInputLeft;
        inputSO.OnRightClicked += GetInputRight;
        inputSO.OnWheelBtnClicked += GetInputMiddle;
        inputSO.OnWheeling += GetInputWheel;
    }

    private void OnDestroy()
    {
        foreach (IUI ui in uiDictionary.Values)
        {
            ui.OnOpen -= AddInputUI;
            ui.OnClose -= RemoveInputUI;
        }

        inputSO.OnBrakePressed -= GetInputForward;
        inputSO.OnDashPressed -= GetInputBack;
        inputSO.OnLeftClicked -= GetInputLeft;
        inputSO.OnRightClicked -= GetInputRight;
        inputSO.OnWheelBtnClicked -= GetInputMiddle;
        inputSO.OnWheeling -= GetInputWheel;
    }

    private void GetInputWheel() => UIInteractive(InteractiveType.Scroll);
    private void GetInputMiddle() => UIInteractive(InteractiveType.Middle);
    private void GetInputBack() => UIInteractive(InteractiveType.Back);
    private void GetInputForward() => UIInteractive(InteractiveType.Forward);
    private void GetInputLeft() => UIInteractive(InteractiveType.Left);
    private void GetInputRight() => UIInteractive(InteractiveType.Right);
    private void UIInteractive(InteractiveType interactiveType)
    {
        foreach (IUI ui in uiDictionary.Values)
        {
            if (ui.OpenInput != interactiveType) continue;
            DoMove(interactiveType, ui);
            return;
        }

        if (openUIList.Count == 0) return;
        IUI inputUI = uiDictionary[openUIList.Last()];
        DoMove(interactiveType, inputUI);
    }

    private void AddInputUI(UIType type)
    {
        openUIList.Add(type);
        OnUIChange?.Invoke(openUIList);
    }

    private void RemoveInputUI(UIType type)
    {
        openUIList.Remove(type);
        OnUIChange?.Invoke(openUIList);
    }

    private void DoMove(InteractiveType interactiveType, IUI inputUI)
    {
        switch (interactiveType)
        {
            case InteractiveType.Forward: inputUI.ForwardMove(); break;
            case InteractiveType.Back: inputUI.BackMove(); break;
            case InteractiveType.Right: inputUI.RightClick(inputSO.rigthPerformed); break;
            case InteractiveType.Left: inputUI.LeftClick(); break;
            case InteractiveType.Middle: inputUI.MiddleMove(inputSO.wheelPerformed); break;
            case InteractiveType.Scroll: inputUI.ScrollMove(-inputSO.XMoveDir); break;
        }
    }
}