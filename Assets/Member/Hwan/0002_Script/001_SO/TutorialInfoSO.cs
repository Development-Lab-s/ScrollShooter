using UnityEngine;

[CreateAssetMenu(fileName = "TutorialInfoSO", menuName = "Scriptable Objects/TutorialInfoSO")]
public class TutorialInfoSO : ScriptableObject
{
    [field: SerializeField] public int Phase { get; private set; }
    [field: SerializeField] public string Text { get; private set; }
    [field: SerializeField] public InteractiveType NeedInput { get; private set; }
    [field: SerializeField] public Vector2 PopUpPos { get; private set; }
}
