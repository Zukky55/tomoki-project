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

namespace VRShooting
{
    public class ScreenFader : MonoBehaviour
    {
        [SerializeField]
        GameObject fadeSphere;

        private void Update()
        {
            GUI.color = Color.black;
        }

        public async UniTask FadeOutAsync(float fadeTime)
        {
            var camera = Camera.main;
            
            var mat = fadeSphere.GetComponent<MeshRenderer>().material;

            var disposable = this.UpdateAsObservable()
                .Select(_ => mat.color.a)
                .Where(alpha => alpha < 1f)
                .Subscribe(alpha =>
                {
                    alpha -= Time.deltaTime * fadeTime;
                    var c = mat.color;
                    mat.color = new Color(c.r, c.g, c.b, alpha);
                })
                .AddTo(this);
            await UniTask.WaitUntil(() => mat.color.a <= 0f);

        }
    }
}