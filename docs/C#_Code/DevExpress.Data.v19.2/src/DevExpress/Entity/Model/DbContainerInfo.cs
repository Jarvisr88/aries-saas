namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DbContainerInfo : DXTypeInfo, IDbContainerInfo, IContainerInfo, IDXTypeInfo, IHasName
    {
        private IEntityContainerInfo container;

        public DbContainerInfo(Type type) : base(type)
        {
        }

        public DbContainerInfo(Type type, IEntityContainerInfo container) : this(type)
        {
            this.container = container;
        }

        public DbContainerInfo(Type type, MetadataWorkspaceInfo metadataWorkspace) : base(type)
        {
            if (metadataWorkspace != null)
            {
                this.MetadataWorkspace = metadataWorkspace.Value;
                this.DbContextType = type;
            }
        }

        public DbContainerInfo(Type type, IEntityContainerInfo container, MetadataWorkspaceInfo metadataWorkspace = null) : this(type, metadataWorkspace)
        {
            this.container = container;
        }

        protected virtual bool IsValidEntitySet(IEntitySetInfo info) => 
            info != null;

        public IEntityContainerInfo EntityContainer
        {
            get => 
                this.container;
            private set => 
                this.container = value;
        }

        public IEnumerable<IEntitySetInfo> EntitySets =>
            new <get_EntitySets>d__9(-2) { <>4__this=this };

        public DbContainerType ContainerType { get; set; }

        public string SourceUrl { get; set; }

        public IEnumerable<IEntityFunctionInfo> EntityFunctions =>
            new <get_EntityFunctions>d__20(-2) { <>4__this=this };

        public object MetadataWorkspace { get; private set; }

        public Type DbContextType { get; private set; }

        [CompilerGenerated]
        private sealed class <get_EntityFunctions>d__20 : IEnumerable<IEntityFunctionInfo>, IEnumerable, IEnumerator<IEntityFunctionInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEntityFunctionInfo <>2__current;
            private int <>l__initialThreadId;
            public DbContainerInfo <>4__this;
            private IEnumerator<IEntityFunctionInfo> <>7__wrap1;

            [DebuggerHidden]
            public <get_EntityFunctions>d__20(int <>1__state)
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
                        if ((this.<>4__this.EntityContainer == null) || (this.<>4__this.EntityContainer.EntityFunctions == null))
                        {
                            return false;
                        }
                        else
                        {
                            this.<>7__wrap1 = this.<>4__this.EntityContainer.EntityFunctions.GetEnumerator();
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
                        IEntityFunctionInfo current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
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
            IEnumerator<IEntityFunctionInfo> IEnumerable<IEntityFunctionInfo>.GetEnumerator()
            {
                DbContainerInfo.<get_EntityFunctions>d__20 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DbContainerInfo.<get_EntityFunctions>d__20(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Entity.Model.IEntityFunctionInfo>.GetEnumerator();

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

            IEntityFunctionInfo IEnumerator<IEntityFunctionInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <get_EntitySets>d__9 : IEnumerable<IEntitySetInfo>, IEnumerable, IEnumerator<IEntitySetInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEntitySetInfo <>2__current;
            private int <>l__initialThreadId;
            public DbContainerInfo <>4__this;
            private IEnumerator<IEntitySetInfo> <>7__wrap1;

            [DebuggerHidden]
            public <get_EntitySets>d__9(int <>1__state)
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
                        if ((this.<>4__this.EntityContainer == null) || (this.<>4__this.EntityContainer.EntitySets == null))
                        {
                            return false;
                        }
                        else
                        {
                            this.<>7__wrap1 = this.<>4__this.EntityContainer.EntitySets.GetEnumerator();
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
                            IEntitySetInfo current = this.<>7__wrap1.Current;
                            if (!this.<>4__this.IsValidEntitySet(current))
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
            IEnumerator<IEntitySetInfo> IEnumerable<IEntitySetInfo>.GetEnumerator()
            {
                DbContainerInfo.<get_EntitySets>d__9 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DbContainerInfo.<get_EntitySets>d__9(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Entity.Model.IEntitySetInfo>.GetEnumerator();

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

            IEntitySetInfo IEnumerator<IEntitySetInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

