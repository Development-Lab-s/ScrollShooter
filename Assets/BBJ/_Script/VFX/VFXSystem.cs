using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class VFXSystem : MonoBehaviour
{
    [SerializeField] public stringDictionary vfxs = new();

    // fx 리스트 데이터 리스트
    public void SpawnVFX(string name)
    {
        SpawnVFX(transform.position, name);
    }
    public VFXObj[] SpawnVFX(Vector2 pos,string name)
    {
        return vfxs[name].OnSpawnVFX(pos);
    }
    // action list
    // 인덱스

}
[Serializable]
public struct VFX
{
    [SerializeField] List<VFXObj> obj;
    [SerializeField] public UnityEvent lisner { get; set; }
    public VFXObj[] OnSpawnVFX(Vector3 pos)
    {
        VFXObj[] popVfx = new VFXObj[obj.Count];
        for (int i = 0; i < obj.Count; ++i)
        {
            //popVfx[i] = PoolManager.Instance.PopByName(obj[i].Name) as VFXObj;
            popVfx[i] = GameObject.Instantiate(obj[i], pos, quaternion.identity);
        }
        return popVfx;
    }
}
[Serializable]
public class stringDictionary : SerializableDictionary<string, VFX>{ };
