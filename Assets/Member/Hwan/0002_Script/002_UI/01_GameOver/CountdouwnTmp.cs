using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdouwnTmp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpPro;
    [SerializeField] private int count;

    public void StartCount(Action action)
    {
        StopAllCoroutines();
        StartCoroutine(Countdown(action));
    }

    private IEnumerator Countdown(Action action)
    {
        WaitForSecondsRealtime waitCor = new WaitForSecondsRealtime(1);
        string baseText = tmpPro.text; // 원래 글자 저장

        for (int i = count; i >= 1; i--)
        {
            tmpPro.text = $"{baseText} <size=82%>({i})</size>";
            yield return waitCor;
        }

        tmpPro.text = $"{baseText} <size=82%>(0)</size>";
        action?.Invoke();
    }
}
