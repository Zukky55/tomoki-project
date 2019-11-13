using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VRShooting
{
    public class InitState : StateBehaviour
    {
        [SerializeField] Canvas StartGameCanvas;


        public override void Enter()
        {
            // DIsplay start canvases.
        }

        public override void Execute()
        {
            // When shot start button then Starting First wave.
            if (Input.GetMouseButtonDown(0))
            {
                FadeNavigator.Instance.ChangeScene("Main");
            }
        }

        public override void Exit()
        {
            // Close Start canvases.
        }

        public void OnShotStartButton()
        {
            stageManager.SetState(StageManager.GameState.FirstWave);
        }
    }
}
