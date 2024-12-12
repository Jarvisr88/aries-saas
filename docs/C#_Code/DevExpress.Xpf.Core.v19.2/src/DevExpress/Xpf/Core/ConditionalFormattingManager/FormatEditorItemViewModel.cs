namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public abstract class FormatEditorItemViewModel : ManagerViewModelBase
    {
        protected FormatEditorItemViewModel(IDialogContext column) : base(column)
        {
        }

        public abstract void Clear();
        protected SolidColorBrush CreateBrush(Color? color) => 
            ((color == null) || (color.Value == Colors.Transparent)) ? null : new SolidColorBrush(color.Value);

        protected Color? GetBrushColor(Brush brush)
        {
            SolidColorBrush brush2 = brush as SolidColorBrush;
            if (brush2 != null)
            {
                return new Color?(brush2.Color);
            }
            return null;
        }

        public abstract void InitFromFormat(Format format);
        protected void OnChanged()
        {
            this.HasChanged = true;
        }

        public abstract void SetFormatProperties(Format format);

        public bool HasChanged { get; set; }
    }
}

