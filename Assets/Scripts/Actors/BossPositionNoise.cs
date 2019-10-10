using UnityEngine;
using System.Collections;

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
        }
        private void Start()
        {
            bossPS.Play();

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
            if (bossPS == null || StageManager.Instance.CurrentState != StageManager.GameState.BossWave) return;
            bossPS.GetParticles(particles);
            var pos = bossPS.transform.TransformPoint(particles[0].position);
            if (pos.y < 0) pos = new Vector3(pos.x, 0f, pos.z);
            boss.transform.position = pos;

            if (!boss) Destroy(gameObject);
        }
    }
}