using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    /// <summary>
    /// 機関銃.
    /// </summary>
    public class MachineGun : ManagedMono
    {
        /// <summary>機関銃のパラメーター</summary>
        public GunStatus Status => status;
        //public Vector2 InputVector =>

        [SerializeField] [Header("機関銃のパラメーター")] GunStatus status;
        [SerializeField] [Header("発射口")] Transform muzzle;

        float horizontal;
        float vertical;

        public void Fire()
        {
            var bullet = Instantiate(status.Bullet, muzzle.position, Quaternion.identity);
            // TODO: bulletに初速を与えるか、bullet自体が勝手に動くかどっちかにして実装
        }

        /// <summary>前フレームのmouse position</summary>
        Vector3 mousePrevPos = Vector3.zero;
        /// <summary>
        /// Managed Update
        /// </summary>
        public override void ManagedUpdate()
        {
            // mouseの差分からInput成分取得
            var mouseDiff = Input.mousePosition - mousePrevPos;
            if (mouseDiff != Vector3.zero)
            {
                horizontal = mouseDiff.x;
                vertical = mouseDiff.y;
                mousePrevPos = Input.mousePosition;
            }
        }
    }
}