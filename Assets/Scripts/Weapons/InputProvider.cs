using UnityEngine;
using System.Collections;
using System;

namespace VRShooting
{
    public abstract class InputProvider : ManagedMono, IInputProvider
    {
        /// <summary>マウスのスクロール差分ベクトル</summary>
        public Vector3 InputVector
        {
            get
            {
                var vec = Vector3.zero;
                switch (clampSetting)
                {
                    case Clamp.Both:
                        vec = new Vector3(Horizontal, Vertical, 0f);
                        break;
                    case Clamp.Horizontal:
                        vec = new Vector3(Horizontal, 0f, 0f);
                        break;
                    case Clamp.Vertical:
                        vec = new Vector3(0f, Vertical, 0f);
                        break;
                    case Clamp.BothNormalized:
                        vec = new Vector3(Horizontal, Vertical, 0f).normalized;
                        break;
                    case Clamp.HorizontalNormalized:
                        vec = new Vector3(Horizontal, 0f, 0f).normalized;
                        break;
                    case Clamp.VerticalNormalized:
                        vec = new Vector3(0f, Vertical, 0f).normalized;
                        break;
                    default:
                        throw new ArgumentException();
                }
                return vec;
            }
        }
        public float Horizontal { get; protected set; } = 0f;

        public float Vertical { get; protected set; } = 0f;

        public Clamp ClampSetting => clampSetting;

        public Quaternion Rotation { get; protected set; } = Quaternion.identity;

        [SerializeField]
        protected Clamp clampSetting = Clamp.Both;

        public override void MUpdate()
        {
            SetInputVector();
        }
        protected abstract void SetInputVector();
    }
}
