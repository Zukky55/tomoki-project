using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VRShooting
{
    public class BossPositionNoise : ManagedMono
    {
        [SerializeField] GameObject bossPrefab;

        ParticleSystem bossPS;

        protected override void Awake()
        {
            base.Awake();
            bossPS = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[bossPS.emission.burstCount];
        }
        private void Start()
        {
            StartCoroutine(EmitTheBoss());
        }
        GameObject boss;
        ParticleSystem.Particle[] particles;
        IEnumerator EmitTheBoss()
        {
            bossPS.Play();
            yield return new WaitUntil(() => bossPS.particleCount >= bossPS.emission.burstCount);
            particles = new ParticleSystem.Particle[bossPS.particleCount];
            bossPS.GetParticles(particles);
            boss = Instantiate(bossPrefab, new Vector3(100, 100, 100), transform.rotation, transform);
            boss.transform.localPosition = particles[0].position;
        }
        public override void MUpdate()
        {
            if (StageManager.Instance.CurrentState != StageManager.GameState.BossWave) return;
            if (bossPS.particleCount >= bossPS.emission.burstCount
                && boss
                && !isToDeath())
            {
                var pos = bossPS.transform.TransformPoint(particles[0].position);
                if (pos.y < 0) pos = new Vector3(pos.x, 0f, pos.z);
                boss.transform.position = pos;
            }
        }

        Animator bossAnim;
        bool isToDeath()
        {
            if (!bossAnim) bossAnim = boss.GetComponent<Animator>();
            return bossAnim.GetBool("ToDeath");
        }
    }
}