namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class LazyGroupBrush : IDisposable
    {
        private readonly Initializer initializer;
        private Brush brush;

        public LazyGroupBrush(Initializer initializer)
        {
            this.initializer = initializer;
        }

        public void Dispose()
        {
            if (this.brush != null)
            {
                this.brush.Dispose();
            }
        }

        public bool HasValue =>
            this.brush != null;

        public Brush Value
        {
            get
            {
                this.brush ??= this.initializer?.Invoke();
                return this.brush;
            }
        }

        public delegate Brush Initializer();
    }
}

