using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    private Dictionary<UIType, IUI> uiDictionary = new();
    public bool MouseEnable { get; set; } = false;

    private void Awake()
    {
        foreach (IUI ui in GetComponentsInChildren<IUI>())
        {
            ui.Initialize();
            uiDictionary.Add(ui.UIType, ui);
        }
    }

    private void Update()
    {
        if (Mouse.current.middleButton.wasPressedThisFrame)
            UIInteractive(InteractiveType.Middle);

        if (Mouse.current.backButton.wasPressedThisFrame)
            UIInteractive(InteractiveType.Back);

        if (Mouse.current.forwardButton.wasPressedThisFrame)
            UIInteractive(InteractiveType.Forward);

        if (Mouse.current.leftButton.wasPressedThisFrame)
            UIInteractive(InteractiveType.Left);

        if (Mouse.current.rightButton.wasPressedThisFrame)
            UIInteractive(InteractiveType.Right);
    }

    private void UIInteractive(InteractiveType interactiveType)
    {
        if (interactiveType == InteractiveType.Middle)
        {
            uiDictionary[UIType.SettingUI].Open();
            return;
        }

        foreach (IUI ui in uiDictionary.Values)
        {
            if (ui.UIObject.activeSelf == true)
            {
                switch (interactiveType)
                {
                    case InteractiveType.Forward: ui.FrontMove(); break;
                    case InteractiveType.Back: ui.BackMove(); break;
                    case InteractiveType.Left: ui.LeftButton(); break;
                    case InteractiveType.Right: ui.RightButton(); break;
                    case InteractiveType.Middle: ui.MiddleButton(); break;
                }
            }
        }
    }
}
