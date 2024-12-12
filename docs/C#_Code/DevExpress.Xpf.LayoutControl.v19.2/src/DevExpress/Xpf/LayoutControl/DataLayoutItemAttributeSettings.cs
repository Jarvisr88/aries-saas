namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class DataLayoutItemAttributeSettings
    {
        private static DataLayoutItemAttributeSettings empty;

        private DataLayoutItemAttributeSettings(Settings settings)
        {
            if (settings == null)
            {
                this.Label = null;
                this.ToolTip = null;
                this.IsReadOnly = false;
                this.IsRequired = false;
                this.NullText = null;
                this.ShowNullTextForEmptyValue = false;
                this.DisplayFormatString = null;
            }
            else
            {
                this.Label = settings.Label;
                this.ToolTip = settings.ToolTip;
                this.IsReadOnly = settings.IsReadOnly;
                this.IsRequired = settings.IsRequired;
                this.NullText = settings.NullText;
                this.ShowNullTextForEmptyValue = settings.ShowNullTextForEmptyValue;
                this.DisplayFormatString = settings.DisplayFormatString;
            }
        }

        public static DataLayoutItemAttributeSettings Create(IEdmPropertyInfo propertyInfo)
        {
            Settings root = new Settings();
            RuntimeEditingContext context = new RuntimeEditingContext(root, null);
            AttributesApplier.ApplyBaseAttributesForLayoutItem(propertyInfo, context.GetRoot(), context.GetRoot(), null);
            AttributesApplier.ApplyDisplayFormatAttributesForEditor(propertyInfo, () => context.GetRoot(), null);
            return new DataLayoutItemAttributeSettings(root);
        }

        public static DataLayoutItemAttributeSettings Empty =>
            empty ??= new DataLayoutItemAttributeSettings(null);

        public string Label { get; private set; }

        public string ToolTip { get; private set; }

        public bool IsReadOnly { get; private set; }

        public bool IsRequired { get; private set; }

        public string NullText { get; private set; }

        public bool ShowNullTextForEmptyValue { get; private set; }

        public string DisplayFormatString { get; private set; }

        private class Settings
        {
            public string Label { get; set; }

            public string ToolTip { get; set; }

            public bool IsReadOnly { get; set; }

            public bool IsRequired { get; set; }

            public string NullText { get; set; }

            public bool ShowNullTextForEmptyValue { get; set; }

            public string DisplayFormatString { get; set; }
        }
    }
}

