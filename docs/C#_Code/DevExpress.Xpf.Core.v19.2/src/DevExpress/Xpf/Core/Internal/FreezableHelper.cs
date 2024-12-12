namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class FreezableHelper
    {
        public static TFreezable TryGetAsFrozen<TFreezable>(this TFreezable element, bool force = false) where TFreezable: Freezable
        {
            Func<TFreezable, Func<Freezable>> freezeFunc = <>c__0<TFreezable>.<>9__0_0;
            if (<>c__0<TFreezable>.<>9__0_0 == null)
            {
                Func<TFreezable, Func<Freezable>> local1 = <>c__0<TFreezable>.<>9__0_0;
                freezeFunc = <>c__0<TFreezable>.<>9__0_0 = x => new Func<Freezable>(x.GetAsFrozen);
            }
            return TryGetAsFrozenImpl<TFreezable>(element, freezeFunc, <>c__0<TFreezable>.<>9__0_1 ??= x => new Func<Freezable>(x.Clone), force);
        }

        private static TFreezable TryGetAsFrozenImpl<TFreezable>(TFreezable element, Func<TFreezable, Func<Freezable>> freezeFunc, Func<TFreezable, Func<Freezable>> createCloneFunc, bool force) where TFreezable: Freezable
        {
            if (element == null)
            {
                return default(TFreezable);
            }
            if (element.CanFreeze)
            {
                Func<Freezable> func = freezeFunc(element);
                return (element.CheckAccess() ? (!element.IsFrozen ? func() : element) : element.Dispatcher.Invoke<Freezable>(func));
            }
            if (!force)
            {
                return element;
            }
            Func<Freezable> callback = createCloneFunc(element);
            return TryGetAsFrozenImpl<TFreezable>(element.CheckAccess() ? callback() : element.Dispatcher.Invoke<Freezable>(callback), freezeFunc, createCloneFunc, false);
        }

        public static TFreezable TryGetCurrentValueAsFrozen<TFreezable>(this TFreezable element, bool force = false) where TFreezable: Freezable
        {
            Func<TFreezable, Func<Freezable>> freezeFunc = <>c__1<TFreezable>.<>9__1_0;
            if (<>c__1<TFreezable>.<>9__1_0 == null)
            {
                Func<TFreezable, Func<Freezable>> local1 = <>c__1<TFreezable>.<>9__1_0;
                freezeFunc = <>c__1<TFreezable>.<>9__1_0 = x => new Func<Freezable>(x.GetCurrentValueAsFrozen);
            }
            return TryGetAsFrozenImpl<TFreezable>(element, freezeFunc, <>c__1<TFreezable>.<>9__1_1 ??= x => new Func<Freezable>(x.CloneCurrentValue), force);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TFreezable> where TFreezable: Freezable
        {
            public static readonly FreezableHelper.<>c__0<TFreezable> <>9;
            public static Func<TFreezable, Func<Freezable>> <>9__0_0;
            public static Func<TFreezable, Func<Freezable>> <>9__0_1;

            static <>c__0()
            {
                FreezableHelper.<>c__0<TFreezable>.<>9 = new FreezableHelper.<>c__0<TFreezable>();
            }

            internal Func<Freezable> <TryGetAsFrozen>b__0_0(TFreezable x) => 
                new Func<Freezable>(x.GetAsFrozen);

            internal Func<Freezable> <TryGetAsFrozen>b__0_1(TFreezable x) => 
                new Func<Freezable>(x.Clone);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<TFreezable> where TFreezable: Freezable
        {
            public static readonly FreezableHelper.<>c__1<TFreezable> <>9;
            public static Func<TFreezable, Func<Freezable>> <>9__1_0;
            public static Func<TFreezable, Func<Freezable>> <>9__1_1;

            static <>c__1()
            {
                FreezableHelper.<>c__1<TFreezable>.<>9 = new FreezableHelper.<>c__1<TFreezable>();
            }

            internal Func<Freezable> <TryGetCurrentValueAsFrozen>b__1_0(TFreezable x) => 
                new Func<Freezable>(x.GetCurrentValueAsFrozen);

            internal Func<Freezable> <TryGetCurrentValueAsFrozen>b__1_1(TFreezable x) => 
                new Func<Freezable>(x.CloneCurrentValue);
        }
    }
}

