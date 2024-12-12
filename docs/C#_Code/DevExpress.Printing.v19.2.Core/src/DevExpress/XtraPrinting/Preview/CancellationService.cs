namespace DevExpress.XtraPrinting.Preview
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class CancellationService : ICancellationService, IDisposable
    {
        private CancellationTokenSource tokenSource;

        public event EventHandler StateChanged;

        public void DisposeTokenSource()
        {
            if (this.tokenSource != null)
            {
                this.tokenSource.Cancel();
                this.tokenSource.Dispose();
                this.tokenSource = null;
            }
        }

        private void RaiseStateChanged()
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, EventArgs.Empty);
            }
        }

        public void ResetTokenSource()
        {
            this.IsResetting = true;
            try
            {
                this.DisposeTokenSource();
                this.tokenSource = new CancellationTokenSource();
                this.tokenSource.Token.Register(new Action(this.RaiseStateChanged));
                this.RaiseStateChanged();
            }
            finally
            {
                this.IsResetting = false;
            }
        }

        void IDisposable.Dispose()
        {
            this.DisposeTokenSource();
        }

        public bool IsResetting { get; private set; }

        public CancellationTokenSource TokenSource =>
            this.tokenSource;
    }
}

