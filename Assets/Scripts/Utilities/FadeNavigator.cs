//  SceneNavigator.cs
//  http://kan-kikuchi.hatenablog.com/entry/CameraFader
//
//  Created by kan.kikuchi on 2018.08.26.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace VRShooting
{
    /// <summary>
    /// シーンの遷移及びフェードを実行、管理するクラス
    /// </summary>
    public class FadeNavigator : MonoSingleton<FadeNavigator>
    {

        //カメラをフェードするクラス
        private CameraFader _cameraFader = null;

        //移動先のシーン名
        private string _nextSceneName;

        //シーン変更中か(フェード中か)
        private bool _isChanging = false;
        public bool IsChanging { get { return _isChanging; } }

        //フェードとフェード間の遅延時間
        private float _fadeDuration, _fadeDelay;

        //=================================================================================
        //初期化
        //=================================================================================

        //ゲーム開始時(シーン読み込み前)に実行される
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateSelf()
        {
            //SceneNavigatorという名前のGameObjectを作成し、SceneNavigatorをAddする
            new GameObject("SceneNavigator", typeof(FadeNavigator));
        }

        /// <summary>
        /// 初期化(Awake時かその前の初アクセスどちらかの一度しか行われない)
        /// </summary>
        public override void OnInitialize()
        {
            //シーンをまたいでも消えないように
            DontDestroyOnLoad(gameObject);
        }

        //=================================================================================
        //フェード
        //=================================================================================

        /// <summary>
        /// シーンを変更する(変更前後にカメラのフェードもする)
        /// </summary>
        public void ChangeScene(string nextSceneName, float fadeDuration = 0.5f, float fadeDelay = 0)
        {
            //変更中の場合はスルー
            if (_isChanging)
            {
                return;
            }
            _isChanging = true;
            _fadeDuration = fadeDuration;
            _fadeDelay = fadeDelay;

            //CameraFaderをメインカメラに設定する
            _cameraFader = Camera.main.gameObject.AddComponent<CameraFader>();

            //次のシーン名を設定し、フェードアウト
            _nextSceneName = nextSceneName;
            _cameraFader.FadeOut(duration: _fadeDuration, callback: FinishFadeOut);
        }

        //フェードアウト終了
        private void FinishFadeOut()
        {
            //シーンを変更し、フェードイン
            SceneManager.LoadScene(_nextSceneName);
            StartCoroutine(FadeIn());
        }

        //フェードイン
        private IEnumerator FadeIn()
        {
            //シーン生成直後だとCameraFaderを設定しても真っ暗に出来ないので、1フレーム待つ
            //また、1フレーム待つことでシーン生成後にフェードインを開始出来る(フェードがカクつかない)
            yield return new WaitForSeconds(0);

            //移動先のシーンのカメラにCameraFaderを設定し、真っ暗にする
            _cameraFader = Camera.main.gameObject.AddComponent<CameraFader>();
            _cameraFader.FadeOut(duration: 0);

            //設定された時間待つ
            if (_fadeDelay > 0)
            {
                yield return new WaitForSeconds(_fadeDelay);
            }

            //フェードイン
            _cameraFader.FadeIn(duration: _fadeDuration, callback: FinishFadeIn);
        }

        //フェードイン終了
        private void FinishFadeIn()
        {
            _isChanging = false;

            //CameraFaderを削除
            Destroy(_cameraFader);
        }

    }
}