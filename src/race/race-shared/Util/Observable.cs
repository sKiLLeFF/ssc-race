using System;
using System.Collections.Generic;

namespace SSC.Shared.Util
{
    public class Observable<T>
    {
        private T Value = default;
        private HashSet<Action<T>> Observers;

        public Observable()
        {
            Observers = new HashSet<Action<T>>();
            Value = default;
        }

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

        //NOTE(bma) isSimian is to avoid tripping broken equality checks. (Player.Equals(..) namely_
        //          This will make the observable update everytime you assign a value when this is true.
        public void Set(T value, bool isSimian = false) 
        {
            if (!isSimian && Value != null && Value.Equals(value))
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
