using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG._Code;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour, IUI
{
    [field: SerializeField]public GameObject UIObject { get; private set; }

    public InteractiveType OpenInput => InteractiveType.Left;
    public UIType UIType => UIType.GameOverUI;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    private NestingOpener nestingOpener;
    private UIController uiController;
    private CountdouwnTmp countdouwn;

    public void BackMove()
    {
        uiController.CanInput = false;
        Close();
        nestingOpener.StartDeNesting(() => { SceneManager.LoadScene(1); uiController.CanInput = true; });
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
        UIObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ForwardMove()
    {
        uiController.CanInput = false;
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
        Close();
    }

    public void LeftMove() => Open();

    public void MiddleMove() { }

    public void Open()
    {
        if (UIObject.activeSelf == true) return;
        
        uiController.CanInput = false;
        nestingOpener.StartNesting(OnPopUp);
    }

    public void OnPopUp()
    {
        uiController.CanInput = true;
        Time.timeScale = 0;
        UIObject.SetActive(true);
        countdouwn.StartCount(BackMove);
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value) { }
}
