namespace ActiproSoftware.WinUICore.Rendering
{
    using #H;
    using #xOk;
    using ActiproSoftware.ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class CanvasDrawContext : DisposableObject
    {
        private Stack<Rectangle> #Isk;
        private Stack<Rectangle> #Jsk;
        private #xOk.#zOk #zOk;

        private static unsafe Rectangle #BOk(Rectangle #Bo, float #COk)
        {
            if (#COk > 1f)
            {
                int num = (int) (#COk / 2f);
                Rectangle* rectanglePtr1 = &#Bo;
                rectanglePtr1.X += num;
                Rectangle* rectanglePtr2 = &#Bo;
                rectanglePtr2.Y += num;
                #Bo.Width = Math.Max(1, #Bo.Width - num);
                #Bo.Height = Math.Max(1, #Bo.Height - num);
            }
            return #Bo;
        }

        internal static DashStyle #Djc(LineKind #Msk)
        {
            switch (#Msk)
            {
                case LineKind.Dot:
                    return DashStyle.Dot;

                case LineKind.Dash:
                    return DashStyle.Dash;

                case LineKind.DashDot:
                    return DashStyle.DashDot;
            }
            return DashStyle.Solid;
        }

        private static Color #DOk(Brush #hwf)
        {
            if (#hwf != null)
            {
                SolidBrush brush = #hwf as SolidBrush;
                if (brush != null)
                {
                    return brush.Color;
                }
                LinearGradientBrush brush2 = #hwf as LinearGradientBrush;
                if ((brush2 != null) && (brush2.LinearColors.Length != 0))
                {
                    return brush2.LinearColors[0];
                }
            }
            return Color.Black;
        }

        internal static Pen #Z6e(Brush #hwf, LineKind #Msk, float #Nsk)
        {
            Pen pen = new Pen(#hwf, #Nsk);
            if (#Msk != LineKind.Solid)
            {
                pen.DashStyle = #Djc(#Msk);
            }
            return pen;
        }

        private Pen #Z6e(Color #eUb, LineKind #Msk, float #Nsk)
        {
            if (#Msk == LineKind.None)
            {
                #eUb = Color.Transparent;
            }
            return #Z6e(this.Canvas.GetSolidColorBrush(#eUb), #Msk, #Nsk);
        }

        public CanvasDrawContext(ICanvas canvas, Graphics platformRenderer, Rectangle bounds)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(#G.#eg(0x60b));
            }
            if (platformRenderer == null)
            {
                throw new ArgumentNullException(#G.#eg(0x614));
            }
            this.Canvas = canvas;
            this.PlatformRenderer = platformRenderer;
            this.Bounds = bounds;
            this.ClipBounds = bounds;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.IsNativeRendering = false;
            }
            base.Dispose(disposing);
        }

        public void DrawEllipse(Rectangle bounds, Pen pen)
        {
            if (pen != null)
            {
                Color color = pen.Color;
                bool local1 = (color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.DrawEllipse(bounds, color, pen.DashStyle, pen.Width);
                }
                else
                {
                    bounds = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
                    this.PlatformRenderer.DrawEllipse(pen, bounds);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void DrawEllipse(Rectangle bounds, Color color, LineKind lineKind, float strokeWidth)
        {
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.DrawEllipse(bounds, color, #Djc(lineKind), strokeWidth);
            }
            else
            {
                using (Pen pen = this.#Z6e(color, lineKind, strokeWidth))
                {
                    bounds = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
                    this.PlatformRenderer.DrawEllipse(pen, bounds);
                }
            }
            if (flag)
            {
                this.IsNativeRendering = true;
            }
        }

        public void DrawGeometry(Point location, GraphicsPath geometry, Pen pen)
        {
            if (pen != null)
            {
                Color color = pen.Color;
                bool local1 = (color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.DrawGeometry(location, geometry, color, pen.DashStyle, pen.Width);
                }
                else
                {
                    this.PlatformRenderer.TranslateTransform((float) location.X, (float) location.Y);
                    this.PlatformRenderer.DrawPath(pen, geometry);
                    this.PlatformRenderer.TranslateTransform((float) -location.X, (float) -location.Y);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void DrawGeometry(Point location, GraphicsPath geometry, Color color, LineKind lineKind, float strokeWidth)
        {
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.DrawGeometry(location, geometry, color, #Djc(lineKind), strokeWidth);
            }
            else
            {
                using (Pen pen = this.#Z6e(color, lineKind, strokeWidth))
                {
                    this.PlatformRenderer.TranslateTransform((float) location.X, (float) location.Y);
                    this.PlatformRenderer.DrawPath(pen, geometry);
                    this.PlatformRenderer.TranslateTransform((float) -location.X, (float) -location.Y);
                }
            }
            if (flag)
            {
                this.IsNativeRendering = true;
            }
        }

        public void DrawImage(Point location, Image imageSource)
        {
            if (this.IsNativeRendering)
            {
                this.#zOk.DrawImage(location, imageSource);
            }
            else
            {
                this.PlatformRenderer.DrawImage(imageSource, location);
            }
        }

        public void DrawLine(Point startLocation, Point endLocation, Pen pen)
        {
            if (pen != null)
            {
                Color color = pen.Color;
                bool flag = (color.A < 0xff) && this.IsNativeRendering;
                if (flag)
                {
                    this.IsNativeRendering = false;
                }
                if (!this.IsNativeRendering)
                {
                    this.PlatformRenderer.DrawLine(pen, startLocation, endLocation);
                }
                else
                {
                    if (startLocation.X == endLocation.X)
                    {
                        if (startLocation.Y < endLocation.Y)
                        {
                            this.#zOk.DrawLine(startLocation.X, startLocation.Y, endLocation.X, endLocation.Y + 1, color, pen.DashStyle, pen.Width);
                            return;
                        }
                    }
                    else if ((startLocation.Y == endLocation.Y) && (startLocation.X < endLocation.X))
                    {
                        this.#zOk.DrawLine(startLocation.X, startLocation.Y, endLocation.X + 1, endLocation.Y, color, pen.DashStyle, pen.Width);
                        return;
                    }
                    this.#zOk.DrawLine(startLocation.X, startLocation.Y, endLocation.X, endLocation.Y, color, pen.DashStyle, pen.Width);
                }
                if (flag)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void DrawLine(Point startLocation, Point endLocation, Color color, LineKind lineKind, float strokeWidth)
        {
            using (Pen pen = this.#Z6e(color, lineKind, strokeWidth))
            {
                this.DrawLine(startLocation, endLocation, pen);
            }
        }

        public void DrawRectangle(Rectangle bounds, Pen pen)
        {
            if (pen != null)
            {
                bounds = #BOk(bounds, pen.Width);
                Color color = pen.Color;
                bool local1 = (color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.DrawRectangle(bounds, color, pen.DashStyle, pen.Width);
                }
                else
                {
                    bounds = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
                    this.PlatformRenderer.DrawRectangle(pen, bounds);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void DrawRectangle(Rectangle bounds, Color color, LineKind lineKind, float strokeWidth)
        {
            bounds = #BOk(bounds, strokeWidth);
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.DrawRectangle(bounds, color, #Djc(lineKind), strokeWidth);
            }
            else
            {
                using (Pen pen = this.#Z6e(color, lineKind, strokeWidth))
                {
                    bounds = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
                    this.PlatformRenderer.DrawRectangle(pen, bounds);
                }
            }
            if (flag)
            {
                this.IsNativeRendering = true;
            }
        }

        public void DrawRoundedRectangle(Rectangle bounds, float radius, Pen pen)
        {
            if (pen != null)
            {
                bounds = #BOk(bounds, pen.Width);
                bool local1 = (pen.Color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.DrawRoundedRectangle(bounds, radius, pen.Color, pen.DashStyle, pen.Width);
                }
                else
                {
                    bounds = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
                    this.PlatformRenderer.DrawRectangle(pen, bounds);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void DrawRoundedRectangle(Rectangle bounds, float radius, Color color, LineKind lineKind, float strokeWidth)
        {
            bounds = #BOk(bounds, strokeWidth);
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.DrawRoundedRectangle(bounds, radius, color, #Djc(lineKind), strokeWidth);
            }
            else
            {
                using (Pen pen = this.#Z6e(color, lineKind, strokeWidth))
                {
                    bounds = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
                    this.PlatformRenderer.DrawRectangle(pen, bounds);
                }
            }
            if (flag)
            {
                this.IsNativeRendering = true;
            }
        }

        public void DrawSquiggleLine(Rectangle bounds, Color color)
        {
            // Invalid method body.
        }

        public void DrawText(Point location, ITextLayoutLine line)
        {
            #MSk sk = line as #MSk;
            if (sk != null)
            {
                sk.Draw(this, location);
            }
        }

        public void FillEllipse(Rectangle bounds, Brush brush)
        {
            if (brush != null)
            {
                Color color = #DOk(brush);
                bool local1 = (color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.FillEllipse(bounds, color);
                }
                else
                {
                    this.PlatformRenderer.FillEllipse(brush, bounds);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillEllipse(Rectangle bounds, Color color)
        {
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.FillEllipse(bounds, color);
            }
            else
            {
                SolidBrush solidColorBrush = this.Canvas.GetSolidColorBrush(color);
                this.FillEllipse(bounds, solidColorBrush);
                if (flag)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillGeometry(Point location, GraphicsPath geometry, Brush brush)
        {
            if (brush != null)
            {
                Color color = #DOk(brush);
                bool local1 = (color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.FillGeometry(location, geometry, color);
                }
                else
                {
                    this.PlatformRenderer.TranslateTransform((float) location.X, (float) location.Y);
                    this.PlatformRenderer.FillPath(brush, geometry);
                    this.PlatformRenderer.TranslateTransform((float) -location.X, (float) -location.Y);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillGeometry(Point location, GraphicsPath geometry, Color color)
        {
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.FillGeometry(location, geometry, color);
            }
            else
            {
                SolidBrush solidColorBrush = this.Canvas.GetSolidColorBrush(color);
                this.FillGeometry(location, geometry, solidColorBrush);
                if (flag)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillRectangle(Rectangle bounds, Brush brush)
        {
            if (brush != null)
            {
                Color color = #DOk(brush);
                bool local1 = (color.A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.FillRectangle(bounds, color);
                }
                else
                {
                    this.PlatformRenderer.FillRectangle(brush, bounds);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillRectangle(Rectangle bounds, Color color)
        {
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.FillRectangle(bounds, color);
            }
            else
            {
                SolidBrush solidColorBrush = this.Canvas.GetSolidColorBrush(color);
                this.FillRectangle(bounds, solidColorBrush);
                if (flag)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillRoundedRectangle(Rectangle bounds, float radius, Brush brush)
        {
            if (brush != null)
            {
                bool local1 = (#DOk(brush).A < 0xff) && this.IsNativeRendering;
                if (local1)
                {
                    this.IsNativeRendering = false;
                }
                if (this.IsNativeRendering)
                {
                    this.#zOk.FillRoundedRectangle(bounds, radius, #DOk(brush));
                }
                else
                {
                    this.PlatformRenderer.FillRectangle(brush, bounds);
                }
                if (local1)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void FillRoundedRectangle(Rectangle bounds, float radius, Color color)
        {
            bool flag = (color.A < 0xff) && this.IsNativeRendering;
            if (flag)
            {
                this.IsNativeRendering = false;
            }
            if (this.IsNativeRendering)
            {
                this.#zOk.FillRoundedRectangle(bounds, radius, color);
            }
            else
            {
                SolidBrush solidColorBrush = this.Canvas.GetSolidColorBrush(color);
                this.FillRoundedRectangle(bounds, radius, solidColorBrush);
                if (flag)
                {
                    this.IsNativeRendering = true;
                }
            }
        }

        public void PopBounds()
        {
            if ((this.#Isk != null) && (this.#Isk.Count > 0))
            {
                this.Bounds = this.#Isk.Pop();
            }
        }

        public void PopClip()
        {
            if ((this.#Jsk != null) && (this.#Jsk.Count > 0))
            {
                this.ClipBounds = this.#Jsk.Pop();
                if (this.IsNativeRendering)
                {
                    this.#zOk.#ERk(this.ClipBounds);
                }
                else
                {
                    this.PlatformRenderer.SetClip(this.ClipBounds);
                }
            }
        }

        public void PushBounds(Rectangle bounds)
        {
            if (this.#Isk == null)
            {
                this.#Isk = new Stack<Rectangle>(2);
            }
            this.#Isk.Push(this.Bounds);
            this.Bounds = bounds;
        }

        public void PushClip(Rectangle clipBounds)
        {
            if (this.#Jsk == null)
            {
                this.#Jsk = new Stack<Rectangle>(2);
            }
            this.#Jsk.Push(this.ClipBounds);
            if (this.IsNativeRendering)
            {
                this.#zOk.#ERk(clipBounds);
            }
            else
            {
                this.PlatformRenderer.SetClip(clipBounds);
            }
            this.ClipBounds = clipBounds;
        }

        public Rectangle Bounds { get; private set; }

        public ICanvas Canvas { get; private set; }

        public Rectangle ClipBounds { get; private set; }

        public float DpiScale { get; internal set; }

        public Graphics PlatformRenderer { get; private set; }

        public double Scale =>
            1.0;

        internal bool IsForPrinter { get; set; }

        internal #xOk.#zOk NativeRenderer =>
            this.#zOk;

        public bool IsNativeRendering
        {
            get => 
                this.#zOk != null;
            set
            {
                bool flag = false;
                if (value)
                {
                    if (this.#zOk == null)
                    {
                        this.#zOk = new #xOk.#zOk(this.PlatformRenderer, this.IsForPrinter);
                        flag = true;
                    }
                }
                else if (this.#zOk != null)
                {
                    this.#zOk.Dispose();
                    this.#zOk = null;
                    flag = true;
                }
                if (flag && !this.ClipBounds.IsEmpty)
                {
                    if (this.IsNativeRendering)
                    {
                        this.#zOk.#ERk(this.ClipBounds);
                    }
                    else
                    {
                        this.PlatformRenderer.SetClip(this.ClipBounds);
                    }
                }
            }
        }
    }
}

