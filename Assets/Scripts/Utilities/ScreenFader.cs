using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UniRx.Async;
using UnityEngine.SceneManagement;

namespace VRShooting
{
    public class ScreenFader : MonoSingleton<ScreenFader>
    {
        [SerializeField]
        GameObject fadeSphere;
        [SerializeField] Animator animator;

        public void FadeOut() => animator.SetTrigger("FadeOut");
        public void FadeIn() => animator.SetTrigger("FadeIn");
        public void ScreenChange(string name) => SceneManager.LoadScene(name);
    }
}