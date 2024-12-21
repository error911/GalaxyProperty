using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Properties
{
    [Serializable]
    public sealed class PropertyMap : ISerializationCallbackReceiver, IPropertyProvider
    {
        private Dictionary<string, IProperty> _entries;

        [ListDrawerSettings(ListElementLabelName = nameof(IProperty.Key))]
        [Searchable]
        [SerializeReference]
        [ValidateInput(nameof(ValidateProperties))]
        private IProperty[] _properties = Array.Empty<IProperty>();

        public IEnumerable<IProperty> Properties
        {
            get => _properties;
            set
            {
                _properties = value.ToArray();
                RefreshEntries();
            }
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local
        private bool ValidateProperties(IProperty[] properties)
        {
            using (HashSetScope<string>.Create(out var keys))
            {
                foreach (var property in properties)
                {
                    if (property == null || string.IsNullOrWhiteSpace(property.Key) || !keys.Add(property.Key))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void RefreshEntries()
        {
            if (_properties.Length == 0)
            {
                _entries = null;

                return;
            }

            if (_entries == null)
            {
                _entries = new Dictionary<string, IProperty>();
            }
            else
            {
                _entries.Clear();
            }

            foreach (var property in _properties)
            {
                if (!string.IsNullOrWhiteSpace(property.Key))
                {
                    _entries[property.Key] = property;
                }
            }
        }

        /*
        public bool TryGetProperty<T>(string key, out IGetProperty<T> property, bool debugOff = false)
        {
            //property = default;
            var val = _entries.TryGetValue(key, out var baseProperty);
            var getted = _entries != null && val;
            //if (!IPropertyProvider.TryGetProperty(key, out IProperty baseProperty))
            if (!getted)
            {
                if (!debugOff)
                {
                    UnityEngine.Debug.LogWarning($"Свойство (Property: {nameof(key)}=\"{key}\") не найдено!", baseProperty as UnityEngine.Object);
                }

                property = default;

                return false;
            }

            if (!(baseProperty is IGetProperty<T> derivedProperty))
            {
                //UnityEngine.Debug.LogError($"Невозможно использовать свойство с {nameof(key)}=\"{key}\" из {baseProperty.GetType().Name} в {typeof(IGetProperty<T>).Name}!", baseProperty as UnityEngine.Object);
                UnityEngine.Debug.LogError($"Невозможно использовать свойство с {nameof(key)}=\"{key}\" из {baseProperty.GetType().Name} в {typeof(T).Name}!", baseProperty as UnityEngine.Object);

                property = default;

                return false;
            }

            property = derivedProperty;

            return true;
        }
        */

        #region ISerializationCallbackReceiver

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            RefreshEntries();
        }

        #endregion

        #region IPropertyProvider

        public bool TryGetProperty(string key, out IProperty property)
        {
            property = default;

            return _entries != null && _entries.TryGetValue(key, out property);
        }


        public bool TryGetPropertyValue<T>(string key, out T property, bool silent = false) //IPropertyProvider.
        {
            property = default;
            if (_entries == null)
            {
                if (!silent)
                {
                    var keyName = "null";
                    if (!string.IsNullOrWhiteSpace(key))
                        keyName = key;
                    UnityEngine.Debug.LogError($"Свойство пустое (Property) {nameof(Type)}=\"{typeof(T).Name}\", {nameof(key)}=\"{keyName}\"");
                }
                return false;
            }
            if (_entries.TryGetValue(key, out var baseProperty))
            {
                if (_entries != null)
                {
                    if (baseProperty is IGetProperty<T> propertyGetter)
                    {
                        property = propertyGetter.Value;
                        return true;
                    }
                    else
                    {
                        if (!silent)
                        {
                            UnityEngine.Debug.LogError($"Невозможно использовать свойство (Property) с {nameof(key)}=\"{key}\" из {baseProperty.GetType().Name} в {typeof(T).Name}!", baseProperty as UnityEngine.Object);
                        }
                    }
                }
            }
            
            return false;
        }


        #endregion

        #region Editor

        /// <summary>
        /// Only EDITOR
        /// </summary>
        /// <param name="properties"></param>
        [Conditional("UNITY_EDITOR")]
        public void GetProperties(ICollection<IProperty> properties)
        {
            Assert.IsTrue(properties.Count == 0);

            //properties.AddRange(_properties);
            
            foreach (var item in _properties)
            {
                properties.Add(item);
            }
        }

        /// <summary>
        /// Only EDITOR
        /// </summary>
        /// <param name="properties"></param>
        [Conditional("UNITY_EDITOR")]
        public void SetProperties(IEnumerable<IProperty> properties)
        {
            _properties = properties.ToArray();
        }

        #endregion
    }
}