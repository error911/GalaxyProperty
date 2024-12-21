using JetBrains.Annotations;
using System;

namespace Properties
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DesignerPropertyAttribute : Attribute
    {
        public readonly string PropertyName;
        [NotNull]
        public readonly Type PropertyType;
        public readonly object DefaultValue;
        public readonly bool Optional;

        public DesignerPropertyAttribute(string propertyName, [NotNull] Type propertyType, object defaultValue, bool optional = false)
        {
            PropertyName = propertyName;
            PropertyType = propertyType;
            DefaultValue = defaultValue;
            Optional = optional;
        }
    }
}
