using System.Collections.Generic;
using UnityEngine;

namespace Member.PTY.Scripts.SO
{
    [CreateAssetMenu(fileName = "SkinList", menuName = "SO/SkinList")]
    public class SkinListSO : ScriptableObject
    {
        public List<SkinSO> skinList;
    }
}

