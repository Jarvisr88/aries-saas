namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Windows.Markup;

    internal class GalleryEditValueEventArgsConverter : MarkupExtension, IEventArgsConverter
    {
        object IEventArgsConverter.Convert(object sender, object args)
        {
            EditValueChangedEventArgs args2 = args as EditValueChangedEventArgs;
            return ((args2 != null) ? ((ImageGalleryItem) args2.NewValue) : null);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

