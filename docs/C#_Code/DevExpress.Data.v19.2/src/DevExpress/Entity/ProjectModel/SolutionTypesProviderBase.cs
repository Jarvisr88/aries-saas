namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class SolutionTypesProviderBase : ISolutionTypesProvider, IHasTypesCache
    {
        private Dictionary<string, IProjectTypes> solutionProjectTypes = new Dictionary<string, IProjectTypes>();

        public SolutionTypesProviderBase()
        {
            MetaDataServices.Initialize(this);
        }

        public void Add(IDXTypeInfo typeInfo)
        {
            if (this.ActiveProjectTypes.ProjectAssembly != null)
            {
                this.ActiveProjectTypes.ProjectAssembly.Add(typeInfo);
            }
        }

        public virtual void AddReference(string assemblyName)
        {
        }

        public virtual void AddReferenceFromFile(string assemblyPath)
        {
        }

        public void ClearCache()
        {
            this.activeProjectTypes = null;
        }

        public bool Contains(IDXTypeInfo typeInfo) => 
            (from asm in this.ActiveProjectTypes
                where typeInfo.Assembly.AssemblyFullName == asm.AssemblyFullName
                select asm).Select<IDXAssemblyInfo, IDXTypeInfo>(delegate (IDXAssemblyInfo asm) {
                Func<IDXTypeInfo, bool> <>9__2;
                Func<IDXTypeInfo, bool> predicate = <>9__2;
                if (<>9__2 == null)
                {
                    Func<IDXTypeInfo, bool> local1 = <>9__2;
                    predicate = <>9__2 = t => t.FullName == typeInfo.FullName;
                }
                return asm.TypesInfo.Where<IDXTypeInfo>(predicate).First<IDXTypeInfo>();
            }).Any<IDXTypeInfo>();

        public IDXTypeInfo FindType(Predicate<IDXTypeInfo> predicate)
        {
            IDXTypeInfo info2;
            if (predicate == null)
            {
                return null;
            }
            using (IEnumerator<IDXTypeInfo> enumerator = this.GetTypes().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IDXTypeInfo current = enumerator.Current;
                        if (!predicate(current))
                        {
                            continue;
                        }
                        info2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info2;
        }

        public IDXTypeInfo FindType(string fullName) => 
            this.FindType(type => (type != null) && (type.FullName == fullName));

        [IteratorStateMachine(typeof(<FindTypes>d__29))]
        public IEnumerable<IDXTypeInfo> FindTypes(Predicate<IDXTypeInfo> predicate)
        {
            <FindTypes>d__29 d__1 = new <FindTypes>d__29(-2);
            d__1.<>4__this = this;
            d__1.<>3__predicate = predicate;
            return d__1;
        }

        [IteratorStateMachine(typeof(<FindTypes>d__28))]
        public IEnumerable<IDXTypeInfo> FindTypes(IDXTypeInfo baseClass, Predicate<IDXTypeInfo> predicate)
        {
            <FindTypes>d__28 d__1 = new <FindTypes>d__28(-2);
            d__1.<>4__this = this;
            d__1.<>3__baseClass = baseClass;
            return d__1;
        }

        protected abstract string GetActiveProjectAssemblyFullName();
        protected abstract IEnumerable<Type> GetActiveProjectTypes();
        public IDXAssemblyInfo GetAssembly(string assemblyName) => 
            this.GetAssemblyCore(assemblyName);

        protected abstract IDXAssemblyInfo GetAssemblyCore(string assemblyName);
        public virtual string GetAssemblyReferencePath(string projectAssemblyFullName, string referenceName) => 
            null;

        protected abstract string GetOutputDir();
        protected abstract string[] GetProjectOutputs();
        public IProjectTypes GetProjectTypes(string assemblyFullName)
        {
            IProjectTypes projectTypesCore;
            if (string.IsNullOrEmpty(assemblyFullName))
            {
                return null;
            }
            if (!this.solutionProjectTypes.TryGetValue(assemblyFullName, out projectTypesCore))
            {
                projectTypesCore = this.GetProjectTypesCore(assemblyFullName);
                if (projectTypesCore == null)
                {
                    return null;
                }
                this.solutionProjectTypes[assemblyFullName] = projectTypesCore;
            }
            return projectTypesCore;
        }

        protected virtual IProjectTypes GetProjectTypesCore(string assemblyFullName) => 
            null;

        private ResourceOptions GetResourceOptionsForActiveProject()
        {
            string[] externalPaths = new string[] { this.GetOutputDir() };
            return new ResourceOptions(true, externalPaths);
        }

        protected abstract IEnumerable<string> GetSolutionAssemblyFullNames();
        public IDXTypeInfo GetTypeInfo(string typeFullName)
        {
            IDXTypeInfo info3;
            using (IEnumerator<IDXAssemblyInfo> enumerator = this.ActiveProjectTypes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IDXAssemblyInfo current = enumerator.Current;
                        using (IEnumerator<IDXTypeInfo> enumerator2 = current.TypesInfo.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator2.MoveNext())
                                {
                                    break;
                                }
                                IDXTypeInfo info2 = enumerator2.Current;
                                if ((info2 != null) && (info2.FullName == typeFullName))
                                {
                                    return info2;
                                }
                            }
                        }
                        continue;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info3;
        }

        [IteratorStateMachine(typeof(<GetTypes>d__19))]
        public IEnumerable<IDXTypeInfo> GetTypes()
        {
            <GetTypes>d__19 d__1 = new <GetTypes>d__19(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        public virtual bool IsLocalType(IDXTypeInfo typeInfo) => 
            typeInfo.Assembly.IsProjectAssembly;

        public virtual bool IsReferenceExists(string assemblyName) => 
            true;

        public void Remove(IDXTypeInfo typeInfo)
        {
            if ((typeInfo != null) && ((typeInfo.Assembly != null) && !string.IsNullOrEmpty(typeInfo.Assembly.AssemblyFullName)))
            {
                IDXAssemblyInfo info = this.ActiveProjectTypes.FirstOrDefault<IDXAssemblyInfo>(asm => typeInfo.Assembly.AssemblyFullName == asm.AssemblyFullName);
                if (info != null)
                {
                    info.Remove(typeInfo);
                }
            }
        }

        private IProjectTypes activeProjectTypes { get; set; }

        public IProjectTypes ActiveProjectTypes
        {
            get
            {
                if (this.activeProjectTypes != null)
                {
                    Func<IDXAssemblyInfo, bool> predicate = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Func<IDXAssemblyInfo, bool> local1 = <>c.<>9__6_0;
                        predicate = <>c.<>9__6_0 = x => x.TypesInfo.Any<IDXTypeInfo>();
                    }
                    if (this.activeProjectTypes.Any<IDXAssemblyInfo>(predicate))
                    {
                        return this.activeProjectTypes;
                    }
                }
                string activeProjectAssemblyFullName = this.GetActiveProjectAssemblyFullName();
                this.activeProjectTypes = new ProjectTypes(activeProjectAssemblyFullName, this.GetSolutionAssemblyFullNames(), this.GetActiveProjectTypes(), this.GetResourceOptionsForActiveProject());
                if (activeProjectAssemblyFullName != null)
                {
                    this.solutionProjectTypes[activeProjectAssemblyFullName] = this.activeProjectTypes;
                }
                return this.activeProjectTypes;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SolutionTypesProviderBase.<>c <>9 = new SolutionTypesProviderBase.<>c();
            public static Func<IDXAssemblyInfo, bool> <>9__6_0;

            internal bool <get_ActiveProjectTypes>b__6_0(IDXAssemblyInfo x) => 
                x.TypesInfo.Any<IDXTypeInfo>();
        }

        [CompilerGenerated]
        private sealed class <FindTypes>d__28 : IEnumerable<IDXTypeInfo>, IEnumerable, IEnumerator<IDXTypeInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDXTypeInfo <>2__current;
            private int <>l__initialThreadId;
            private IDXTypeInfo baseClass;
            public IDXTypeInfo <>3__baseClass;
            public SolutionTypesProviderBase <>4__this;
            private Type <baseType>5__1;
            private IDXTypeInfo <tyeInfo>5__2;
            private IEnumerator<IDXAssemblyInfo> <>7__wrap1;
            private IEnumerator<IDXTypeInfo> <>7__wrap2;

            [DebuggerHidden]
            public <FindTypes>d__28(int <>1__state)
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
                    Type type;
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<baseType>5__1 = this.baseClass?.ResolveType();
                            this.<>7__wrap1 = this.<>4__this.ActiveProjectTypes.Assemblies.GetEnumerator();
                            this.<>1__state = -3;
                            break;

                        case 1:
                            this.<>1__state = -4;
                            goto TR_0008;

                        case 2:
                            this.<>1__state = -4;
                            goto TR_0007;

                        default:
                            return false;
                    }
                    goto TR_0010;
                TR_0007:
                    this.<tyeInfo>5__2 = null;
                    goto TR_000D;
                TR_0008:
                    type = this.<tyeInfo>5__2.ResolveType();
                    if ((type == null) || !this.<baseType>5__1.IsAssignableFrom(type))
                    {
                        goto TR_0007;
                    }
                    else
                    {
                        this.<>2__current = this.<tyeInfo>5__2;
                        this.<>1__state = 2;
                        flag = true;
                    }
                    return flag;
                TR_000D:
                    while (true)
                    {
                        if (this.<>7__wrap2.MoveNext())
                        {
                            this.<tyeInfo>5__2 = this.<>7__wrap2.Current;
                            if (this.<baseType>5__1 == null)
                            {
                                this.<>2__current = this.<tyeInfo>5__2;
                                this.<>1__state = 1;
                                return true;
                            }
                            goto TR_0008;
                        }
                        else
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap2 = null;
                        }
                        break;
                    }
                TR_0010:
                    while (true)
                    {
                        if (this.<>7__wrap1.MoveNext())
                        {
                            IDXAssemblyInfo current = this.<>7__wrap1.Current;
                            this.<>7__wrap2 = current.TypesInfo.GetEnumerator();
                            this.<>1__state = -4;
                        }
                        else
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            return false;
                        }
                        break;
                    }
                    goto TR_000D;
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
                SolutionTypesProviderBase.<FindTypes>d__28 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new SolutionTypesProviderBase.<FindTypes>d__28(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.baseClass = this.<>3__baseClass;
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
                switch (num)
                {
                    case -4:
                    case -3:
                    case 1:
                    case 2:
                        try
                        {
                            if (((num == -4) || (num == 1)) || (num == 2))
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
                        break;

                    case -2:
                    case -1:
                    case 0:
                        break;

                    default:
                        return;
                }
            }

            IDXTypeInfo IEnumerator<IDXTypeInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <FindTypes>d__29 : IEnumerable<IDXTypeInfo>, IEnumerable, IEnumerator<IDXTypeInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDXTypeInfo <>2__current;
            private int <>l__initialThreadId;
            private Predicate<IDXTypeInfo> predicate;
            public Predicate<IDXTypeInfo> <>3__predicate;
            public SolutionTypesProviderBase <>4__this;
            private IEnumerator<IDXTypeInfo> <>7__wrap1;

            [DebuggerHidden]
            public <FindTypes>d__29(int <>1__state)
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
                        if (this.predicate != null)
                        {
                            this.<>7__wrap1 = this.<>4__this.GetTypes().GetEnumerator();
                            this.<>1__state = -3;
                        }
                        else
                        {
                            return false;
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
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            IDXTypeInfo current = this.<>7__wrap1.Current;
                            if (!this.predicate(current))
                            {
                                continue;
                            }
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
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
                SolutionTypesProviderBase.<FindTypes>d__29 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new SolutionTypesProviderBase.<FindTypes>d__29(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.predicate = this.<>3__predicate;
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

            IDXTypeInfo IEnumerator<IDXTypeInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetTypes>d__19 : IEnumerable<IDXTypeInfo>, IEnumerable, IEnumerator<IDXTypeInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDXTypeInfo <>2__current;
            private int <>l__initialThreadId;
            public SolutionTypesProviderBase <>4__this;
            private IEnumerator<IDXAssemblyInfo> <>7__wrap1;
            private IEnumerator<IDXTypeInfo> <>7__wrap2;

            [DebuggerHidden]
            public <GetTypes>d__19(int <>1__state)
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
                        this.<>7__wrap1 = this.<>4__this.ActiveProjectTypes.GetEnumerator();
                        this.<>1__state = -3;
                        goto TR_0005;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -4;
                    }
                    else
                    {
                        return false;
                    }
                    goto TR_0009;
                TR_0005:
                    if (this.<>7__wrap1.MoveNext())
                    {
                        IDXAssemblyInfo current = this.<>7__wrap1.Current;
                        this.<>7__wrap2 = current.TypesInfo.GetEnumerator();
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
                SolutionTypesProviderBase.<GetTypes>d__19 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new SolutionTypesProviderBase.<GetTypes>d__19(0) {
                        <>4__this = this.<>4__this
                    };
                }
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
    }
}

