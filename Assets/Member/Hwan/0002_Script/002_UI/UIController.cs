using Member.JYG.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Dictionary<UIType, IUI> uiDictionary = new();
    public bool MouseEnable { get; set; } = false;
    [SerializeField] private PlayerInputSO inputSO;

    public List<UIType> InputList { get; private set; } = new();

    private void Awake()
    {
        foreach (IUI ui in GetComponentsInChildren<IUI>())
        {
            ui.Initialize();
            ui.OnOpen += AddInputUI;
            ui.OnClose += RemoveInputUI;
            uiDictionary.Add(ui.UIType, ui);
        }

        inputSO.OnBrakePressed += GetInputBack;
        inputSO.OnDashPressed += GetInputForward;
        inputSO.OnLeftClicked += GetInputLeft;
        inputSO.OnRightClicked += GetInputRight;
        inputSO.OnWheelBtnClicked += GetInputMiddle;
    }

    private void AddInputUI(UIType type)
    {
        InputList.Add(type);
    }

    private void RemoveInputUI(UIType type)
    {
        InputList.Remove(type);
    }

    public int GetInputListCnt() => InputList.Count;

    private void OnDestroy()
    {
        foreach (IUI ui in uiDictionary.Values)
        {
            ui.OnOpen -= AddInputUI;
            ui.OnClose -= RemoveInputUI;
        }

        inputSO.OnBrakePressed -= GetInputBack;
        inputSO.OnDashPressed -= GetInputForward;
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
        if (InputList.Count == 0) return;
        IUI inputUI = uiDictionary[InputList[InputList.Count - 1]];
        switch (interactiveType)
        {
            case InteractiveType.Forward: inputUI.FrontMove(); break;
            case InteractiveType.Back: inputUI.BackMove(); break;
            case InteractiveType.Left: inputUI.LeftMove(); break;
            case InteractiveType.Right: inputUI.RightMove(); break;
            case InteractiveType.Middle: inputUI.MiddleMove(); break;
            case InteractiveType.Scroll: inputUI.ScrollMove(inputSO.XMoveDir); break;
        }
    }
}