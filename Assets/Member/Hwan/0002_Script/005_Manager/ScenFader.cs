using System;
using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public Action FadeInEnded;
    public Action EndFadeOut;

    public bool IsSceneFading { get; private set; }
    [SerializeField] private Material _material;
    private readonly int _fadeID = Shader.PropertyToID("_HalftoneFade");

    public void StartFadeIn(float fadeTime)
    {
        if (fadeTime <= 0 || IsSceneFading) return;
        IsSceneFading = true;
        StartCoroutine(FadeIn(fadeTime));
    }
    public void StartFadeOut(float fadeTime)
    {
        if (fadeTime <= 0 || IsSceneFading) return;
        IsSceneFading = true;
        StartCoroutine(FadeOut(fadeTime));
    }
    private IEnumerator FadeIn(float fadeTime)
    {
        float elapseTime = 0;
        while (true)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            elapseTime += Time.unscaledDeltaTime;
            elapseTime = Mathf.Clamp(elapseTime, 0, fadeTime);

            float p = Mathf.Lerp(-10f, 10f, elapseTime / fadeTime);

            _material.SetFloat(_fadeID, p);

            if (elapseTime >= fadeTime)
            {
                IsSceneFading = false;
                FadeInEnded?.Invoke();
                yield break;
            }
        }
    }
    private IEnumerator FadeOut(float fadeTime)
    {
        float elapseTime = 0;
        while (true)
        {
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            elapseTime += Time.deltaTime;
            elapseTime = Mathf.Clamp(elapseTime, 0, fadeTime);

            float p = Mathf.Lerp(10f, -10f, elapseTime / fadeTime);

            _material.SetFloat(_fadeID, p);

            if (elapseTime >= fadeTime)
            {
                EndFadeOut?.Invoke();
                IsSceneFading = false;
                yield break;
            }
        }
    }
}
