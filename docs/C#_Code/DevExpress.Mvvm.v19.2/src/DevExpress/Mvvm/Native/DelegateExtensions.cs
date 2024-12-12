namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class DelegateExtensions
    {
        public static Action<TNew> ChangeArgType<T, TNew>(this Action<T> action, TNew ignore) where T: TNew => 
            (action != null) ? delegate (TNew x) {
                action((T) x);
            } : null;

        public static Action<TNew, T2> ChangeArgType0<T, TNew, T2>(this Action<T, T2> action, TNew ignore) where T: TNew => 
            (action != null) ? delegate (TNew x, T2 x2) {
                action((T) x, x2);
            } : null;

        public static Action<TNew, T2, T3> ChangeArgType0<T, TNew, T2, T3>(this Action<T, T2, T3> action, TNew ignore) where T: TNew => 
            (action != null) ? delegate (TNew x, T2 x2, T3 x3) {
                action((T) x, x2, x3);
            } : null;

        public static Action ThisOrDefault(this Action action, Action defaultAction) => 
            action ?? delegate {
                defaultAction();
            };

        public static Action<T> ThisOrDefault<T>(this Action<T> action, Action defaultAction) => 
            action ?? delegate (T x) {
                defaultAction();
            };

        public static Action<T1, T2> ThisOrDefault<T1, T2>(this Action<T1, T2> action, Action defaultAction) => 
            action ?? delegate (T1 x1, T2 x2) {
                defaultAction();
            };

        public static Action<T1, T2, T3> ThisOrDefault<T1, T2, T3>(this Action<T1, T2, T3> action, Action defaultAction) => 
            action ?? delegate (T1 x1, T2 x2, T3 x3) {
                defaultAction();
            };

        internal static Func<TR> ThisOrDefault<TR>(this Func<TR> func, TR defaultValue) => 
            func ?? () => defaultValue;

        internal static Func<T, TR> ThisOrDefault<T, TR>(this Func<T, TR> func, TR defaultValue) => 
            func ?? x => defaultValue;

        internal static Func<T1, T2, TR> ThisOrDefault<T1, T2, TR>(this Func<T1, T2, TR> func, TR defaultValue) => 
            func ?? (x1, x2) => defaultValue;

        internal static Func<T1, T2, T3, TR> ThisOrDefault<T1, T2, T3, TR>(this Func<T1, T2, T3, TR> func, TR defaultValue) => 
            func ?? (x1, x2, t3) => defaultValue;

        public static Action ThisOrEmpty(this Action action)
        {
            Action action1 = action;
            if (action == null)
            {
                Action local1 = action;
                action1 = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Action local2 = <>c.<>9__0_0;
                    action1 = <>c.<>9__0_0 = delegate {
                    };
                }
            }
            return action1;
        }

        public static Action ToAction<TResult>(this Func<TResult> f, Action<TResult> setResult) => 
            (f != null) ? delegate {
                setResult(f());
            } : null;

        public static Action<T> ToAction<T, TResult>(this Func<T, TResult> f, Action<TResult> setResult) => 
            (f != null) ? delegate (T x) {
                setResult(f(x));
            } : null;

        public static Action<T1, T2> ToAction<T1, T2, TResult>(this Func<T1, T2, TResult> f, Action<TResult> setResult) => 
            (f != null) ? delegate (T1 x1, T2 x2) {
                setResult(f(x1, x2));
            } : null;

        public static Action<T1, T2, T3> ToAction<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> f, Action<TResult> setResult) => 
            (f != null) ? delegate (T1 x1, T2 x2, T3 x3) {
                setResult(f(x1, x2, x3));
            } : null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DelegateExtensions.<>c <>9 = new DelegateExtensions.<>c();
            public static Action <>9__0_0;

            internal void <ThisOrEmpty>b__0_0()
            {
            }
        }
    }
}

