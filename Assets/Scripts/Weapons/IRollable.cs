using UnityEngine;


namespace VRShooting
{
    interface IRollable
    {
        void Roll(IInputProvider porvider, ref Transform target);
    }
}
