using Member.JYG._Code;
using System;
using TMPro;
using UnityEngine;
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
    private float StartTime;
    public UIType UIType => UIType.ClearUI;

    public InteractiveType OpenInput => InteractiveType.None;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    private CountdouwnTmp countdouwn;

    public void BackMove()
    {
        SceneManager.LoadScene(1); 
        Close();
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
        TimeManager.Instance.UnStopTime();
        UIObject.SetActive(false);
    }

    public void ForwardMove()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        Close();
    }

    public void Initialize()
    {
        GameManager.Instance.OnClear += Open;
        countdouwn = GetComponent<CountdouwnTmp>();
        StartTime = Time.time;
        Close();
    }

    private void Open(int _) => Open();

    public void LeftMove()
    {
    }

    public void MiddleMove()
    {
    }

    public void Open()
    {
        countdouwn.StartCount(() => SceneManager.LoadScene(1));
        ClearShow(SceneManager.GetActiveScene().buildIndex); //아마도 1스테이지가 Buildindex가 2겠지?
        TimeManager.Instance.StopTime();
        float t = Time.time - StartTime;
        playTime.text = $"ClearTime:{t.ToString("F2")}";
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value)
    {
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