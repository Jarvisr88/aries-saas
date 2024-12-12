namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class TrackBarIncrementEnumExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this.IncrementTarget;

        public TrackBarIncrementTargetEnum IncrementTarget { get; set; }
    }
}

