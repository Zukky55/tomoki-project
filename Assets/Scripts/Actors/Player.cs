using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace VRShooting
{
    public class Player : ManagedMono
    {
        /// <summary>hit point</summary>
        public int Hp { get => hp; set => hp = value; }

        [SerializeField] [Header("Hit Point")] int hp;
        [SerializeField] Transform barrel;

        public override void MFixedUpdate()
        {
            // TODO: コントローラー完成したら消す
#if false
            var diff = barrel.localRotation.eulerAngles.y - transform.localRotation.eulerAngles.y;
            transform.RotateAround(barrel.position, Vector3.up, diff);
#endif
        }

        public void TakeDamage(int pow)
        {
            // TODO post processing stack使って画面をどんどん赤くする
            hp -= pow;
            if (hp <= 0)
            {
                StageManager.Instance.SetState(StageManager.GameState.GameOver);
            }
        }
    }
}