namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.Mvvm.UI.ViewGenerator.Metadata;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class StandardColumnsProvider
    {
        private readonly FrameworkElement ownerElement;

        public StandardColumnsProvider(FrameworkElement ownerElement)
        {
            this.ownerElement = ownerElement;
        }

        public object FindResource(object resourceKey) => 
            this.ownerElement.FindResource(resourceKey);

        public PropertyDescription GetStandardColumn(PropertyDescriptor descriptor)
        {
            PropertyDescription currentColumn;
            try
            {
                if (descriptor == null)
                {
                    currentColumn = null;
                }
                else
                {
                    GenerateEditorOptions options = GenerateEditorOptions.ForRuntime();
                    EditorsSource.GenerateEditor(new EdmPropertyInfo(descriptor, DataColumnAttributesProvider.GetAttributes(descriptor, null, null), false, false), new StandaloneColumnGenerator(new RuntimeEditingContext(this, null).GetRoot(), this.ownerElement), null, options.CollectionProperties, options.GuessImageProperties, options.GuessDisplayMembers, false);
                    currentColumn = this.CurrentColumn;
                }
            }
            finally
            {
                this.CurrentColumn = null;
            }
            return currentColumn;
        }

        public PropertyDescription CurrentColumn { get; set; }
    }
}

