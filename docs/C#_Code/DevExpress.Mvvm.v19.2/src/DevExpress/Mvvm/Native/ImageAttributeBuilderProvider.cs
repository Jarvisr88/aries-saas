namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.DataAnnotations;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ImageAttributeBuilderProvider : CustomAttributeBuilderProviderBase<ImageAttribute>
    {
        internal override Expression<Func<ImageAttribute>> GetConstructorExpression()
        {
            Expression[] expressionArray1 = new Expression[] { Expression.Constant(null, typeof(string)) };
            return Expression.Lambda<Func<ImageAttribute>>(Expression.New((ConstructorInfo) methodof(ImageAttribute..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]);
        }

        [IteratorStateMachine(typeof(<GetConstructorParameters>d__1))]
        internal override IEnumerable<object> GetConstructorParameters(ImageAttribute attribute)
        {
            <GetConstructorParameters>d__1 d__1 = new <GetConstructorParameters>d__1(-2);
            d__1.<>3__attribute = attribute;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetPropertyValuePairs>d__2))]
        internal override IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairs(ImageAttribute attribute)
        {
            <GetPropertyValuePairs>d__2 d__1 = new <GetPropertyValuePairs>d__2(-2);
            d__1.<>4__this = this;
            d__1.<>3__attribute = attribute;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <GetConstructorParameters>d__1 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private ImageAttribute attribute;
            public ImageAttribute <>3__attribute;

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
                    this.<>2__current = this.attribute.ImageUri;
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
                ImageAttributeBuilderProvider.<GetConstructorParameters>d__1 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new ImageAttributeBuilderProvider.<GetConstructorParameters>d__1(0);
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

        [CompilerGenerated]
        private sealed class <GetPropertyValuePairs>d__2 : IEnumerable<Tuple<PropertyInfo, object>>, IEnumerable, IEnumerator<Tuple<PropertyInfo, object>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tuple<PropertyInfo, object> <>2__current;
            private int <>l__initialThreadId;
            public ImageAttributeBuilderProvider <>4__this;
            private ImageAttribute attribute;
            public ImageAttribute <>3__attribute;

            [DebuggerHidden]
            public <GetPropertyValuePairs>d__2(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num == 1)
                    {
                        this.<>1__state = -1;
                    }
                    return false;
                }
                this.<>1__state = -1;
                ParameterExpression expression = Expression.Parameter(typeof(ImageAttribute), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                this.<>2__current = this.<>4__this.GetPropertyValuePair<ImageAttribute, string>(this.attribute, Expression.Lambda<Func<ImageAttribute, string>>(Expression.Property(expression, (MethodInfo) methodof(ImageAttribute.get_ImageUri)), parameters));
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<Tuple<PropertyInfo, object>> IEnumerable<Tuple<PropertyInfo, object>>.GetEnumerator()
            {
                ImageAttributeBuilderProvider.<GetPropertyValuePairs>d__2 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new ImageAttributeBuilderProvider.<GetPropertyValuePairs>d__2(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.attribute = this.<>3__attribute;
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

