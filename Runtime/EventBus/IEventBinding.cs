using System;

namespace Core.Runtime
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
}