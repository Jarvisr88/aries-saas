namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class ViewModelSourceBuilderBase
    {
        private static ViewModelSourceBuilderBase _default;

        public void BuildBindableProperties(Type type, TypeBuilder typeBuilder, MethodInfo raisePropertyChangedMethod, MethodInfo raisePropertyChangingMethod)
        {
            IEnumerable<PropertyInfo> bindableProperties = this.GetBindableProperties(type);
            Dictionary<string, IEnumerable<string>> propertyRelations = GetPropertyRelations(type, bindableProperties);
            foreach (PropertyInfo info in bindableProperties)
            {
                PropertyBuilder builder = BuilderBindableProperty.BuildBindableProperty(type, typeBuilder, info, raisePropertyChangedMethod, raisePropertyChangingMethod, propertyRelations.GetValueOrDefault<string, IEnumerable<string>>(info.Name, null));
                this.BuildBindablePropertyAttributes(info, builder);
            }
        }

        protected virtual void BuildBindablePropertyAttributes(PropertyInfo property, PropertyBuilder builder)
        {
        }

        protected virtual bool ForceOverrideProperty(PropertyInfo property) => 
            false;

        public IEnumerable<PropertyInfo> GetBindableProperties(Type type) => 
            from x in type.GetProperties()
                where BuilderCommon.IsBindableProperty(x) || this.ForceOverrideProperty(x)
                select x;

        private static Dictionary<string, IEnumerable<string>> GetPropertyRelations(Type type, IEnumerable<PropertyInfo> bindableProperties)
        {
            Dictionary<string, IEnumerable<string>> dictionary = new Dictionary<string, IEnumerable<string>>();
            PropertyInfo[] properties = type.GetProperties();
            Func<PropertyInfo, string> selector = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<PropertyInfo, string> local1 = <>c.<>9__7_0;
                selector = <>c.<>9__7_0 = x => x.Name;
            }
            IEnumerable<string> source = properties.Select<PropertyInfo, string>(selector);
            Func<PropertyInfo, string> func2 = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<PropertyInfo, string> local2 = <>c.<>9__7_1;
                func2 = <>c.<>9__7_1 = x => x.Name;
            }
            IEnumerable<string> enumerable2 = bindableProperties.Select<PropertyInfo, string>(func2);
            foreach (PropertyInfo info in properties)
            {
                IEnumerable<string> enumerable1;
                IEnumerable<DependsOnPropertiesAttribute> attributes = BuilderCommon.GetAttributes<DependsOnPropertiesAttribute>(info, true);
                if (!attributes.Any<DependsOnPropertiesAttribute>())
                {
                    enumerable1 = null;
                }
                else
                {
                    Func<DependsOnPropertiesAttribute, string[]> func3 = <>c.<>9__7_2;
                    if (<>c.<>9__7_2 == null)
                    {
                        Func<DependsOnPropertiesAttribute, string[]> local3 = <>c.<>9__7_2;
                        func3 = <>c.<>9__7_2 = x => x.Properties;
                    }
                    Func<string[], string[], string[]> func = <>c.<>9__7_3;
                    if (<>c.<>9__7_3 == null)
                    {
                        Func<string[], string[], string[]> local4 = <>c.<>9__7_3;
                        func = <>c.<>9__7_3 = (x, y) => x.Concat<string>(y).ToArray<string>();
                    }
                    enumerable1 = attributes.Select<DependsOnPropertiesAttribute, string[]>(func3).Aggregate<string[]>(func).Distinct<string>();
                }
                IEnumerable<string> enumerable4 = enumerable1;
                if (enumerable4 != null)
                {
                    foreach (string str in enumerable4)
                    {
                        if (!source.Contains<string>(str))
                        {
                            throw new ViewModelSourceException($"The {info.Name} property cannot depend on the {str} property, because the latter does not exist.");
                        }
                        if (!enumerable2.Contains<string>(str))
                        {
                            throw new ViewModelSourceException($"The {info.Name} property cannot depend on the {str} property, because the latter is not bindable.");
                        }
                        if (!dictionary.ContainsKey(str))
                        {
                            dictionary.Add(str, new string[0]);
                        }
                        dictionary[str] = dictionary[str].Concat<string>(info.Name.Yield<string>());
                    }
                }
            }
            return dictionary;
        }

        internal static ViewModelSourceBuilderBase Default =>
            _default ??= new ViewModelSourceBuilderBase();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelSourceBuilderBase.<>c <>9 = new ViewModelSourceBuilderBase.<>c();
            public static Func<PropertyInfo, string> <>9__7_0;
            public static Func<PropertyInfo, string> <>9__7_1;
            public static Func<DependsOnPropertiesAttribute, string[]> <>9__7_2;
            public static Func<string[], string[], string[]> <>9__7_3;

            internal string <GetPropertyRelations>b__7_0(PropertyInfo x) => 
                x.Name;

            internal string <GetPropertyRelations>b__7_1(PropertyInfo x) => 
                x.Name;

            internal string[] <GetPropertyRelations>b__7_2(DependsOnPropertiesAttribute x) => 
                x.Properties;

            internal string[] <GetPropertyRelations>b__7_3(string[] x, string[] y) => 
                x.Concat<string>(y).ToArray<string>();
        }
    }
}

