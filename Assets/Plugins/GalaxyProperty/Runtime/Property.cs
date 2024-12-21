using Game.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Properties
{
    [DebuggerDisplay("{" + nameof(_key) + "}")]
    [Serializable]
    public abstract class Property : IProperty
    {
        [HideLabel]
        [SerializeField]
        [ValidateInput(nameof(ValidateKey))]
        [ValueDropdown(nameof(Keys))]
        private string _key;

#if UNITY_EDITOR
        public string Key
        {
            get => _key;
            set => _key = value;
        }
#endif

        private static IEnumerable<string> Keys
        {
            get
            {
                var textAsset = Resources.Load<TextAsset>($"{nameof(Property)}{nameof(Keys)}");
                var text = textAsset.text.Trim();
                var keys = SerializeFieldUtility.Deserialize<string[]>(text);

                return keys;
            }
        }

        private bool ValidateKey(string key)
        {
            return !string.IsNullOrWhiteSpace(key);
        }

        #region IProperty

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string IProperty.Key => _key;

        #endregion
    }
}
