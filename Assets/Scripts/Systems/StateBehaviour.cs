using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace VRShooting
{
    public abstract class StateBehaviour : MonoBehaviour, IGameState
    {
        public StageManager.GameState StateId => stateId;

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
        protected void SetGameState(StageManager.GameState nextState) => stageManager.SetState(nextState);


        protected IEnumerator TransitionWaveCoroutine(StageManager.GameState gs,float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            SetGameState(gs);
        }
    }
    public interface IGameState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
