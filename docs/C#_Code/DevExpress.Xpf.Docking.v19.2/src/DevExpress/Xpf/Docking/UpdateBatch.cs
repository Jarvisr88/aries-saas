namespace DevExpress.Xpf.Docking
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class UpdateBatch : IDisposable
    {
        private ISupportBatchUpdate Container;

        public UpdateBatch(ISupportBatchUpdate container)
        {
            this.Container = container;
            if (this.Container != null)
            {
                this.Container.BeginUpdate();
            }
        }

        public void Dispose()
        {
            if (this.Container != null)
            {
                this.Container.EndUpdate();
            }
            this.Container = null;
        }
    }
}

