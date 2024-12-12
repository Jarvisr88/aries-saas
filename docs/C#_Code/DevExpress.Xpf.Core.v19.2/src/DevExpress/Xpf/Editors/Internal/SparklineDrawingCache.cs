namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public class SparklineDrawingCache : IDisposable
    {
        private readonly Dictionary<Color, SolidColorBrush> brushes = new Dictionary<Color, SolidColorBrush>();
        private readonly Dictionary<SolidColorBrush, List<Pen>> pens = new Dictionary<SolidColorBrush, List<Pen>>();

        public void Dispose()
        {
            this.brushes.Clear();
            this.pens.Clear();
        }

        public Pen GetPen(SolidColorBrush brush, int thinkness)
        {
            Pen pen4;
            if (!this.pens.ContainsKey(brush))
            {
                this.pens[brush] = new List<Pen>();
                Pen pen1 = new Pen(brush, (double) thinkness);
                pen1.EndLineCap = PenLineCap.Round;
                pen1.StartLineCap = PenLineCap.Round;
                Pen item = pen1;
                this.pens[brush].Add(item);
                return item;
            }
            using (List<Pen>.Enumerator enumerator = this.pens[brush].GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Pen current = enumerator.Current;
                        if (current.Thickness != thinkness)
                        {
                            continue;
                        }
                        pen4 = current;
                    }
                    else
                    {
                        Pen pen5 = new Pen(brush, (double) thinkness);
                        pen5.EndLineCap = PenLineCap.Round;
                        pen5.StartLineCap = PenLineCap.Round;
                        Pen item = pen5;
                        this.pens[brush].Add(item);
                        return item;
                    }
                    break;
                }
            }
            return pen4;
        }

        public SolidColorBrush GetSolidBrush(Color color)
        {
            if (!this.brushes.ContainsKey(color))
            {
                this.brushes.Add(color, new SolidColorBrush(color));
            }
            return this.brushes[color];
        }
    }
}

