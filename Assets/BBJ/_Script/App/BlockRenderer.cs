using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class BlockRenderer : MonoBehaviour
{
    [SerializeField] private AppTypeDataSO typeData;
    [SerializeField] private string fileName;

    public SpriteRenderer SrCompo { get; private set; }

    private TextMeshPro tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        SrCompo = GetComponentInChildren<SpriteRenderer>();

        string a= fileName;
        if(typeData.fileType != "")
        {
            a += ".";
            tmp.text = typeData.fileType;
        }
        tmp.text = a;
        tmp.color = typeData.textColor;
        SrCompo.sprite = typeData.Icon;
    }
}
