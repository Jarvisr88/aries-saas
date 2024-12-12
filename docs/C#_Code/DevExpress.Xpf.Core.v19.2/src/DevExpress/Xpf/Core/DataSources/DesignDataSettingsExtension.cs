namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DesignDataSettingsExtension : MarkupExtension, IDesignDataSettings
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            DesignDataSettings settings1 = new DesignDataSettings();
            settings1.RowCount = this.RowCount;
            settings1.UseDistinctValues = this.UseDistinctValues;
            settings1.DataObjectType = this.DataObjectType;
            settings1.FlattenHierarchy = this.FlattenHierarchy;
            return settings1;
        }

        public bool FlattenHierarchy { get; set; }

        public int RowCount { get; set; }

        public bool UseDistinctValues { get; set; }

        public Type DataObjectType { get; set; }
    }
}

