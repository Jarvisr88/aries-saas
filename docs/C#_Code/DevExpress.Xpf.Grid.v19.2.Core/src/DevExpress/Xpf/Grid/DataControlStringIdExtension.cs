namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DataControlStringIdExtension : MarkupExtension
    {
        public DataControlStringIdExtension(GridControlStringId stringId)
        {
            this.StringId = stringId;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            GridControlLocalizer.GetString(this.StringId);

        public GridControlStringId StringId { get; set; }
    }
}

