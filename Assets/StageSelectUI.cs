using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public StageListSO stageList;
    
    public void Initialize()
    {
        Open();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
        PlayerPrefs.SetInt("IsFirst", 1);
        for (int i = 0; i < buttonAmount; i++)
        {
            var button = Instantiate(buttonPrefab, transform);
            button.transform.SetParent(buttonsParent.transform);
            button.name = "stage" + (i + 1);
            button.GetComponentInChildren<TextMeshProUGUI>().text = stageList.stageList[i].stageName;
            _stageButtons.Add(button.GetComponentInChildren<SkinButton>());
            
            if (PlayerPrefs.GetInt("clearedstage") > stageList.stageList[i].stageIndex || (PlayerPrefs.GetInt("clearedstage") == 0 && i == 0))
            {
                _stageButtons[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
            }
            else
            {
                _stageButtons[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
            }
        }

        Highlight(0);
        ScrollTo();
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
        Hwan.SceneManager.Instance.OnLoadScene(_currentIndex + 4);
    }

    public void LeftClick() { }

    public void MiddleMove() { }

    public void ScrollMove(int value)
    {
        value = -value;
        if (value == 0) return;

        int next = Mathf.Clamp(_currentIndex + value, 0, _stageButtons.Count - 1);

        if (next == _currentIndex) return;

        if (stageList.stageList[next].stageIndex >= PlayerPrefs.GetInt("clearedstage"))
        {
            Debug.LogError($"스테이지 {stageList.stageList[next].stageIndex + 1}을/를 클리어하지 않아 스킨을 선택할 수 없습니다.");
            return;
        }
        
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