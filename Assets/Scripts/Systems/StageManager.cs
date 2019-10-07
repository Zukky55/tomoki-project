using System;
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

        public delegate void GameStateDel(GameState wave);
        public event GameStateDel GameStateEnter = v => Debug.Log($"Called {v} state.");
        public event GameStateDel GameStateExecute = v => { };
        public event GameStateDel GameStateExit = v => Debug.Log($"Exit {v} state.");

        public GameState CurrentState => currentState;
        public GameObject BeeFlock => beeFlock;
        public GameObject Spider => spider;
        public GameObject Boss => boss;

        [SerializeField] GameObject beeFlock;
        [SerializeField] GameObject spider;
        [SerializeField] GameObject boss;
        [SerializeField] GameState firstState = GameState.InitState;

        GameState currentState = GameState.None;

        private void Start()
        {
            InvokeState(firstState);
        }

        public override void MUpdate()
        {
            GameStateExecute?.Invoke(currentState);
        }

        /// <summary>
        /// 指定Stateに遷移させる.
        /// </summary>
        /// <param name="nextState"></param>
        public void InvokeState(GameState nextState)
        {
            GameStateExit?.Invoke(currentState);
            currentState = nextState;
            GameStateEnter?.Invoke(currentState);
        }

        public enum GameState
        {
            None,
            InitState,
            FirstWave,
            SecondWave,
            BossWave,
            GameCrear,
            GameOver,
        }
    }
}