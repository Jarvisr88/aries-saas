namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [Browsable(false), DXToolboxBrowsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class PreviewTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            !(item is Format) ? (!(item is ColorScaleFormat) ? (!(item is DataBarFormat) ? (!(item is IconFormatStyle) ? this.EmptyTemplate : this.IconSetTemplate) : this.DataBarTemplate) : this.ColorTemplate) : this.FormatTemplate;

        public DataTemplate ColorTemplate { get; set; }

        public DataTemplate DataBarTemplate { get; set; }

        public DataTemplate IconSetTemplate { get; set; }

        public DataTemplate FormatTemplate { get; set; }

        public DataTemplate EmptyTemplate { get; set; }
    }
}

