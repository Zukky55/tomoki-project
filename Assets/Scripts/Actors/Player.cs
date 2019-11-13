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
        [SerializeField] DamageEffectFader damageEffectFader;

        StageManager stageManager;

        private void Start()
        {
            stageManager = StageManager.Instance;
        }

        public override void MUpdate()
        {
            // TODO: コントローラー完成したら消す
#if true
            var diff = barrel.localRotation.eulerAngles.y - transform.localRotation.eulerAngles.y;
            transform.RotateAround(barrel.position, Vector3.up, diff);
            //transform.localRotation = Quaternion.Lerp(transform.localRotation, barrel.localRotation, 0.9f);
#endif
        }

        public void TakeDamage(int pow)
        {
            if(stageManager.CurrentState == StageManager.GameState.GameOver) return;
            // TODO post processing stack使って画面をどんどん赤くする
            hp -= pow;
            damageEffectFader.Fade();
            if (hp <= 0 )
            {
                stageManager.SetState(StageManager.GameState.GameOver);
            }
        }
    }
}