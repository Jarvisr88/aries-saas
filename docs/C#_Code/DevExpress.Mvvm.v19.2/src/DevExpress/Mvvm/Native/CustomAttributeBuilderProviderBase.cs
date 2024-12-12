namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public abstract class CustomAttributeBuilderProviderBase : ICustomAttributeBuilderProvider
    {
        protected CustomAttributeBuilderProviderBase()
        {
        }

        CustomAttributeBuilder ICustomAttributeBuilderProvider.CreateAttributeBuilder(Attribute attribute)
        {
            IEnumerable<Tuple<PropertyInfo, object>> propertyValuePairsCore = this.GetPropertyValuePairsCore(attribute);
            Func<Tuple<PropertyInfo, object>, PropertyInfo> selector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<Tuple<PropertyInfo, object>, PropertyInfo> local1 = <>c.<>9__4_0;
                selector = <>c.<>9__4_0 = x => x.Item1;
            }
            return new CustomAttributeBuilder(ExpressionHelper.GetConstructorCore(this.GetConstructorExpressionCore()), this.GetConstructorParametersCore(attribute).ToArray<object>(), propertyValuePairsCore.Select<Tuple<PropertyInfo, object>, PropertyInfo>(selector).ToArray<PropertyInfo>(), propertyValuePairsCore.Select<Tuple<PropertyInfo, object>, object>((<>c.<>9__4_1 ??= x => x.Item2)).ToArray<object>());
        }

        internal abstract LambdaExpression GetConstructorExpressionCore();
        [IteratorStateMachine(typeof(<GetConstructorParametersCore>d__5))]
        internal virtual IEnumerable<object> GetConstructorParametersCore(Attribute attribute) => 
            new <GetConstructorParametersCore>d__5(-2);

        [IteratorStateMachine(typeof(<GetPropertyValuePairsCore>d__6))]
        internal virtual IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairsCore(Attribute attribute) => 
            new <GetPropertyValuePairsCore>d__6(-2);

        Type ICustomAttributeBuilderProvider.AttributeType =>
            this.AttributeType;

        protected abstract Type AttributeType { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomAttributeBuilderProviderBase.<>c <>9 = new CustomAttributeBuilderProviderBase.<>c();
            public static Func<Tuple<PropertyInfo, object>, PropertyInfo> <>9__4_0;
            public static Func<Tuple<PropertyInfo, object>, object> <>9__4_1;

            internal PropertyInfo <DevExpress.Mvvm.Native.ICustomAttributeBuilderProvider.CreateAttributeBuilder>b__4_0(Tuple<PropertyInfo, object> x) => 
                x.Item1;

            internal object <DevExpress.Mvvm.Native.ICustomAttributeBuilderProvider.CreateAttributeBuilder>b__4_1(Tuple<PropertyInfo, object> x) => 
                x.Item2;
        }

        [CompilerGenerated]
        private sealed class <GetConstructorParametersCore>d__5 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetConstructorParametersCore>d__5(int <>1__state)
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
                CustomAttributeBuilderProviderBase.<GetConstructorParametersCore>d__5 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new CustomAttributeBuilderProviderBase.<GetConstructorParametersCore>d__5(0);
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
        private sealed class <GetPropertyValuePairsCore>d__6 : IEnumerable<Tuple<PropertyInfo, object>>, IEnumerable, IEnumerator<Tuple<PropertyInfo, object>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tuple<PropertyInfo, object> <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetPropertyValuePairsCore>d__6(int <>1__state)
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
                CustomAttributeBuilderProviderBase.<GetPropertyValuePairsCore>d__6 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new CustomAttributeBuilderProviderBase.<GetPropertyValuePairsCore>d__6(0);
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

