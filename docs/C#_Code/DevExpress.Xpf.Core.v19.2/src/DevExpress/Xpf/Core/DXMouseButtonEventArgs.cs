namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DXMouseButtonEventArgs : DXMouseEventArgs
    {
        private bool _Handled;

        public DXMouseButtonEventArgs()
        {
        }

        public DXMouseButtonEventArgs(MouseButtonEventArgs args, Action<DXMouseButtonEventArgs> handledChanged) : base(args)
        {
            this.Handled = args.Handled;
            this.HandledChanged = handledChanged;
        }

        public bool Handled
        {
            get => 
                this._Handled;
            set
            {
                if (this.Handled != value)
                {
                    this._Handled = value;
                    if (this.HandledChanged != null)
                    {
                        this.HandledChanged(this);
                    }
                }
            }
        }

        protected Action<DXMouseButtonEventArgs> HandledChanged { get; set; }
    }
}

