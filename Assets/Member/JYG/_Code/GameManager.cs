using System;
using UnityEngine;

namespace Member.JYG._Code
{
    public class GameManager : MonoBehaviour
    {
        public Player Player { get; private set; }

        private void Start()
        {
            Player = FindFirstObjectByType<Player>();
        }
    }
}
