namespace DMEWorks.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal abstract class LinqPropertyDescriptor<T> : PropertyDescriptor
    {
        protected LinqPropertyDescriptor(string name) : base(name, null)
        {
        }

        public override bool CanResetValue(object component) => 
            false;

        public static LinqPropertyDescriptor<T> CreateDescriptor(PropertyInfo propertyInfo)
        {
            ParameterExpression instance = Expression.Parameter(typeof(T), "instance");
            ParameterExpression[] parameters = new ParameterExpression[] { instance };
            LambdaExpression expression2 = Expression.Lambda(Expression.Call(instance, propertyInfo.GetGetMethod()), parameters);
            Type[] typeArguments = new Type[] { typeof(T), propertyInfo.PropertyType };
            object[] objArray1 = new object[] { propertyInfo.Name, expression2 };
            return (LinqPropertyDescriptor<T>) typeof(LinqPropertyDescriptor<,>).MakeGenericType(typeArguments).GetConstructors()[0].Invoke(objArray1);
        }

        public static LinqPropertyDescriptor<T>[] CreateDescriptors(string[] names)
        {
            IEnumerable<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if ((names != null) && (names.Length != 0))
            {
                properties = from p in properties
                    where names.Any<string>(n => string.Equals(n, p.Name, StringComparison.OrdinalIgnoreCase))
                    select p;
            }
            Func<PropertyInfo, LinqPropertyDescriptor<T>> selector = <>c<T>.<>9__12_1;
            if (<>c<T>.<>9__12_1 == null)
            {
                Func<PropertyInfo, LinqPropertyDescriptor<T>> local1 = <>c<T>.<>9__12_1;
                selector = <>c<T>.<>9__12_1 = p => LinqPropertyDescriptor<T>.CreateDescriptor(p);
            }
            return properties.Select<PropertyInfo, LinqPropertyDescriptor<T>>(selector).ToArray<LinqPropertyDescriptor<T>>();
        }

        public abstract IComparer<T> GetComparer(ListSortDirection sortDirection);
        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component) => 
            false;

        public abstract LambdaExpression ToExpression();

        public override Type ComponentType =>
            typeof(T);

        public override bool IsReadOnly =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinqPropertyDescriptor<T>.<>c <>9;
            public static Func<PropertyInfo, LinqPropertyDescriptor<T>> <>9__12_1;

            static <>c()
            {
                LinqPropertyDescriptor<T>.<>c.<>9 = new LinqPropertyDescriptor<T>.<>c();
            }

            internal LinqPropertyDescriptor<T> <CreateDescriptors>b__12_1(PropertyInfo p) => 
                LinqPropertyDescriptor<T>.CreateDescriptor(p);
        }
    }
}

