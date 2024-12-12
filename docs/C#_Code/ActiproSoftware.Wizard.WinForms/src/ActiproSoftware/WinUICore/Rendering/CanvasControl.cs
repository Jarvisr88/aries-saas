namespace ActiproSoftware.WinUICore.Rendering
{
    using #xOk;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class CanvasControl : UIElement, ICanvas
    {
        private #Gsk #rJ;

        public event EventHandler<CanvasDrawEventArgs> Draw;

        private void #dsk()
        {
            if (this.#rJ != null)
            {
                this.#rJ.Dispose();
                this.#rJ = null;
            }
        }

        private void #fsk(Graphics #gsk)
        {
            EventHandler<CanvasDrawEventArgs> handler = this.#J4;
            if ((handler != null) && (#gsk != null))
            {
                Rectangle bounds = base.Bounds;
                using (CanvasDrawContext context = this.CreateDrawContext(this, #gsk, bounds))
                {
                    context.IsForPrinter = this.IsForPrinter;
                    context.DpiScale = this.IsForPrinter ? 1f : (#gsk.DpiX / 96f);
                    CanvasDrawEventArgs e = new CanvasDrawEventArgs(context);
                    handler(this, e);
                }
            }
        }

        private float #uOk()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                if (graphics != null)
                {
                    return (graphics.DpiX / 96f);
                }
            }
            return 1f;
        }

        public CanvasControl() : this(false)
        {
        }

        public CanvasControl(bool isForPrinter)
        {
            this.IsForPrinter = isForPrinter;
        }

        protected virtual CanvasDrawContext CreateDrawContext(ICanvas canvas, Graphics platformRenderer, Rectangle bounds) => 
            new CanvasDrawContext(canvas, platformRenderer, bounds);

        public IDisposable CreateTextBatch() => 
            new #4sk(this);

        public ITextLayout CreateTextLayout(string text, float maxWidth, string fontFamilyName, float fontSize, Color foreground)
        {
            StringTextProvider textProvider = new StringTextProvider(text);
            return this.CreateTextLayout(textProvider, maxWidth, fontFamilyName, fontSize, foreground, null);
        }

        public ITextLayout CreateTextLayout(ITextProvider textProvider, float maxWidth, string fontFamilyName, float fontSize, Color foreground, IEnumerable<ITextSpacer> spacers) => 
            new #xtk(this.Cache, textProvider, maxWidth, fontFamilyName, fontSize, new Color?(foreground), spacers);

        public ITextSpacer CreateTextSpacer(int characterIndex, object key, Size size, float baseline) => 
            new #yuk(characterIndex, key, size, baseline);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.#dsk();
            }
            base.Dispose(disposing);
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public T ExecuteFunc<T>(Func<T> func)
        {
            T local = default(T);
            try
            {
                if (func != null)
                {
                    local = func();
                }
            }
            catch
            {
            }
            return local;
        }

        public SolidBrush GetSolidColorBrush(Color color) => 
            this.Cache.GetSolidColorBrush(color);

        public Pen GetSquiggleLinePen(Color color) => 
            this.Cache.GetSquiggleLinePen(color);

        public void InvalidateRender()
        {
            this.Invalidate();
        }

        protected override void OnRender(PaintEventArgs e)
        {
            if (e != null)
            {
                this.#fsk(e.Graphics);
            }
        }

        protected void RenderToPrinter(Graphics platformRenderer, Rectangle bounds)
        {
            this.#dsk();
            this.#fsk(platformRenderer);
            this.#dsk();
        }

        private #Gsk Cache
        {
            get
            {
                if (this.#rJ == null)
                {
                    float fontScale = 1f;
                    if (this.IsForPrinter)
                    {
                        fontScale = 1f / this.#uOk();
                    }
                    this.#rJ = new #Gsk(this, fontScale);
                }
                return this.#rJ;
            }
        }

        protected internal bool IsForPrinter { get; private set; }
    }
}

