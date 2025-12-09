using System;
using System.Collections.Generic;
using UnityEngine;

public class VFXSystem : MonoBehaviour
{
    [SerializeField] public SerializableDictionary<string, VFX> vfxs = new();

    public void SpawnVFX(string name)
    {
        SpawnVFX(transform.position, name);
    }
    public VFXObj[] SpawnVFX(Vector2 pos, string name)
    {
        return vfxs[name].OnSpawnVFX(pos);
    }
    
}
[Serializable]
public struct VFX
{
    [SerializeField] List<VFXListSO> obj;
    public VFXObj[] OnSpawnVFX(Vector3 pos)
    {
        VFXObj[] popVfx = new VFXObj[obj.Count];
        for (int i = 0; i < obj.Count; ++i)
        {
            List<VFXObj> list = obj[i];
            for (int j = 0; j < list.Count; ++j)
            {
                var item = list[j];
                var vfx = PoolManager.Instance.PopByName(item.name) as VFXObj;
                vfx.transform.position = pos;
                popVfx[i] = vfx;
            }
        }
        return popVfx;
    }
}
