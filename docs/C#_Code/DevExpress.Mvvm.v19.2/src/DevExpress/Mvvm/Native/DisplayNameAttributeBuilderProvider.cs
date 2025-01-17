﻿namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DisplayNameAttributeBuilderProvider : CustomAttributeBuilderProviderBase<DisplayNameAttribute>
    {
        internal override Expression<Func<DisplayNameAttribute>> GetConstructorExpression()
        {
            Expression[] expressionArray1 = new Expression[] { Expression.Constant(null, typeof(string)) };
            return Expression.Lambda<Func<DisplayNameAttribute>>(Expression.New((ConstructorInfo) methodof(DisplayNameAttribute..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]);
        }

        [IteratorStateMachine(typeof(<GetConstructorParameters>d__1))]
        internal override IEnumerable<object> GetConstructorParameters(DisplayNameAttribute attribute)
        {
            <GetConstructorParameters>d__1 d__1 = new <GetConstructorParameters>d__1(-2);
            d__1.<>3__attribute = attribute;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <GetConstructorParameters>d__1 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private DisplayNameAttribute attribute;
            public DisplayNameAttribute <>3__attribute;

            [DebuggerHidden]
            public <GetConstructorParameters>d__1(int <>1__state)
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
                    this.<>2__current = this.attribute.DisplayName;
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                DisplayNameAttributeBuilderProvider.<GetConstructorParameters>d__1 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new DisplayNameAttributeBuilderProvider.<GetConstructorParameters>d__1(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.attribute = this.<>3__attribute;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

