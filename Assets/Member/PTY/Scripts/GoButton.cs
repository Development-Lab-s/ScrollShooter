using UnityEngine;

public class GoButton : MonoBehaviour
{
    private GoButtonUI _goButtonUI;

    private void Awake()
    {
        _goButtonUI = GetComponent<GoButtonUI>();
    }
    
    private void Start()
    {
        _goButtonUI.ButtonUp();
    }
}
