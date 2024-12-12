namespace DevExpress.Xpf.Core
{
    using System.Windows.Controls;

    public interface IColumnChooserFactory
    {
        IColumnChooser Create(Control owner);
    }
}

