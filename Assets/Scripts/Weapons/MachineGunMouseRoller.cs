using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;

namespace VRShooting
{
    public class MachineGunMouseRoller : IRollable
    {
        float elevationAngleLimit = 0f;
        float rollSpeed = 0f;

        public MachineGunMouseRoller(float elevationAngleLimit, float rollSpeed)
        {
            this.elevationAngleLimit = elevationAngleLimit;
            this.rollSpeed = rollSpeed;
        }

        public void Roll(IInputProvider inputProvider, ref Transform barrel)
        {
            var vec = inputProvider.InputVector * rollSpeed; 
            if (vec == Vector3.zero) return;

            var azimuthRot = Quaternion.Euler(0, vec.x, 0f);
            barrel.localRotation = azimuthRot * barrel.localRotation;
            barrel.localRotation = Quaternion.AngleAxis(vec.y, barrel.right) * barrel.localRotation;

            // 可動域制限を超えた場合クランプを掛ける
            var eulerX = barrel.localRotation.eulerAngles.x;
            // 仰角が無回転時0fの状態から手前に旋回すると360fにループするのでそれ用のclamp変数
            var elevationAngleUpperLimit = 360f - elevationAngleLimit;
            if (eulerX > elevationAngleLimit && eulerX < elevationAngleUpperLimit)
            {
                /// ひとまず閾値を超えた時<see cref="elevationAngleLimit"/>を引いた値が<see cref="elevationAngleLimit"/>以上の場合は
                /// 最低でも<see cref="elevationAngleLimit"/>*2以上あるので360側にループした回転値だと分かる.その為暫定でこの判定にしている
                var xClamp = eulerX - elevationAngleLimit > elevationAngleLimit ? elevationAngleUpperLimit : elevationAngleLimit;
                barrel.localRotation = Quaternion.Euler(new Vector3(xClamp, barrel.localRotation.eulerAngles.y, 0f));
            }
        }
    }
}
