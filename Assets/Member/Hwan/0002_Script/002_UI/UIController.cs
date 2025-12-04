using Member.JYG._Code;
using Member.JYG.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerInputSO inputSO;

    private Dictionary<UIType, IUI> uiDictionary = new();
    private UIType openUI;
    private GoButtonUI goButtonUI;

    public bool CanInput { get; set; }

    private void Awake()
    {
        CanInput = true; 

        goButtonUI = GetComponentInChildren<GoButtonUI>();
        goButtonUI.Initialize(GetInputForward, GetInputBack);

        foreach (IUI ui in GetComponentsInChildren<IUI>())
        {
            ui.OnOpen += AddInputUI;
            ui.OnClose += RemoveInputUI;
            uiDictionary.Add(ui.UIType, ui);
            ui.Initialize(this);
        }

        inputSO.OnBrakePressed += GetInputBack;
        inputSO.OnDashPressed += GetInputForward;
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
        if (CanInput == false) return;

        if (openUI == UIType.None)
        {
            foreach (IUI ui in uiDictionary.Values)
            {
                if (ui.OpenInput != interactiveType) continue;
                DoMove(interactiveType, ui);
                return;
            }
            return;
        }
        IUI inputUI = uiDictionary[openUI];
        DoMove(interactiveType, inputUI);
    }
    private void AddInputUI(UIType type)
    {
        GameManager.Instance.Player.PlayerInputSO.OffInput();
        openUI = type;
        goButtonUI.ButtonUp();
    }

    private void RemoveInputUI(UIType type)
    {
        GameManager.Instance.Player.PlayerInputSO.ActiveInput();
        openUI = UIType.None;
        goButtonUI.ButtonDown();
    }

    private void DoMove(InteractiveType interactiveType, IUI inputUI)
    {
        switch (interactiveType)
        {
            case InteractiveType.Forward: inputUI.FrontMove(); break;
            case InteractiveType.Back: inputUI.BackMove(); break;
            case InteractiveType.Left: inputUI.LeftMove(); break;
            case InteractiveType.Right: inputUI.RightMove(); break;
            case InteractiveType.Middle: inputUI.MiddleMove(); break;
            case InteractiveType.Scroll: inputUI.ScrollMove(-inputSO.XMoveDir); break;
        }
    }
}