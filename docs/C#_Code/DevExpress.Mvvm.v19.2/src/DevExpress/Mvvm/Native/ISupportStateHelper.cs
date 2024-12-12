namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ISupportStateHelper
    {
        private static Type GetISupportStateImplementation(Type vmType)
        {
            Func<Type, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = x => x.IsGenericType;
            }
            Func<Type, bool> func2 = <>c.<>9__3_1;
            if (<>c.<>9__3_1 == null)
            {
                Func<Type, bool> local2 = <>c.<>9__3_1;
                func2 = <>c.<>9__3_1 = x => x.GetGenericTypeDefinition() == typeof(ISupportState<>);
            }
            return vmType.GetInterfaces().Where<Type>(predicate).Where<Type>(func2).FirstOrDefault<Type>();
        }

        public static object GetState(object vm)
        {
            if (vm == null)
            {
                return null;
            }
            Type iSupportStateImplementation = GetISupportStateImplementation(vm.GetType());
            return iSupportStateImplementation?.GetMethod("SaveState").Invoke(vm, null);
        }

        public static Type GetStateType(Type vmType)
        {
            Type iSupportStateImplementation = GetISupportStateImplementation(vmType);
            return ((iSupportStateImplementation != null) ? iSupportStateImplementation.GetGenericArguments().First<Type>() : null);
        }

        public static void RestoreState(object vm, object state)
        {
            if ((vm != null) && (state != null))
            {
                Type iSupportStateImplementation = GetISupportStateImplementation(vm.GetType());
                if (iSupportStateImplementation != null)
                {
                    object[] parameters = new object[] { state };
                    iSupportStateImplementation.GetMethod("RestoreState").Invoke(vm, parameters);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ISupportStateHelper.<>c <>9 = new ISupportStateHelper.<>c();
            public static Func<Type, bool> <>9__3_0;
            public static Func<Type, bool> <>9__3_1;

            internal bool <GetISupportStateImplementation>b__3_0(Type x) => 
                x.IsGenericType;

            internal bool <GetISupportStateImplementation>b__3_1(Type x) => 
                x.GetGenericTypeDefinition() == typeof(ISupportState<>);
        }
    }
}

