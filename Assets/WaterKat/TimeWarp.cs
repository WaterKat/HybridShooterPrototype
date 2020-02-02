using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.TimeWarping
{
    public class TimeWarp
    {
        public static float DefaultTimeScale = 1;
        public static float DefaultFixedTimeStep = 0.02f;

        public static float SlowedTimeScale = .25f;

        public static float WarpedDeltaTime { get { return Time.deltaTime*(DefaultTimeScale / Time.timeScale); } }
    }
}