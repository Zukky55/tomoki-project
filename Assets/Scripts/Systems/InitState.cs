using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VRShooting
{
    public class InitState : StateBehaviour
    {
        [SerializeField] Canvas StartGameCanvas;
        protected override void OnGameStateEnter(StageManager.GameState wave)
        {
            // DIsplay start canvases.
        }
        protected override void OnGameStateExecute(StageManager.GameState wave)
        {
            // When shot start button then Starting First wave.
        }
        protected override void OnGameStateExit(StageManager.GameState wave)
        {
            // Close Start canvases.
        }

        public void OnShotStartButton()
        {
            stageManager.InvokeState(StageManager.GameState.FirstWave);
        }
    }
}
