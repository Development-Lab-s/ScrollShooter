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
    }
}
