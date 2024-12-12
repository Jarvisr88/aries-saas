namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RuntimePropertyCustomization : RuntimeCustomization
    {
        public RuntimePropertyCustomization();
        public RuntimePropertyCustomization(DependencyObject forcedTarget);
        protected override bool ApplyOverride(bool silent);
        protected virtual bool DoOverwrite(RuntimePropertyCustomization rp);
        protected override DependencyObject FindTarget();
        private object GetValue(object element, string propertyName, bool lastButOne);
        private bool SetCurrentValue(object value, bool silent);
        private void SetCurrentValueForDependencyProperty(object value, DependencyObject target, DependencyProperty property);
        private void SetValue(object element, string propertyName, object value);
        private void SetValueForSimpleProperty(object target, object value);
        protected sealed override bool TryOverwriteOverride(RuntimeCustomization second);
        protected override bool UndoOverride();

        [XtraSerializableProperty]
        public string PropertyName { get; set; }

        [XtraSerializableProperty]
        public object NewValue { get; set; }

        [XtraSerializableProperty]
        public object OldValue { get; set; }

        [XtraSerializableProperty]
        public bool ActOnHost { get; set; }

        public override bool IsInformativeCustomization { get; }
    }
}

