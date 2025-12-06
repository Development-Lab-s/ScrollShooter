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

    public IEnumerator FadeStopTime(float fadeTime)
    {
        Time.timeScale = 1;

        while (Time.timeScale != 0)
        {
            Time.timeScale -= Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }

        Time.timeScale = 0;
    }
}
