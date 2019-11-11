using UnityEngine;
using System.Collections;

namespace VRShooting
{

    public class OculusTouchInputProvider : InputProvider
    {
        [SerializeField]
        OVRInput.Controller controller = OVRInput.Controller.RTouch;

        protected override void SetInputVector()
        {
            Rotation = OVRInput.GetLocalControllerRotation(controller);
        }
    }
}
