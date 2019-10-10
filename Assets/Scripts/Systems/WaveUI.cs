using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace VRShooting
{
    public class WaveUI : ManagedMono
    {
        [SerializeField] int waitMilliSecconds = 2000;
        [SerializeField] float linearAmount = 0.1f;
        [SerializeField] GameObject WaveUIObj;
        [SerializeField] Transform barrel;
        [SerializeField] Transform crossHair;
        [SerializeField] Text textBox;
        [SerializeField] [Multiline(3)] string firstWave;
        [SerializeField] [Multiline(3)] string secondWave;
        [SerializeField] [Multiline(3)] string bossWave;

        StageManager stageManager;
        protected override void Awake()
        {
            base.Awake();
            stageManager = StageManager.Instance;
            stageManager.OnGameStateEnter += DisplayInfoAsync;
            WaveUIObj.SetActive(false);
        }

        public override void MFixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, crossHair.position +crossHair.up,linearAmount);
            transform.rotation = Quaternion.Lerp(transform.rotation, barrel.rotation, linearAmount);
        }

        private async void DisplayInfoAsync(StageManager.GameState gameState)
        {
            switch (gameState)
            {
                case StageManager.GameState.FirstWave:
                    await DisplayWaveInfoAsync(firstWave);
                    break;
                case StageManager.GameState.SecondWave:
                    await DisplayWaveInfoAsync(secondWave);
                    break;
                case StageManager.GameState.BossWave:
                    await DisplayWaveInfoAsync(bossWave);
                    break;
                case StageManager.GameState.GameCrear:
                case StageManager.GameState.GameOver:
                case StageManager.GameState.None:
                case StageManager.GameState.InitState:
                    break;
                default:
                    break;
            }
        }
        public async Task DisplayWaveInfoAsync(string textScript)
        {
            WaveUIObj.SetActive(true);
            textBox.text = textScript;
            await Task.Delay(waitMilliSecconds);
            WaveUIObj.SetActive(false);
        }
    }
}