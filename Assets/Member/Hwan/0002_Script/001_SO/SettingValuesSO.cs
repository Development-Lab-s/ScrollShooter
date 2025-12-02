using UnityEngine;

[CreateAssetMenu(fileName = "SettingValuesSO", menuName = "Scriptable Objects/SettingValuesSO")]
public class SettingValuesSO : ScriptableObject
{
    [field: SerializeField] public SettingValueSO[] SettingValues { get; private set; }
}
