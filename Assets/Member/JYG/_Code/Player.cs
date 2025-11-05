using System;
using System.Collections.Generic;
using Member.JYG.Input;
using UnityEngine;

namespace Member.JYG._Code
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class Player : MonoBehaviour
    {
        [field:SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        [field:SerializeField] public float MaxSpeed { get; private set; }
        [field:SerializeField] public float ReverseForce { get; private set; }

        private float _xVelocity;

        public float XVelocity
        {
            get
            {
                return _xVelocity;
            }
            private set
            {
                if (Mathf.Abs(value) < MaxSpeed)
                {
                    _xVelocity = value;
                }
                else
                {
                    _xVelocity = Mathf.Sign(value) * MaxSpeed;
                }
            }
            
        }
        [field: SerializeField] public float MovePower { get; private set; } //유저가 가속할 때 더해주는 크기

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Rigidbody2D.gravityScale = 0;
        }

        private void Update()
        {
            float moveDir = PlayerInputSO.XMoveDir;
            if (moveDir == 1) //우측으로 이동한다.
            {
                if(XVelocity < -0.1f) // 우측으로 이동하는 도중에 좌측으로 이동하려 한다.
                    XVelocity += Time.deltaTime * MovePower * 2 * moveDir; //파워 2배로 증가
                    /*XVelocity = 0;*/
                
                XVelocity += Time.deltaTime * MovePower * moveDir;
            }
            else if (moveDir == -1) //좌측으로 이동한다.
            {
                if(XVelocity > 0.1f) // 좌측으로 이동하는 도중에 우측으로 이동하려 한다.
                    XVelocity +=  Time.deltaTime * MovePower * 2 * moveDir; //파워 2배로 증가
                /*    XVelocity = 0;*/
                
                XVelocity += Time.deltaTime * MovePower * moveDir;
            }
            
            //겹치는 코드가 많은데 함수로 빼서 10줄정도로 줄이자.
            
        }

        private void FixedUpdate()
        {
            SetXMove(XVelocity);
        }


        private void SetXMove(float speed)
        {
            Rigidbody2D.linearVelocityX = speed;
        }
    }
}
