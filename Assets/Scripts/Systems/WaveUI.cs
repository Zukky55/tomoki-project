using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

namespace VRShooting
{
    public class WaveUI : ManagedMono
    {
        [SerializeField] float linearAmount = 0.1f;
        [SerializeField] GameObject WaveUIObj;
        [SerializeField] Transform barrel;
        [SerializeField] Transform crossHair;
        [SerializeField] Text textBox;
        [SerializeField] List<UIMaterial> uiMats;

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
            transform.position = Vector3.Lerp(transform.position, crossHair.position + crossHair.up, linearAmount);
            transform.rotation = Quaternion.Lerp(transform.rotation, barrel.rotation, linearAmount);
        }

        private async void DisplayInfoAsync(StageManager.GameState gameState)
        {
            var mat = uiMats.FirstOrDefault(m => m.TargetState == gameState);
            if (mat != null) await DisplayWaveInfoAsync(mat);
        }

        async UniTask DisplayWaveInfoAsync(UIMaterial mat)
        {
            WaveUIObj.SetActive(true);
            textBox.text = mat.Script;
            textBox.color = mat.TextColor;
            await UniTask.Delay(TimeSpan.FromSeconds(mat.WaitSecond));
            if (WaveUIObj != null)
            {
                WaveUIObj.SetActive(false);
            }
        }

        [Serializable]
        class UIMaterial
        {
            public string Script => script;
            public Color TextColor => textColor;
            public StageManager.GameState TargetState => targetState;
            public int WaitSecond => waitMilliSecconds;
            [SerializeField] Color textColor;
            [SerializeField] int waitMilliSecconds = 0;
            [SerializeField] StageManager.GameState targetState;
            [SerializeField] [Multiline(3)] string script;
        }
    }
}