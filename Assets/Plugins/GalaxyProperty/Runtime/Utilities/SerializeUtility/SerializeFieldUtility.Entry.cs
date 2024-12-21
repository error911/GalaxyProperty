using System;
using UnityEngine;

namespace Game.Utilities
{
    public static partial class SerializeFieldUtility
    {
        [Serializable]
        private sealed class Entry<T>
            where T : class
        {
            [SerializeField]
            private T _value;

            public string ToJson(T value, bool prettyPrint)
            {
                try
                {
                    _value = value;

                    return JsonUtility.ToJson(this, prettyPrint);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    throw;
                }
                finally
                {
                    _value = default;
                }
            }

            public T FromJson(string json)
            {
                try
                {
                    JsonUtility.FromJsonOverwrite(json, this);

                    return _value;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    throw;
                }
                finally
                {
                    _value = default;
                }
            }
        }
    }
}
