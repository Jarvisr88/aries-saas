namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class psvGrid : Grid, IDisposable
    {
        public psvGrid()
        {
            base.Focusable = false;
            base.Loaded += new RoutedEventHandler(this.psvGrid_Loaded);
            base.Unloaded += new RoutedEventHandler(this.psvGrid_Unloaded);
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Loaded -= new RoutedEventHandler(this.psvGrid_Loaded);
                base.Unloaded -= new RoutedEventHandler(this.psvGrid_Unloaded);
                this.OnDispose();
            }
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnLoaded()
        {
        }

        protected virtual void OnUnloaded()
        {
        }

        private void psvGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsLoadedComplete = true;
            this.OnLoaded();
        }

        private void psvGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
            this.IsLoadedComplete = false;
        }

        public bool IsDisposing { get; private set; }

        protected internal bool IsLoadedComplete { get; set; }
    }
}

