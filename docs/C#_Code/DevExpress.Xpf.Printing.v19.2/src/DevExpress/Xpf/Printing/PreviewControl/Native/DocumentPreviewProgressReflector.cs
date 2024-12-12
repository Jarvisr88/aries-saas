namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public class DocumentPreviewProgressReflector : ReflectorBarBase
    {
        private bool visible;

        public event EventHandler VisibilityChanged;

        protected override void Invalidate(bool withChildren)
        {
        }

        private void RaiseVisibilityChanged()
        {
            this.VisibilityChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        public override bool Visible
        {
            get => 
                this.visible;
            set
            {
                if (this.visible != value)
                {
                    this.visible = value;
                    this.RaiseVisibilityChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }
    }
}

