namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ViewModelSourceExtension : MarkupExtension
    {
        public ViewModelSourceExtension()
        {
        }

        public ViewModelSourceExtension(System.Type type)
        {
            this.Type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            (this.Type != null) ? ViewModelSourceHelper.Create(this.Type) : null;

        public System.Type Type { get; set; }
    }
}

