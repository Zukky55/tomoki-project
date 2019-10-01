using System;
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
        /// <summary>マウスのスクロール差分ベクトル</summary>
        public Vector3 InputVector
        {
            get
            {
                switch (clamp)
                {
                    case Clamp.Both:
                        return new Vector3(horizontal, vertical, 0f);
                    case Clamp.Horizontal:
                        return new Vector3(horizontal, 0f, 0f);
                    case Clamp.Vertical:
                        return new Vector3(0f, vertical, 0f);
                    case Clamp.BothNormalized:
                        return new Vector3(horizontal, vertical, 0f).normalized;
                    case Clamp.HorizontalNormalized:
                        return new Vector3(horizontal, 0f, 0f).normalized;
                    case Clamp.VerticalNormalized:
                        return new Vector3(0f, vertical, 0f).normalized;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        /// <summary>機関銃のパラメーター</summary>
        [SerializeField] [Header("機関銃のパラメーター")] GunStatus status;
        /// <summary>玉の射出座標</summary>
        [SerializeField] [Header("玉の射出座標")] Transform muzzleNode;
        /// <summary>Barrel's transform</summary>
        [SerializeField] Transform barrel;
        /// <summary>Clamp of <see cref="InputVector"/></summary>
        [SerializeField] Clamp clamp;
        /// <summary><see cref="barrel"/>の仰角の回転制限値の閾値</summary>
        [SerializeField] float elevationAngleLimit = 45f;


        /// <summary>
        /// Managed Update
        /// </summary>
        public override void MUpdate()
        {
            SetInputVector();
            Roll();
            if (Input.GetMouseButton(0)) Fire();
        }

        /// <summary>
        /// 機関銃の旋回処理
        /// </summary>
        private void Roll()
        {
            if (InputVector == Vector3.zero) return;

            var tripodRot = Quaternion.Euler(0f, InputVector.x * status.RollSpd, 0f);
            var barrelRot = Quaternion.Euler(InputVector.y * status.RollSpd, 0f, 0f);
            transform.rotation = tripodRot * transform.rotation;
            barrel.localRotation = barrelRot * barrel.localRotation;
            // 可動域制限を超えた場合クランプを掛ける
            var eulerX = barrel.localRotation.eulerAngles.x;
            // 仰角が無回転時0fの状態から手前に旋回すると360fにループするのでそれ用のclamp変数
            var elevationAngleUpperLimit = 360f - elevationAngleLimit;
            if (eulerX > elevationAngleLimit && eulerX < elevationAngleUpperLimit)
            {
                /// ひとまず閾値を超えた時<see cref="elevationAngleLimit"/>を引いた値が<see cref="elevationAngleLimit"/>以上の場合は
                /// 最低でも<see cref="elevationAngleLimit"/>*2以上あるので360側にループした回転値だと分かる.その為暫定でこの判定にしている
                var xClamp = eulerX - elevationAngleLimit > elevationAngleLimit ? elevationAngleUpperLimit : elevationAngleLimit;
                barrel.localRotation = Quaternion.Euler(new Vector3(xClamp, 0f, 0f));
            }
        }

        public void Fire()
        {
            // TODO: bulletに初速を与えるか、bullet自体が勝手に動くかどっちかにして実装
            var go = Instantiate(status.Bullet, muzzleNode.position + muzzleNode.forward, Quaternion.identity);
            var bullet = go.GetComponent<Bullet>();
            bullet.Init(muzzleNode.position);
        }

        /// <summary>前フレームのmouse position</summary>
        Vector3 mousePrevPos = Vector3.zero;
        /// <summary>Input X axis.</summary>
        float horizontal = 0f;
        /// <summary>Input Y axis.</summary>
        float vertical = 0f;

        /// <summary>
        /// マウスのインプット取得処理.
        /// </summary>
        /// <remarks>
        /// <see cref="Input.mousePosition"/>の差分から<see cref="InputVector"/>の成分取得.
        /// </remarks>
        void SetInputVector()
        {
            var mouseDiff = Input.mousePosition - mousePrevPos;
            if (mouseDiff != Vector3.zero)
            {
                horizontal = mouseDiff.x;
                vertical = mouseDiff.y;
                mousePrevPos = Input.mousePosition;
            }
            else
            {
                horizontal = vertical = 0f;
            }
        }

        /// <summary>
        /// <see cref="InputVector"/>に制限を掛けるか,どう掛けるかを設定するenum
        /// </summary>
        public enum Clamp
        {
            Both,
            Horizontal,
            Vertical,
            BothNormalized,
            HorizontalNormalized,
            VerticalNormalized,
        }
    }
}