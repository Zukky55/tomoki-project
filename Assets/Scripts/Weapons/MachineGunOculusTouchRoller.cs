using UnityEngine;
using System.Collections;
using System;

namespace VRShooting
{
    public class MachineGunOculusTouchRoller : IRollable
    {
        float elevationAngleLimit = 0f;
        float rollSpeed = 0f;

        bool initialized = false;
        Quaternion firstTargetRot;
        Quaternion firstControllerRot;

        readonly Quaternion initialRot = new Quaternion(0.58f, 0.56f, 0.50f, 0.3f);

        public MachineGunOculusTouchRoller(float elevationAngleLimit, float rollSpeed)
        {
            this.elevationAngleLimit = elevationAngleLimit;
            this.rollSpeed = rollSpeed;
        }

        public void Roll(IInputProvider provider, ref Transform target)
        {
            //if (!initialized)
            //{
            //    initialized = true;

            //    firstTargetRot = target.rotation;
            //    firstControllerRot = provider.Rotation;
            //}
            //var diff = firstTargetRot * Quaternion.Inverse(initialRot);
            var q = provider.Rotation;

            target.localRotation = q;

            //Debug.Log($"firstRot = {firstRot}");
            //Debug.Log($"diff = {diff}");
            //var q = ;
            //Debug.Log($"rot = {q}");
        }
    }
}