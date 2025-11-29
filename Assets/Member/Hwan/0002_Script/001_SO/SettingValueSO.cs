using UnityEngine;

[CreateAssetMenu(fileName = "SettingValueSO", menuName = "HwanSO/SettingValueSO")]
public class SettingValueSO : ScriptableObject
{
    [field: SerializeField] public float MaxValue { get; private set; }
    [field: SerializeField] public float MinValue { get; private set; }
    [field: SerializeField] public SettingType MyType { get; private set; }
    [field: SerializeField]  public string Text { get; private set; }
}
