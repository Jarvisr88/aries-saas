namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class DynamicTypeBuilder
    {
        private static readonly object Locker = new object();
        private static readonly System.Reflection.AssemblyName AssemblyName;
        private static readonly System.Reflection.Emit.ModuleBuilder ModuleBuilder;
        private static readonly Dictionary<string, Tuple<string, Type>> BuiltTypes;

        static DynamicTypeBuilder()
        {
            System.Reflection.AssemblyName name1 = new System.Reflection.AssemblyName();
            name1.Name = "XpfCoreDynamicTypes";
            AssemblyName = name1;
            BuiltTypes = new Dictionary<string, Tuple<string, Type>>();
            ModuleBuilder = AssemblyBuilder.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule(AssemblyName.Name);
        }

        private static void GenerateEquals(TypeBuilder tb, IEnumerable<FieldInfo> fields)
        {
            Type[] parameterTypes = new Type[] { typeof(object) };
            ILGenerator iLGenerator = tb.DefineMethod("Equals", MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public, typeof(bool), parameterTypes).GetILGenerator();
            LocalBuilder local = iLGenerator.DeclareLocal(tb);
            Label label = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Isinst, tb);
            iLGenerator.Emit(OpCodes.Stloc, local);
            iLGenerator.Emit(OpCodes.Ldloc, local);
            iLGenerator.Emit(OpCodes.Brtrue_S, label);
            iLGenerator.Emit(OpCodes.Ldc_I4_0);
            iLGenerator.Emit(OpCodes.Ret);
            iLGenerator.MarkLabel(label);
            foreach (FieldInfo info in fields)
            {
                Type fieldType = info.FieldType;
                Type[] typeArguments = new Type[] { fieldType };
                Type type2 = typeof(EqualityComparer<>).MakeGenericType(typeArguments);
                label = iLGenerator.DefineLabel();
                iLGenerator.EmitCall(OpCodes.Call, type2.GetMethod("get_Default"), null);
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldfld, info);
                iLGenerator.Emit(OpCodes.Ldloc, local);
                iLGenerator.Emit(OpCodes.Ldfld, info);
                Type[] types = new Type[] { fieldType, fieldType };
                iLGenerator.EmitCall(OpCodes.Callvirt, type2.GetMethod("Equals", types), null);
                iLGenerator.Emit(OpCodes.Brtrue_S, label);
                iLGenerator.Emit(OpCodes.Ldc_I4_0);
                iLGenerator.Emit(OpCodes.Ret);
                iLGenerator.MarkLabel(label);
            }
            iLGenerator.Emit(OpCodes.Ldc_I4_1);
            iLGenerator.Emit(OpCodes.Ret);
        }

        private static void GenerateGetHashCode(TypeBuilder tb, IEnumerable<FieldInfo> fields)
        {
            ILGenerator iLGenerator = tb.DefineMethod("GetHashCode", MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public, typeof(int), Type.EmptyTypes).GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldc_I4_0);
            foreach (FieldInfo info in fields)
            {
                Type fieldType = info.FieldType;
                Type[] typeArguments = new Type[] { fieldType };
                Type type2 = typeof(EqualityComparer<>).MakeGenericType(typeArguments);
                iLGenerator.EmitCall(OpCodes.Call, type2.GetMethod("get_Default"), null);
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldfld, info);
                Type[] types = new Type[] { fieldType };
                iLGenerator.EmitCall(OpCodes.Callvirt, type2.GetMethod("GetHashCode", types), null);
                iLGenerator.Emit(OpCodes.Xor);
            }
            iLGenerator.Emit(OpCodes.Ret);
        }

        private static IEnumerable<FieldInfo> GenerateProperties(TypeBuilder tb, IList<DynamicProperty> properties)
        {
            FieldInfo[] infoArray = new FieldBuilder[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                DynamicProperty property = properties[i];
                FieldBuilder field = tb.DefineField("_" + property.Name, property.Type, FieldAttributes.Private);
                PropertyBuilder builder2 = tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.Type, null);
                MethodBuilder mdBuilder = tb.DefineMethod("get_" + property.Name, MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, property.Type, Type.EmptyTypes);
                ILGenerator iLGenerator = mdBuilder.GetILGenerator();
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldfld, field);
                iLGenerator.Emit(OpCodes.Ret);
                Type[] parameterTypes = new Type[] { property.Type };
                MethodBuilder builder4 = tb.DefineMethod("set_" + property.Name, MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, null, parameterTypes);
                ILGenerator generator2 = builder4.GetILGenerator();
                generator2.Emit(OpCodes.Ldarg_0);
                generator2.Emit(OpCodes.Ldarg_1);
                generator2.Emit(OpCodes.Stfld, field);
                generator2.Emit(OpCodes.Ret);
                builder2.SetGetMethod(mdBuilder);
                builder2.SetSetMethod(builder4);
                infoArray[i] = field;
            }
            return infoArray;
        }

        public static Type GetDynamicType(Dictionary<string, Type> fields, Type basetype, Type[] interfaces)
        {
            Type type;
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }
            if (fields.Count == 0)
            {
                throw new ArgumentOutOfRangeException("fields", "fields must have at least 1 field definition");
            }
            try
            {
                Monitor.Enter(Locker);
                string typeKey = GetTypeKey(fields, basetype);
                if (BuiltTypes.ContainsKey(typeKey))
                {
                    type = BuiltTypes[typeKey].Item2;
                }
                else
                {
                    string name = "DataProxy" + BuiltTypes.Count;
                    TypeBuilder tb = ModuleBuilder.DefineType(name, TypeAttributes.Serializable | TypeAttributes.Public, basetype, Type.EmptyTypes);
                    Func<KeyValuePair<string, Type>, DynamicProperty> selector = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Func<KeyValuePair<string, Type>, DynamicProperty> local1 = <>c.<>9__6_0;
                        selector = <>c.<>9__6_0 = x => new DynamicProperty(x.Key, x.Value);
                    }
                    IEnumerable<FieldInfo> enumerable = GenerateProperties(tb, fields.Select<KeyValuePair<string, Type>, DynamicProperty>(selector).ToList<DynamicProperty>());
                    GenerateEquals(tb, enumerable);
                    GenerateGetHashCode(tb, enumerable);
                    BuiltTypes[typeKey] = new Tuple<string, Type>(name, tb.CreateType());
                    type = BuiltTypes[typeKey].Item2;
                }
            }
            finally
            {
                Monitor.Exit(Locker);
            }
            return type;
        }

        private static string GetTypeKey(Dictionary<string, Type> fields, Type baseType)
        {
            Func<KeyValuePair<string, Type>, string> keySelector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<KeyValuePair<string, Type>, string> local1 = <>c.<>9__5_0;
                keySelector = <>c.<>9__5_0 = v => v.Key;
            }
            Func<KeyValuePair<string, Type>, string> func2 = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Func<KeyValuePair<string, Type>, string> local2 = <>c.<>9__5_1;
                func2 = <>c.<>9__5_1 = v => v.Value.Name;
            }
            string str = fields.OrderBy<KeyValuePair<string, Type>, string>(keySelector).ThenBy<KeyValuePair<string, Type>, string>(func2).Aggregate<KeyValuePair<string, Type>, string>(string.Empty, <>c.<>9__5_2 ??= (current, field) => (current + field.Key + ";" + field.Value.FullName + ";"));
            return ((baseType != null) ? (baseType.FullName + "+" + str) : str);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DynamicTypeBuilder.<>c <>9 = new DynamicTypeBuilder.<>c();
            public static Func<KeyValuePair<string, Type>, string> <>9__5_0;
            public static Func<KeyValuePair<string, Type>, string> <>9__5_1;
            public static Func<string, KeyValuePair<string, Type>, string> <>9__5_2;
            public static Func<KeyValuePair<string, Type>, DynamicProperty> <>9__6_0;

            internal DynamicProperty <GetDynamicType>b__6_0(KeyValuePair<string, Type> x) => 
                new DynamicProperty(x.Key, x.Value);

            internal string <GetTypeKey>b__5_0(KeyValuePair<string, Type> v) => 
                v.Key;

            internal string <GetTypeKey>b__5_1(KeyValuePair<string, Type> v) => 
                v.Value.Name;

            internal string <GetTypeKey>b__5_2(string current, KeyValuePair<string, Type> field)
            {
                string[] textArray1 = new string[] { current, field.Key, ";", field.Value.FullName, ";" };
                return string.Concat(textArray1);
            }
        }
    }
}

