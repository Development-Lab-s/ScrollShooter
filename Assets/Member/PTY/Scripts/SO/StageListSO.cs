using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageList", menuName = "SO/StageList")]
public class StageListSO : ScriptableObject
{
    public List<Member.PTY.Scripts.SO.StageSO> stageList;
}
