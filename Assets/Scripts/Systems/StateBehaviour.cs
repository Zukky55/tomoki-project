using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public abstract class StateBehaviour : MonoBehaviour
    {
        public float WaveTime => WaveTime;

        [SerializeField] protected float waveTime;
        [SerializeField] protected StageManager.GameState targetState;

        protected StageManager stageManager;

        protected virtual void Awake()
        {
            stageManager = StageManager.Instance;
            stageManager.GameStateEnter += OnGameStateEnter;
            stageManager.GameStateExecute += OnGameStateExecute;
            stageManager.GameStateExit += OnGameStateExit;
        }
        protected abstract void OnGameStateEnter(StageManager.GameState wave);
        protected abstract void OnGameStateExecute(StageManager.GameState wave);
        protected abstract void OnGameStateExit(StageManager.GameState wave);
    }
}
