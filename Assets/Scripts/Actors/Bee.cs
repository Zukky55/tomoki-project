using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Collections;

namespace VRShooting
{
    /// <summary>
    /// 蜂のベースクラス
    /// </summary>
    public class Bee : Enemy
    {
        [SerializeField] PlayableTrack pTrack;
        [SerializeField] PlayableDirector pDirector;
        [SerializeField] PlayableAsset pAsset;
        [SerializeField] Vector3 vec;

        private void Start()
        {
            //pDirector.Play();
        }

        private void OnEnable()
        {
        }
        public override void MUpdate()
        {
            base.MUpdate();
            transform.forward = vec;
            //if (forwardDirection != Vector3.zero)
            //{
            //    transform.forward = forwardDirection;
            //}
        }

        public void StopTrack()
        {
            pDirector.Stop();
        }
    }
}