using csiimnida.CSILib.SoundManager.RunTime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NestingOpener : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private RectTransform nestingObject;
    [SerializeField] private int nestingOffset;
    [SerializeField] private int nestingCount;
    [SerializeField] private float nestingTime;

    private RectTransform[] UIObjects;

    public void Initialize()
    {
        UIObjects = new RectTransform[nestingCount];

        float middleX = Screen.width / 2;
        float middleY = Screen.height / 2;
        float startX = -middleX + nestingObject.sizeDelta.x / 2 + nestingOffset;
        float startY = middleY - nestingObject.sizeDelta.y / 2 - nestingOffset;

        for (int i = 0; i < nestingCount; i++)
        {
            float t = (float)i / (nestingCount - 1);
            RectTransform rect = Instantiate(nestingObject, parent).GetComponent<RectTransform>();
            float anchorX = Mathf.Lerp(startX, 0, t);
            float anchorY = Mathf.Lerp(startY, 0, t);
            rect.anchoredPosition = new Vector2(anchorX, anchorY);
            rect.gameObject.SetActive(false);
            UIObjects[i] = rect;
        }
    }

    public void StartNesting(Action OpenAction)
    {
        StartCoroutine(Nesting(OpenAction));
    }

    private IEnumerator Nesting(Action OpenAction)
    {
        WaitForSecondsRealtime waitCoroutine = new WaitForSecondsRealtime(nestingTime / nestingCount);
        for (int i = 0; i < nestingCount; i++)
        {
            SoundManager.Instance.PlaySound("GameOver");
            UIObjects[i].gameObject.SetActive(true);
            yield return waitCoroutine;
        }

        SoundManager.Instance.PlaySound("GameOver");
        OpenAction?.Invoke();
    }
    public void StartDeNesting(Action CloseAction)
    {
        StartCoroutine(DeNesting(CloseAction));
    }

    public IEnumerator DeNesting(Action CloseAction)
    {
        WaitForSecondsRealtime waitCoroutine = new WaitForSecondsRealtime(nestingTime / nestingCount);

        for (int i = UIObjects.Length - 1; i >= 0; i--)
        {
            yield return waitCoroutine;
            UIObjects[i].gameObject.SetActive(false);
        }

        yield return waitCoroutine;
        CloseAction?.Invoke();
    }
}
