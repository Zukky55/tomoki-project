﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                var vec = Vector3.zero;
                switch (clamp)
                {
                    case Clamp.Both:
                        vec = new Vector3(horizontal, vertical, 0f);
                        break;
                    case Clamp.Horizontal:
                        vec = new Vector3(horizontal, 0f, 0f);
                        break;
                    case Clamp.Vertical:
                        vec = new Vector3(0f, vertical, 0f);
                        break;
                    case Clamp.BothNormalized:
                        vec = new Vector3(horizontal, vertical, 0f).normalized;
                        break;
                    case Clamp.HorizontalNormalized:
                        vec = new Vector3(horizontal, 0f, 0f).normalized;
                        break;
                    case Clamp.VerticalNormalized:
                        vec = new Vector3(0f, vertical, 0f).normalized;
                        break;
                    default:
                        throw new ArgumentException();
                }
                vec *= status.RollSpd;
                return vec;
            }
        }

        /// <summary>機関銃のパラメーター</summary>
        [SerializeField] [Header("機関銃のパラメーター")] GunStatus masterData;
        /// <summary>玉の射出座標</summary>
        [SerializeField] [Header("玉の射出座標")] Transform muzzleNode;
        /// <summary>Barrel's transform</summary>
        [SerializeField] Transform barrel;
        /// <summary>Clamp of <see cref="InputVector"/></summary>
        [SerializeField] Clamp clamp;
        /// <summary><see cref="barrel"/>の仰角の回転制限値の閾値</summary>
        [SerializeField] float elevationAngleLimit = 45f;
        GunStatus status;

        protected override void Awake()
        {
            base.Awake();
            status = Instantiate(masterData);
        }

        /// <summary>
        /// Managed Update
        /// </summary>
        public override void MUpdate()
        {
            SetInputVector();
            Roll();
            Fire();
        }

        /// <summary>
        /// 機関銃の旋回処理
        /// </summary>
        private void Roll()
        {
            if (InputVector == Vector3.zero) return;

            var azimuthRot = Quaternion.Euler(0, InputVector.x, 0f);
            barrel.localRotation = azimuthRot * barrel.localRotation;
            barrel.localRotation = Quaternion.AngleAxis(InputVector.y, barrel.right) * barrel.localRotation;

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

        /// <summary>銃弾発射してからの経過時間</summary>
        float elapsedTimeSinseFire = 0f;
        /// <summary>
        /// 銃弾発射処理.
        /// </summary>
        public void Fire()
        {
            // InitStateのstateの時だけ撃ったベクトルにRayを飛ばしてButtonを押す。
            if (StageManager.Instance.CurrentState == StageManager.GameState.InitState)
            {
                var ray = Camera.main.ScreenPointToRay(barrel.forward);
                var hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    Button button;
                    if (hit.collider.TryGetComponent<Button>(out button))
                    {
                        button.onClick.Invoke();
                        button.interactable = false;
                    }
                }
            }

            elapsedTimeSinseFire += Time.deltaTime;
            if (!Input.GetMouseButton(0) || elapsedTimeSinseFire < status.FireInterval) return;
            var go = Instantiate(status.BulletPrefab, muzzleNode.position + muzzleNode.forward, Quaternion.identity);
            var bullet = go.GetComponent<Bullet>();
            bullet.GiveInitialVelocity(muzzleNode.forward * status.BulletSpd);
            elapsedTimeSinseFire = 0f;
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