using UnityEngine;
using YGPacks.PoolManager;

public class VFXObj : MonoBehaviour, IYgPoolable
{
    public string Name => name;
    public GameObject GameObject => gameObject;

    public void AppearanceItem(){}

    public void ResetItem(){}
    private void OnParticleSystemStopped()
    {
        //Destroy(gameObject);
        PoolManager.Instance.Push(this);
    }

}
