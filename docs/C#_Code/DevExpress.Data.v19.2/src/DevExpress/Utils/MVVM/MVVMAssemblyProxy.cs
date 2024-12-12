namespace DevExpress.Utils.MVVM
{
    using DevExpress.Utils;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class MVVMAssemblyProxy
    {
        private const string typePrefix = "DevExpress.Mvvm.";
        private static Assembly mvvmAssembly;
        private static bool loadFailed;

        private static void EnsureMvvmAssemblyLoaded()
        {
            Assembly loadedAssembly = AssemblyHelper.GetLoadedAssembly("DevExpress.Mvvm.v19.2");
            Assembly assembly2 = loadedAssembly;
            if (loadedAssembly == null)
            {
                Assembly local1 = loadedAssembly;
                Func<Assembly> load = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<Assembly> local2 = <>c.<>9__4_0;
                    load = <>c.<>9__4_0 = () => Assembly.Load("DevExpress.Mvvm.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a");
                }
                assembly2 = LoadSafe(load);
            }
            mvvmAssembly = assembly2;
        }

        private static Assembly GetMVVMAssembly()
        {
            if (mvvmAssembly == null)
            {
                EnsureMvvmAssemblyLoaded();
            }
            return mvvmAssembly;
        }

        private static Type GetMvvmType(string typeName) => 
            GetMVVMAssembly()?.GetType("DevExpress.Mvvm." + typeName);

        public static Type GetMvvmType(ref Type typeRef, string typeName)
        {
            if (typeRef == null)
            {
                typeRef = GetMvvmType(typeName);
            }
            return typeRef;
        }

        private static Assembly LoadSafe(Func<Assembly> load)
        {
            try
            {
                return (loadFailed ? null : load());
            }
            catch
            {
                loadFailed = true;
                return null;
            }
        }

        public static void Reset()
        {
            mvvmAssembly = null;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MVVMAssemblyProxy.<>c <>9 = new MVVMAssemblyProxy.<>c();
            public static Func<Assembly> <>9__4_0;

            internal Assembly <EnsureMvvmAssemblyLoaded>b__4_0() => 
                Assembly.Load("DevExpress.Mvvm.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a");
        }
    }
}

