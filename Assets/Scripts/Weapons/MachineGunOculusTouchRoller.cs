using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class MachineGunOculusTouchRoller : IRollable
    {
        public void Roll(IInputProvider provider, ref Transform target)
        {
            target.localRotation = provider.Rotation;
        }
    }
}