namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class BindingContainer
    {
        public BindingContainer(BindingBase binding)
        {
            this.Binding = binding;
        }

        public BindingBase Binding { get; private set; }
    }
}

