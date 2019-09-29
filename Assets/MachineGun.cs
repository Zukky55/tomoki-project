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

        [SerializeField, Header("機関銃のパラメーター")] GunStatus status;

        public void Fire()
        {
            var bullet = Instantiate(status.Bullet, transform.position + Vector3.forward, Quaternion.identity);
            // TODO: bulletに初速を与えるか、bullet自体が勝手に動くかどっちかにして実装
        }

        /// <summary>
        /// Managed Update
        /// </summary>
        public override void ManagedUpdate()
        {

        }
    }
}