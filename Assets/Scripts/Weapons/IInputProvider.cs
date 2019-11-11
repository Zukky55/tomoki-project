using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;


namespace VRShooting
{
    public interface IInputProvider
    {
        Vector3 InputVector { get; } 
        float Horizontal { get; }
        float Vertical { get; }
         Clamp ClampSetting{ get; }
         Quaternion Rotation { get;}
    }
    /// <summary>
    /// <see cref="InputVector"/>に制限を掛けるか,どう掛けるかを設定するenum
    /// </summary>
    public enum Clamp
    {
        Both,
        Horizontal,
        Vertical,
        BothNormalized,
        HorizontalNormalized,
        VerticalNormalized,
    }
}
