using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG._Code;
using Member.PTY.Scripts.SO;
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

    [SerializeField]private SkinListSO skinListSO;
    [SerializeField] private SkinListSO HiddenskinListSO;
    [SerializeField] private Sprite nullSpace;
    public UIType UIType => UIType.ClearUI;

    public InteractiveType OpenInput => InteractiveType.None;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    private CountdouwnTmp countdouwn;

    public void BackMove()
    {
        Hwan.SceneManager.Instance.OnLoadScene(1); 
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
        Hwan.SceneManager.Instance.OnLoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        Close();
    }

    public void Initialize()
    {
        GameManager.Instance.OnClear += Open;
        countdouwn = GetComponent<CountdouwnTmp>();
        Close();
    }

    private void Open(int _) => Open();

    public void RightClick(bool _)
    {
    }

    public void MiddleMove(bool _)
    {
    }

    public void Open()
    {
        SoundManager.Instance.PlaySound("ClearSFX");

        countdouwn.StartCount(() => Hwan.SceneManager.Instance.OnLoadScene(1));
        SkinMark.sprite = GameManager.Instance.GotSkin == null ? nullSpace : GameManager.Instance.GotSkin.skin;
        //ClearShow(SceneManager.GetActiveScene().buildIndex-2); //아마도 1스테이지가 Buildindex가 2겠지?
        TimeManager.Instance.StopTime();
        float t = PCM.PlayTime.Instance.CurrentTime;
        playTime.text = $"ClearTime:{t.ToString("F2")}";
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void LeftClick() { }

    public void ScrollMove(int value)
    {
    }
}