namespace DevExpress.Data.TreeList
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TreeListDisplayTextPropertyDescriptor : PropertyDescriptor
    {
        public TreeListDisplayTextPropertyDescriptor(TreeListDataControllerBase controller, string name);
        public override bool CanResetValue(object component);
        public override object GetValue(object component);
        protected virtual object GetValueCore(TreeListNodeBase node);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public TreeListDataControllerBase Controller { get; private set; }

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }
    }
}

