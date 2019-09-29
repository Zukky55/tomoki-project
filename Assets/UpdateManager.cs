using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace VRShooting
{
    /// <summary>
    /// <see cref="ManagedMono"/>の<see cref="ManagedMono.ManagedUpdate"/>を統合して呼び出すクラス。.
    /// Class that unify and Call <see cref="ManagedMono.ManagedUpdate"/> of <see cref="ManagedMono"/>.
    /// </summary>
    public class UpdateManager : MonoSingleton<UpdateManager>
    {
        LinkedList<IUpdatable> updatables;
        private void Update()
        {
            foreach (var updatable in updatables)
            {
                updatable.ManagedUpdate();
            }
        }

        public void SubscribeUpdate(LinkedListNode<IUpdatable> updatable)
        {
            updatables.AddLast(updatable);
        }
        public void UnsubscribeUpdate(LinkedListNode<IUpdatable> updatable)
        {
            updatables.Remove(updatable);
        }

        public override void OnInitialize() => DontDestroyOnLoad(gameObject);
    }
}