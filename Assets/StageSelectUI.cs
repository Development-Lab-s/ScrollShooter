using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour, IUI
{
    public int buttonAmount;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonsParent;
    
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;
    
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Transform stageContent;
    private List<SkinButton> _stageButtons = new();

    private int _currentIndex = 0;
    
    [field: SerializeField] public GameObject UIObject { get; private set; }
    public UIType UIType => UIType.StageSelectUI;
    public InteractiveType OpenInput => InteractiveType.None;

    public void Initialize()
    {
        for (int i = 0; i < buttonAmount; i++)
        {
            var button = Instantiate(buttonPrefab, transform);
            button.transform.SetParent(buttonsParent.transform);
            button.name = "stage" + i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "스테이지" + i;
            _stageButtons.Add(button.GetComponentInChildren<SkinButton>());
        }

        Highlight(0);
        ScrollTo();
        
        Open();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        Hwan.SceneManager.Instance.OnLoadScene(1);
    }

    public void ForwardMove()
    {
        Hwan.SceneManager.Instance.OnLoadScene(_currentIndex + 3);
    }

    public void LeftClick() { }

    public void MiddleMove() { }

    public void ScrollMove(int value)
    {
        value = -value;
        if (value == 0) return;

        int next = Mathf.Clamp(_currentIndex + value, 0, _stageButtons.Count - 1);

        if (next == _currentIndex) return;

        _currentIndex = next;

        Highlight(_currentIndex);
        ScrollTo();
    }
    
    private void Highlight(int index)
    {
        for (int i = 0; i < _stageButtons.Count; i++)
            _stageButtons[i].SetHighlight(i == index);
    }

    private void ScrollTo()
    {
        scrollbar.value = (float)_currentIndex / (_stageButtons.Count - 1);
    }

    public void RightClick(bool isPerformed) { }
}