using System;
using UnityEngine;

namespace Member.JYG._Code
{
    public class ObjectViewportKeeper : MonoBehaviour
    {
        private PolygonCollider2D _collider2D;
        private Player _player;
        [field: SerializeField] public float Offset { get; private set; }

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _collider2D = _player.GetComponent<PolygonCollider2D>();
        }

        void Update()
        {
            
            

        }
    }
}
