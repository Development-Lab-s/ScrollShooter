using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minimize : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField] private GameObject Screen1;
    [SerializeField] private SideButtonClick ScreenButton;
    [SerializeField] private Slider slider;
    [SerializeField] private Member.PTY.Scripts.SO.SkinListSO _skinList;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        Screen1.SetActive(false);
        rt.offsetMax = new Vector2(-2000, -1200);
    }
    public void Starts(GameObject gameObject)
    {
        PlayerPrefs.SetInt(_skinList.skinList[0].prefsName, 1);
        Screen1.SetActive(true);
        gameObject.GetComponent<Button>().enabled = false;
        DOTween.To(
           () => rt.offsetMax,
           x => rt.offsetMax = x,
           new Vector2(0, 0),
           0.2f
       );
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void RemoveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(_skinList.skinList[0].prefsName, 1);
    }
    public void minimize()
    {
        if (slider.value >= 1)
        {
            DOTween.To(
            () => rt.offsetMax,
            x => rt.offsetMax = x,
            new Vector2(-2000, -1200),
            0.2f
        ).OnComplete(
          () =>
          {
              if (PlayerPrefs.GetInt("didTutorial", 0) == 0)
              {
                  Hwan.SceneManager.Instance.OnLoadScene(3);
              }
              else
              {
                  Hwan.SceneManager.Instance.OnLoadScene(1);
              }
              ScreenButton.isOpen = true;
          });
        }
    }

}
