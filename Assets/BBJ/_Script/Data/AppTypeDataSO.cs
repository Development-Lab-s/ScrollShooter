using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockDataSO", menuName = "SO/App/Type")]
public class AppTypeDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string fileType { get; set; } = "";
    [field: SerializeField] public Color textColor { get; set; } = Color.white;
}
