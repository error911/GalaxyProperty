using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using UnityEngine;

namespace Properties
{
    [Serializable]
    public abstract class ValueProperty<T> : Property, IGetProperty<T>, ISetProperty<T>
    {
        [HideLabel]
        [SerializeField]
        [ValidateInput(nameof(ValidateValue))]
        private T _value;

        public T Value => _value;

        protected virtual bool ValidateValue(T value)
        {
            return _value != null;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        #region IGetProperty<T>

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        T IGetProperty<T>.Value => _value;

        #endregion

        #region ISetProperty<T>

        T ISetProperty<T>.Value
        {
            set => _value = value;
        }

        #endregion

        #region Editor

        // ReSharper disable once RedundantAssignment
        [Conditional("UNITY_EDITOR")]
        public void GetValue(ref T value)
        {
            value = _value;
        }

        [Conditional("UNITY_EDITOR")]
        public void SetValue(in T value)
        {
            _value = value;
        }

        #endregion
    }
}
