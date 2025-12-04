using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SideButtonClick : MonoBehaviour
{
    [SerializeField] private Minimize window;
    [SerializeField] private GameObject btA;
    [SerializeField] private GameObject btB;
    private void Update()
    {
        if (Mouse.current.backButton.wasPressedThisFrame)
        {
            GetOut();
        }
        if (Mouse.current.forwardButton.wasPressedThisFrame)
        {
            StageGo();
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
