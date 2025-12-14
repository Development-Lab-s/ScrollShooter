using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IUI
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonsParent;
    
    [SerializeField] private TextMeshProUGUI previewSkinName;
    
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Transform skinContent;
    
    private List<SkinButton> _skinButtons = new();

    private int _currentIndex = 0;

    private Member.PTY.Scripts.SO.SkinListSO _skinList;

    public GameObject UIObject { get; }
    public UIType UIType => UIType.ShopUI;
    public InteractiveType OpenInput => InteractiveType.None;

    private void Start()
    {
        PlayerPrefs.SetInt(_skinList.skinList[0].prefsName, 1);
        PlayerPrefs.SetInt(_skinList.skinList[1].prefsName, 1);
        PlayerPrefs.SetInt(_skinList.skinList[4].prefsName, 1);
    }

    public void Initialize()
    {
        if (ShopManager.Instance.skinList != null)
        {
            _skinList = ShopManager.Instance.skinList;
        }
        else
        {
            Debug.LogError("ShopManager의 SkinList가 지정되어있지 않습니다.");
        }
        
        Open();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
        List<SkinSO> lockSkinList = new List<SkinSO>();
        int index = 0;
        for (int i = 0; i < _skinList.skinList.Count; i++)
        {
            if (PlayerPrefs.GetInt(_skinList.skinList[i].prefsName, 0) == 1)
            {
                Debug.Log(_skinList.skinList[i].prefsName + "Is Activated");
                var button = Instantiate(buttonPrefab, transform);
                button.name = _skinList.skinList[i].skinName;
                button.transform.SetParent(buttonsParent.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = _skinList.skinList[i].skinName;
                button.transform.GetChild(1).GetComponent<Image>().sprite = _skinList.skinList[i].skin;
                _skinButtons.Add(button.GetComponentInChildren<SkinButton>());
                _skinButtons[_skinButtons.Count - 1].skinSO = _skinList.skinList[i];

                _skinButtons[_skinButtons.Count - 1].transform.GetChild(3).GetComponent<Image>().enabled = false;
                Debug.Log(_skinButtons[_skinButtons.Count - 1].transform.GetChild(3).GetComponent<Image>().gameObject.name);
                index++;
            }
            else
            {
                Debug.Log(_skinList.skinList[i].prefsName + "Is Not Activated");
                lockSkinList.Add(_skinList.skinList[i]);
            }
        }

        for (int i = 0; i < lockSkinList.Count; i++)
        {
            Debug.Log(lockSkinList[i] + "Is Lock Item");
            var button = Instantiate(buttonPrefab, transform);
            button.name = lockSkinList[i].skinName;
            button.transform.SetParent(buttonsParent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = lockSkinList[i].skinName;
            button.transform.GetChild(1).GetComponent<Image>().sprite = lockSkinList[i].skin;
            _skinButtons.Add(button.GetComponentInChildren<SkinButton>());
            _skinButtons[_skinButtons.Count - 1].skinSO = lockSkinList[i];

            _skinButtons[i + index].transform.GetChild(3).GetComponent<Image>().enabled = true;
        }

        string userskin = PlayerPrefs.GetString("userskin");
        foreach (var skin in _skinButtons)
        {
            if (skin.skinSO.prefsName == userskin)
            {
                _currentIndex = _skinButtons.IndexOf(skin);
                Debug.Log($"Current Index is {_currentIndex}");
            }
        }
        
        Highlight(_currentIndex);
        ScrollTo();
        StartCoroutine(WaitLoad());
    }

    private IEnumerator WaitLoad()
    {
        yield return null;
        BackMove();
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        //스킨 적용하기
        if (PlayerPrefs.GetInt(_skinButtons[_currentIndex].skinSO.prefsName) != 0)
        {
            _skinButtons[_currentIndex].OnClick();
            ShopManager.Instance.ChangeSkin(_skinButtons[_currentIndex].skinSO);
            _skinButtons[_currentIndex].name = _skinButtons[_currentIndex].skinSO.name;
            previewSkinName.text = _skinButtons[_currentIndex].GetComponentInChildren<TextMeshProUGUI>().text;
        }
        else
        {
            Debug.LogError($"스테이지 {_skinList.skinList[_currentIndex].unlockStage}을/를 클리어하지 않아 스킨을 사용할 수 없습니다.");
        }
    }

    public void ForwardMove()
    {
        SceneManager.LoadScene(2);
    }

    public void RightClick(bool _)
    {

    }

    public void LeftClick()
    {

    }

    public void MiddleMove(bool _)
    {

    }

    public void ScrollMove(int value)
    {
        value = -value;
        //스킨 둘러보기
        if (value == 0) return;

        int next = Mathf.Clamp(_currentIndex + value, 0, _skinButtons.Count - 1);

        if (next == _currentIndex) return;

        if (PlayerPrefs.GetInt(_skinButtons[next].skinSO.prefsName) == 0)
        {
            Debug.LogError($"{_skinButtons[next].skinSO.prefsName} Is Not Activated");
            return;
        }

        _currentIndex = next;
        
        Highlight(_currentIndex);
        ScrollTo();
    }
    
    private void Highlight(int index)
    {
        for (int i = 0; i < _skinButtons.Count; i++)
            _skinButtons[i].SetHighlight(i == index);
    }

    private void ScrollTo()
    {
        if ((int)Mouse.current.scroll.ReadValue().y == -1)
        {
            if (_currentIndex % 3 == 0)
                scrollbar.value = 1 - _currentIndex / 3 * 0.5f;
        }
        else if ((int)Mouse.current.scroll.ReadValue().y == 1)
        {
            if ((_currentIndex + 1) % 3 == 0)
                scrollbar.value = 1 - _currentIndex / 3 * 0.5f;
        }
    }
}
