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

        private void Start()
        {
            pDirector.Play();
        }
        public override void MUpdate()
        {
            base.MUpdate();
        }
    }
}