namespace DevExpress.Mvvm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class LocatorBase
    {
        private static Assembly entryAssembly;
        private Dictionary<string, Type> registeredTypes = new Dictionary<string, Type>();
        private Dictionary<string, Type> shortNameToTypeMapping = new Dictionary<string, Type>();
        private Dictionary<string, Type> fullNameToTypeMapping = new Dictionary<string, Type>();
        private IEnumerator<Type> enumerator;

        protected LocatorBase()
        {
        }

        protected virtual object CreateInstance(Type type, string typeName)
        {
            if (type == null)
            {
                return null;
            }
            object obj2 = null;
            try
            {
                ConstructorInfo info = type.GetConstructor(BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null);
                if (info != null)
                {
                    obj2 = info.Invoke(null);
                }
                obj2 ??= Activator.CreateInstance(type, BindingFlags.OptionalParamBinding | BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance, null, null, null);
            }
            catch (Exception exception)
            {
                throw new LocatorException(base.GetType().Name, typeName, exception);
            }
            if (obj2 == null)
            {
                throw new LocatorException(base.GetType().Name, typeName, null);
            }
            return obj2;
        }

        protected static string CreateTypeProperties(IDictionary<string, string> properties)
        {
            if (properties == null)
            {
                return null;
            }
            string str = string.Empty;
            foreach (KeyValuePair<string, string> pair in properties)
            {
                str = (str + pair.Key + "=") + pair.Value + ";";
            }
            return str;
        }

        [IteratorStateMachine(typeof(<GetTypes>d__13))]
        protected virtual IEnumerator<Type> GetTypes()
        {
            <GetTypes>d__13 d__1 = new <GetTypes>d__13(0);
            d__1.<>4__this = this;
            return d__1;
        }

        public void RegisterType(string name, Type type)
        {
            this.registeredTypes.Add(name, type);
        }

        protected Type ResolveType(string name, out IDictionary<string, string> properties)
        {
            ResolveTypeProperties(ref name, out properties);
            if (!string.IsNullOrEmpty(name))
            {
                Type type;
                if (this.registeredTypes.TryGetValue(name, out type))
                {
                    return type;
                }
                if (this.shortNameToTypeMapping.TryGetValue(name, out type) || this.fullNameToTypeMapping.TryGetValue(name, out type))
                {
                    return type;
                }
                this.enumerator ??= this.GetTypes();
                while (this.enumerator.MoveNext())
                {
                    if (!this.fullNameToTypeMapping.ContainsKey(this.enumerator.Current.FullName))
                    {
                        this.shortNameToTypeMapping[this.enumerator.Current.Name] = this.enumerator.Current;
                        this.fullNameToTypeMapping[this.enumerator.Current.FullName] = this.enumerator.Current;
                    }
                    if ((this.enumerator.Current.Name == name) || (this.enumerator.Current.FullName == name))
                    {
                        return this.enumerator.Current;
                    }
                }
            }
            return null;
        }

        protected string ResolveTypeName(Type type, IDictionary<string, string> properties) => 
            (type != null) ? (CreateTypeProperties(properties) + type.FullName) : null;

        protected static void ResolveTypeProperties(ref string name, out IDictionary<string, string> properties)
        {
            Func<string, string> func = <>c.<>9__14_0 ??= delegate (string x) {
                int index = x.IndexOf('=');
                return ((index == -1) ? null : x.Substring(0, index));
            };
            Func<string, string> func1 = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                Func<string, string> local2 = <>c.<>9__14_1;
                func1 = <>c.<>9__14_1 = delegate (string x) {
                    int index = x.IndexOf('=');
                    int num2 = x.IndexOf(';');
                    return ((index == -1) || (num2 == -1)) ? null : x.Substring(index + 1, (num2 - index) - 1);
                };
            }
            Func<string, string> func2 = func1;
            Func<string, string> func4 = <>c.<>9__14_2;
            if (<>c.<>9__14_2 == null)
            {
                Func<string, string> local3 = <>c.<>9__14_2;
                func4 = <>c.<>9__14_2 = x => x.Remove(0, x.IndexOf(';') + 1);
            }
            Func<string, string> func3 = func4;
            properties = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(name))
            {
                while (true)
                {
                    string key = func(name);
                    string str2 = func2(name);
                    if ((key == null) || (str2 == null))
                    {
                        return;
                    }
                    properties.Add(key, str2);
                    name = func3(name);
                }
            }
        }

        protected static Assembly EntryAssembly
        {
            get
            {
                if (entryAssembly == null)
                {
                    entryAssembly = Assembly.GetEntryAssembly();
                }
                return entryAssembly;
            }
            set => 
                entryAssembly = value;
        }

        protected abstract IEnumerable<Assembly> Assemblies { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LocatorBase.<>c <>9 = new LocatorBase.<>c();
            public static Func<string, string> <>9__14_0;
            public static Func<string, string> <>9__14_1;
            public static Func<string, string> <>9__14_2;

            internal string <ResolveTypeProperties>b__14_0(string x)
            {
                int index = x.IndexOf('=');
                return ((index == -1) ? null : x.Substring(0, index));
            }

            internal string <ResolveTypeProperties>b__14_1(string x)
            {
                int index = x.IndexOf('=');
                int num2 = x.IndexOf(';');
                return (((index == -1) || (num2 == -1)) ? null : x.Substring(index + 1, (num2 - index) - 1));
            }

            internal string <ResolveTypeProperties>b__14_2(string x)
            {
                int index = x.IndexOf(';');
                return x.Remove(0, index + 1);
            }
        }

        [CompilerGenerated]
        private sealed class <GetTypes>d__13 : IEnumerator<Type>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Type <>2__current;
            public LocatorBase <>4__this;
            private IEnumerator<Assembly> <>7__wrap1;
            private Type[] <>7__wrap2;
            private int <>7__wrap3;

            [DebuggerHidden]
            public <GetTypes>d__13(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.Assemblies.GetEnumerator();
                        this.<>1__state = -3;
                        goto TR_0009;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                        goto TR_0011;
                    }
                    else
                    {
                        flag = false;
                    }
                    return flag;
                TR_0009:
                    if (this.<>7__wrap1.MoveNext())
                    {
                        Assembly current = this.<>7__wrap1.Current;
                        Type[] types = new Type[0];
                        try
                        {
                            types = current.GetTypes();
                        }
                        catch (ReflectionTypeLoadException exception1)
                        {
                            types = exception1.Types;
                        }
                        this.<>7__wrap2 = types;
                        this.<>7__wrap3 = 0;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        return false;
                    }
                TR_000F:
                    while (true)
                    {
                        if (this.<>7__wrap3 < this.<>7__wrap2.Length)
                        {
                            Type type = this.<>7__wrap2[this.<>7__wrap3];
                            if (type != null)
                            {
                                this.<>2__current = type;
                                this.<>1__state = 1;
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            this.<>7__wrap2 = null;
                            goto TR_0009;
                        }
                        goto TR_0011;
                    }
                    return flag;
                TR_0011:
                    while (true)
                    {
                        this.<>7__wrap3++;
                        break;
                    }
                    goto TR_000F;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            Type IEnumerator<Type>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

