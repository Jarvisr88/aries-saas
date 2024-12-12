namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class OptionExtensions
    {
        public static Option<TOut> Apply<TIn, TOut>(this Option<TIn> t, Func<TIn, TOut> apply) => 
            t.Match<Option<TOut>>(x => apply(x).AsOption<TOut>(), Option<TOut>.Empty);

        public static Option<T> AsOption<T>(this T value) => 
            new Option<T>(value);

        public static Option<TOut> Bind<TIn, TOut>(this Option<TIn> t, Func<TIn, Option<TOut>> apply) => 
            t.Match<Option<TOut>>(x => apply(x), Option<TOut>.Empty);

        public static Option<T> Or<T>(this Option<T> t, Option<T> fallback) => 
            t.HasValue ? t : fallback;

        public static IEnumerable<TOut> SelectWhile<TIn, TOut>(this IEnumerable<TIn> enumerable, Func<TIn, Option<TOut>> selector)
        {
            Func<Option<TOut>, bool> predicate = <>c__8<TIn, TOut>.<>9__8_0;
            if (<>c__8<TIn, TOut>.<>9__8_0 == null)
            {
                Func<Option<TOut>, bool> local1 = <>c__8<TIn, TOut>.<>9__8_0;
                predicate = <>c__8<TIn, TOut>.<>9__8_0 = x => x.HasValue;
            }
            Func<Option<TOut>, TOut> func2 = <>c__8<TIn, TOut>.<>9__8_1;
            if (<>c__8<TIn, TOut>.<>9__8_1 == null)
            {
                Func<Option<TOut>, TOut> local2 = <>c__8<TIn, TOut>.<>9__8_1;
                func2 = <>c__8<TIn, TOut>.<>9__8_1 = x => x.ToValue();
            }
            return enumerable.Select<TIn, Option<TOut>>(selector).TakeWhile<Option<TOut>>(predicate).Select<Option<TOut>, TOut>(func2);
        }

        public static T? ToMaybe<T>(this Option<T> t) where T: struct
        {
            Func<T, T?> getValue = <>c__5<T>.<>9__5_0;
            if (<>c__5<T>.<>9__5_0 == null)
            {
                Func<T, T?> local1 = <>c__5<T>.<>9__5_0;
                getValue = <>c__5<T>.<>9__5_0 = x => new T?(x);
            }
            T? noValue = null;
            return t.Match<T?>(getValue, noValue);
        }

        public static Option<T> ToOption<T>(this T? value) where T: struct => 
            (value != null) ? new Option<T>(value.Value) : Option<T>.Empty;

        public static Option<T> ToOption<T>(this T value) where T: class => 
            (value != null) ? new Option<T>(value) : Option<T>.Empty;

        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue local;
            return (dictionary.TryGetValue(key, out local) ? local.AsOption<TValue>() : Option<TValue>.Empty);
        }

        public static IEnumerable<T> Values<T>(this IEnumerable<Option<T>> enumerable)
        {
            Func<Option<T>, bool> predicate = <>c__9<T>.<>9__9_0;
            if (<>c__9<T>.<>9__9_0 == null)
            {
                Func<Option<T>, bool> local1 = <>c__9<T>.<>9__9_0;
                predicate = <>c__9<T>.<>9__9_0 = x => x.HasValue;
            }
            Func<Option<T>, T> selector = <>c__9<T>.<>9__9_1;
            if (<>c__9<T>.<>9__9_1 == null)
            {
                Func<Option<T>, T> local2 = <>c__9<T>.<>9__9_1;
                selector = <>c__9<T>.<>9__9_1 = x => x.ToValue();
            }
            return enumerable.Where<Option<T>>(predicate).Select<Option<T>, T>(selector);
        }

        public static Option<TOut> WithOption<TIn, TOut>(this TIn value, Func<TIn, TOut> selector) where TIn: class => 
            (value != null) ? selector(value).AsOption<TOut>() : Option<TOut>.Empty;

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T> where T: struct
        {
            public static readonly OptionExtensions.<>c__5<T> <>9;
            public static Func<T, T?> <>9__5_0;

            static <>c__5()
            {
                OptionExtensions.<>c__5<T>.<>9 = new OptionExtensions.<>c__5<T>();
            }

            internal T? <ToMaybe>b__5_0(T x) => 
                new T?(x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__8<TIn, TOut>
        {
            public static readonly OptionExtensions.<>c__8<TIn, TOut> <>9;
            public static Func<Option<TOut>, bool> <>9__8_0;
            public static Func<Option<TOut>, TOut> <>9__8_1;

            static <>c__8()
            {
                OptionExtensions.<>c__8<TIn, TOut>.<>9 = new OptionExtensions.<>c__8<TIn, TOut>();
            }

            internal bool <SelectWhile>b__8_0(Option<TOut> x) => 
                x.HasValue;

            internal TOut <SelectWhile>b__8_1(Option<TOut> x) => 
                x.ToValue();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__9<T>
        {
            public static readonly OptionExtensions.<>c__9<T> <>9;
            public static Func<Option<T>, bool> <>9__9_0;
            public static Func<Option<T>, T> <>9__9_1;

            static <>c__9()
            {
                OptionExtensions.<>c__9<T>.<>9 = new OptionExtensions.<>c__9<T>();
            }

            internal bool <Values>b__9_0(Option<T> x) => 
                x.HasValue;

            internal T <Values>b__9_1(Option<T> x) => 
                x.ToValue();
        }
    }
}

