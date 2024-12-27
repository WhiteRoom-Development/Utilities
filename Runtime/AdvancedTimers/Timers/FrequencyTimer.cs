using System;
using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// Timer that ticks at a specific frequency. (N times per second)
    /// </summary>
    public class FrequencyTimer : Timer
    {
        public Action OnTick = delegate { };

        public override bool IsFinished => !IsRunning;
        public int TicksPerSecond { get; private set; }

        private float timeThreshold;

        public FrequencyTimer(int ticksPerSecond) : base(0)
        {
            CalculateTimeThreshold(ticksPerSecond);
        }

        public override void Tick()
        {
            if (IsRunning && CurrentTime >= timeThreshold)
            {
                CurrentTime -= timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < timeThreshold)
            {
                CurrentTime += Time.deltaTime;
            }
        }

        public override void Reset()
        {
            CurrentTime = 0;
        }

        public void Reset(int newTicksPerSecond)
        {
            CalculateTimeThreshold(newTicksPerSecond);
            Reset();
        }

        private void CalculateTimeThreshold(int ticksPerSecond)
        {
            TicksPerSecond = ticksPerSecond;
            timeThreshold = 1f / TicksPerSecond;
        }
    }
}