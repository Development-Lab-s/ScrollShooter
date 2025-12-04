using Member.JYG._Code;
using Member.JYG.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerInputSO inputSO;

    private Dictionary<UIType, IUI> uiDictionary = new();
    private List<UIType> openUIList = new();
    private GoButtonUI goButtonUI;

    public bool CanInput { get; set; }

    private void Awake()
    {
        CanInput = true; 

        goButtonUI = GetComponentInChildren<GoButtonUI>();
        goButtonUI.Initialize(GetInputForward, GetInputBack);

        foreach (IUI ui in GetComponentsInChildren<IUI>(true))
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

        if (openUIList.Count == 0)
        {
            foreach (IUI ui in uiDictionary.Values)
            {
                if (ui.OpenInput != interactiveType) continue;
                ui.Open();
                return;
            }
            return;
        }

        IUI inputUI = uiDictionary[openUIList.Last()];
        DoMove(interactiveType, inputUI);
    }

    private void AddInputUI(UIType type)
    {
        openUIList.Add(type);
        if (openUIList.Count == 1)
        {
            GameManager.Instance.Player.PlayerInputSO.OffInput();
            goButtonUI.ButtonUp();
        }
    }

    private void RemoveInputUI(UIType type)
    {
        openUIList.Remove(type);
        if (openUIList.Count == 0)
        {
            GameManager.Instance.Player.PlayerInputSO.ActiveInput();
            goButtonUI.ButtonDown();
        }
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