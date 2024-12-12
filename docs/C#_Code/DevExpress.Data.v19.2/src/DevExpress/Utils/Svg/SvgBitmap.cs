namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Filtering.Internal;
    using DevExpress.Utils.Helpers;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SvgBitmap
    {
        private static string EmptySvgImagePath = "Data.Utils.Svg.Resources.EmptySvgImage.svg";
        private static int DefaultLenght = 0x600;
        private static int AdditionalLenght = 100;
        private PointF drawingOffset;
        private DevExpress.Utils.Svg.SvgImage svgImageCore;
        private ISvgElementWrapperFactory wrapperFactory;
        [ThreadStatic]
        internal static Image bufferedImage;
        private SvgRenderedImageCache renderedImageCache;
        private IEnumerable<SvgElementWrapper> rootElements;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static object DisabledTag = new object();
        private Type[] indirectElementTypes = new Type[] { typeof(SvgDefinitionsWrapper), typeof(SvgClipPathWrapper) };
        [ThreadStatic]
        private static ImageAttributes transparentAttributes;

        public SvgBitmap(DevExpress.Utils.Svg.SvgImage svgImage)
        {
            this.Elements = new List<SvgElementWrapper>(svgImage.Elements.Count);
            this.svgImageCore = svgImage;
            if ((svgImage.Width < 16.0) && (svgImage.Height < 16.0))
            {
                this.Scale = ((int) (512.0 / Math.Max(svgImage.Width, svgImage.Height))) + 1;
            }
            else if ((svgImage.Width < 32.0) && (svgImage.Height < 32.0))
            {
                this.Scale = 32.0;
            }
            else if ((svgImage.Width <= 96.0) && (svgImage.Height <= 96.0))
            {
                this.Scale = 16.0;
            }
            else
            {
                this.Scale = (int) (((double) DefaultLenght) / Math.Max(svgImage.Width, svgImage.Height));
                if (this.Scale > 2.0)
                {
                    this.Scale -= this.Scale % 2.0;
                }
                if (this.Scale < 14.0)
                {
                    this.Scale = 1.0;
                }
            }
            this.DrawingOffset = new PointF((float) (-this.SvgImage.OffsetX * this.Scale), (float) (-this.SvgImage.OffsetY * this.Scale));
            this.wrapperFactory = this.CreateFactory();
            this.rootElements = this.WrapElements(this.SvgImage.Elements).ToArray<SvgElementWrapper>();
            this.renderedImageCache = new SvgRenderedImageCache();
            svgImage.Owner = this;
        }

        public virtual void AddToCache(Size imageSize, ISvgPaletteProvider paletteProvider, Image result)
        {
            if (this.RenderedImageCache.Count > 5)
            {
                this.RenderedImageCache.Remove(this.RenderedImageCache.Keys.First<SvgImageKey>());
            }
            Func<ISvgPaletteProvider, int?> get = <>c.<>9__42_0;
            if (<>c.<>9__42_0 == null)
            {
                Func<ISvgPaletteProvider, int?> local1 = <>c.<>9__42_0;
                get = <>c.<>9__42_0 = x => new int?(x.GetHashCode());
            }
            int? defaultValue = null;
            this.RenderedImageCache.Add(new SvgImageKey(string.Empty, imageSize, paletteProvider.Get<ISvgPaletteProvider, int?>(get, defaultValue)), result);
        }

        public void ClearCache(bool disposeImages)
        {
            Image[] imageArray = this.RenderedImageCache.Values.ToArray<Image>();
            this.RenderedImageCache.Clear();
            if (disposeImages)
            {
                foreach (Image image in imageArray)
                {
                    image.Dispose();
                }
            }
        }

        public static SvgBitmap Create(DevExpress.Utils.Svg.SvgImage svgImage) => 
            (svgImage.Owner as SvgBitmap) ?? new SvgBitmap(svgImage);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static SvgBitmap Create(Size size)
        {
            SvgBitmap bitmap = null;
            try
            {
                Stream stream = AssemblyHelper.GetEmbeddedResourceStream(Assembly.GetAssembly(typeof(DevExpress.Utils.Svg.SvgImage)), EmptySvgImagePath, true);
                bitmap = FromStream(stream);
                bitmap.SvgImage.Root.Background.Height = size.Height;
                bitmap.SvgImage.Root.Background.Width = size.Width;
                bitmap.SvgImage.Root.ViewBox.Height = size.Height;
                bitmap.SvgImage.Root.ViewBox.Width = size.Width;
                stream.Close();
            }
            catch
            {
            }
            return bitmap;
        }

        protected virtual ISvgElementWrapperFactory CreateFactory() => 
            SvgElementWrapperFactory.Default;

        public static SvgBitmap FromFile(string path) => 
            SvgLoader.LoadSvgBitmapFromFile(path);

        public static SvgBitmap FromStream(Stream stream) => 
            SvgLoader.LoadSvgBitmapFromStream(stream);

        public RectangleF GetBounds(bool withViewboxTransform = true)
        {
            Matrix transformMatrix = withViewboxTransform ? this.SvgImage.GetViewBoxTransform() : null;
            List<float> list = new List<float>();
            List<float> list2 = new List<float>();
            foreach (SvgElementWrapper element in this.rootElements)
            {
                if (!this.indirectElementTypes.Any<Type>(x => x.IsAssignableFrom(element.GetType())))
                {
                    RectangleF bounds = element.GetBounds(transformMatrix);
                    if (!bounds.IsEmpty)
                    {
                        float[] collection = new float[] { bounds.X, bounds.Right };
                        list.AddRange(collection);
                        float[] singleArray2 = new float[] { bounds.Y, bounds.Bottom };
                        list2.AddRange(singleArray2);
                    }
                }
            }
            if (transformMatrix != null)
            {
                transformMatrix.Dispose();
            }
            if ((list.Count == 0) || (list2.Count == 0))
            {
                return RectangleF.Empty;
            }
            float num = ((IEnumerable<float>) list).Min();
            float y = ((IEnumerable<float>) list2).Min();
            return new RectangleF(num, y, ((IEnumerable<float>) list).Max() - num, ((IEnumerable<float>) list2).Max() - y);
        }

        protected internal SvgElementWrapper GetElementWrapperById(string id)
        {
            if (id.StartsWith("url("))
            {
                id = id.Substring(4);
                char[] trimChars = new char[] { ')' };
                id = id.TrimEnd(trimChars);
            }
            if (id.StartsWith("#"))
            {
                id = id.Substring(1);
            }
            return this.Elements.FirstOrDefault<SvgElementWrapper>(x => string.Equals(x.Element.Id, id));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool HitTest(PointF point, double outlineSize)
        {
            Matrix viewBoxTransform = this.SvgImage.GetViewBoxTransform();
            bool flag = false;
            foreach (SvgElementWrapper wrapper in this.rootElements)
            {
                flag |= this.HitTestCore(wrapper, point, viewBoxTransform, outlineSize);
            }
            return flag;
        }

        private bool HitTestCore(SvgElementWrapper item, PointF point, Matrix matrix, double outlineSize)
        {
            bool flag2;
            Pen hitTestPen = item.GetHitTestPen(1.0, outlineSize);
            Pen pen = item.GetPen(1.0);
            GraphicsPath path = item.GetPath(1.0).Clone() as GraphicsPath;
            GraphicsPath path2 = null;
            Region region = null;
            try
            {
                if ((pen != null) && item.CheckPathData(path))
                {
                    path2 = (GraphicsPath) path.Clone();
                    path2.Widen(pen, null, 0.01f);
                }
                Matrix matrix2 = null;
                if (item is SvgTransformGroupWrapper)
                {
                    Matrix matrix3 = item.GetTransform(new Matrix(), 1.0, true);
                    matrix3.Multiply(matrix);
                    matrix2 = matrix3;
                }
                else
                {
                    matrix2 = item.GetTransform(matrix, 1.0, true);
                }
                path.Transform(matrix2);
                region = new Region(path);
                if (path2 != null)
                {
                    path2.Transform(matrix2);
                    region.Union(path2);
                    path = path2;
                }
                bool flag = (!region.IsVisible(point) || (string.Equals(item.Element.Fill, "none", StringComparison.OrdinalIgnoreCase) && ((path2 == null) || !path2.IsVisible(point)))) ? path.IsOutlineVisible(point, hitTestPen) : true;
                if (flag)
                {
                    flag2 = flag;
                }
                else
                {
                    foreach (SvgElementWrapper wrapper in item.Childs)
                    {
                        flag |= this.HitTestCore(wrapper, point, matrix2, outlineSize);
                    }
                    flag2 = flag;
                }
            }
            finally
            {
                if (path != null)
                {
                    path.Dispose();
                }
                if (path2 != null)
                {
                    path2.Dispose();
                }
                if (region != null)
                {
                    region.Dispose();
                }
                if (hitTestPen != null)
                {
                    hitTestPen.Dispose();
                }
                if (pen != null)
                {
                    pen.Dispose();
                }
            }
            return flag2;
        }

        private bool IsHighSpeedRender(double scale, DefaultBoolean useHighSpeedRendering) => 
            (useHighSpeedRendering != DefaultBoolean.Default) ? ((Environment.OSVersion.Version.Major < 6) || (useHighSpeedRendering == DefaultBoolean.True)) : ((scale == 1.0) || ((SvgImageRenderingMode == DevExpress.Utils.Svg.SvgImageRenderingMode.HighSpeed) || (Environment.OSVersion.Version.Major < 6)));

        public Image Render(ISvgPaletteProvider paletteProvider, double scaleFactor, DefaultBoolean useHighSpeedRendering = 2, DefaultBoolean allowCache = 2)
        {
            Size imageSize = new Size((int) (this.SvgImage.Width * scaleFactor), (int) (this.SvgImage.Height * scaleFactor));
            return this.Render(imageSize, paletteProvider, useHighSpeedRendering, allowCache);
        }

        public Image Render(Size imageSize, ISvgPaletteProvider paletteProvider, DefaultBoolean useHighSpeedRendering = 2, DefaultBoolean allowCache = 2)
        {
            Image image = null;
            Func<ISvgPaletteProvider, int?> get = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Func<ISvgPaletteProvider, int?> local1 = <>c.<>9__39_0;
                get = <>c.<>9__39_0 = x => new int?(x.GetHashCode());
            }
            int? defaultValue = null;
            SvgImageKey key = new SvgImageKey(string.Empty, imageSize, paletteProvider.Get<ISvgPaletteProvider, int?>(get, defaultValue));
            if (this.renderedImageCache.TryGetValue(key, out image))
            {
                if (!ImageGuard.IsDisposedOrInvalid(image))
                {
                    return image;
                }
                this.renderedImageCache.Remove(key);
            }
            bufferedImage ??= new Bitmap(DefaultLenght, DefaultLenght, PixelFormat.Format32bppArgb);
            image = this.RenderCore(imageSize, paletteProvider, bufferedImage, this.Scale, useHighSpeedRendering);
            if (allowCache != DefaultBoolean.False)
            {
                this.AddToCache(imageSize, paletteProvider, image);
            }
            return image;
        }

        protected internal Image RenderCore(Size imageSize, ISvgPaletteProvider paletteProvider, Image bufferedImage, double scale, DefaultBoolean useHighSpeedRendering)
        {
            Image image = null;
            DevExpress.Utils.Svg.SvgImageRenderingMode svgImageRenderingMode = SvgImageRenderingMode;
            using (Graphics graphics = Graphics.FromImage(bufferedImage))
            {
                if (this.IsHighSpeedRender(scale, useHighSpeedRendering))
                {
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.TextContrast = 1;
                    SvgImageRenderingMode = DevExpress.Utils.Svg.SvgImageRenderingMode.HighSpeed;
                    double num = Math.Max((double) (((double) imageSize.Width) / this.SvgImage.Width), (double) (((double) imageSize.Height) / this.SvgImage.Height));
                    if ((this.SvgImage.Width > DefaultLenght) || ((this.SvgImage.Height > DefaultLenght) || (num > 0.0715)))
                    {
                        scale = num;
                    }
                }
                graphics.SetClip(new Rectangle(Point.Empty, new Size(((int) (this.SvgImage.Width * scale)) + AdditionalLenght, ((int) (this.SvgImage.Height * scale)) + AdditionalLenght)));
                graphics.Clear(Color.Transparent);
                using (SvgGraphics graphics2 = new SvgGraphics(graphics))
                {
                    foreach (SvgElementWrapper wrapper in this.rootElements)
                    {
                        wrapper.Render(graphics2, paletteProvider, scale);
                    }
                }
                Func<ISvgPaletteProvider, double> get = <>c.<>9__40_0;
                if (<>c.<>9__40_0 == null)
                {
                    Func<ISvgPaletteProvider, double> local1 = <>c.<>9__40_0;
                    get = <>c.<>9__40_0 = x => x.Opacity;
                }
                image = this.ScaleImg(imageSize, new Size((int) Math.Ceiling((double) (this.SvgImage.Width * scale)), (int) Math.Ceiling((double) (this.SvgImage.Height * scale))), bufferedImage, paletteProvider.Get<ISvgPaletteProvider, double>(get, 1.0));
                if (paletteProvider is ISvgPaletteProviderExt)
                {
                    image.Tag = (paletteProvider as ISvgPaletteProviderExt).Disabled ? DisabledTag : null;
                }
            }
            SvgImageRenderingMode = svgImageRenderingMode;
            return image;
        }

        public void RenderToGraphics(Graphics g, ISvgPaletteProvider paletteProvider = null, double scaleFactor = 1.0, DefaultBoolean useHighSpeedRendering = 2)
        {
            GraphicsState gstate = g.Save();
            DevExpress.Utils.Svg.SvgImageRenderingMode svgImageRenderingMode = SvgImageRenderingMode;
            try
            {
                if (useHighSpeedRendering == DefaultBoolean.True)
                {
                    SvgImageRenderingMode = DevExpress.Utils.Svg.SvgImageRenderingMode.HighSpeed;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.TextContrast = 1;
                }
                g.CompositingQuality = CompositingQuality.HighSpeed;
                using (SvgGraphics graphics = new SvgGraphics(g))
                {
                    foreach (SvgElementWrapper wrapper in this.rootElements)
                    {
                        wrapper.Render(graphics, paletteProvider, scaleFactor);
                    }
                }
            }
            finally
            {
                SvgImageRenderingMode = svgImageRenderingMode;
                g.Restore(gstate);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void RenderToGraphicsBase(IGraphicsBase g, ISvgPaletteProvider paletteProvider = null, double scaleFactor = 1.0)
        {
            IGraphicsState gstate = g.Save();
            try
            {
                using (SvgGraphicsBase base2 = new SvgGraphicsBase(g))
                {
                    foreach (SvgElementWrapper wrapper in this.rootElements)
                    {
                        wrapper.Render(base2, paletteProvider, scaleFactor);
                    }
                }
            }
            finally
            {
                g.Restore(gstate);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRenderedImage(bool disposeImage)
        {
            if (this.RenderedImageCache.Count != 0)
            {
                Image[] array = new Image[this.RenderedImageCache.Count<KeyValuePair<SvgImageKey, Image>>()];
                this.RenderedImageCache.Values.CopyTo(array, 0);
                this.RenderedImageCache.Clear();
                if (disposeImage)
                {
                    foreach (Image image in array)
                    {
                        image.Dispose();
                    }
                }
            }
        }

        public void Save(string path)
        {
            SvgSerializer.SaveSvgImageToXML(path, this.SvgImage);
        }

        private Image ScaleImg(Size outputSize, Size srcSize, Image bufferedImage, double opacity)
        {
            outputSize.Width = (outputSize.Width > 0) ? outputSize.Width : 1;
            outputSize.Height = (outputSize.Height > 0) ? outputSize.Height : 1;
            Bitmap image = new Bitmap(outputSize.Width, outputSize.Height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
                Rectangle srcRect = new Rectangle(Point.Empty, srcSize);
                if (opacity != 1.0)
                {
                    ColorMatrix newColorMatrix = new ColorMatrix {
                        Matrix33 = (float) opacity
                    };
                    transparentAttributes ??= new ImageAttributes();
                    transparentAttributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    graphics.DrawImage(bufferedImage, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, transparentAttributes);
                }
                else
                {
                    graphics.DrawImage(bufferedImage, destRect, srcRect, GraphicsUnit.Pixel);
                    return image;
                }
            }
            return image;
        }

        [IteratorStateMachine(typeof(<WrapElements>d__30))]
        protected virtual IEnumerable<SvgElementWrapper> WrapElements(IList<SvgElement> elements)
        {
            <WrapElements>d__30 d__1 = new <WrapElements>d__30(-2);
            d__1.<>4__this = this;
            d__1.<>3__elements = elements;
            return d__1;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static DevExpress.Utils.Svg.SvgImageRenderingMode SvgImageRenderingMode { get; set; }

        protected SvgRenderedImageCache RenderedImageCache =>
            this.renderedImageCache;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<SvgElementWrapper> Elements { get; set; }

        public double Scale { get; set; }

        public DevExpress.Utils.Svg.SvgImage SvgImage =>
            this.svgImageCore;

        public PointF DrawingOffset
        {
            get => 
                this.drawingOffset;
            set => 
                this.drawingOffset = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgBitmap.<>c <>9 = new SvgBitmap.<>c();
            public static Func<ISvgPaletteProvider, int?> <>9__39_0;
            public static Func<ISvgPaletteProvider, double> <>9__40_0;
            public static Func<ISvgPaletteProvider, int?> <>9__42_0;

            internal int? <AddToCache>b__42_0(ISvgPaletteProvider x) => 
                new int?(x.GetHashCode());

            internal int? <Render>b__39_0(ISvgPaletteProvider x) => 
                new int?(x.GetHashCode());

            internal double <RenderCore>b__40_0(ISvgPaletteProvider x) => 
                x.Opacity;
        }

        [CompilerGenerated]
        private sealed class <WrapElements>d__30 : IEnumerable<SvgElementWrapper>, IEnumerable, IEnumerator<SvgElementWrapper>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private SvgElementWrapper <>2__current;
            private int <>l__initialThreadId;
            private IList<SvgElement> elements;
            public IList<SvgElement> <>3__elements;
            public SvgBitmap <>4__this;
            private IEnumerator<SvgElement> <>7__wrap1;

            [DebuggerHidden]
            public <WrapElements>d__30(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.elements.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            SvgBitmap.<>c__DisplayClass30_0 class_2;
                            SvgBitmap <>4__this = this.<>4__this;
                            SvgElement element = this.<>7__wrap1.Current;
                            SvgElementWrapper elementWrapper = this.<>4__this.wrapperFactory.Wrap(element);
                            elementWrapper.Do<SvgElementWrapper>(new Action<SvgElementWrapper>(class_2.<WrapElements>b__0));
                            if (elementWrapper == null)
                            {
                                continue;
                            }
                            this.<>2__current = elementWrapper;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<SvgElementWrapper> IEnumerable<SvgElementWrapper>.GetEnumerator()
            {
                SvgBitmap.<WrapElements>d__30 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new SvgBitmap.<WrapElements>d__30(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.elements = this.<>3__elements;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Svg.SvgElementWrapper>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            SvgElementWrapper IEnumerator<SvgElementWrapper>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

