using System.Collections;
using System.Collections.Generic;
using Member.JYG._Code;
using Unity.Cinemachine;
using UnityEngine;
using YGPacks;

public enum ImpulseType
{
    RECOIL,
    BUMP,
    EXPLOSION,
    RUMBLE,
    SHAKE
}
public class CameraShaker : Singleton<CameraShaker>
{
    [SerializeField] private CinemachinePositionComposer targetCamera;
    public List<CameraShakeItem> shakeItems = new List<CameraShakeItem>();
    [SerializeField] private float roughness;      //거칠기 정도
    [SerializeField] private float magnitude;      //움직임 범위

    protected override void Awake()
    {
        base.Awake();
    }

    public void ImpulseCamera(ImpulseType type, float power)
    {
        CinemachineImpulseSource source = null;
        foreach (CameraShakeItem impulseType in shakeItems)
        {
            if (impulseType.type == type)
            {
                source = impulseType.impulse;
                break;
            }
        }

        if (source != null)
        {
            source.GenerateImpulse(power);
        }
        else if (type == ImpulseType.SHAKE)
        {
            StartCoroutine(Shake(power));
            Debug.Log("흔들기");
        }
    }

    private IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-2f, 2f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * roughness;
            targetCamera.Composition.ScreenPosition = new Vector3(Mathf.PerlinNoise(tick, 0) - .5f, Mathf.PerlinNoise(0, tick) - .5f, 0f) * (magnitude * Mathf.PingPong(elapsed, halfDuration));

            yield return null;
        }
    }
}

[System.Serializable]
public class CameraShakeItem
{
    public CinemachineImpulseSource impulse;
    public ImpulseType type;
}
