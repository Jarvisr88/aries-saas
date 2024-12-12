namespace DevExpress.Xpf.Core
{
    using System.Windows;
    using System.Windows.Data;

    public interface IBindableUriQualifier : IBaseUriQualifier
    {
        Binding GetBinding(RelativeSource source);
        Binding GetBinding(DependencyObject source);
    }
}

