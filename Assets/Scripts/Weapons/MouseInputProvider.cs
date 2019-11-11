using UnityEngine;
using System.Collections;
using System;

namespace VRShooting
{

    public class MouseInputProvider : InputProvider
    {
        /// <summary>前フレームのmouse position</summary>
        Vector3 mousePrevPos = Vector3.zero;

        /// <summary>
        /// マウスのインプット取得処理.
        /// </summary>
        /// <remarks>
        /// <see cref="Input.mousePosition"/>の差分から<see cref="InputVector"/>の成分取得.
        /// </remarks>
       override protected void SetInputVector()
        {
            var mouseDiff = mousePrevPos != Vector3.zero ? Input.mousePosition - mousePrevPos : Vector3.zero;
            if (mouseDiff != Vector3.zero)
            {
                Horizontal = mouseDiff.x;
                Vertical = mouseDiff.y;
            }
            else
            {
                Horizontal = Vertical = 0f;
            }
            mousePrevPos = Input.mousePosition;

            Rotation = Quaternion.identity ;
        }
    }
}
