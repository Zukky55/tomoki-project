using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class BossWave : StateBehaviour
    {
        GameObject boss;
        public override void Enter()
        {
            boss = Instantiate(stageManager.Boss, new Vector3(10, 10, 10), Quaternion.identity);
        }

        public override void Execute()
        {
            if (!boss)
            {
                stageManager.SetState(StageManager.GameState.GameCrear);
            }
        }

        public override void Exit()
        {
        }
    }
}
