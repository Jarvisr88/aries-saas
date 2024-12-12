namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class DynamicAssemblyHelper
    {
        private static Lazy<Assembly> dataAssembly = new Lazy<Assembly>(() => ResolveAssembly("DevExpress.Data.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"));
        private static Lazy<Assembly> xpfCoreAssembly = new Lazy<Assembly>(() => ResolveAssembly("DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"));

        private static string GetPartialName(string asmName)
        {
            int index = asmName.IndexOf(',');
            return ((index < 0) ? asmName : asmName.Remove(index));
        }

        private static bool PartialNameEquals(string asmName0, string asmName1) => 
            string.Equals(GetPartialName(asmName0), GetPartialName(asmName1), StringComparison.InvariantCultureIgnoreCase);

        private static Assembly ResolveAssembly(string asmName)
        {
            Assembly assembly2;
            using (IEnumerator enumerator = AppDomain.CurrentDomain.GetAssemblies().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Assembly current = (Assembly) enumerator.Current;
                        if (!PartialNameEquals(current.FullName, asmName))
                        {
                            continue;
                        }
                        assembly2 = current;
                    }
                    else
                    {
                        return Assembly.Load(asmName);
                    }
                    break;
                }
            }
            return assembly2;
        }

        public static Assembly DataAssembly =>
            dataAssembly.Value;

        public static Assembly XpfCoreAssembly =>
            xpfCoreAssembly.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DynamicAssemblyHelper.<>c <>9 = new DynamicAssemblyHelper.<>c();

            internal Assembly <.cctor>b__9_0() => 
                DynamicAssemblyHelper.ResolveAssembly("DevExpress.Data.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a");

            internal Assembly <.cctor>b__9_1() => 
                DynamicAssemblyHelper.ResolveAssembly("DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a");
        }
    }
}

