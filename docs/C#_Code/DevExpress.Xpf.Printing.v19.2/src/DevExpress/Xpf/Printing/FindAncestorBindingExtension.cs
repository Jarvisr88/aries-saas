namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows.Data;

    public abstract class FindAncestorBindingExtension : BindingExtensionBase
    {
        protected FindAncestorBindingExtension()
        {
        }

        protected abstract Type AncestorType { get; }

        protected override System.Windows.Data.RelativeSource RelativeSource
        {
            get
            {
                System.Windows.Data.RelativeSource source1 = new System.Windows.Data.RelativeSource();
                source1.Mode = RelativeSourceMode.FindAncestor;
                source1.AncestorType = this.AncestorType;
                return source1;
            }
        }
    }
}

