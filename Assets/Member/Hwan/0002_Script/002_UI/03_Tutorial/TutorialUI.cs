using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; private set; }

    public UIType UIType => UIType.TurorialUI;

    public InteractiveType OpenInput => InteractiveType.None;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    [SerializeField] private TextMeshProUGUI tmp;
    private RectTransform uiObjectRect;
    private UIController uiController;

    public void BackMove() => TutorialManager.Instance.GetInput(InteractiveType.Back);

    public void Close()
    {
        TimeManager.Instance.UnStopTime();
        UIObject.SetActive(false);
        OnClose?.Invoke(UIType);
    }

    public void Initialize(UIController uiController)
    {
        this.uiController = uiController;
        uiObjectRect = UIObject.GetComponent<RectTransform>();
        TutorialManager.Instance.OnPlayerNearObstacle += Open;
        TutorialManager.Instance.OnSkipPhaze += Close;
        UIObject.SetActive(false);
    }

    private void Open(TutorialInfoSO tutoInfo)
    {
        uiObjectRect.anchoredPosition = tutoInfo.PopUpPos;
        tmp.text = tutoInfo.Text;
        Open();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
        UIObject.SetActive(true);
    }

    public void ForwardMove() => TutorialManager.Instance.GetInput(InteractiveType.Forward);

    public void LeftMove() => TutorialManager.Instance.GetInput(InteractiveType.Left);

    public void MiddleMove() => TutorialManager.Instance.GetInput(InteractiveType.Middle);

    public void RightMove() => TutorialManager.Instance.GetInput(InteractiveType.Right);

    public void ScrollMove(int value) => TutorialManager.Instance.GetInput(InteractiveType.Scroll);

    private IEnumerator waitForInput()
    {
        uiController.CanInput = false;
        yield return new WaitForSecondsRealtime(1);
        uiController.CanInput = true;

    }
}
