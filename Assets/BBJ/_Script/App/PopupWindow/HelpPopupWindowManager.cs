
using CSILib.SoundManager.RunTime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPopupWindowManager : MonoSingleton<HelpPopupWindowManager>
{
    [SerializeField] private CanvasScaler scaler;
    [SerializeField] private HelpPopupUI prifab;
    private Factory<PopupWindowBase> _factory;
    private void Awake()
    {
        _factory = new Factory<PopupWindowBase>(prifab, 10, transform);
    }

    public void OnActivePopupWindow()
    {
        var popup = _factory.Get();

        var maxWH = scaler.referenceResolution;
        var wh = popup.RectCompo.sizeDelta;
        var temp = maxWH - wh;
        temp = new Vector2(temp.x * Random.value, temp.y * Random.value);

        popup.Activate(temp);
        popup.OnPopup();

        popup.Destroyed += OnClossedPopup;
    }
    private void OnClossedPopup(IRecycle clossPopup)
    {
        var popup = clossPopup as PopupWindowBase;
        popup.Destroyed -= OnClossedPopup;
        _factory.Restore(popup);
    }
}
