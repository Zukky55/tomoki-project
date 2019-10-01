using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace VRShooting
{
    /// <summary>
    /// Managed update interface.
    /// </summary>
    public interface IUpdatable : IEventSystemHandler
    {
        /// <summary>
        /// Managed update method.
        /// </summary>
        void MUpdate();

        /// <summary>
        /// Managed fixed update method.
        /// </summary>
        void MFixedUpdate();
    }
}
