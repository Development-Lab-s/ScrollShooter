using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YGPacks;

public class TimeManager : Singleton<TimeManager>
{
    private Dictionary<string, float> timeDictionary = new();

    public void StopTime()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
    }

    public void UnStopTime()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    public void FadeStopTime(float fadeTime, float lastScale = 0)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTimeCoroutine(fadeTime, lastScale));
    }

    private IEnumerator FadeTimeCoroutine(float fadeTime, float lastScale = 0)
    {
        Time.timeScale = 1;

        while (Time.timeScale >= lastScale)
        {
            float tempTime = Time.timeScale;
            Time.timeScale = Mathf.Clamp01(tempTime - Time.unscaledDeltaTime / fadeTime);
            yield return null;
        }

        Time.timeScale = lastScale;
    }

    public void StopAndSaveTime(string key)
    {
        StopAllCoroutines();
        timeDictionary.Add(key, Time.timeScale);
        StopTime();
    }

    public void LoadTime(string key)
    {
        if (timeDictionary.ContainsKey(key) == false) return;
        StopAllCoroutines();
        Time.timeScale = timeDictionary[key];
        timeDictionary.Remove(key);
    }
}
