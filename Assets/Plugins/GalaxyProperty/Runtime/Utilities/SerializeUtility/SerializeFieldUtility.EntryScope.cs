using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Game.Utilities
{
    public static partial class SerializeFieldUtility
    {
        private readonly struct EntryScope<T> : IDisposable
            where T : class
        {
            private static readonly Stack<Entry<T>> _entries = new Stack<Entry<T>>();
            private readonly Entry<T> _entry;

            private EntryScope(Entry<T> entry)
            {
                _entry = entry;
            }

            public static EntryScope<T> Create(out Entry<T> entry)
            {
                entry = _entries.Count > 0
                    ? _entries.Pop()
                    : new Entry<T>();

                return new EntryScope<T>(entry);
            }

            void IDisposable.Dispose()
            {
                Assert.IsFalse(_entries.Contains(_entry));

                _entries.Push(_entry);
            }
        }
    }
}
