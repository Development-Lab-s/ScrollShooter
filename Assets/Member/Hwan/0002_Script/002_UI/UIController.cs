using Member.JYG._Code;
using Member.JYG.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerInputSO inputSO;

    private List<UIType> openUIList = new();
    private GoButtonUI goButtonUI;
    private Dictionary<UIType, IUI> uiDictionary = new();

    public bool CanInput { get; set; }

    private void Awake()
    {
        uiDictionary = UIManager.Instance.UIDictionary;
        CanInput = true; 

        goButtonUI = GetComponentInChildren<GoButtonUI>();
        goButtonUI.Initialize(GetInputForward, GetInputBack);

        foreach (IUI ui in uiDictionary.Values)
        {
            ui.OnOpen += AddInputUI;
            ui.OnClose += RemoveInputUI;
            ui.Initialize(this);
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
        if (CanInput == false) return;
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
        OnInputUIChange(type, true);
    }

    private void RemoveInputUI(UIType type)
    {
        if (openUIList.Remove(type) == false) return;
        if (openUIList.Count == 0)
        {
            OnInputUIChange(type, false);
            return;
        }
        OnInputUIChange(openUIList.Last(), true);
    }

    private void OnInputUIChange(UIType type, bool canInteractive)
    {
        GameManager.Instance.SetCursorActive(canInteractive);
        goButtonUI.ButtonMove(type, canInteractive);
    }

    private void DoMove(InteractiveType interactiveType, IUI inputUI)
    {
        switch (interactiveType)
        {
            case InteractiveType.Forward: inputUI.ForwardMove(); break;
            case InteractiveType.Back: inputUI.BackMove(); break;
            case InteractiveType.Left: inputUI.LeftMove(); break;
            case InteractiveType.Right: inputUI.RightMove(); break;
            case InteractiveType.Middle: inputUI.MiddleMove(); break;
            case InteractiveType.Scroll: inputUI.ScrollMove(-inputSO.XMoveDir); break;
        }
    }
}