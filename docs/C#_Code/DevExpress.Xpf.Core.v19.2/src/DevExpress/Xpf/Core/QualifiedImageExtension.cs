namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class QualifiedImageExtension : MarkupExtension
    {
        public QualifiedImageExtension()
        {
        }

        public QualifiedImageExtension(string uri)
        {
            this.Uri = uri;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            UriQualifierHelper.ProvideValueOrExpression(this, serviceProvider, this.Uri);

        public string Uri { get; set; }
    }
}

