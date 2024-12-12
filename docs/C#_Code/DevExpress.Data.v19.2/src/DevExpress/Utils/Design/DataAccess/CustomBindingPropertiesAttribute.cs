namespace DevExpress.Utils.Design.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=false)]
    public abstract class CustomBindingPropertiesAttribute : Attribute, ICustomBindingPropertiesProvider
    {
        protected CustomBindingPropertiesAttribute()
        {
        }

        [IteratorStateMachine(typeof(<GetCustomBindingProperties>d__0))]
        public virtual IEnumerable<ICustomBindingProperty> GetCustomBindingProperties()
        {
            <GetCustomBindingProperties>d__0 d__1 = new <GetCustomBindingProperties>d__0(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <GetCustomBindingProperties>d__0 : IEnumerable<ICustomBindingProperty>, IEnumerable, IEnumerator<ICustomBindingProperty>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ICustomBindingProperty <>2__current;
            private int <>l__initialThreadId;
            public CustomBindingPropertiesAttribute <>4__this;
            private object[] <attributes>5__1;
            private int <i>5__2;

            [DebuggerHidden]
            public <GetCustomBindingProperties>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<attributes>5__1 = this.<>4__this.GetType().GetCustomAttributes(typeof(CustomBindingPropertiesAttribute.CustomBindingPropertyAttribute), true);
                    this.<i>5__2 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__2;
                    this.<i>5__2 = num2 + 1;
                }
                if (this.<i>5__2 >= this.<attributes>5__1.Length)
                {
                    return false;
                }
                this.<>2__current = this.<attributes>5__1[this.<i>5__2] as ICustomBindingProperty;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<ICustomBindingProperty> IEnumerable<ICustomBindingProperty>.GetEnumerator()
            {
                CustomBindingPropertiesAttribute.<GetCustomBindingProperties>d__0 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new CustomBindingPropertiesAttribute.<GetCustomBindingProperties>d__0(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Design.DataAccess.ICustomBindingProperty>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            ICustomBindingProperty IEnumerator<ICustomBindingProperty>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=true)]
        protected class CustomBindingPropertyAttribute : Attribute, ICustomBindingProperty
        {
            public CustomBindingPropertyAttribute(string propertyName, string displayName, string description)
            {
                this.PropertyName = propertyName;
                this.DisplayName = displayName;
                this.Description = description;
            }

            public string PropertyName { get; private set; }

            public string DisplayName { get; private set; }

            public string Description { get; private set; }
        }

        [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=true)]
        protected class DataMemberBindingPropertyAttribute : CustomBindingPropertiesAttribute.CustomBindingPropertyAttribute, IDataMemberBindingProperty, ICustomBindingProperty
        {
            public DataMemberBindingPropertyAttribute(string dataMember, string propertyName, string displayName, string description) : base(propertyName, displayName, description)
            {
                this.DataMember = dataMember;
            }

            public string DataMember { get; private set; }
        }
    }
}

