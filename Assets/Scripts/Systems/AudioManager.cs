using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRShooting
{
    public class AudioManager : ManagedMono
    {
        [SerializeField]
        BGMList bgmList;
        [SerializeField] AudioSource audioSource;

        private void OnEnable() => StageManager.Instance.OnGameStateEnter += OnGameStateEnter;
        private void OnDisable() => StageManager.Instance.OnGameStateEnter -= OnGameStateEnter;

        private void OnGameStateEnter(StageManager.GameState gameState)
        {
            if (gameState == StageManager.GameState.SecondWave) return;
            BGMList.BGMMaterial mat = null;
            foreach (var m in bgmList.Materials)
            {
                if(m.Tag == gameState)
                {
                    mat = m;
                }
            }
            if (mat == null) return;
            var clip = mat.Clip;
            audioSource.clip = clip;
            audioSource.loop = mat.IsLoop;
            audioSource.Play();
        }


        [Serializable]
        public class BGMList
        {
            public List<BGMMaterial> Materials => materials;

            [SerializeField]
            List<BGMMaterial> materials;

            [Serializable]
            public class BGMMaterial
            {
                public AudioClip Clip => clip;
                public bool IsLoop => isLoop;
                public StageManager.GameState Tag => tag;

                [SerializeField]
                AudioClip clip;

                [SerializeField]
                StageManager.GameState tag;

                [SerializeField]
                bool isLoop;
            }
        }
    }
}
