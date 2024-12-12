namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(ActiproSoftware.WinUICore.ScrollBarRenderer)), TypeConverter(typeof(GenericExpandableNullableObjectConverter))]
    public abstract class ScrollBarRenderer : DisposableObject, IDisposable, IScrollBarRenderer, IUIRenderer
    {
        [Category("Action"), Description("Occurs after a property is changed.")]
        public event EventHandler PropertyChanged;

        public abstract void DrawScrollBarBackground(PaintEventArgs e, Rectangle bounds, ActiproSoftware.WinUICore.ScrollBar scrollBar);
        public abstract void DrawScrollBarButton(PaintEventArgs e, Rectangle bounds, ScrollBarButton button);
        public abstract void DrawScrollBarThumb(PaintEventArgs e, Rectangle bounds, ScrollBarThumb thumb);
        public override bool Equals(object obj) => 
            false;

        public override int GetHashCode() => 
            this.GetHashCode();

        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (this.#Sj != null)
            {
                this.#Sj(this, e);
            }
        }
    }
}

