namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [DebuggerStepThrough]
    public static class MayBe
    {
        public static TI Do<TI>(this TI input, Action<TI> action) where TI: class
        {
            if (input == null)
            {
                return default(TI);
            }
            action(input);
            return input;
        }

        public static TI If<TI>(this TI input, Func<TI, bool> evaluator) where TI: class
        {
            if ((input != null) && evaluator(input))
            {
                return input;
            }
            return default(TI);
        }

        public static TI IfNot<TI>(this TI input, Func<TI, bool> evaluator) where TI: class
        {
            if ((input != null) && !evaluator(input))
            {
                return input;
            }
            return default(TI);
        }

        public static TR Return<TI, TR>(this TI? input, Func<TI?, TR> evaluator, Func<TR> fallback) where TI: struct
        {
            if (input != null)
            {
                return evaluator(new TI?(input.Value));
            }
            if (fallback != null)
            {
                return fallback();
            }
            return default(TR);
        }

        public static TR Return<TI, TR>(this TI input, Func<TI, TR> evaluator, Func<TR> fallback) where TI: class
        {
            if (input != null)
            {
                return evaluator(input);
            }
            if (fallback != null)
            {
                return fallback();
            }
            return default(TR);
        }

        public static bool ReturnSuccess<TI>(this TI input) where TI: class => 
            input != null;

        public static TR With<TI, TR>(this TI input, Func<TI, TR> evaluator) where TI: class where TR: class
        {
            if (input != null)
            {
                return evaluator(input);
            }
            return default(TR);
        }

        public static TR WithString<TR>(this string input, Func<string, TR> evaluator) where TR: class
        {
            if (!string.IsNullOrEmpty(input))
            {
                return evaluator(input);
            }
            return default(TR);
        }
    }
}

