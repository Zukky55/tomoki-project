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

        //public void FadeIn(float fadeTime=05f)
        //{
        //    var mat = fadeSphere.GetComponent<MeshRenderer>().material;
        //    mat.color = Color.black ;

        //    var disposable = this.UpdateAsObservable()
        //        .Select(_ => mat.color.a)
        //        .Where(alpha => alpha > 0f)
        //        .Subscribe(alpha =>
        //        {
        //            alpha -= Time.deltaTime * fadeTime;
        //            var c = mat.color;
        //            mat.color = new Color(c.r, c.g, c.b, alpha);
        //        })
        //        .AddTo(this);
        //}

        //public void FadeOut(float fadeTime = 0.5f)
        //{
        //    var mat = fadeSphere.GetComponent<MeshRenderer>().material;
        //    mat.color = Color.clear;

        //    var disposable = this.UpdateAsObservable()
        //        .Select(_ => mat.color.a)
        //        .Where(alpha => alpha < 1f)
        //        .Subscribe(alpha =>
        //        {
        //            alpha += Time.deltaTime * fadeTime;
        //            var c = mat.color;
        //            mat.color = new Color(c.r, c.g, c.b, alpha);
        //        })
        //        .AddTo(this);
        //}

        public void FadeOut() => animator.SetTrigger("FadeOut");
        public void FadeIn() => animator.SetTrigger("FadeIn");
        public void ScreenChange(string name) => SceneManager.LoadScene(name);
    }
}