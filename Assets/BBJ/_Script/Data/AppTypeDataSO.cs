using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockDataSO", menuName = "SO/BlockShape")]
public class AppTypeDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [SerializeField] private string fileType = "";
}
