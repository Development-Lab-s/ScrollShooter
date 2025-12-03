using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestingOpener : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private RectTransform nestingObject;
    [SerializeField] private int nestingOffset;
    [SerializeField] private int nestingCount;
    [SerializeField] private float nestingTime;

    private Queue<RectTransform> UIObjects = new();

    public void Initialize()
    {
        Transform subParent = transform.GetChild(1);

        float middleX = Screen.width / 2;
        float middleY = Screen.height / 2;
        float startX = -middleX + nestingObject.sizeDelta.x / 2 + nestingOffset;
        float startY = middleY - nestingObject.sizeDelta.y / 2 - nestingOffset;

        for (int i = 0; i < nestingCount; i++)
        {
            float t = (float)i / (nestingCount - 1); // 0 ~ 1
            RectTransform rect = Instantiate(nestingObject, subParent).GetComponent<RectTransform>();
            float anchorX = Mathf.Lerp(startX, 0, t);
            float anchorY = Mathf.Lerp(startY, 0, t);
            rect.anchoredPosition = new Vector2(anchorX, anchorY);
            rect.gameObject.SetActive(false);
            UIObjects.Enqueue(rect);
        }
    }

    public void StartNesting(GameObject mainUI)
    {
        StartCoroutine(Nesting(mainUI));
    }

    private IEnumerator Nesting(GameObject mainUI)
    {
        WaitForSeconds waitCoroutine = new WaitForSeconds(nestingTime / nestingCount);
        for (int i = 0; i < nestingCount; i++)
        {
            UIObjects.Dequeue().gameObject.SetActive(false);
            yield return waitCoroutine;
        }

        mainUI.SetActive(true);
    }
}
