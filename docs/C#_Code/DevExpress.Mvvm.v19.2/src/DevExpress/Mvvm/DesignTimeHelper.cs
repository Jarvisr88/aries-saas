namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class DesignTimeHelper
    {
        public static T CreateDesignTimeObject<T>() where T: class => 
            CreateDesignTimeObject<T>(0);

        private static T CreateDesignTimeObject<T>(int index) where T: class
        {
            T local2;
            try
            {
                T local = Activator.CreateInstance<T>();
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                int num = 0;
                while (true)
                {
                    if (num >= properties.Length)
                    {
                        local2 = local;
                        break;
                    }
                    PropertyInfo info = properties[num];
                    if (info.CanWrite && (info.GetSetMethod() != null))
                    {
                        info.SetValue(local, DesignTimeValuesProvider.GetDesignTimeValue(info.PropertyType, index), null);
                    }
                    num++;
                }
            }
            catch
            {
                local2 = default(T);
            }
            return local2;
        }

        public static T[] CreateDesignTimeObjects<T>(int count) where T: class
        {
            Func<int, T> selector = <>c__0<T>.<>9__0_0;
            if (<>c__0<T>.<>9__0_0 == null)
            {
                Func<int, T> local1 = <>c__0<T>.<>9__0_0;
                selector = <>c__0<T>.<>9__0_0 = x => CreateDesignTimeObject<T>(x);
            }
            Func<T, bool> predicate = <>c__0<T>.<>9__0_1;
            if (<>c__0<T>.<>9__0_1 == null)
            {
                Func<T, bool> local2 = <>c__0<T>.<>9__0_1;
                predicate = <>c__0<T>.<>9__0_1 = x => x != null;
            }
            return Enumerable.Range(0, count).Select<int, T>(selector).Where<T>(predicate).ToArray<T>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T> where T: class
        {
            public static readonly DesignTimeHelper.<>c__0<T> <>9;
            public static Func<int, T> <>9__0_0;
            public static Func<T, bool> <>9__0_1;

            static <>c__0()
            {
                DesignTimeHelper.<>c__0<T>.<>9 = new DesignTimeHelper.<>c__0<T>();
            }

            internal T <CreateDesignTimeObjects>b__0_0(int x) => 
                DesignTimeHelper.CreateDesignTimeObject<T>(x);

            internal bool <CreateDesignTimeObjects>b__0_1(T x) => 
                x != null;
        }
    }
}

