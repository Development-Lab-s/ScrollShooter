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
    private CountdouwnTmp countdouwn;

    public void BackMove()
    {
        InputControlleManager.Instance.ChangeUIInputActive(false);
        Close();
        nestingOpener.StartDeNesting(() => {
            SceneManager.LoadScene(1);
            InputControlleManager.Instance.ChangeUIInputActive(true);
        });
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
        UIObject.SetActive(false);
        TimeManager.Instance.UnStopTime();
    }

    public void ForwardMove()
    {
        InputControlleManager.Instance.ChangeUIInputActive(false);
        Close();
        nestingOpener.StartDeNesting(() => { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            InputControlleManager.Instance.ChangeUIInputActive(true);
        });
    }

    public void Initialize()
    {
        GameManager.Instance.Player.GetComponent<HitSystem>().onDead.AddListener(Open);
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

        InputControlleManager.Instance.ChangeUIInputActive(false);
        nestingOpener.StartNesting(OnPopUp);
    }

    public void OnPopUp()
    {
        InputControlleManager.Instance.ChangeUIInputActive(true);
        TimeManager.Instance.StopTime();
        UIObject.SetActive(true);
        countdouwn.StartCount(BackMove);
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value) { }
}
