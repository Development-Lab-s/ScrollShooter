using UnityEngine;

public enum BlockType
{
    DontBreak,
    DashBreak,
    Break
}
[CreateAssetMenu(fileName = "BlockDataSO", menuName = "SO/Block")]
public class BlockDataSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite Icon { get; private set; }
    [field: SerializeField]
    public string[] Names { get; private set; }
    [field: SerializeField]
    public BlockType Type { get; private set; }

    public string GetRendomName()
    {
        if (Names == null)
            return "";
        string name = Names[Random.Range(0, Names.Length)];
        if (name == null) name = "";
        return name;
    }
    // 경도가 있으면 좋을듯

}
