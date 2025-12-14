using Member.JYG._Code;
using Member.JYG.Input;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; private set; }

    public UIType UIType => UIType.TurorialUI;

    public InteractiveType OpenInput => InteractiveType.None;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private TextMeshProUGUI tmp;
    private RectTransform uiObjectRect;


    public void Close()
    {
        InputControlManager.Instance.ChangeUIInputActive(true);
        TimeManager.Instance.UnStopTime();
        UIObject.SetActive(false);
        OnClose?.Invoke(UIType);
    }

    public void Initialize()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            UIObject.SetActive(false);
            return;
        }

        playerInputSO.OnBrakePressed += ForwardMove;
        playerInputSO.OnDashPressed += BackMove;
        playerInputSO.OnLeftClicked += LeftMove;
        playerInputSO.OnRightClicked += RightClick;
        playerInputSO.OnWheelBtnClicked += MiddleMove;
        playerInputSO.OnWheeling += Scroll;

        uiObjectRect = UIObject.GetComponent<RectTransform>();
        TutorialManager.Instance.OnPlayerNearObstacle += Open;
        TutorialManager.Instance.OnSkipPhaze += Close;
        UIObject.SetActive(false);
    }

    private void Scroll() => ScrollMove(playerInputSO.XMoveDir);

    private void Open(TutorialInfoSO tutoInfo)
    {
        GameManager.Instance.Player.SetMaxSpeed(3, 1);
        SetPopUp(tutoInfo);

        StartCoroutine(WaitForInputCor());
        InputControlManager.Instance.ChangeUIInputActive(false);
        Open();
    }

    private void SetPopUp(TutorialInfoSO tutoInfo)
    {
        uiObjectRect.anchoredPosition = tutoInfo.PopUpPos;
        tmp.text = tutoInfo.Text;
    }

    public void Open()
    {
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void ForwardMove() => TutorialManager.Instance.GetInput(InteractiveType.Forward);

    public void LeftMove() => TutorialManager.Instance.GetInput(InteractiveType.Left);

    public void MiddleMove(bool _) { }
    public void MiddleMove() => TutorialManager.Instance.GetInput(InteractiveType.Middle);

    public void LeftClick() => TutorialManager.Instance.GetInput(InteractiveType.Right);

    public void ScrollMove(int value) => TutorialManager.Instance.GetInput(InteractiveType.Scroll);

    public void BackMove() => TutorialManager.Instance.GetInput(InteractiveType.Back);

    private IEnumerator WaitForInputCor()
    {
        InputControlManager.Instance.ChangePlayerInputActive(false);
        yield return new WaitForSecondsRealtime(1);
        InputControlManager.Instance.ChangePlayerInputActive(true);
    }

    private void OnDestroy()
    {
        playerInputSO.OnBrakePressed -= ForwardMove;
        playerInputSO.OnDashPressed -= BackMove;
        playerInputSO.OnLeftClicked -= LeftMove;
        playerInputSO.OnRightClicked -= RightClick;
        playerInputSO.OnWheelBtnClicked -= MiddleMove;
        playerInputSO.OnWheeling -= Scroll;
    }

    public void RightClick(bool isPerformed) { }
    public void RightClick() => TutorialManager.Instance.GetInput(InteractiveType.Back);
}
