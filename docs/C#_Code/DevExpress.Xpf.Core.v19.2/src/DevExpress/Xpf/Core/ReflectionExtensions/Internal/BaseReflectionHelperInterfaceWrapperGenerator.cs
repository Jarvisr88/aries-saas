namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class BaseReflectionHelperInterfaceWrapperGenerator
    {
        private readonly object element;
        private readonly Type elementType;
        protected internal readonly bool isStatic;
        protected internal readonly ModuleBuilder moduleBuilder;
        protected internal readonly Dictionary<MemberInfo, ReflectionHelperInterfaceWrapperSetting> settings;
        protected internal TypeBuilder typeBuilder;
        protected internal List<object> ctorArgs;
        protected internal List<FieldInfo> ctorInfos;
        protected internal FieldBuilder sourceObjectField;
        protected static readonly Dictionary<InstanceCacheKey, Func<object, object>> CachedConstructors = new Dictionary<InstanceCacheKey, Func<object, object>>(InstanceCacheKey.InstanceCacheKeyComparer);
        protected internal BindingFlags defaultFlags = (BindingFlags.Public | BindingFlags.Instance);
        protected internal ReflectionHelperFallbackModeInternal defaultFallbackModeField = ReflectionHelperFallbackModeInternal.FallbackWithValidation;
        protected internal Type tWrapper;

        static BaseReflectionHelperInterfaceWrapperGenerator()
        {
            SubscribeTypeResolve();
        }

        protected internal BaseReflectionHelperInterfaceWrapperGenerator(ModuleBuilder builder, object element, bool isStatic, Type tWrapper)
        {
            this.tWrapper = tWrapper;
            if (isStatic)
            {
                this.element = null;
                this.elementType = (Type) element;
            }
            else
            {
                IReflectionHelperInterfaceWrapper wrapper = element as IReflectionHelperInterfaceWrapper;
                if (wrapper != null)
                {
                    element = wrapper.Source;
                }
                this.element = element;
                this.elementType = this.element?.GetType();
            }
            this.moduleBuilder = builder;
            this.isStatic = isStatic;
            this.settings = new Dictionary<MemberInfo, ReflectionHelperInterfaceWrapperSetting>();
        }

        protected object CachedCreateImpl()
        {
            Func<object, object> func;
            if (this.ElementType == null)
            {
                return null;
            }
            InstanceCacheKey key = new InstanceCacheKey(this.ElementType, this.tWrapper, this.GetSettingCode(), this.isStatic);
            return (!CachedConstructors.TryGetValue(key, out func) ? this.CreateImpl() : func(this.Element));
        }

        protected object CachedCreateInstance(Type result, List<object> ctorArgs)
        {
            if (this.ElementType == null)
            {
                return null;
            }
            InstanceCacheKey key = new InstanceCacheKey(this.ElementType, this.tWrapper, this.GetSettingCode(), this.isStatic);
            Func<object, object> func = this.CreateConstructor(result, ctorArgs);
            CachedConstructors[key] = func;
            return func(((ctorArgs == null) || (ctorArgs.Count == 0)) ? null : ctorArgs[0]);
        }

        private bool CheckAssignableFromAttribute()
        {
            Func<Type, IEnumerable<object>> selector = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<Type, IEnumerable<object>> local1 = <>c.<>9__24_0;
                selector = <>c.<>9__24_0 = (Func<Type, IEnumerable<object>>) (x => x.GetCustomAttributes(typeof(AssignableFromAttribute), true));
            }
            IEnumerable<AssignableFromAttribute> source = this.IterateInterfaces().SelectMany<Type, object>(selector).Distinct<object>().OfType<AssignableFromAttribute>();
            Func<AssignableFromAttribute, bool> predicate = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Func<AssignableFromAttribute, bool> local2 = <>c.<>9__24_1;
                predicate = <>c.<>9__24_1 = x => !x.Inverse;
            }
            Func<AssignableFromAttribute, string> func3 = <>c.<>9__24_2;
            if (<>c.<>9__24_2 == null)
            {
                Func<AssignableFromAttribute, string> local3 = <>c.<>9__24_2;
                func3 = <>c.<>9__24_2 = x => x.GetTypeName();
            }
            List<string> list = source.Where<AssignableFromAttribute>(predicate).Select<AssignableFromAttribute, string>(func3).ToList<string>();
            Func<AssignableFromAttribute, bool> func4 = <>c.<>9__24_3;
            if (<>c.<>9__24_3 == null)
            {
                Func<AssignableFromAttribute, bool> local4 = <>c.<>9__24_3;
                func4 = <>c.<>9__24_3 = x => x.Inverse;
            }
            Func<AssignableFromAttribute, string> func5 = <>c.<>9__24_4;
            if (<>c.<>9__24_4 == null)
            {
                Func<AssignableFromAttribute, string> local5 = <>c.<>9__24_4;
                func5 = <>c.<>9__24_4 = x => x.GetTypeName();
            }
            List<string> list2 = source.Where<AssignableFromAttribute>(func4).Select<AssignableFromAttribute, string>(func5).ToList<string>();
            foreach (Type type in TypeGeneratorHelper.FlatternType(this.ElementType, true))
            {
                if ((list.Count == 0) && (list2.Count == 0))
                {
                    return true;
                }
                if (list.Contains(type.FullName))
                {
                    list.Remove(type.FullName);
                }
                if (list2.Contains(type.FullName))
                {
                    return false;
                }
            }
            return (list.Count == 0);
        }

        private Func<object, object> CreateConstructor(Type result, List<object> ctorArgs)
        {
            if (result == null)
            {
                return (<>c.<>9__31_0 ??= x => null);
            }
            object[] ctorArgsExceptThisArg = ctorArgs.Skip<object>(1).ToArray<object>();
            Func<object, Type> selector = <>c.<>9__31_1;
            if (<>c.<>9__31_1 == null)
            {
                Func<object, Type> local2 = <>c.<>9__31_1;
                selector = <>c.<>9__31_1 = _ => typeof(object);
            }
            Func<IEnumerable, object> ctor = ReflectionHelper.CreateConstructor<Func<IEnumerable, object>>(result, BindingFlags.Public | BindingFlags.Instance, ctorArgs.Select<object, Type>(selector), true);
            return x => ctor(Yield<object>(x).Concat<object>(ctorArgsExceptThisArg));
        }

        private object CreateImpl()
        {
            if (!this.CheckAssignableFromAttribute())
            {
                return this.CachedCreateInstance(null, null);
            }
            this.typeBuilder = this.moduleBuilder.DefineType(this.tWrapper.Name + Guid.NewGuid(), TypeAttributes.Public, typeof(ReflectionHelperInterfaceWrapper));
            this.typeBuilder.AddInterfaceImplementation(this.tWrapper);
            this.ctorArgs = new List<object>();
            this.ctorInfos = new List<FieldInfo>();
            this.sourceObjectField = this.typeBuilder.DefineField("fieldSourceObject", typeof(object), FieldAttributes.Private);
            this.ctorInfos.Add(this.sourceObjectField);
            this.ctorArgs.Add(this.element);
            foreach (MethodInfo info in this.GetMethods())
            {
                if (!info.IsSpecialName)
                {
                    TypeGeneratorMethodBuilder.DefineMethod(this, info, null, this.GetSetting(info, false), MemberInfoKind.Method);
                }
            }
            foreach (PropertyInfo info2 in this.GetProperties())
            {
                BaseReflectionHelperInterfaceWrapperSetting setting = this.GetSetting(info2, false);
                bool flag = setting.FieldAccessor(info2);
                MethodInfo getMethod = info2.GetGetMethod(true);
                MethodInfo setMethod = info2.GetSetMethod(true);
                if (getMethod != null)
                {
                    if (flag)
                    {
                        TypeGeneratorFieldBuilder.DefineFieldGetterOrSetter(this, info2, getMethod, setting, MemberInfoKind.Field | MemberInfoKind.PropertyGetter);
                    }
                    else
                    {
                        TypeGeneratorMethodBuilder.DefineMethod(this, getMethod, info2, setting, MemberInfoKind.PropertyGetter);
                    }
                }
                if (setMethod != null)
                {
                    if (flag)
                    {
                        TypeGeneratorFieldBuilder.DefineFieldGetterOrSetter(this, info2, setMethod, setting, MemberInfoKind.Field | MemberInfoKind.PropertySetter);
                    }
                    else
                    {
                        TypeGeneratorMethodBuilder.DefineMethod(this, setMethod, info2, setting, MemberInfoKind.PropertySetter);
                    }
                }
            }
            foreach (EventInfo info5 in this.GetEvents())
            {
                MethodInfo addMethod = info5.GetAddMethod(true);
                MethodInfo removeMethod = info5.GetRemoveMethod(true);
                BaseReflectionHelperInterfaceWrapperSetting setting = this.GetSetting(info5, false);
                TypeGeneratorMethodBuilder.DefineMethod(this, addMethod, info5, setting, MemberInfoKind.EventAdd);
                TypeGeneratorMethodBuilder.DefineMethod(this, removeMethod, info5, setting, MemberInfoKind.EventRemove);
            }
            Func<FieldInfo, Type> selector = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<FieldInfo, Type> local1 = <>c.<>9__23_0;
                selector = <>c.<>9__23_0 = x => typeof(object);
            }
            ILGenerator iLGenerator = this.typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, this.ctorInfos.Select<FieldInfo, Type>(selector).ToArray<Type>()).GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            Type[] types = new Type[] { typeof(object) };
            iLGenerator.Emit(OpCodes.Call, typeof(ReflectionHelperInterfaceWrapper).GetConstructor(types));
            for (byte i = 0; i < this.ctorArgs.Count; i = (byte) (i + 1))
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldarg, (int) (i + 1));
                iLGenerator.Emit(OpCodes.Stfld, this.ctorInfos[i]);
            }
            iLGenerator.Emit(OpCodes.Ret);
            Type result = this.typeBuilder.CreateType();
            return this.CachedCreateInstance(result, this.ctorArgs);
        }

        internal object CreateInternal() => 
            this.CachedCreateImpl();

        private static Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args) => 
            null;

        private IEnumerable<EventInfo> GetEvents()
        {
            Func<Type, IEnumerable<EventInfo>> selector = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<Type, IEnumerable<EventInfo>> local1 = <>c.<>9__26_0;
                selector = <>c.<>9__26_0 = (Func<Type, IEnumerable<EventInfo>>) (x => x.GetEvents());
            }
            return this.IterateInterfaces().SelectMany<Type, EventInfo>(selector);
        }

        private IEnumerable<MethodInfo> GetMethods()
        {
            Func<Type, IEnumerable<MethodInfo>> selector = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Func<Type, IEnumerable<MethodInfo>> local1 = <>c.<>9__28_0;
                selector = <>c.<>9__28_0 = (Func<Type, IEnumerable<MethodInfo>>) (x => x.GetMethods());
            }
            return this.IterateInterfaces().SelectMany<Type, MethodInfo>(selector);
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            Func<Type, IEnumerable<PropertyInfo>> selector = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<Type, IEnumerable<PropertyInfo>> local1 = <>c.<>9__25_0;
                selector = <>c.<>9__25_0 = (Func<Type, IEnumerable<PropertyInfo>>) (x => x.GetProperties());
            }
            return this.IterateInterfaces().SelectMany<Type, PropertyInfo>(selector);
        }

        private BaseReflectionHelperInterfaceWrapperSetting GetSetting(MemberInfo wrapperMethodInfo, bool createNew = false)
        {
            ReflectionHelperInterfaceWrapperSetting setting;
            if (!this.settings.TryGetValue(wrapperMethodInfo, out setting))
            {
                if (!createNew)
                {
                    return new NullReflectionHelperInterfaceWrapperSetting(this);
                }
                setting = new ReflectionHelperInterfaceWrapperSetting(this);
                this.settings[wrapperMethodInfo] = setting;
            }
            return setting;
        }

        protected int GetSettingCode()
        {
            int num = 0;
            foreach (ReflectionHelperInterfaceWrapperSetting setting in this.settings.Values)
            {
                num = (num * 0x18d) ^ setting.ComputeKey();
            }
            return num;
        }

        [IteratorStateMachine(typeof(<IterateInterfaces>d__27))]
        private IEnumerable<Type> IterateInterfaces()
        {
            yield return this.tWrapper;
            Type[] interfaces = this.tWrapper.GetInterfaces();
            int index = 0;
            while (true)
            {
                if (index >= interfaces.Length)
                {
                    interfaces = null;
                }
                Type type = interfaces[index];
                yield return type;
                index++;
            }
        }

        [SecuritySafeCritical]
        private static void SubscribeTypeResolve()
        {
            AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(BaseReflectionHelperInterfaceWrapperGenerator.CurrentDomain_TypeResolve);
        }

        internal void WriteSetting(MemberInfo info, Action<ReflectionHelperInterfaceWrapperSetting> func)
        {
            ReflectionHelperInterfaceWrapperSetting setting = (ReflectionHelperInterfaceWrapperSetting) this.GetSetting(info, true);
            func(setting);
        }

        [IteratorStateMachine(typeof(<Yield>d__32))]
        private static IEnumerable<TArg> Yield<TArg>(TArg arg)
        {
            <Yield>d__32<TArg> d__1 = new <Yield>d__32<TArg>(-2);
            d__1.<>3__arg = arg;
            return d__1;
        }

        protected internal object Element =>
            this.element;

        protected internal Type ElementType =>
            this.elementType;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseReflectionHelperInterfaceWrapperGenerator.<>c <>9 = new BaseReflectionHelperInterfaceWrapperGenerator.<>c();
            public static Func<FieldInfo, Type> <>9__23_0;
            public static Func<Type, IEnumerable<object>> <>9__24_0;
            public static Func<AssignableFromAttribute, bool> <>9__24_1;
            public static Func<AssignableFromAttribute, string> <>9__24_2;
            public static Func<AssignableFromAttribute, bool> <>9__24_3;
            public static Func<AssignableFromAttribute, string> <>9__24_4;
            public static Func<Type, IEnumerable<PropertyInfo>> <>9__25_0;
            public static Func<Type, IEnumerable<EventInfo>> <>9__26_0;
            public static Func<Type, IEnumerable<MethodInfo>> <>9__28_0;
            public static Func<object, object> <>9__31_0;
            public static Func<object, Type> <>9__31_1;

            internal IEnumerable<object> <CheckAssignableFromAttribute>b__24_0(Type x) => 
                x.GetCustomAttributes(typeof(AssignableFromAttribute), true);

            internal bool <CheckAssignableFromAttribute>b__24_1(AssignableFromAttribute x) => 
                !x.Inverse;

            internal string <CheckAssignableFromAttribute>b__24_2(AssignableFromAttribute x) => 
                x.GetTypeName();

            internal bool <CheckAssignableFromAttribute>b__24_3(AssignableFromAttribute x) => 
                x.Inverse;

            internal string <CheckAssignableFromAttribute>b__24_4(AssignableFromAttribute x) => 
                x.GetTypeName();

            internal object <CreateConstructor>b__31_0(object x) => 
                null;

            internal Type <CreateConstructor>b__31_1(object _) => 
                typeof(object);

            internal Type <CreateImpl>b__23_0(FieldInfo x) => 
                typeof(object);

            internal IEnumerable<EventInfo> <GetEvents>b__26_0(Type x) => 
                x.GetEvents();

            internal IEnumerable<MethodInfo> <GetMethods>b__28_0(Type x) => 
                x.GetMethods();

            internal IEnumerable<PropertyInfo> <GetProperties>b__25_0(Type x) => 
                x.GetProperties();
        }


        [CompilerGenerated]
        private sealed class <Yield>d__32<TArg> : IEnumerable<TArg>, IEnumerable, IEnumerator<TArg>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TArg <>2__current;
            private int <>l__initialThreadId;
            private TArg arg;
            public TArg <>3__arg;

            [DebuggerHidden]
            public <Yield>d__32(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = this.arg;
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<TArg> IEnumerable<TArg>.GetEnumerator()
            {
                BaseReflectionHelperInterfaceWrapperGenerator.<Yield>d__32<TArg> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new BaseReflectionHelperInterfaceWrapperGenerator.<Yield>d__32<TArg>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (BaseReflectionHelperInterfaceWrapperGenerator.<Yield>d__32<TArg>) this;
                }
                d__.arg = this.<>3__arg;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TArg>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            TArg IEnumerator<TArg>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct InstanceCacheKey
        {
            private static readonly IEqualityComparer<BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey> instanceCacheKeyComparer;
            private Type elementType;
            private Type type;
            private int v;
            private bool isStatic;
            public static IEqualityComparer<BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey> InstanceCacheKeyComparer =>
                instanceCacheKeyComparer;
            public bool Equals(BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey other) => 
                this.elementType.Equals(other.elementType) && (this.type.Equals(other.type) && ((this.v == other.v) && (this.isStatic == other.isStatic)));

            public override bool Equals(object obj) => 
                (obj != null) ? ((obj is BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey) && this.Equals((BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey) obj)) : false;

            public override int GetHashCode()
            {
                int num1 = (this.type.GetHashCode() * 0x18d) ^ this.elementType.GetHashCode();
                return ((((num1 * 0x18d) ^ this.v) * 0x18d) ^ this.isStatic.GetHashCode());
            }

            public static bool operator ==(BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey left, BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey right) => 
                left.Equals(right);

            public static bool operator !=(BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey left, BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey right) => 
                !left.Equals(right);

            public InstanceCacheKey(Type elementType, Type type, int v, bool isStatic)
            {
                this.elementType = elementType;
                this.type = type;
                this.v = v;
                this.isStatic = isStatic;
            }

            static InstanceCacheKey()
            {
                instanceCacheKeyComparer = new InstanceCacheKeyEqualityComparer();
            }
            private sealed class InstanceCacheKeyEqualityComparer : IEqualityComparer<BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey>
            {
                public bool Equals(BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey x, BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey y) => 
                    (x.elementType != null) && ((y.elementType != null) && (x.elementType.Equals(y.elementType) && (x.type.Equals(y.type) && ((x.v == y.v) && (x.isStatic == y.isStatic)))));

                public int GetHashCode(BaseReflectionHelperInterfaceWrapperGenerator.InstanceCacheKey obj) => 
                    obj.GetHashCode();
            }
        }
    }
}

