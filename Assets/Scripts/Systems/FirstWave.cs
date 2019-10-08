using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Threading;


namespace VRShooting
{
    public class FirstWave : StateBehaviour
    {
        [SerializeField] PlayableDirector BeesDirector;
        public async override void Enter()
        {
            BeesDirector.Play();
            Debug.Log($"ManagedThreadId = {Thread.CurrentThread.ManagedThreadId}");
            await TransitionWaveAsync(StageManager.GameState.SecondWave);
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }
}
