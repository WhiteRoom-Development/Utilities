using System.Collections.Generic;

namespace Core.Runtime
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> _bindings = new();

        public static void Register(EventBinding<T> binding)
        {
            _bindings.Add(binding);
        }

        public static void Deregister(EventBinding<T> binding) => _bindings.Remove(binding);

        public static void Raise(T @event)
        {
            var snapshot = new HashSet<IEventBinding<T>>(_bindings);

            foreach (var binding in snapshot)
            {
                if (_bindings.Contains(binding))
                {
                    binding.OnEvent.Invoke(@event);
                    binding.OnEventNoArgs.Invoke();
                }
            }
        }
    }
}