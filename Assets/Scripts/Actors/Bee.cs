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
        [SerializeField] PlayableTrack playableTrack;
        [SerializeField] PlayableDirector playableDirector;
        [SerializeField] PlayableAsset playableAsset;

        private void Start()
        {
        }
        public override void MUpdate()
        {
            base.MUpdate();
        }
    }
}