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
    /// <summary>
    /// 機関銃.
    /// </summary>
    public class MachineGun : ManagedMono
    {
        /// <summary>機関銃のパラメーター</summary>
        public GunStatus Status => status;

        /// <summary>玉の射出座標</summary>
        [SerializeField] [Header("玉の射出座標")] Transform muzzle;
        /// <summary>Barrel's transform</summary>
        [SerializeField] Transform barrel;
        /// <summary>Transform of CrossHair</summary>
        [SerializeField] Transform crossHair;
        /// <summary>The casing position</summary>
        [SerializeField] Transform casingNode;

        /// <summary>機関銃のパラメーター</summary>
        [SerializeField] [Header("機関銃のパラメーター")] GunStatus masterData;
        /// <summary>銃弾のprefab</summary>
        [SerializeField] [Header("Casing effect")] GameObject casingEffect;
        /// <summary>発射エフェクト</summary>
        [SerializeField] [Header("発射エフェクト")] ParticleSystem fireEffect;
        /// <summary>Reload SE</summary>
        [SerializeField] [Header("Reload SE")] GameObject reloadSEObj;
        /// <summary>The animator of Machinegun.</summary>
        [SerializeField] Animator animator;
        [SerializeField] Transform playerEye;

        /// <summary>Clamp of <see cref="InputVector"/></summary>
        [SerializeField] Clamp clamp;
        /// <summary><see cref="barrel"/>の仰角の回転制限値の閾値</summary>
        [SerializeField] float elevationAngleLimit = 45f;
        /// <summary>Variable for storing <see cref="masterData"/></summary>
        GunStatus status;

        /// <summary>
        /// input provider
        /// </summary>
        IInputProvider inputProvider = null;

        /// <summary>
        /// MachineGun roller
        /// </summary>
        IRollable roller =null;

        protected override void Awake()
        {
            base.Awake();
            status = Instantiate(masterData);
            inputProvider = GetComponent<IInputProvider>();
            roller = new MachineGunOculusTouchRoller(elevationAngleLimit,status.RollSpd);
        }
        
        /// <summary>
        /// Managed Update
        /// </summary>
        public override void MUpdate()
        {
            roller.Roll(inputProvider, ref barrel);
            Fire();
            Debug.DrawLine(muzzle.position, crossHair.position, Color.red);
        }

        /// <summary>銃弾発射してからの経過時間</summary>
        float elapsedTimeSinseFire = 0f;
        /// <summary>リロードしてからの発射弾数</summary>
        int fireCountSinceReloaded = 0;
        /// <summary>reload中かどうか</summary>
        bool isReloading = false;
        /// <summary>
        /// 銃弾発射処理.
        /// </summary>
        public void Fire()
        {
            elapsedTimeSinseFire += Time.deltaTime;

            if (!Input.GetMouseButton(1)
                || elapsedTimeSinseFire < status.FireInterval
                || isReloading)
            {
                return;
            }

            ++fireCountSinceReloaded;

            // InitStateのstateの時だけ撃ったベクトルにRayを飛ばしてButtonを押す。
            if (StageManager.Instance.CurrentState == StageManager.GameState.InitState) PerformButtonRayCastingAndProcessing();

            animator.SetTrigger(MachinegunAnimParam.Shoot.ToString());
            Instantiate(casingEffect, casingNode.position, Quaternion.identity);
            fireEffect.Play(true);
            elapsedTimeSinseFire = 0f;

            var dir = (crossHair.position - playerEye.position).normalized;
            var ray = new Ray(playerEye.position, dir);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Component component;
                Enemy enemy;
                if (!hit.collider.TryGetComponent(typeof(Enemy), out component)) return;
                enemy = component as Enemy;
                enemy.TakeDamage(status.BulletPow);
            }

            if (fireCountSinceReloaded >= status.Magazine)
            {
                ReloadAsync();
            }
        }

        /// <summary>
        /// The Reload on async.
        /// </summary>
        async void ReloadAsync()
        {
            isReloading = true;
            animator.SetTrigger(MachinegunAnimParam.DoReload.ToString());
            Instantiate(reloadSEObj, transform.position, Quaternion.identity);

            var animInfo = animator.GetCurrentAnimatorClipInfo(0);
            await UniTask.WaitUntil(() =>
            {
                animInfo = animator.GetCurrentAnimatorClipInfo(0);
                if (animInfo.Length == 0) return false;

                return animInfo[0].clip.name == "MachineGun_reload";
            });

            await UniTask.Delay(TimeSpan.FromSeconds(animInfo[0].clip.length));

            // reset.
            fireCountSinceReloaded = 0;
            isReloading = false;
        }

        /// <summary>
        /// ボタンのraycastをし、見つかった場合はonClick処理を実行する
        /// </summary>
        void PerformButtonRayCastingAndProcessing()
        {
            var viewportOfCrossHair = Camera.main.WorldToScreenPoint(crossHair.position /*- barrel.position*/);
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = viewportOfCrossHair
            };
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);
            if (raycastResults.Any())
            {
                RaycastResult detectedUIOfButton = raycastResults.FirstOrDefault(ui => ui.gameObject.CompareTag("Button"));
                if (detectedUIOfButton.gameObject)
                {
                    var button = detectedUIOfButton.gameObject.GetComponent<Button>();
                    button.onClick.Invoke();
                    button.gameObject.SetActive(false);
                }
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
        public enum MachinegunAnimParam
        {
            DoOpen,
            DoReload,
            Shoot,
        }
    }
}