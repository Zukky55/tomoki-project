using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace VRShooting
{
    public class StageManager : ManagedMono
    {
        #region Singleton
        static StageManager instance;
        public static StageManager Instance
        {
            get
            {
                if (instance) return instance;
                var type = typeof(StageManager);
                var ins = FindObjectOfType(type) as StageManager;
                if (!ins)
                {
                    var typeName = type.ToString();
                    var go = new GameObject(typeName, type);
                    ins = go.GetComponent<StageManager>();
                    if (!ins) throw new InvalidOperationException($"{typeName} を生成するのに失敗しました.");
                }
                instance = ins;
                return instance;
            }
        }
        #endregion

        public delegate void GameStateDel(GameState gameState);
        public event GameStateDel OnGameStateEnter = gs => { };


        public GameObject BeeFlock => beeFlock;
        public GameObject Spider => spider;
        public GameObject Boss => boss;
        public GameState CurrentState => currentState.StateId;

        [SerializeField] GameState firstState ;
        [SerializeField] GameObject beeFlock;
        [SerializeField] GameObject spider;
        [SerializeField] GameObject boss;
        [SerializeField] GameObject individualState;
        [SerializeField] GameState now;

        StateBehaviour currentState;
        StateBehaviour[] gameStates;

        protected override void Awake()
        {
            base.Awake();
            gameStates = individualState.GetComponents<StateBehaviour>();
            Debug.Log($"gameStates.Count() = {gameStates.Count()}");
        }

        private void Start()
        {
            SetState(firstState);
        }

        public override void MUpdate()
        {
            currentState?.Execute();
        }

        /// <summary>
        /// 指定Stateに遷移させる.
        /// </summary>
        /// <param name="nextState"></param>
        public void SetState(GameState nextState)
        {
            if (nextState != GameState.InitState) currentState?.Exit();
            currentState = gameStates.First(gs => gs.StateId == nextState);
            currentState.Enter();
            OnGameStateEnter?.Invoke(currentState.StateId);
            Debug.Log($"currentState = {currentState.StateId}");
        }

        public enum GameState
        {
            None,
            InitState,
            FirstWave,
            SecondWave,
            BossWave,
            GameClear,
            GameOver,
        }
    }
}