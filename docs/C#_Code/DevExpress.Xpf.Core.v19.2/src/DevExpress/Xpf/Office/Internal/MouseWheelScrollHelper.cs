namespace DevExpress.Xpf.Office.Internal
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class MouseWheelScrollHelper
    {
        private int distance;
        private DateTime lastEvent = DateTime.MinValue;
        private const int SkipInterval = 400;
        private const int PixelLineHeight = 120;
        private IMouseWheelScrollClient client;

        public MouseWheelScrollHelper(IMouseWheelScrollClient client, bool usePixeScrolling)
        {
            this.client = client;
            this.UsePixelScrolling = usePixeScrolling;
        }

        private int GetDirection(MouseWheelEventArgs e) => 
            (e.Delta > 0) ? -1 : 1;

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            MouseWheelEventArgsEx ex = e as MouseWheelEventArgsEx;
            if (DateTime.Now.Subtract(this.lastEvent).TotalMilliseconds > 400.0)
            {
                this.ResetDistance();
            }
            this.lastEvent = DateTime.Now;
            int linesCount = (ex == null) ? e.Delta : ex.DeltaX;
            if (this.UsePixelScrolling)
            {
                this.OnScrollLine(e, linesCount, true);
            }
            else if (((linesCount % 120) == 0) && (this.distance == 0))
            {
                this.OnScrollLine(e, linesCount / 120, true);
            }
            else
            {
                if (Math.Sign(this.distance) != Math.Sign(linesCount))
                {
                    this.distance = 0;
                }
                this.distance += linesCount;
                int num2 = this.distance / 120;
                this.distance = this.distance % 120;
                if (num2 != 0)
                {
                    this.OnScrollLine(e, num2, false);
                }
            }
        }

        private void OnScrollLine(MouseWheelEventArgs e, int linesCount, bool allowSystemLinesCount)
        {
            if (this.client != null)
            {
                if (e is MouseWheelEventArgsEx)
                {
                    this.client.OnMouseWheel(new MouseWheelEventArgsEx(e.MouseDevice, e.Timestamp, linesCount * 120, 0));
                }
                else
                {
                    this.client.OnMouseWheel(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, linesCount * 120));
                }
            }
        }

        private void ResetDistance()
        {
            this.distance = 0;
        }

        public bool UsePixelScrolling { get; private set; }
    }
}

