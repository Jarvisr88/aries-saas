namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DXMouseWheelEventArgs : DXMouseEventArgs
    {
        private bool _Handled;

        public DXMouseWheelEventArgs()
        {
        }

        public DXMouseWheelEventArgs(MouseWheelEventArgs args, Action<DXMouseWheelEventArgs> handledChanged) : base(args)
        {
            this.Delta = args.Delta;
            this.Handled = args.Handled;
            this.HandledChanged = handledChanged;
        }

        public int Delta { get; protected set; }

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

        protected Action<DXMouseWheelEventArgs> HandledChanged { get; set; }
    }
}

