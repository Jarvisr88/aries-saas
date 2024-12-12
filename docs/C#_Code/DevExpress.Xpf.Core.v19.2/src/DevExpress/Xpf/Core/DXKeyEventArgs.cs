namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DXKeyEventArgs : EventArgs
    {
        private bool _Handled;

        public DXKeyEventArgs()
        {
        }

        public DXKeyEventArgs(KeyEventArgs args, Action<DXKeyEventArgs> handledChanged)
        {
            this.Handled = args.Handled;
            this.Key = args.Key;
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

        public System.Windows.Input.Key Key { get; protected set; }

        protected Action<DXKeyEventArgs> HandledChanged { get; set; }
    }
}

