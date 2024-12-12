namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class PainterCache : IDisposable
    {
        [ThreadStatic]
        private static PainterCache defaultCache;
        private Dictionary<Color, SolidBrush> solidBrushes = new Dictionary<Color, SolidBrush>(new ColorComparer());
        private Dictionary<PenKey, Pen> pens = new Dictionary<PenKey, Pen>(new PenKeyComparer());

        private void Clear()
        {
            this.ClearCache(this.pens);
            this.ClearCache(this.solidBrushes);
        }

        private void ClearCache(IDictionary cache)
        {
            if (cache != null)
            {
                foreach (IDisposable disposable in cache.Values)
                {
                    disposable.Dispose();
                }
                cache.Clear();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Clear();
            }
        }

        ~PainterCache()
        {
            this.Dispose(false);
        }

        public Pen GetPen(Color color) => 
            this.GetPen(color, 1);

        public Pen GetPen(Color color, int width)
        {
            Pen pen;
            if ((width == 1) && color.IsSystemColor)
            {
                return SystemPens.FromSystemColor(color);
            }
            PenKey key = new PenKey(color, width);
            if (!this.pens.TryGetValue(key, out pen))
            {
                pen = new Pen(color, (float) width);
                this.pens.Add(key, pen);
            }
            return pen;
        }

        public SolidBrush GetSolidBrush(Color color)
        {
            SolidBrush brush;
            if (color.IsSystemColor)
            {
                return (SolidBrush) SystemBrushes.FromSystemColor(color);
            }
            if (!this.solidBrushes.TryGetValue(color, out brush))
            {
                brush = new SolidBrush(color);
                this.solidBrushes.Add(color, brush);
            }
            return brush;
        }

        public static PainterCache DefaultCache
        {
            get
            {
                defaultCache ??= new PainterCache();
                return defaultCache;
            }
        }

        private class ColorComparer : IEqualityComparer<Color>
        {
            public bool Equals(Color x, Color y) => 
                x == y;

            public int GetHashCode(Color obj) => 
                obj.GetHashCode();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PenKey
        {
            public System.Drawing.Color Color;
            public int Width;
            public PenKey(System.Drawing.Color color, int width)
            {
                this.Color = color;
                this.Width = width;
            }
        }

        private class PenKeyComparer : IEqualityComparer<PainterCache.PenKey>
        {
            public bool Equals(PainterCache.PenKey x, PainterCache.PenKey y) => 
                (x.Color == y.Color) && (x.Width == y.Width);

            public int GetHashCode(PainterCache.PenKey obj) => 
                obj.Color.GetHashCode() ^ obj.Width;
        }
    }
}

