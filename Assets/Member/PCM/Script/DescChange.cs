using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DescChange : MonoBehaviour
{
    [SerializeField] private string Original;
    [SerializeField]private string Change;
    private TextMeshProUGUI texts;
    public event Action<bool> OnChange;

    private void Awake()
    {
        texts = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        Debug.Log(Original);
        texts.text = Original;
    }

    public void ChangeText(bool isTrigger)
    {
        if (isTrigger)
        {
            texts.text = Change;
        }
        if (!isTrigger)
        {
            texts.text = Original;
        }   
    }
}
