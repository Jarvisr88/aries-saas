namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class CriteriaCompilerContextDescriptorDefaultBase : CriteriaCompilerDescriptor
    {
        protected CriteriaCompilerContextDescriptorDefaultBase();
        public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
        public static object KillDBNull(object nullableSomethig);
        private Expression LeafProcessing(Expression ex, string propertyPathForException);
        protected virtual Expression MakePathAccess(Expression baseExpression, string propertyPath);
        public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);
        protected abstract Expression MakePropertyAccessCore(Expression baseExpression, string property);
        protected Expression MakePropertyAccessCoreChecked(Expression baseExpression, string property);
        protected virtual Expression MakeThisAccess(Expression baseExpression);
        private static object MayBeRefAsCollectionExtractor(object mayBeRefAsCollection, string propertyPathForException);
        private static IEnumerable ToCollection(object o);
        [IteratorStateMachine(typeof(CriteriaCompilerContextDescriptorDefaultBase.<WrapTypedCollection>d__1))]
        private static IEnumerable WrapTypedCollection(ITypedList tl, IEnumerable e);

        [CompilerGenerated]
        private sealed class <WrapTypedCollection>d__1 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private ITypedList tl;
            public ITypedList <>3__tl;
            private IEnumerable e;
            public IEnumerable <>3__e;
            private CriteriaCompilerContextDescriptorReflective.TypedListUndObjectPair <rv>5__1;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <WrapTypedCollection>d__1(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

