namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class CustomAttributeBuilderProviderBase<T> : CustomAttributeBuilderProviderBase where T: Attribute
    {
        protected CustomAttributeBuilderProviderBase()
        {
        }

        internal abstract Expression<Func<T>> GetConstructorExpression();
        internal sealed override LambdaExpression GetConstructorExpressionCore() => 
            this.GetConstructorExpression();

        [IteratorStateMachine(typeof(<GetConstructorParameters>d__6))]
        internal virtual IEnumerable<object> GetConstructorParameters(T attribute) => 
            new <GetConstructorParameters>d__6<T>(-2);

        internal sealed override IEnumerable<object> GetConstructorParametersCore(Attribute attribute) => 
            this.GetConstructorParameters((T) attribute);

        protected Tuple<PropertyInfo, object> GetPropertyValuePair<TAttribute, TProperty>(TAttribute attribute, Expression<Func<TAttribute, TProperty>> propertyExpression) => 
            DataAnnotationsAttributeHelper.GetPropertyValuePair<TAttribute, TProperty>(attribute, propertyExpression);

        [IteratorStateMachine(typeof(<GetPropertyValuePairs>d__7))]
        internal virtual IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairs(T attribute) => 
            new <GetPropertyValuePairs>d__7<T>(-2);

        internal sealed override IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairsCore(Attribute attribute) => 
            this.GetPropertyValuePairs((T) attribute);

        protected sealed override Type AttributeType =>
            typeof(T);

        [CompilerGenerated]
        private sealed class <GetConstructorParameters>d__6 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetConstructorParameters>d__6(int <>1__state)
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
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                CustomAttributeBuilderProviderBase<T>.<GetConstructorParameters>d__6 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new CustomAttributeBuilderProviderBase<T>.<GetConstructorParameters>d__6(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (CustomAttributeBuilderProviderBase<T>.<GetConstructorParameters>d__6) this;
                }
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

        [CompilerGenerated]
        private sealed class <GetPropertyValuePairs>d__7 : IEnumerable<Tuple<PropertyInfo, object>>, IEnumerable, IEnumerator<Tuple<PropertyInfo, object>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tuple<PropertyInfo, object> <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetPropertyValuePairs>d__7(int <>1__state)
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
            IEnumerator<Tuple<PropertyInfo, object>> IEnumerable<Tuple<PropertyInfo, object>>.GetEnumerator()
            {
                CustomAttributeBuilderProviderBase<T>.<GetPropertyValuePairs>d__7 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new CustomAttributeBuilderProviderBase<T>.<GetPropertyValuePairs>d__7(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (CustomAttributeBuilderProviderBase<T>.<GetPropertyValuePairs>d__7) this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Tuple<System.Reflection.PropertyInfo,System.Object>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tuple<PropertyInfo, object> IEnumerator<Tuple<PropertyInfo, object>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

