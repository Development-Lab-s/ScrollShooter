using UnityEngine;

namespace Member.PTY.Scripts.SO
{
    [CreateAssetMenu(fileName = "Stage", menuName = "SO/Stage")]
    public class StageSO : ScriptableObject
    {
        public string stageName;
        public int stageIndex;
    }
}