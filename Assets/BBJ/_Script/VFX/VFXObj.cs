using UnityEngine;
using YGPacks.PoolManager;

public class VFXObj : MonoBehaviour, IYgPoolable
{
    public string Name => name;
    public GameObject GameObject => gameObject;

    public void AppearanceItem(){}

    public void ResetItem(){}
    public void OnParticleTrigger()
    {
    }
    private void OnParticleSystemStopped()
    {
        Debug.Log("a");
        Destroy(gameObject);
        //PoolManager.Instance.Push(this);
    }

}
