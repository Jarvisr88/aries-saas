namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal static class IEnumerableExtensions
    {
        [IteratorStateMachine(typeof(IEnumerableExtensions.<ConvertAll>d__0<>))]
        public static IEnumerable<T> ConvertAll<T>(this IEnumerable en, Converter<object, T> converter);
        public static T[] Exclude<T>(this IEnumerable<T> list, T exclude);
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getItems);
        public static void ForEach<T>(this IEnumerable<T> en, Action<T> action);

        [CompilerGenerated]
        private sealed class <ConvertAll>d__0<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable en;
            public IEnumerable <>3__en;
            private Converter<object, T> converter;
            public Converter<object, T> <>3__converter;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <ConvertAll>d__0(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

