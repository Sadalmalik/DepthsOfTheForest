using System;

namespace Sadalmalik.Forest
{
    public struct ReactiveProperty<T> where T : class
    {
        public T Value { get; private set; }

        public event Action<T> OnChanged;

        public void Set(T newValue, bool silent = false)
        {
            bool notifyChange = !silent && newValue != Value;

            Value = newValue;

            if (notifyChange)
                OnChanged?.Invoke(Value);
        }
    }
}