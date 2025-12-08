using Member.JYG._Code;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour, IUI
{
    [field: SerializeField]public GameObject UIObject { get; private set; }

    public InteractiveType OpenInput => InteractiveType.None;
    public UIType UIType => UIType.GameOverUI;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    private NestingOpener nestingOpener;
    private UIController uiController;
    private CountdouwnTmp countdouwn;

    public void BackMove()
    {
        Close();
        nestingOpener.StartDeNesting(() => { SceneManager.LoadScene(1); uiController.CanInput = true; });
    }

    public void Close()
    {
        uiController.CanInput = false;
        OnClose?.Invoke(UIType);
        UIObject.SetActive(false);
        TimeManager.Instance.UnStopTime();
    }

    public void ForwardMove()
    {
        Close();
        nestingOpener.StartDeNesting(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); uiController.CanInput = true; });
    }

    public void Initialize(UIController uiController)
    {
        GameManager.Instance.Player.GetComponent<HitSystem>().onDead.AddListener(Open);
        this.uiController = uiController;
        nestingOpener = GetComponent<NestingOpener>();
        nestingOpener.Initialize();
        countdouwn = GetComponent<CountdouwnTmp>();
        UIObject.SetActive(false);
    }

    public void LeftMove() { }

    public void MiddleMove() { }

    public void Open()
    {
        if (UIObject.activeSelf == true) return;
        UIManager.Instance.CloseAllUI();

        uiController.CanInput = false;
        nestingOpener.StartNesting(OnPopUp);
    }

    public void OnPopUp()
    {
        uiController.CanInput = true;
        TimeManager.Instance.StopTime();
        UIObject.SetActive(true);
        countdouwn.StartCount(BackMove);
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value) { }
}
