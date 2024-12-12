namespace DevExpress.Utils.Helpers
{
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class NativeField<T>
    {
        public static Func<T, IntPtr> EmitAccessor(FieldInfo f_nativeField, DynamicMethod accessor, string fallbackProperty = null)
        {
            ILGenerator iLGenerator = accessor.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            if ((f_nativeField != null) || string.IsNullOrEmpty(fallbackProperty))
            {
                iLGenerator.Emit(OpCodes.Ldfld, f_nativeField);
            }
            else
            {
                PropertyInfo property = typeof(T).GetProperty(fallbackProperty, BindingFlags.NonPublic | BindingFlags.Instance);
                if ((property == null) || !property.CanRead)
                {
                    throw new NativeFieldFallbackError<T>(fallbackProperty);
                }
                MethodInfo getMethod = property.GetGetMethod(true);
                iLGenerator.Emit(OpCodes.Castclass, typeof(T));
                iLGenerator.Emit(OpCodes.Call, getMethod);
            }
            iLGenerator.Emit(OpCodes.Ret);
            return (accessor.CreateDelegate(typeof(Func<T, IntPtr>)) as Func<T, IntPtr>);
        }

        public static FieldInfo Ensure(string fieldName) => 
            typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance) ?? NativeField<T>.EnsureNetCoreField(fieldName);

        public static FieldInfo Ensure(string fieldName, string propertyName) => 
            typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance) ?? NativeField<T>.EnsureNetCoreBackingField(fieldName, propertyName);

        public static object EnsureEventKey(string eventName) => 
            (typeof(T).GetField(NativeField<T>.EventName(eventName), BindingFlags.NonPublic | BindingFlags.Static) ?? NativeField<T>.EnsureNetCoreStaticField(NativeField<T>.NetCoreEventName(eventName))).GetValue(null);

        private static FieldInfo EnsureNetCoreBackingField(string fieldName, string propertyName)
        {
            FieldInfo info = null;
            if (FrameworkVersions.IsNetCore3AndAbove())
            {
                info = typeof(T).GetField("_" + fieldName, BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(T).GetField("<" + propertyName + ">k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
            }
            return info;
        }

        private static FieldInfo EnsureNetCoreField(string fieldName)
        {
            Func<string, string> transform = <>c<T>.<>9__10_0;
            if (<>c<T>.<>9__10_0 == null)
            {
                Func<string, string> local1 = <>c<T>.<>9__10_0;
                transform = <>c<T>.<>9__10_0 = x => "_" + x;
            }
            return NativeField<T>.EnsureNetCoreFieldCore(fieldName, BindingFlags.NonPublic | BindingFlags.Instance, transform);
        }

        private static FieldInfo EnsureNetCoreFieldCore(string fieldName, BindingFlags bindingFlags, Func<string, string> transform)
        {
            FieldInfo field = null;
            if (FrameworkVersions.IsNetCore3AndAbove())
            {
                field = typeof(T).GetField(transform(fieldName), bindingFlags);
            }
            return field;
        }

        private static FieldInfo EnsureNetCoreStaticField(string fieldName)
        {
            Func<string, string> transform = <>c<T>.<>9__11_0;
            if (<>c<T>.<>9__11_0 == null)
            {
                Func<string, string> local1 = <>c<T>.<>9__11_0;
                transform = <>c<T>.<>9__11_0 = x => "s_" + x;
            }
            return NativeField<T>.EnsureNetCoreFieldCore(fieldName, BindingFlags.NonPublic | BindingFlags.Static, transform);
        }

        public static FieldInfo EnsureStatic(string fieldName) => 
            typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static) ?? NativeField<T>.EnsureNetCoreStaticField(fieldName);

        private static string EventName(string eventName) => 
            "Event" + eventName;

        private static string NetCoreEventName(string eventName) => 
            char.ToLowerInvariant(eventName[0]).ToString() + eventName.Substring(1) + "Event";

        public static FieldInfo Try(string fieldName) => 
            typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance) ?? NativeField<T>.TryNetCoreField(fieldName);

        private static FieldInfo TryNetCoreField(string fieldName)
        {
            Func<string, string> transform = <>c<T>.<>9__12_0;
            if (<>c<T>.<>9__12_0 == null)
            {
                Func<string, string> local1 = <>c<T>.<>9__12_0;
                transform = <>c<T>.<>9__12_0 = x => "_" + x;
            }
            return NativeField<T>.TryNetCoreFieldCore(fieldName, BindingFlags.NonPublic | BindingFlags.Instance, transform);
        }

        private static FieldInfo TryNetCoreFieldCore(string fieldName, BindingFlags bindingFlags, Func<string, string> transform)
        {
            FieldInfo field = null;
            if (FrameworkVersions.IsNetCore3AndAbove())
            {
                field = typeof(T).GetField(transform(fieldName), bindingFlags);
            }
            return field;
        }

        private static FieldInfo TryNetCoreStaticField(string fieldName)
        {
            Func<string, string> transform = <>c<T>.<>9__13_0;
            if (<>c<T>.<>9__13_0 == null)
            {
                Func<string, string> local1 = <>c<T>.<>9__13_0;
                transform = <>c<T>.<>9__13_0 = x => "s_" + x;
            }
            return NativeField<T>.TryNetCoreFieldCore(fieldName, BindingFlags.NonPublic | BindingFlags.Static, transform);
        }

        public static FieldInfo TryStatic(string fieldName) => 
            typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static) ?? NativeField<T>.TryNetCoreStaticField(fieldName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NativeField<T>.<>c <>9;
            public static Func<string, string> <>9__10_0;
            public static Func<string, string> <>9__11_0;
            public static Func<string, string> <>9__12_0;
            public static Func<string, string> <>9__13_0;

            static <>c()
            {
                NativeField<T>.<>c.<>9 = new NativeField<T>.<>c();
            }

            internal string <EnsureNetCoreField>b__10_0(string x) => 
                "_" + x;

            internal string <EnsureNetCoreStaticField>b__11_0(string x) => 
                "s_" + x;

            internal string <TryNetCoreField>b__12_0(string x) => 
                "_" + x;

            internal string <TryNetCoreStaticField>b__13_0(string x) => 
                "s_" + x;
        }

        private sealed class NativeFieldChangedDramatically : NotSupportedException
        {
            public NativeFieldChangedDramatically(string memberName) : base(string.Concat(textArray1))
            {
                string[] textArray1 = new string[] { "Native API for the ", typeof(T).Name, ".", memberName, " acessor is changed dramatically! Please check the actual sources." };
            }
        }

        private sealed class NativeFieldFallbackError : NotSupportedException
        {
            public NativeFieldFallbackError(string memberName) : base(string.Concat(textArray1))
            {
                string[] textArray1 = new string[] { "There is no get-method found for the ", typeof(T).Name, ".", memberName, " property! Please check the actual sources." };
            }
        }
    }
}

