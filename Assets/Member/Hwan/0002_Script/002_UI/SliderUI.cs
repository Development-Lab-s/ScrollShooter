using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    private Slider slider;
    public void InitializeSlider()
    {
        slider = GetComponent<Slider>();
    }
}
