namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomAppearanceEventArgs : EventArgs
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private DependencyProperty property;

        public CustomAppearanceEventArgs(CustomAppearanceEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            this.SetArgs(args.Property, args.OriginalValue, args.ConditionalValue);
            this.Result = args.Result;
        }

        public CustomAppearanceEventArgs(DependencyProperty property, object originalValue, object conditionalValue)
        {
            this.SetArgs(property, originalValue, conditionalValue);
        }

        private void SetArgs(DependencyProperty property, object originalValue, object conditionalValue)
        {
            this.Property = property;
            this.OriginalValue = originalValue;
            this.ConditionalValue = conditionalValue;
        }

        public DependencyProperty Property
        {
            get => 
                this.property;
            private set => 
                this.property = value;
        }

        public object OriginalValue { get; private set; }

        public object ConditionalValue { get; private set; }

        public object Result { get; set; }

        public bool Handled { get; set; }
    }
}

