using UnityEngine;
using YGPacks;

namespace Member.JYG._Code
{
    [DefaultExecutionOrder(-1000)]
    public class GameManager : Singleton<GameManager>
    {
        public Player Player { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Player = FindFirstObjectByType<Player>();
        }
    }
}
