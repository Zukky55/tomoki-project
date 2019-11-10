using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx.Async;

namespace VRShooting
{
    /// <summary>
    /// 蜂の群れ.
    /// </summary>
    public class BeeFlock : ManagedMono
    {
        public int SurvivingBeesCount => bees.Count(bee => bee != null);

        public bool AnySurvivingBees => bees != null ? bees.Any(bee => bee != null) : true;//これはもう応急処置

        /// <summary>Emitする蜂の群れの個体数</summary>
        [SerializeField] short populationOfFlock;
        /// <summary>蜂のprefab</summary>
        [SerializeField] GameObject beePrefab;
        /// <summary>particle system of "Bee flock"</summary>
        ParticleSystem flockingPS;

        protected override void Awake()
        {
            base.Awake();
            flockingPS = GetComponent<ParticleSystem>();
        }
        private void Start()
        {
            //StartCoroutine(EmitTheBees());
        }

        /// <summary>生成した蜂の群れ</summary>
        GameObject[] bees = null;
        /// <summary>生成したそれぞれのParticle</summary>
        ParticleSystem.Particle[] particles;
        Animator[] animators;
        /// <summary>
        /// Particleを<see cref="populationOfFlock"/>分emitし,その数分<see cref="Bee"/>を生成する。
        /// </summary>
        /// <returns>Emissionが終わって<see cref="ParticleSystem.particleCount"/>に反映される迄の待機時間</returns>
        public async void EmitTheBeesAsync()
        {
            flockingPS.emission.SetBurst(0, new ParticleSystem.Burst(0, populationOfFlock, populationOfFlock, 1, 0.01f));
            flockingPS.Play();
            await UniTask.WaitUntil(() => flockingPS.particleCount >= flockingPS.emission.burstCount);
            bees = new GameObject[flockingPS.particleCount];
            animators = new Animator[flockingPS.particleCount];
            particles = new ParticleSystem.Particle[flockingPS.particleCount];
            flockingPS.GetParticles(particles);
            for (int index = 0; index < particles.Length; index++)
            {
                // vector3に100入れてるのは,world coordinates原点に出すと1フレームだけ画面に映ってしまうから
                bees[index] = Instantiate(beePrefab, new Vector3(100, 100, 100), transform.rotation, transform);
                animators[index] = bees[index].GetComponent<Animator>();
                bees[index].transform.localPosition = particles[index].position;
            }
        }

        /// <summary>
        /// 群れのそれぞれの個体の座標を<see cref="flockingPS"/>がemitしているそれぞれのparticleの座標に合わせる。
        /// </summary>
        public override void MUpdate()
        {
            if (bees == null) return;
            flockingPS.GetParticles(particles);
            for (int index = 0; index < bees.Length; index++)
            {
                if (bees[index] == null || animators[index] == null) continue;
                if (animators[index].GetBool("ToDeath"))
                {
                    bees[index].transform.parent = null;
                    continue;
                }

                var pos = flockingPS.transform.TransformPoint(particles[index].position);
                if (pos.y < 0) pos = new Vector3(pos.x, 0f, pos.z);
                bees[index].transform.position = pos;
            }
        }
    }
}