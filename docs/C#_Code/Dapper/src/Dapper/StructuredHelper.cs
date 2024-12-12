namespace Dapper
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    internal static class StructuredHelper
    {
        private static readonly Hashtable s_udt = new Hashtable();
        private static readonly Hashtable s_tvp = new Hashtable();

        internal static void ConfigureTVP(IDbDataParameter parameter, string typeName)
        {
            GetTVP(parameter.GetType())(parameter, typeName);
        }

        internal static void ConfigureUDT(IDbDataParameter parameter, string typeName)
        {
            GetUDT(parameter.GetType())(parameter, typeName);
        }

        private static Action<IDbDataParameter, string> CreateFor(Type type, string nameProperty, int sqlDbType)
        {
            PropertyInfo property = type.GetProperty(nameProperty, BindingFlags.Public | BindingFlags.Instance);
            if ((property == null) || !property.CanWrite)
            {
                return (<>c.<>9__5_0 ??= delegate (IDbDataParameter p, string n) {
                });
            }
            PropertyInfo info2 = type.GetProperty("SqlDbType", BindingFlags.Public | BindingFlags.Instance);
            if ((info2 != null) && !info2.CanWrite)
            {
                info2 = null;
            }
            Type[] parameterTypes = new Type[] { typeof(IDbDataParameter), typeof(string) };
            DynamicMethod method = new DynamicMethod("CreateFor_" + type.Name, null, parameterTypes, true);
            ILGenerator iLGenerator = method.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Castclass, type);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.EmitCall(OpCodes.Callvirt, property.GetSetMethod(), null);
            if (info2 != null)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Castclass, type);
                iLGenerator.Emit(OpCodes.Ldc_I4, sqlDbType);
                iLGenerator.EmitCall(OpCodes.Callvirt, info2.GetSetMethod(), null);
            }
            iLGenerator.Emit(OpCodes.Ret);
            return (Action<IDbDataParameter, string>) method.CreateDelegate(typeof(Action<IDbDataParameter, string>));
        }

        private static Action<IDbDataParameter, string> GetTVP(Type type) => 
            ((Action<IDbDataParameter, string>) s_tvp[type]) ?? SlowGetHelper(type, s_tvp, "TypeName", 30);

        private static Action<IDbDataParameter, string> GetUDT(Type type) => 
            ((Action<IDbDataParameter, string>) s_udt[type]) ?? SlowGetHelper(type, s_udt, "UdtTypeName", 0x1d);

        private static Action<IDbDataParameter, string> SlowGetHelper(Type type, Hashtable hashtable, string nameProperty, int sqlDbType)
        {
            Hashtable hashtable2 = hashtable;
            lock (hashtable2)
            {
                Action<IDbDataParameter, string> action = (Action<IDbDataParameter, string>) hashtable[type];
                if (action == null)
                {
                    action = CreateFor(type, nameProperty, sqlDbType);
                    hashtable.Add(type, action);
                }
                return action;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StructuredHelper.<>c <>9 = new StructuredHelper.<>c();
            public static Action<IDbDataParameter, string> <>9__5_0;

            internal void <CreateFor>b__5_0(IDbDataParameter p, string n)
            {
            }
        }
    }
}

