using Member.JYG._Code;
using System;
using System.Collections;
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
        InputControlManager.Instance.ChangeUIInputActive(false);
        Close();
        nestingOpener.StartDeNesting(() => {
            SceneManager.LoadScene(1);
            InputControlManager.Instance.ChangeUIInputActive(true);
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
        InputControlManager.Instance.ChangeUIInputActive(false);
        Close();
        nestingOpener.StartDeNesting(() => { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            InputControlManager.Instance.ChangeUIInputActive(true);
        });
    }

    public void Initialize()
    {
        GameManager.Instance.Player.GetComponentInChildren<PlayerDeadEvent>().afterEffect.AddListener(WaitForOpen);
        nestingOpener = GetComponent<NestingOpener>();
        nestingOpener.Initialize();
        countdouwn = GetComponent<CountdouwnTmp>();
        UIObject.SetActive(false);
    }

    private void WaitForOpen() => StartCoroutine(WaitForOpenCor());

    private IEnumerator WaitForOpenCor()
    {
        yield return new WaitForSeconds(1.5f);
        Open();
    }

    public void LeftMove(bool _) { }

    public void MiddleMove() { }

    public void Open()
    {
        if (UIObject.activeSelf == true) return;
        UIManager.Instance.CloseAllUI();

        InputControlManager.Instance.ChangeUIInputActive(false);
        nestingOpener.StartNesting(OnPopUp);
    }

    public void OnPopUp()
    {
        InputControlManager.Instance.ChangeUIInputActive(true);
        TimeManager.Instance.StopTime();
        UIObject.SetActive(true);
        countdouwn.StartCount(BackMove);
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value) { }
}
