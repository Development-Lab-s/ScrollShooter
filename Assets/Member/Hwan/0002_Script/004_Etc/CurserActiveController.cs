using System.Collections.Generic;
using UnityEngine;

public class CursetActiveController : MonoBehaviour
{
    private UIController uiController;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        uiController.OnUIChange += (List<UIType> list) => SetCursorActive(list.Count != 0);
    }

    public void SetCursorActive(bool isActive)
    {
        InputControlManager.Instance.ChangePlayerInputActive(!isActive);
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}