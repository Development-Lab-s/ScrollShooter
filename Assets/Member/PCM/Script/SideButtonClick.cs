using UnityEngine;
using UnityEngine.InputSystem;

public class SideButtonClick : MonoBehaviour
{
    [SerializeField] private Minimize window;
    [SerializeField] private GameObject btA;
    [SerializeField] private GameObject btB;
    public bool isOpen;
    private void Start()
    {
        isOpen = false;
    }
    private void Update()
    {
        if (isOpen)
        {
            if (Mouse.current.backButton.wasPressedThisFrame || Keyboard.current.zKey.wasPressedThisFrame)
            {
                GetOut();
            }
            if (Mouse.current.forwardButton.wasPressedThisFrame || Keyboard.current.xKey.wasPressedThisFrame)
            {
                StageGo();
            }
        }
    }
    public void StageGo()
    {
        btA.SetActive(false);
        btB.SetActive(false);
        window.GetComponent<Minimize>().minimize();
    }
    public void GetOut()
    {
        btA.SetActive(false);
        btB.SetActive(false);
        Application.Quit();
    }
}
