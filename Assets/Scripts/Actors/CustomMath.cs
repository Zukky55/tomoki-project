using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace VRShooting
{
    public static class CustomMath
    {
        public static Vector2 onUnitCircle
        {
            get
            {
                var radius = 1f;
                var degree = Random.Range(0f, 360f);
                var radian = degree * Mathf.Deg2Rad;
                var cos = Mathf.Cos(radian);
                var sin = Mathf.Sin(radian);

                return new Vector2(cos * radius, sin * radius);
            }
        }
        /// <summary>
        /// ランダムな円周上の座標を返す
        /// </summary>
        /// <param name="degree">角度</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        public static Vector2 GetRandomCircumferenceCoordinates(float radius, float degree = 360)
        {
            degree = degree % 360;

            var radian = degree * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radian);
            var sin = Mathf.Sin(radian);

            return new Vector2(cos * radius, sin * radius);
        }

    }
}