//  CameraFader.cs
//  http://kan-kikuchi.hatenablog.com/entry/CameraFader
//
//  Created by kan.kikuchi on 2018.08.26.

using UnityEngine;
using System;


namespace VRShooting
{

    /// <summary>
    /// カメラをフェードするクラス
    /// </summary>
    public class CameraFader : MonoBehaviour
    {

        //カメラフェード用のマテリアル
        private Material _material = null;

        //フェード終了時のコールバック
        private Action _callback = null;

        //フェード開始時、終了時の色
        private float _fromColor, _toColor;

        //フェード時間
        private float _duration = 0, _currentTime = 0;

        //TimeScale(時間の倍率)を無視するか
        private bool _isIgnoreTimeScale = true;

        //実行中か
        private bool _isPlaying = false;
        public bool IsPlaying { get { return _isPlaying; } }

        //=================================================================================
        //初期化
        //=================================================================================

        private void Awake()
        {
            //シェーダーを検索し、それを使ったマテリアルを作成
            _material = new Material(Shader.Find("CameraFade"));
        }

        //=================================================================================
        //更新
        //=================================================================================

        private void Update()
        {
            if (!_isPlaying)
            {
                return;
            }

            //時間経過
            _currentTime += _isIgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;

            //時間の割合から色を計算、設定
            float timeRate = Mathf.Min(1, _currentTime / _duration);
            SetColor(_toColor * timeRate + _fromColor * (1 - timeRate));

            //終了判定
            if (_currentTime >= _duration)
            {
                EndFade();
            }
        }

        //すべてのレンダリングが完了したら呼ばれる、ポストプロセスをかけるためのメソッド(カメラがAddされていないと呼ばれない)
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            //_materialを使って、ポストプロセスをかける
            Graphics.Blit(src, dest, _material);
        }

        //=================================================================================
        //フェード
        //=================================================================================

        /// <summary>
        /// フェードアウトを実行
        /// </summary>
        public void FadeOut(float duration = 0.5f, bool isIgnoreTimeScale = true, Action callback = null)
        {
            BeginFade(1, 0, duration, isIgnoreTimeScale, callback);
        }

        /// <summary>
        /// フェードインを実行
        /// </summary>
        public void FadeIn(float duration = 0.5f, bool isIgnoreTimeScale = true, Action callback = null)
        {
            BeginFade(0, 1, duration, isIgnoreTimeScale, callback);
        }

        //フェードを開始
        private void BeginFade(float fromColor, float toColor, float duration, bool isIgnoreTimeScale, Action callback)
        {
            _currentTime = 0;
            _fromColor = fromColor;
            _toColor = toColor;
            _duration = duration;
            _isIgnoreTimeScale = isIgnoreTimeScale;
            _callback = callback;

            _isPlaying = true;

            //時間が０以下なら即反映
            if (duration <= 0)
            {
                SetColor(_toColor);
                EndFade();
            }
            else
            {
                SetColor(_fromColor);
            }
        }

        //フェード終了
        private void EndFade()
        {
            _isPlaying = false;

            //コールバックが設定されていれば実行
            if (_callback != null)
            {
                _callback();
            }
        }

        //マテリアルの色を設定
        private void SetColor(float color)
        {
            _material.SetColor("_Color", new Color(color, color, color, color));
        }

    }
}