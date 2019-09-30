using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VRShooting
{
    /// <summary>
    /// マネージドなUpdate関数を扱う全てのMonoBehaviourの親クラス
    /// All MonoBehaviour parent classes that handle managed Update methods.
    /// </summary>
    public abstract class ManagedMono : MonoBehaviour, IUpdatable
    {
        UpdateManager updateManager;
        protected virtual void Awake()
        {
            updateManager = UpdateManager.Instance;
            updateManager.SubscribeUpdate(this);
        }
        protected virtual void OnDestroy() => updateManager.UnsubscribeUpdate(this);
        public abstract void ManagedUpdate();
    }
}