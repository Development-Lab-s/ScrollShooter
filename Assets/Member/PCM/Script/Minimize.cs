using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minimize : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField] private GameObject Screen1;
    [SerializeField] private GameObject Screen2;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        Screen1.SetActive(false);
        rt.offsetMax = new Vector2(-2000, -1200);
    }
    public void Normal(GameObject gameObject)
    {
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
              Hwan.SceneManager.Instance.OnLodeScene(3);
          });
        }
    }

}
