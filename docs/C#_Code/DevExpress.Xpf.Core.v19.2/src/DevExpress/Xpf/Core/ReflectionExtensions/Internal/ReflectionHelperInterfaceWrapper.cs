namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.ReflectionExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ReflectionHelperInterfaceWrapper : IReflectionHelperInterfaceWrapper
    {
        private const int CallsToCleanup = 100;
        public static readonly MethodInfo GetDelegateMethodInfo;
        public static readonly MethodInfo GetGenericDelegateMethodInfo;
        public static readonly MethodInfo GetFieldGetterMethodInfo;
        public static readonly MethodInfo GetFieldSetterMethodInfo;
        public static readonly MethodInfo WrapMethodInfo;
        public static readonly MethodInfo UnwrapMethodInfo;
        public static readonly MethodInfo WrapDelegateMethodInfo;
        private static readonly Dictionary<LocalClosureFactoryCacheKey, WeakReference<Func<object, object>>> globalClosureFactoryCache = new Dictionary<LocalClosureFactoryCacheKey, WeakReference<Func<object, object>>>();
        private static readonly Dictionary<LocalFieldCacheKey, WeakReference<Delegate>> globalFieldGetterCache = new Dictionary<LocalFieldCacheKey, WeakReference<Delegate>>();
        private static readonly Dictionary<LocalFieldCacheKey, WeakReference<Delegate>> globalFieldSetterCache = new Dictionary<LocalFieldCacheKey, WeakReference<Delegate>>();
        private static readonly Dictionary<LocalGenericDelegateCacheKey, WeakReference<Delegate>> globalGenericDelegateCache = new Dictionary<LocalGenericDelegateCacheKey, WeakReference<Delegate>>();
        private static readonly Dictionary<LocalDelegateCacheKey, WeakReference<Delegate>> globalDelegateCache = new Dictionary<LocalDelegateCacheKey, WeakReference<Delegate>>();
        private readonly Dictionary<LocalFieldCacheKey, Delegate> localFieldGetterCache;
        private readonly Dictionary<LocalFieldCacheKey, Delegate> localFieldSetterCache;
        private readonly Dictionary<LocalGenericDelegateCacheKey, Delegate> localGenericDelegateCache;
        private readonly Dictionary<LocalDelegateCacheKey, Delegate> localDelegateCache;
        private readonly Dictionary<LocalClosureFactoryCacheKey, Func<object, object>> localClosureFactoryCache;
        private readonly Dictionary<LocalClosureCacheKey, WeakReference<Delegate>> localClosureCache;
        private object source;
        private static int currentCalls;
        private static int performingCleanup;
        private static readonly object cleanupLock = new object();

        static ReflectionHelperInterfaceWrapper()
        {
            ParameterExpression instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] arguments = new Expression[] { Expression.Constant(null, typeof(MethodInfo)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(Type)), Expression.Constant(false, typeof(bool)) };
            ParameterExpression[] parameters = new ParameterExpression[] { instance };
            GetDelegateMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(instance, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.GetDelegate), arguments), parameters));
            instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] expressionArray3 = new Expression[] { Expression.Constant(null, typeof(MethodInfo)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(Type)), Expression.Constant(false, typeof(bool)), Expression.Constant(null, typeof(Type[])) };
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { instance };
            GetGenericDelegateMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(instance, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.GetGenericDelegate), expressionArray3), expressionArray4));
            instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] expressionArray5 = new Expression[] { Expression.Constant(null, typeof(FieldInfo)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(Type)), Expression.Constant(false, typeof(bool)) };
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { instance };
            GetFieldGetterMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(instance, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.GetFieldGetter), expressionArray5), expressionArray6));
            instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] expressionArray7 = new Expression[] { Expression.Constant(null, typeof(FieldInfo)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(Type)), Expression.Constant(false, typeof(bool)) };
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { instance };
            GetFieldSetterMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(instance, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.GetFieldSetter), expressionArray7), expressionArray8));
            instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] expressionArray9 = new Expression[] { Expression.Constant(null, typeof(Delegate)), Expression.Constant(null, typeof(Type)), Expression.Constant(null, typeof(ReflectionHelperInterfaceWrapper)) };
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { instance };
            WrapDelegateMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(null, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.WrapDelegate), expressionArray9), expressionArray10));
            instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] expressionArray11 = new Expression[] { Expression.Constant(null, typeof(object)), Expression.Constant(null, typeof(Type)) };
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { instance };
            WrapMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(null, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.Wrap), expressionArray11), expressionArray12));
            instance = Expression.Parameter(typeof(ReflectionHelperInterfaceWrapper), "x");
            Expression[] expressionArray13 = new Expression[] { Expression.Constant(null, typeof(ReflectionHelperInterfaceWrapper)) };
            ParameterExpression[] expressionArray14 = new ParameterExpression[] { instance };
            UnwrapMethodInfo = GetMethodInfo(Expression.Lambda<Action<ReflectionHelperInterfaceWrapper>>(Expression.Call(null, (MethodInfo) methodof(ReflectionHelperInterfaceWrapper.Unwrap), expressionArray13), expressionArray14));
        }

        public ReflectionHelperInterfaceWrapper()
        {
            this.localFieldGetterCache = new Dictionary<LocalFieldCacheKey, Delegate>();
            this.localFieldSetterCache = new Dictionary<LocalFieldCacheKey, Delegate>();
            this.localGenericDelegateCache = new Dictionary<LocalGenericDelegateCacheKey, Delegate>();
            this.localDelegateCache = new Dictionary<LocalDelegateCacheKey, Delegate>();
            this.localClosureFactoryCache = new Dictionary<LocalClosureFactoryCacheKey, Func<object, object>>();
            this.localClosureCache = new Dictionary<LocalClosureCacheKey, WeakReference<Delegate>>();
        }

        public ReflectionHelperInterfaceWrapper(object source) : this()
        {
            this.source = source;
        }

        private static void CheckCleanup()
        {
            currentCalls++;
            if ((currentCalls > 100) && (performingCleanup == 0))
            {
                object cleanupLock = ReflectionHelperInterfaceWrapper.cleanupLock;
                lock (cleanupLock)
                {
                    if ((performingCleanup == 0) && (currentCalls >= 100))
                    {
                        try
                        {
                            performingCleanup++;
                            DoLockedCleanup<LocalDelegateCacheKey>(globalDelegateCache);
                            DoLockedCleanup<LocalFieldCacheKey>(globalFieldGetterCache);
                            DoLockedCleanup<LocalFieldCacheKey>(globalFieldSetterCache);
                            DoLockedCleanup<LocalGenericDelegateCacheKey>(globalGenericDelegateCache);
                        }
                        finally
                        {
                            performingCleanup--;
                        }
                    }
                }
            }
        }

        private static Func<object, object> CreateClosureFactory(Type sourceType, Type targetType)
        {
            TypeBuilder builder2 = ReflectionHelperExtensions.GetModuleBuilder(typeof(object)).DefineType("closure" + Guid.NewGuid(), TypeAttributes.Public);
            builder2.AddInterfaceImplementation(typeof(IReflectionHelperClosure));
            FieldBuilder field = builder2.DefineField("delegate", typeof(Delegate), FieldAttributes.Private);
            Type[] parameterTypes = new Type[] { typeof(Delegate) };
            ILGenerator iLGenerator = builder2.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes).GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Stfld, field);
            iLGenerator.Emit(OpCodes.Ret);
            MethodInfo method = targetType.GetMethod("Invoke");
            MethodInfo methodInfo = sourceType.GetMethod("Invoke");
            Func<ParameterInfo, Type> selector = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__48_0;
                selector = <>c.<>9__48_0 = x => x.ParameterType;
            }
            Type[] typeArray = method.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>();
            Func<ParameterInfo, Type> func2 = <>c.<>9__48_1;
            if (<>c.<>9__48_1 == null)
            {
                Func<ParameterInfo, Type> local2 = <>c.<>9__48_1;
                func2 = <>c.<>9__48_1 = x => x.ParameterType;
            }
            Type[] typeArray2 = methodInfo.GetParameters().Select<ParameterInfo, Type>(func2).ToArray<Type>();
            MethodBuilder meth = builder2.DefineMethod("Invoke", MethodAttributes.Public, CallingConventions.Standard, method.ReturnType, typeArray);
            ILGenerator generator = meth.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, field);
            for (int i = 0; i < typeArray.Length; i++)
            {
                Type type2 = typeArray2[i];
                Type type3 = typeArray[i];
                generator.Emit(OpCodes.Ldarg, (int) (i + 1));
                ReflectionHelper.CastClass(generator, type3, type2, true);
            }
            generator.EmitCall(OpCodes.Call, methodInfo, null);
            ReflectionHelper.CastClass(generator, methodInfo.ReturnType, method.ReturnType, true);
            generator.Emit(OpCodes.Ret);
            MethodBuilder methodInfoBody = builder2.DefineMethod("GetDelegate", MethodAttributes.Virtual | MethodAttributes.Public, CallingConventions.Standard, typeof(Delegate), new Type[0]);
            ILGenerator generator3 = methodInfoBody.GetILGenerator();
            generator3.Emit(OpCodes.Ldarg_0);
            generator3.Emit(OpCodes.Ldftn, meth);
            generator3.Emit(OpCodes.Newobj, targetType.GetConstructors()[0]);
            generator3.Emit(OpCodes.Ret);
            builder2.DefineMethodOverride(methodInfoBody, typeof(IReflectionHelperClosure).GetMethod("GetDelegate"));
            return ReflectionHelper.CreateConstructor(builder2.CreateType(), typeof(Delegate));
        }

        private Delegate CreateDelegate(MethodInfo info, Type instanceType, Type delegateType, bool useTuple) => 
            (Delegate) ReflectionHelper.CreateMethodHandlerImpl(info, instanceType, delegateType, true, new bool?(useTuple));

        private static void DoLockedCleanup<TKey>(Dictionary<TKey, WeakReference<Delegate>> dictionary) where TKey: struct
        {
            foreach (TKey local in dictionary.Keys.ToArray<TKey>())
            {
                if (!dictionary[local].IsAlive)
                {
                    dictionary.Remove(local);
                }
            }
        }

        private static void DoNotRemove(object obj)
        {
        }

        private bool GetCachedValue<TKey, TValue>(TKey key, Dictionary<TKey, WeakReference<TValue>> globalCache, Dictionary<TKey, TValue> localCache, out TValue result) where TKey: struct where TValue: class
        {
            if (!localCache.TryGetValue(key, out result))
            {
                WeakReference<TValue> reference;
                if (!globalCache.TryGetValue(key, out reference))
                {
                    return false;
                }
                result = reference.Target;
                if (!reference.IsAlive)
                {
                    globalCache.Remove(key);
                    return false;
                }
                localCache[key] = result;
                CheckCleanup();
            }
            return true;
        }

        public Delegate GetDelegate(MethodInfo info, Type instanceType, Type delegateType, bool useTuple)
        {
            Delegate delegate2;
            LocalDelegateCacheKey key = new LocalDelegateCacheKey(info, instanceType, delegateType, useTuple);
            if (!this.GetCachedValue<LocalDelegateCacheKey, Delegate>(key, globalDelegateCache, this.localDelegateCache, out delegate2))
            {
                delegate2 = this.CreateDelegate(info, instanceType, delegateType, useTuple);
                this.SetCachedValue<LocalDelegateCacheKey, Delegate>(key, delegate2, globalDelegateCache, this.localDelegateCache);
            }
            return delegate2;
        }

        public Delegate GetFieldGetter(FieldInfo info, Type delegateType, Type tElement, Type tField, bool addThisArgForStatic)
        {
            Delegate delegate2;
            LocalFieldCacheKey key = new LocalFieldCacheKey(info, delegateType, tElement, tField, addThisArgForStatic);
            if (!this.GetCachedValue<LocalFieldCacheKey, Delegate>(key, globalFieldGetterCache, this.localFieldGetterCache, out delegate2))
            {
                delegate2 = ReflectionHelper.CreateFieldGetterOrSetter(true, delegateType, info, tElement, tField, !addThisArgForStatic);
                this.SetCachedValue<LocalFieldCacheKey, Delegate>(key, delegate2, globalFieldGetterCache, this.localFieldGetterCache);
            }
            return delegate2;
        }

        public Delegate GetFieldSetter(FieldInfo info, Type delegateType, Type tElement, Type tField, bool addThisArgForStatic)
        {
            Delegate delegate2;
            LocalFieldCacheKey key = new LocalFieldCacheKey(info, delegateType, tElement, tField, addThisArgForStatic);
            if (!this.GetCachedValue<LocalFieldCacheKey, Delegate>(key, globalFieldSetterCache, this.localFieldSetterCache, out delegate2))
            {
                delegate2 = ReflectionHelper.CreateFieldGetterOrSetter(false, delegateType, info, tElement, tField, !addThisArgForStatic);
                this.SetCachedValue<LocalFieldCacheKey, Delegate>(key, delegate2, globalFieldSetterCache, this.localFieldSetterCache);
            }
            return delegate2;
        }

        public Delegate GetGenericDelegate(MethodInfo info, Type instanceType, Type delegateType, bool useTyple, Type[] paramTypes)
        {
            Delegate delegate2;
            LocalGenericDelegateCacheKey key = new LocalGenericDelegateCacheKey(info, instanceType, delegateType, useTyple, paramTypes);
            if (!this.GetCachedValue<LocalGenericDelegateCacheKey, Delegate>(key, globalGenericDelegateCache, this.localGenericDelegateCache, out delegate2))
            {
                delegate2 = this.CreateDelegate(info.MakeGenericMethod(paramTypes), instanceType, delegateType, useTyple);
                this.SetCachedValue<LocalGenericDelegateCacheKey, Delegate>(key, delegate2, globalGenericDelegateCache, this.localGenericDelegateCache);
            }
            return delegate2;
        }

        private static MethodInfo GetMethodInfo(Expression<Action<ReflectionHelperInterfaceWrapper>> expr) => 
            (expr.Body as MethodCallExpression).Method;

        private Delegate GetOrCreateClosure(Delegate source, Type targetType)
        {
            WeakReference<Delegate> reference;
            Delegate target;
            LocalClosureCacheKey key = new LocalClosureCacheKey(source, targetType);
            if (this.localClosureCache.TryGetValue(key, out reference))
            {
                target = reference.Target;
                if (target != null)
                {
                    return target;
                }
                this.localClosureCache.Remove(key);
            }
            target = ((IReflectionHelperClosure) this.GetOrCreateClosureFactory(source, targetType)(source)).GetDelegate();
            this.localClosureCache.Add(key, new WeakReference<Delegate>(target));
            return target;
        }

        private Func<object, object> GetOrCreateClosureFactory(Delegate source, Type targetType)
        {
            Func<object, object> func;
            LocalClosureFactoryCacheKey key = new LocalClosureFactoryCacheKey(source.Method, targetType);
            if (!this.GetCachedValue<LocalClosureFactoryCacheKey, Func<object, object>>(key, globalClosureFactoryCache, this.localClosureFactoryCache, out func))
            {
                func = CreateClosureFactory(source.GetType(), targetType);
                this.SetCachedValue<LocalClosureFactoryCacheKey, Func<object, object>>(key, func, globalClosureFactoryCache, this.localClosureFactoryCache);
            }
            return func;
        }

        private void SetCachedValue<TKey, TValue>(TKey key, TValue result, Dictionary<TKey, WeakReference<TValue>> globalCache, Dictionary<TKey, TValue> localCache) where TKey: struct where TValue: class
        {
            performingCleanup++;
            localCache[key] = result;
            object cleanupLock = ReflectionHelperInterfaceWrapper.cleanupLock;
            lock (cleanupLock)
            {
                globalCache[key] = new WeakReference<TValue>(result);
            }
            performingCleanup--;
            CheckCleanup();
        }

        public static object Unwrap(ReflectionHelperInterfaceWrapper wrapper)
        {
            DoNotRemove(wrapper);
            return ((wrapper != null) ? wrapper.source : null);
        }

        public static object Wrap(object obj, Type wrapperType)
        {
            DoNotRemove(obj);
            DoNotRemove(wrapperType);
            return ((obj != null) ? ReflectionHelperExtensions.Wrap(obj, wrapperType) : null);
        }

        public static Delegate WrapDelegate(Delegate source, Type targetType, ReflectionHelperInterfaceWrapper _this)
        {
            if (source == null)
            {
                return null;
            }
            if (targetType == null)
            {
                throw new NullReferenceException();
            }
            return _this.GetOrCreateClosure(source, targetType);
        }

        object IReflectionHelperInterfaceWrapper.Source =>
            this.source;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReflectionHelperInterfaceWrapper.<>c <>9 = new ReflectionHelperInterfaceWrapper.<>c();
            public static Func<ParameterInfo, Type> <>9__48_0;
            public static Func<ParameterInfo, Type> <>9__48_1;

            internal Type <CreateClosureFactory>b__48_0(ParameterInfo x) => 
                x.ParameterType;

            internal Type <CreateClosureFactory>b__48_1(ParameterInfo x) => 
                x.ParameterType;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LocalClosureCacheKey
        {
            private readonly ReflectionHelperInterfaceWrapper.WeakReference<Delegate> _Source;
            private readonly Type _TargetType;
            public bool Equals(ReflectionHelperInterfaceWrapper.LocalClosureCacheKey other) => 
                this._Source.Equals(other._Source) && this._TargetType.Equals(other._TargetType);

            public override bool Equals(object obj) => 
                (obj != null) ? ((obj is ReflectionHelperInterfaceWrapper.LocalClosureCacheKey) && this.Equals((ReflectionHelperInterfaceWrapper.LocalClosureCacheKey) obj)) : false;

            public override int GetHashCode() => 
                (this._Source.GetHashCode() * 0x18d) ^ this._TargetType.GetHashCode();

            public static bool operator ==(ReflectionHelperInterfaceWrapper.LocalClosureCacheKey left, ReflectionHelperInterfaceWrapper.LocalClosureCacheKey right) => 
                left.Equals(right);

            public static bool operator !=(ReflectionHelperInterfaceWrapper.LocalClosureCacheKey left, ReflectionHelperInterfaceWrapper.LocalClosureCacheKey right) => 
                !left.Equals(right);

            public LocalClosureCacheKey(Delegate source, Type targetType)
            {
                this._Source = new ReflectionHelperInterfaceWrapper.WeakReference<Delegate>(source);
                this._TargetType = targetType;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LocalClosureFactoryCacheKey
        {
            private readonly MethodInfo _MethodInfo;
            private readonly Type _Type;
            public bool Equals(ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey other) => 
                this._MethodInfo.Equals(other._MethodInfo) && this._Type.Equals(other._Type);

            public override bool Equals(object obj) => 
                (obj != null) ? ((obj is ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey) && this.Equals((ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey) obj)) : false;

            public override int GetHashCode() => 
                (this._MethodInfo.GetHashCode() * 0x18d) ^ this._Type.GetHashCode();

            public static bool operator ==(ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey left, ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey right) => 
                left.Equals(right);

            public static bool operator !=(ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey left, ReflectionHelperInterfaceWrapper.LocalClosureFactoryCacheKey right) => 
                !left.Equals(right);

            public LocalClosureFactoryCacheKey(MethodInfo methodInfo, Type type)
            {
                this._MethodInfo = methodInfo;
                this._Type = type;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LocalDelegateCacheKey
        {
            private readonly Type delegateType;
            private readonly MethodInfo info;
            private readonly Type instanceType;
            private readonly bool useTuple;
            private bool Equals(ReflectionHelperInterfaceWrapper.LocalDelegateCacheKey other) => 
                (this.delegateType == other.delegateType) && (Equals(this.info, other.info) && ((this.instanceType == other.instanceType) && (this.useTuple == other.useTuple)));

            public override bool Equals(object obj) => 
                (obj != null) ? ((obj is ReflectionHelperInterfaceWrapper.LocalDelegateCacheKey) && this.Equals((ReflectionHelperInterfaceWrapper.LocalDelegateCacheKey) obj)) : false;

            public override int GetHashCode() => 
                (((((((this.delegateType != null) ? this.delegateType.GetHashCode() : 0) * 0x18d) ^ ((this.info != null) ? this.info.GetHashCode() : 0)) * 0x18d) ^ ((this.instanceType != null) ? this.instanceType.GetHashCode() : 0)) * 0x18d) ^ this.useTuple.GetHashCode();

            public LocalDelegateCacheKey(MethodInfo info, Type instanceType, Type delegateType, bool useTuple)
            {
                this.info = info;
                this.instanceType = instanceType;
                this.delegateType = delegateType;
                this.useTuple = useTuple;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LocalFieldCacheKey
        {
            private readonly bool addThisArgForStatic;
            private readonly Type delegateType;
            private readonly FieldInfo info;
            private readonly Type tElement;
            private readonly Type tField;
            private bool Equals(ReflectionHelperInterfaceWrapper.LocalFieldCacheKey other) => 
                (this.addThisArgForStatic == other.addThisArgForStatic) && ((this.delegateType == other.delegateType) && (Equals(this.info, other.info) && ((this.tElement == other.tElement) && (this.tField == other.tField))));

            public override bool Equals(object obj) => 
                (obj != null) ? ((obj is ReflectionHelperInterfaceWrapper.LocalFieldCacheKey) && this.Equals((ReflectionHelperInterfaceWrapper.LocalFieldCacheKey) obj)) : false;

            public override int GetHashCode() => 
                (((((((this.addThisArgForStatic.GetHashCode() * 0x18d) ^ ((this.delegateType != null) ? this.delegateType.GetHashCode() : 0)) * 0x18d) ^ ((this.info != null) ? this.info.GetHashCode() : 0)) * 0x18d) ^ ((this.tElement != null) ? this.tElement.GetHashCode() : 0)) * 0x18d) ^ ((this.tField != null) ? this.tField.GetHashCode() : 0);

            public LocalFieldCacheKey(FieldInfo info, Type delegateType, Type tElement, Type tField, bool addThisArgForStatic)
            {
                this.info = info;
                this.delegateType = delegateType;
                this.tElement = tElement;
                this.tField = tField;
                this.addThisArgForStatic = addThisArgForStatic;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LocalGenericDelegateCacheKey
        {
            private readonly Type delegateType;
            private readonly MethodInfo info;
            private readonly Type instanceType;
            private readonly Type[] paramTypes;
            private readonly bool useTyple;
            private bool Equals(ReflectionHelperInterfaceWrapper.LocalGenericDelegateCacheKey other) => 
                (this.delegateType == other.delegateType) && (Equals(this.info, other.info) && ((this.instanceType == other.instanceType) && (Equals(this.paramTypes, other.paramTypes) && (this.useTyple == other.useTyple))));

            public override bool Equals(object obj) => 
                (obj != null) ? ((obj is ReflectionHelperInterfaceWrapper.LocalGenericDelegateCacheKey) && this.Equals((ReflectionHelperInterfaceWrapper.LocalGenericDelegateCacheKey) obj)) : false;

            public override int GetHashCode() => 
                (((((((((this.delegateType != null) ? this.delegateType.GetHashCode() : 0) * 0x18d) ^ ((this.info != null) ? this.info.GetHashCode() : 0)) * 0x18d) ^ ((this.instanceType != null) ? this.instanceType.GetHashCode() : 0)) * 0x18d) ^ ((this.paramTypes != null) ? this.paramTypes.GetHashCode() : 0)) * 0x18d) ^ this.useTyple.GetHashCode();

            public LocalGenericDelegateCacheKey(MethodInfo info, Type instanceType, Type delegateType, bool useTyple, Type[] paramTypes)
            {
                this.info = info;
                this.instanceType = instanceType;
                this.delegateType = delegateType;
                this.useTyple = useTyple;
                this.paramTypes = paramTypes;
            }
        }

        private class WeakReference<T> where T: class
        {
            private WeakReference impl;
            private int hashCode;

            public WeakReference() : this(null)
            {
            }

            public WeakReference(object obj) : this(obj, false)
            {
            }

            public WeakReference(object obj, bool trackResurrection)
            {
                this.impl = new WeakReference(obj, trackResurrection);
                if (obj != null)
                {
                    this.hashCode = obj.GetHashCode();
                }
            }

            public override bool Equals(object obj)
            {
                ReflectionHelperInterfaceWrapper.WeakReference<T> objA = obj as ReflectionHelperInterfaceWrapper.WeakReference<T>;
                return ((objA != null) ? ((objA.GetHashCode() == this.hashCode) ? (ReferenceEquals(objA, this) || Equals(objA.Target, this.Target)) : false) : false);
            }

            public override int GetHashCode() => 
                this.hashCode;

            public bool IsAlive =>
                this.impl.IsAlive;

            public T Target =>
                (T) this.impl.Target;

            public bool TrackResurrection =>
                this.impl.TrackResurrection;
        }
    }
}

