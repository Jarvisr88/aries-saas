namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class StandardIndicatorFormatInfoExtension : StandardFormatInfoExtension
    {
        protected override ImageSource GetIcon() => 
            this.Icon;

        public ImageSource Icon { get; set; }
    }
}

