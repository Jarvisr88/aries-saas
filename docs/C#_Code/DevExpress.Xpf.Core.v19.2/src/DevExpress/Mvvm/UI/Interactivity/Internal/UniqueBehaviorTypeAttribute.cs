namespace DevExpress.Mvvm.UI.Interactivity.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public sealed class UniqueBehaviorTypeAttribute : Attribute
    {
        public static Type GetDeclaredType(Type type)
        {
            Type baseType = type;
            if (!baseType.GetCustomAttributes(typeof(UniqueBehaviorTypeAttribute), true).Any<object>())
            {
                return null;
            }
            while (true)
            {
                if (baseType.BaseType != null)
                {
                    IEnumerable<UniqueBehaviorTypeAttribute> source = baseType.BaseType.GetCustomAttributes(true).OfType<UniqueBehaviorTypeAttribute>();
                    if (source.Any<UniqueBehaviorTypeAttribute>())
                    {
                        baseType = baseType.BaseType;
                        continue;
                    }
                }
                return baseType;
            }
        }
    }
}

