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
            //context.Post(,state =>
            //{
            ////Debug.Log($"ManagedThreadId = {Thread.CurrentThread.ManagedThreadId}");
            //TransitionGameState(nextState);
            //});
        }
    }
    public interface IGameState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
