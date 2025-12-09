using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXDataSO", menuName = "SO/VFXListSO")]
public class VFXListSO : ScriptableObject
{
    [field: SerializeField]
    public List<VFXObj> vfxPrefabList { get; private set; }
    public static implicit operator List<VFXObj>(VFXListSO list)
    {
        return list.vfxPrefabList;
    }
}
