using System;
using System.Collections.Generic;

namespace SSC.Shared.Wrappers
{
    public class RaceStateCollection
    {
        private Dictionary<Type, object> states;

        public RaceStateCollection()
        {
            states = new Dictionary<Type, object>();
        }

        public void SetState<T>(T instance)
        {
            states.Add(typeof(T), instance);
        }

        public T GetState<T>()
        {
            bool found = states.TryGetValue(typeof(T), out object state);

            if (!found)
            {
                return default;
            }

            return (T)Convert.ChangeType(state, typeof(T));
        }
    }
}
