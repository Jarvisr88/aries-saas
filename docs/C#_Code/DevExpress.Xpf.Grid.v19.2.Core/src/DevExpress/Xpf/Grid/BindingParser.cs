namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    internal static class BindingParser
    {
        internal static IEnumerable<Binding> ExtractBindings(BindingBase binding)
        {
            Binding singleElement = binding as Binding;
            if (singleElement != null)
            {
                return singleElement.Yield<Binding>();
            }
            MultiBinding binding3 = binding as MultiBinding;
            if (binding3 != null)
            {
                return binding3.Bindings.OfType<Binding>();
            }
            PriorityBinding binding4 = binding as PriorityBinding;
            return ((binding4 == null) ? Enumerable.Empty<Binding>() : binding4.Bindings.OfType<Binding>());
        }

        [IteratorStateMachine(typeof(<GetPathHeaders>d__2))]
        private static IEnumerable<string> GetPathHeaders()
        {
            yield return "RowData.Row.";
            yield return "Data.";
        }

        internal static BindingParseResult Parse(BindingBase binding)
        {
            BindingParseResult result;
            if (binding == null)
            {
                return BindingParseResult.CreateEmpty();
            }
            List<string> list = new List<string>();
            using (IEnumerator<Binding> enumerator = ExtractBindings(binding).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Binding current = enumerator.Current;
                        Func<PropertyPath, string> evaluator = <>c.<>9__0_0;
                        if (<>c.<>9__0_0 == null)
                        {
                            Func<PropertyPath, string> local1 = <>c.<>9__0_0;
                            evaluator = <>c.<>9__0_0 = x => x.Path;
                        }
                        string str = current.Path.With<PropertyPath, string>(evaluator);
                        if (!string.IsNullOrEmpty(str))
                        {
                            if (str == "RowData.Row")
                            {
                                result = BindingParseResult.CreateForRow();
                                break;
                            }
                            foreach (string str2 in GetPathHeaders())
                            {
                                if (str.StartsWith(str2))
                                {
                                    list.Add(str.Substring(str2.Length));
                                }
                            }
                        }
                        continue;
                    }
                    return BindingParseResult.CreateForProperties(list.ToArray());
                }
            }
            return result;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BindingParser.<>c <>9 = new BindingParser.<>c();
            public static Func<PropertyPath, string> <>9__0_0;

            internal string <Parse>b__0_0(PropertyPath x) => 
                x.Path;
        }

    }
}

