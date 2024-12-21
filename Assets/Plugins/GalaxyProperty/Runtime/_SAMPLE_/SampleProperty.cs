using Sirenix.OdinInspector;
using UnityEngine;

namespace Properties.Samples
{
    [System.Serializable]
    [CreateAssetMenu(fileName = nameof(SampleProperty),
        menuName = nameof(SampleProperty) + "/TEST/" + nameof(SampleProperty))]
    public class SampleProperty : ScriptableObject
    {
        [SerializeReference] public IPropertyProvider i_propertyMap;
        [SerializeField] private PropertyMap _propertyMap;

        [Button("Test interface")]
        private void Test_01()
        {
            if (i_propertyMap == null) return;

            if (i_propertyMap.TryGetPropertyValue<float>("Settings", out var res1))
            {
                Debug.Log($"Prop1> {res1}");
            }

            if (i_propertyMap.TryGetProperty("Settings", out var res2))
            {
                Debug.Log($"Prop2> {res2}");
            }
        }

        [Button("Test class")]
        private void Test_02()
        {
            if (_propertyMap.TryGetProperty("Settings", out var prop))
            {
                Debug.Log($"Prop1> {prop.ToString()}");
            }

            if (_propertyMap.TryGetPropertyValue<float>("Settings", out var prop2))
            {
                Debug.Log($"Prop2> {prop2.ToString()}");
            }

        }
    }
}