namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows.Controls;

    public sealed class DefaultColumnChooserFactory : IColumnChooserFactory
    {
        public static readonly DefaultColumnChooserFactory Instance = new DefaultColumnChooserFactory();

        IColumnChooser IColumnChooserFactory.Create(Control owner) => 
            new DefaultColumnChooser((DataViewBase) owner);
    }
}

