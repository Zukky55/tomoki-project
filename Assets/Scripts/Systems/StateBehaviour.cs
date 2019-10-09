using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace VRShooting
{
    public abstract class StateBehaviour : MonoBehaviour, IGameState
    {
        public float WaveTime => WaveTime;
        public StageManager.GameState StateId => stateId;

        [SerializeField] protected int waveTimeSeconds;
        [SerializeField] protected StageManager.GameState stateId;

        protected StageManager stageManager;
        protected SynchronizationContext context = SynchronizationContext.Current;

        protected virtual void Awake()
        {
            stageManager = StageManager.Instance;
        }
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
        protected void TransitionGameState(StageManager.GameState nextState) => stageManager.SetState(nextState);

        protected async Task TransitionWaveAsync(StageManager.GameState nextState)
        {
            await Task.Delay(1000);
            TransitionGameState(nextState);
        }
        protected IEnumerator TransitionWaveCoroutine(StageManager.GameState gs)
        {
            yield return new WaitForSeconds(waveTimeSeconds);
            TransitionGameState(gs);
        }
        protected IEnumerator TransitionWaveCoroutine(StageManager.GameState gs,float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            TransitionGameState(gs);
        }
    }
    public interface IGameState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
