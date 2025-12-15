using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Image highlightImage;
    public SkinSO skinSO;

    public void OnClick()
    {
        Debug.Log($"{name} 선택됨!");
    }

    public void SetHighlight(bool on)
    {
        if (highlightImage != null)
            highlightImage.enabled = on;
    }
}