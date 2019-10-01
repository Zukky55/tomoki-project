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
        [SerializeField] float elevationAngleLimit = 45f;

        /// <summary>
        /// Managed Update
        /// </summary>
        public override void MUpdate()
        {
            SetInputVector();

            if (InputVector != Vector3.zero)
            {
                Roll();
            }
        }


        Vector2 rollBuffer = new Vector2();
        private void Roll()
        {
            // マウススクロールした分を加算
            //rollBuffer.x += InputVector.y;
            //rollBuffer.y += InputVector.x;

            if (InputVector.x != 0f)
            {

                var tripodRot = Quaternion.Euler(0f, InputVector.x * status.RollSpd, 0f);
                var barrelRot = Quaternion.Euler(InputVector.y * status.RollSpd, 0f, 0f);
                transform.rotation = tripodRot * transform.rotation;
                barrel.localRotation = barrelRot * barrel.localRotation;
                //Debug.Log($"x = {Mathf.Abs(barrel.localRotation.eulerAngles.x) }");
                // 可動域制限を超えた場合クランプを掛ける
                var eulerX = barrel.localRotation.eulerAngles.x;
                if (eulerX > elevationAngleLimit && eulerX < 315f)
                {
                    Debug.Log($"barrel.localRotation.eulerAngles.x  = {barrel.localRotation.eulerAngles.x }");
                    var xClamp = eulerX - elevationAngleLimit > elevationAngleLimit ? 315f : elevationAngleLimit;
                    barrel.localRotation = Quaternion.Euler(new Vector3(xClamp, 0f, 0f));
                }
            }
        }

        public void Fire()
        {
            var bullet = Instantiate(status.Bullet, muzzleNode.position, Quaternion.identity);
            // TODO: bulletに初速を与えるか、bullet自体が勝手に動くかどっちかにして実装

        }

        /// <summary>前フレームのmouse position</summary>
        Vector3 mousePrevPos = Vector3.zero;
        /// <summary>Input X axis.</summary>
        float horizontal = 0f;
        /// <summary>Input Y axis.</summary>
        float vertical = 0f;

        /// <summary>
        /// <see cref="Input.mousePosition"/>の差分から<see cref="InputVector"/>の成分取得。
        /// </summary>
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
            //Debug.Log($"horizontal = {InputVector.x}, vertical = {InputVector.y}");
        }
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