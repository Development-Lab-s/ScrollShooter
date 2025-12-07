using System.Collections;
using UnityEngine;
using YGPacks;

public class TimeManager : Singleton<TimeManager>
{
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void UnStopTime()
    {
        Time.timeScale = 1;
    }

    private IEnumerator FadeTimeCoroutine(float fadeTime, float lastScale = 0)
    {
        Time.timeScale = 1;

        while (Time.timeScale != lastScale)
        {
            Time.timeScale -= Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }

        Time.timeScale = lastScale;
    }

    public void FadeStopTime(float fadeTime, float lastScale = 0)
    {
        StartCoroutine(FadeTimeCoroutine(fadeTime, lastScale));
    }
}
