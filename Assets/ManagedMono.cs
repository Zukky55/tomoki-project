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
        LinkedListNode<IUpdatable> updatable;
        protected virtual void Awake()
        {
            updatable = new LinkedListNode<IUpdatable>(this);
            UpdateManager.Instance.SubscribeUpdate(updatable);
        }
        protected virtual void OnDestroy() => UpdateManager.Instance.UnsubscribeUpdate(updatable);
        public abstract void ManagedUpdate();
    }
}