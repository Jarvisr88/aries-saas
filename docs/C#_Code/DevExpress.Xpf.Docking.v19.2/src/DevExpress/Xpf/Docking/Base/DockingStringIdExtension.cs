namespace DevExpress.Xpf.Docking.Base
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DockingStringIdExtension : MarkupExtension
    {
        public DockingStringIdExtension(DockingStringId stringId)
        {
            this.StringId = stringId;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            DockingLocalizer.GetString(this.StringId);

        public DockingStringId StringId { get; set; }
    }
}

