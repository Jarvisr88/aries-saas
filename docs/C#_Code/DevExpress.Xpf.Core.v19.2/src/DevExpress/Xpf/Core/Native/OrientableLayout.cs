namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class OrientableLayout
    {
        private ObservableCollection<RowDefinition> rowDefinitions;
        private ObservableCollection<ColumnDefinition> columnDefinitions;

        public OrientableLayout();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<RowDefinition> RowDefinitions { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<ColumnDefinition> ColumnDefinitions { get; }
    }
}

