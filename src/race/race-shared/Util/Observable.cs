using System;
using System.Collections.Generic;

namespace SSC.Shared.Util
{
    public class Observable<T>
    {
        private T Value = default;
        private HashSet<Action<T>> Observers;

        public Observable(T defaultValue)
        {
            Observers = new HashSet<Action<T>>();
            Value = defaultValue;
        }

        public void Observe(Action<T> callback)
        {
            Observers.Add(callback);
        }

        public T Get()
        {
            return Value;
        }

        public void Set(T value)
        {
            if (Value != null && Value.Equals(value))
                return;

            Value = value;

            foreach (Action<T> observer in Observers)
            {
                observer.Invoke(Value);
            }
        }

        public void Unset()
        {
            Value = default;
        }
    }
}
