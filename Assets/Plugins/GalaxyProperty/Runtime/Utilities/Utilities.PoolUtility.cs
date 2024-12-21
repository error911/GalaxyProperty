using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Game.Utilities
{
    public static class PoolUtility<T> where T : class, new()
    {
        private static readonly Stack<T> _values = new Stack<T>();

#if ENABLE_PROFILING
        public static int FreeCount => _values.Count;

        public static int SummaryCount
        {
            get;
            private set;
        }
#endif

        public static void Push(T value)
        {
            Assert.IsFalse(_values.Contains(value));

            _values.Push(value);
        }

        public static T Pull()
        {
            if (_values.Count > 0)
            {
                return _values.Pop();
            }
#if ENABLE_PROFILING
            SummaryCount++;
#endif
            return new T();
        }
    }
}