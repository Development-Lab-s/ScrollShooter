using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageMark : MonoBehaviour
{
    [SerializeField]
    private StageMove Move;
    [SerializeField] private CanvasGroup[] rectTransformList;
    private void OnValidate()
    {
        rectTransformList = GetComponentsInChildren<CanvasGroup>();
        if (rectTransformList.Length != 0)
            OnStageValueChanged(Move.Stage);
    }
    private void Awake()
    {
        rectTransformList = GetComponentsInChildren<CanvasGroup>();
        if(rectTransformList.Length != 0)
        OnStageValueChanged(Move.Stage);
    }
    public void OnStageValueChanged(int value)
    {
        for (int i = 0; i < rectTransformList.Length; i++)
        {
            rectTransformList[i].gameObject.GetComponent<RawImage>().color = value ==i ? Color.red: Color.white;
        }
    }
}
