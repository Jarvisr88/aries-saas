namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ProjectTypes : IProjectTypes, IEnumerable<IDXAssemblyInfo>, IEnumerable
    {
        private List<IDXAssemblyInfo> assemblies;
        private string projectAssemblyName;
        private IEnumerable<string> solutionAssemblyNames;

        public ProjectTypes(string projectAssemblyName, IEnumerable<string> solutionAssemblyNames, IEnumerable<Type> allTypes) : this(projectAssemblyName, solutionAssemblyNames, allTypes, null)
        {
        }

        public ProjectTypes(string projectAssemblyName, IEnumerable<string> solutionAssemblyNames, IEnumerable<Type> allTypes, IResourceOptions options)
        {
            this.ResourceOptions = options;
            this.projectAssemblyName = projectAssemblyName;
            this.solutionAssemblyNames = (solutionAssemblyNames == null) ? ((IEnumerable<string>) new string[0]) : solutionAssemblyNames;
            this.Initialize(allTypes);
        }

        private IDXTypeInfo CreateNew(IDXAssemblyInfo assemblyInfo, Type type)
        {
            if (type == null)
            {
                return null;
            }
            Assembly assembly = type.Assembly;
            assemblyInfo ??= this.Assemblies.FirstOrDefault<IDXAssemblyInfo>(ai => (string.Compare(ai.AssemblyFullName, assembly.FullName, true) == 0));
            if (assemblyInfo == null)
            {
                Type[] typeArray1 = new Type[] { type };
                assemblyInfo = new DXAssemblyInfo(assembly, false, false, DevExpress.Entity.ProjectModel.ResourceOptions.DefaultOptions, typeArray1);
                this.assemblies ??= new List<IDXAssemblyInfo>();
                this.assemblies.Add(assemblyInfo);
            }
            return assemblyInfo.GetTypeInfo(type.FullName);
        }

        private List<IDXAssemblyInfo> GetAllAssemblies(IEnumerable<Type> projectTypes)
        {
            if (projectTypes == null)
            {
                return new List<IDXAssemblyInfo>();
            }
            Dictionary<string, DXAssemblyInfo> dictionary = new Dictionary<string, DXAssemblyInfo>();
            DXAssemblyInfo info = null;
            DXAssemblyInfo info2 = null;
            foreach (Type type in projectTypes)
            {
                Assembly assembly = type.Assembly;
                if ((assembly != null) && (!string.IsNullOrEmpty(assembly.Location) || IsEntityFrameworkServer(assembly.GetName().Name)))
                {
                    string fullName = assembly.FullName;
                    if (!string.IsNullOrEmpty(fullName))
                    {
                        if (dictionary.ContainsKey(fullName))
                        {
                            DXAssemblyInfo info4 = dictionary[fullName];
                            if (info4 == null)
                            {
                                continue;
                            }
                            info4.Add(new DXTypeInfo(type));
                            continue;
                        }
                        bool isProjectAssembly = this.IsProjectAssembly(fullName);
                        IResourceOptions resourceOptions = null;
                        if (isProjectAssembly)
                        {
                            resourceOptions = this.ResourceOptions;
                        }
                        Type[] typeArray1 = new Type[] { type };
                        DXAssemblyInfo info3 = new DXAssemblyInfo(assembly, isProjectAssembly, this.IsInSolution(fullName), resourceOptions, typeArray1);
                        if ((info == null) && IsEntityFramework(info3.Name))
                        {
                            info = info3;
                        }
                        if ((info2 == null) && IsEntityFrameworkServer(info3.Name))
                        {
                            info2 = info3;
                        }
                        dictionary.Add(fullName, info3);
                    }
                }
            }
            return dictionary.Values.ToList<IDXAssemblyInfo>();
        }

        public IEnumerator<IDXAssemblyInfo> GetEnumerator() => 
            this.Assemblies.GetEnumerator();

        public IDXTypeInfo GetExistingOrCreateNew(Type type)
        {
            if (type == null)
            {
                return null;
            }
            Assembly assembly = type.Assembly;
            IDXAssemblyInfo assemblyInfo = this.Assemblies.FirstOrDefault<IDXAssemblyInfo>(ai => string.Compare(ai.AssemblyFullName, assembly.FullName, true) == 0);
            if (assemblyInfo == null)
            {
                return this.CreateNew(null, type);
            }
            IDXTypeInfo typeInfo = assemblyInfo.GetTypeInfo(type.FullName);
            return ((typeInfo == null) ? this.CreateNew(assemblyInfo, type) : typeInfo);
        }

        [IteratorStateMachine(typeof(<GetTypes>d__6))]
        public IEnumerable<IDXTypeInfo> GetTypes(Func<IDXTypeInfo, bool> filter)
        {
            <GetTypes>d__6 d__1 = new <GetTypes>d__6(-2);
            d__1.<>4__this = this;
            d__1.<>3__filter = filter;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetTypesPerAssembly>d__7))]
        public IEnumerable<IDXAssemblyInfo> GetTypesPerAssembly(Func<IDXTypeInfo, bool> filter)
        {
            <GetTypesPerAssembly>d__7 d__1 = new <GetTypesPerAssembly>d__7(-2);
            d__1.<>4__this = this;
            d__1.<>3__filter = filter;
            return d__1;
        }

        private void Initialize(IEnumerable<Type> allTypes)
        {
            this.assemblies = this.GetAllAssemblies(allTypes);
        }

        private static bool IsEntityFramework(string assemblyName) => 
            string.Equals(assemblyName, "EntityFramework", StringComparison.OrdinalIgnoreCase);

        private static bool IsEntityFrameworkServer(string assemblyName) => 
            !string.Equals(assemblyName, "System.Data.SQLite", StringComparison.OrdinalIgnoreCase) ? (!string.Equals(assemblyName, "EntityFramework.SqlServerCompact", StringComparison.OrdinalIgnoreCase) ? string.Equals(assemblyName, "EntityFramework.SqlServer", StringComparison.OrdinalIgnoreCase) : true) : true;

        private bool IsInSolution(string fullName) => 
            !string.IsNullOrEmpty(fullName) && ((this.solutionAssemblyNames != null) && this.solutionAssemblyNames.Contains<string>(fullName));

        private bool IsProjectAssembly(string fullName) => 
            !string.IsNullOrEmpty(fullName) ? (this.projectAssemblyName == fullName) : false;

        IEnumerator IEnumerable.GetEnumerator() => 
            this.Assemblies.GetEnumerator();

        public IEnumerable<IDXAssemblyInfo> Assemblies =>
            this.assemblies;

        public IDXAssemblyInfo ProjectAssembly
        {
            get
            {
                Func<IDXAssemblyInfo, bool> predicate = <>c.<>9__18_0;
                if (<>c.<>9__18_0 == null)
                {
                    Func<IDXAssemblyInfo, bool> local1 = <>c.<>9__18_0;
                    predicate = <>c.<>9__18_0 = ai => ai.IsProjectAssembly;
                }
                IDXAssemblyInfo info = this.Assemblies.FirstOrDefault<IDXAssemblyInfo>(predicate);
                if ((info != null) || string.IsNullOrEmpty(this.projectAssemblyName))
                {
                    return info;
                }
                DXAssemblyInfo item = new DXAssemblyInfo(this.projectAssemblyName, true, true, this.ResourceOptions);
                this.assemblies ??= new List<IDXAssemblyInfo>();
                this.assemblies.Add(item);
                return item;
            }
        }

        public IResourceOptions ResourceOptions { get; private set; }

        public string ProjectAssemblyName =>
            this.projectAssemblyName;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProjectTypes.<>c <>9 = new ProjectTypes.<>c();
            public static Func<IDXAssemblyInfo, bool> <>9__18_0;

            internal bool <get_ProjectAssembly>b__18_0(IDXAssemblyInfo ai) => 
                ai.IsProjectAssembly;
        }

        [CompilerGenerated]
        private sealed class <GetTypes>d__6 : IEnumerable<IDXTypeInfo>, IEnumerable, IEnumerator<IDXTypeInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDXTypeInfo <>2__current;
            private int <>l__initialThreadId;
            public ProjectTypes <>4__this;
            private Func<IDXTypeInfo, bool> filter;
            public Func<IDXTypeInfo, bool> <>3__filter;
            private IEnumerator<IDXAssemblyInfo> <>7__wrap1;
            private IEnumerator<IDXTypeInfo> <>7__wrap2;

            [DebuggerHidden]
            public <GetTypes>d__6(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -3;
                if (this.<>7__wrap2 != null)
                {
                    this.<>7__wrap2.Dispose();
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
                        if ((this.<>4__this.Assemblies == null) || (this.filter == null))
                        {
                            return false;
                        }
                        else
                        {
                            this.<>7__wrap1 = this.<>4__this.Assemblies.GetEnumerator();
                            this.<>1__state = -3;
                        }
                    }
                    else
                    {
                        if (num == 1)
                        {
                            this.<>1__state = -4;
                        }
                        else
                        {
                            return false;
                        }
                        goto TR_0009;
                    }
                TR_0005:
                    if (this.<>7__wrap1.MoveNext())
                    {
                        IDXAssemblyInfo current = this.<>7__wrap1.Current;
                        this.<>7__wrap2 = current.TypesInfo.Where<IDXTypeInfo>(this.filter).GetEnumerator();
                        this.<>1__state = -4;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        return false;
                    }
                TR_0009:
                    while (true)
                    {
                        if (this.<>7__wrap2.MoveNext())
                        {
                            IDXTypeInfo current = this.<>7__wrap2.Current;
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        else
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap2 = null;
                            goto TR_0005;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<IDXTypeInfo> IEnumerable<IDXTypeInfo>.GetEnumerator()
            {
                ProjectTypes.<GetTypes>d__6 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new ProjectTypes.<GetTypes>d__6(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.filter = this.<>3__filter;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Entity.ProjectModel.IDXTypeInfo>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if (((num == -4) || (num == -3)) || (num == 1))
                {
                    try
                    {
                        if ((num == -4) || (num == 1))
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.<>m__Finally2();
                            }
                        }
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            IDXTypeInfo IEnumerator<IDXTypeInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetTypesPerAssembly>d__7 : IEnumerable<IDXAssemblyInfo>, IEnumerable, IEnumerator<IDXAssemblyInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDXAssemblyInfo <>2__current;
            private int <>l__initialThreadId;
            public ProjectTypes <>4__this;
            private Func<IDXTypeInfo, bool> filter;
            public Func<IDXTypeInfo, bool> <>3__filter;
            private IEnumerator<IDXAssemblyInfo> <>7__wrap1;

            [DebuggerHidden]
            public <GetTypesPerAssembly>d__7(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
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
                        if ((this.<>4__this.Assemblies == null) || (this.filter == null))
                        {
                            return false;
                        }
                        else
                        {
                            this.<>7__wrap1 = this.<>4__this.Assemblies.GetEnumerator();
                            this.<>1__state = -3;
                        }
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        IDXAssemblyInfo current = this.<>7__wrap1.Current;
                        DXAssemblyInfo info2 = new DXAssemblyInfo(current);
                        info2.AddRange(current.TypesInfo.Where<IDXTypeInfo>(this.filter));
                        this.<>2__current = info2;
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<IDXAssemblyInfo> IEnumerable<IDXAssemblyInfo>.GetEnumerator()
            {
                ProjectTypes.<GetTypesPerAssembly>d__7 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new ProjectTypes.<GetTypesPerAssembly>d__7(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.filter = this.<>3__filter;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Entity.ProjectModel.IDXAssemblyInfo>.GetEnumerator();

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

            IDXAssemblyInfo IEnumerator<IDXAssemblyInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

