using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationDataSO", menuName = "Scriptable Objects/AnimationDataSO")]
public class AnimationHashDataSO : ScriptableObject,ISerializationCallbackReceiver
{
    [SerializeField] private string animationString;
    public int AnimationHash { get; private set; }
    public void OnBeforeSerialize(){}
    public void OnAfterDeserialize()
    {
        AnimationHash = Animator.StringToHash(animationString);
    }
    public static implicit operator int(AnimationHashDataSO data)
    {
        return data.AnimationHash;
    }
}
