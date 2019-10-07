using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VRShooting
{
    public class InitState : StateBehaviour
    {
        [SerializeField] Canvas StartGameCanvas;
        [SerializeField] SpiderSpawner spiderSpawner;
        [SerializeField] int amount;
        [SerializeField] float interval;

        protected override void OnGameStateEnter(StageManager.GameState wave)
        {
            if (wave != targetState) return;
            // DIsplay start canvases.
            spiderSpawner.Spawn(amount);
        }
        protected override void OnGameStateExecute(StageManager.GameState wave)
        {
            if (wave != targetState) return;
            // When shot start button then Starting First wave.
        }
        protected override void OnGameStateExit(StageManager.GameState wave)
        {
            if (wave != targetState) return;
            // Close Start canvases.
        }

        public void OnShotStartButton()
        {
            stageManager.InvokeState(StageManager.GameState.FirstWave);
        }
    }
}
