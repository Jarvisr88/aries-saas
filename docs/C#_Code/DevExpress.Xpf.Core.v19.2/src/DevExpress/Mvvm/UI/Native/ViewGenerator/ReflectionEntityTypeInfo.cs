namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class ReflectionEntityTypeInfo : ReflectionEntityProperties, IEntityTypeInfo, IEntityProperties
    {
        private readonly Type entityType;

        public ReflectionEntityTypeInfo(Type entityType, bool includeReadonly = false) : base(TypeDescriptor.GetProperties(entityType).Cast<PropertyDescriptor>(), entityType, includeReadonly, null)
        {
            this.entityType = entityType;
        }

        public IEdmPropertyInfo GetDependentProperty(IEdmPropertyInfo foreignKey) => 
            null;

        public IEdmPropertyInfo GetForeignKey(IEdmPropertyInfo dependentProperty) => 
            null;

        IEnumerable<IEdmPropertyInfo> IEntityTypeInfo.KeyMembers =>
            new <DevExpress-Entity-Model-IEntityTypeInfo-get_KeyMembers>d__5(-2);

        Type IEntityTypeInfo.Type =>
            this.entityType;

        IEnumerable<IEdmAssociationPropertyInfo> IEntityTypeInfo.LookupTables =>
            new <DevExpress-Entity-Model-IEntityTypeInfo-get_LookupTables>d__9(-2);

        [CompilerGenerated]
        private sealed class <DevExpress-Entity-Model-IEntityTypeInfo-get_KeyMembers>d__5 : IEnumerable<IEdmPropertyInfo>, IEnumerable, IEnumerator<IEdmPropertyInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEdmPropertyInfo <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <DevExpress-Entity-Model-IEntityTypeInfo-get_KeyMembers>d__5(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<IEdmPropertyInfo> IEnumerable<IEdmPropertyInfo>.GetEnumerator()
            {
                ReflectionEntityTypeInfo.<DevExpress-Entity-Model-IEntityTypeInfo-get_KeyMembers>d__5 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new ReflectionEntityTypeInfo.<DevExpress-Entity-Model-IEntityTypeInfo-get_KeyMembers>d__5(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Entity.Model.IEdmPropertyInfo>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IEdmPropertyInfo IEnumerator<IEdmPropertyInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Entity-Model-IEntityTypeInfo-get_LookupTables>d__9 : IEnumerable<IEdmAssociationPropertyInfo>, IEnumerable, IEnumerator<IEdmAssociationPropertyInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEdmAssociationPropertyInfo <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <DevExpress-Entity-Model-IEntityTypeInfo-get_LookupTables>d__9(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<IEdmAssociationPropertyInfo> IEnumerable<IEdmAssociationPropertyInfo>.GetEnumerator()
            {
                ReflectionEntityTypeInfo.<DevExpress-Entity-Model-IEntityTypeInfo-get_LookupTables>d__9 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new ReflectionEntityTypeInfo.<DevExpress-Entity-Model-IEntityTypeInfo-get_LookupTables>d__9(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Entity.Model.IEdmAssociationPropertyInfo>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IEdmAssociationPropertyInfo IEnumerator<IEdmAssociationPropertyInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

