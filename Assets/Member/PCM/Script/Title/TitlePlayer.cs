using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitlePlayer : MonoBehaviour
{
    private Vector2 StageScroll;
    [SerializeField]private Slider slider;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private float sliderSpeed = 0;
    private Tween rotateTween;
    void Update()
    {
        Vector3 scroll = Mouse.current.scroll.ReadValue();
        // 스크롤 변화량이 있을 때만 적용
        if (scroll.y != 0)
        {
            rotateTween.Kill();
            rotateTween = transform.DORotate(new Vector3(0,0,-(scroll.y*100)),0.1f);
            slider.value += scroll.y * sliderSpeed; // 스크롤 감도 조절
            int percentValue = (int)(slider.value *100);
            percent.text = percentValue.ToString() + "%";


        }
        else
        {
            rotateTween.Kill();
            rotateTween = transform.DORotate(new Vector3(0, 0, -(scroll.y * 100)), 0.1f);
        }
    }


}

