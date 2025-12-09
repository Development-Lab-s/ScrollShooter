using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinList", menuName = "SO/SkinList")]
public class SkinListSO : ScriptableObject
{
    public List<SkinSO> skinList;
}
