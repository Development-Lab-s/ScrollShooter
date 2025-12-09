using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkinList", menuName = "Scriptable Objects/SkinList")]
public class SkinListSO : ScriptableObject
{
    [field:SerializeField]public List<Sprite> Skin = new();
}
