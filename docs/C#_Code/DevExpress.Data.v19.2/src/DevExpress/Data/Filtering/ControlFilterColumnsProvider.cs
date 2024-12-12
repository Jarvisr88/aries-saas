namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ControlFilterColumnsProvider : IControlFilterColumnsProvider
    {
        protected ControlFilterColumnsProvider();
        protected abstract PropertyDescriptor CreateFindPropertyDescriptor(IControlFilterColumn column);
        protected abstract PropertyDescriptor CreatePropertyDescriptor(IControlFilterColumn column);
        PropertyDescriptorCollection IControlFilterColumnsProvider.GetColumnDescriptors();
        protected abstract IEnumerable<IControlFilterColumn> GetColumns();
        protected PropertyDescriptorCollection GetFilterColumnDescriptors();
        protected PropertyDescriptorCollection GetFilterColumnDescriptors(IControlFilterColumn[] columns, bool skipFindDescriptors);
        protected abstract bool GetIsFindFilterActive();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ControlFilterColumnsProvider.<>c <>9;
            public static Func<IControlFilterColumn, bool> <>9__1_0;

            static <>c();
            internal bool <GetFilterColumnDescriptors>b__1_0(IControlFilterColumn x);
        }

        protected abstract class FilterPropertyDescriptor : PropertyDescriptor
        {
            private readonly IControlFilterColumn column;

            protected FilterPropertyDescriptor(IControlFilterColumn column);
            protected FilterPropertyDescriptor(IControlFilterColumn column, string name);
            protected static string AddPrefix(string name);
            public sealed override bool CanResetValue(object component);
            public sealed override void ResetValue(object component);
            public sealed override void SetValue(object component, object value);
            public sealed override bool ShouldSerializeValue(object component);

            public override Type PropertyType { get; }

            public sealed override string DisplayName { get; }

            public sealed override bool IsReadOnly { get; }
        }
    }
}

