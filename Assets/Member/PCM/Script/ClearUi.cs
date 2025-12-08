
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClearUi : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; set; }
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private Image SkinMark;

    [SerializeField] private SkinListSO Skin;
    [SerializeField] private SkinListSO hiddenSkin;
    [SerializeField] private Sprite nullSpace;
    public UIType UIType => UIType.ClearUI;

    public InteractiveType OpenInput => InteractiveType.None;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;


    private NestingOpener nestingOpener;
    private UIController uiController;
    private CountdouwnTmp countdouwn;

    private void Start()
    {
        UIObject.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Open();
            Debug.Log(Time.time.ToString("F2"));
        }
    }
    public void BackMove()
    {
        uiController.CanInput = false;
        Close();
        nestingOpener.StartDeNesting(() => { SceneManager.LoadScene(1); uiController.CanInput = true; });
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
        TimeManager.Instance.UnStopTime();
        UIObject.SetActive(false);
    }

    public void ForwardMove()
    {
        uiController.CanInput = false;
        Close();
        nestingOpener.StartDeNesting(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); uiController.CanInput = true; });
    }

    public void Initialize(UIController uIController)
    {
        throw new NotImplementedException();
    }

    public void LeftMove()
    {
        throw new NotImplementedException();
    }

    public void MiddleMove()
    {
        throw new NotImplementedException();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
        ClearShow(SceneManager.GetActiveScene().buildIndex); //아마도 1스테이지가 Buildindex가 2겠지?
        TimeManager.Instance.StopTime();
        playTime.text = $"ClearTime:{Time.time.ToString("F2")}";
        UIObject.SetActive(true);
    }

    public void RightMove()
    {
        throw new NotImplementedException();
    }

    public void ScrollMove(int value)
    {
        throw new NotImplementedException();
    }
    public void ClearShow(int stage)
    {
        if (Skin.Skin[stage] != null)
            SkinMark.sprite = Skin.Skin[stage];
        else if (hiddenSkin.Skin[stage] != null)
            SkinMark.sprite = hiddenSkin.Skin[stage];
        else
            SkinMark.sprite = nullSpace;
        //스킨을 가지고 있는애를 만들함
    }
}

