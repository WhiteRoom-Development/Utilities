using System;
using UnityEngine;

namespace Utilities.Runtime
{
    public abstract class Timer : IDisposable
    {
        public Action OnStart = delegate { };
        public Action OnStop = delegate { };
        
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }
        
        public abstract bool IsFinished { get; }

        protected float m_initialTime;

        public float Progress => Mathf.Clamp(CurrentTime / m_initialTime, 0, 1);
        
        private bool disposed;

        protected Timer(float value)
        {
            m_initialTime = value;
        }

        public void Start()
        {
            CurrentTime = m_initialTime;
            
            if (!IsRunning)
            {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnStart.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                TimerManager.DeregisterTimer(this);
                OnStop.Invoke();
            }
        }

        public abstract void Tick();

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public virtual void Reset() => CurrentTime = m_initialTime;

        public virtual void Reset(float newTime)
        {
            m_initialTime = newTime;
            Reset();
        }

        // Call the dispose function when deleting from memory
        ~Timer()
        {
            Dispose(false);
        }

        // Call Dispose to ensure deregistration of the timer from the TimerManager
        // when the consumer is done with the timer or being destroyed
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                TimerManager.DeregisterTimer(this);
            }

            disposed = true;
        }
    }
}