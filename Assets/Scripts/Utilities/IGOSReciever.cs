﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRShooting
{
    public interface IGOSReciever : IEventSystemHandler
    {
        void OnGORecieve(GameObject detectedGO);
    }
}
