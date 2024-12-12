namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class MetafileCreator
    {
        public static Metafile CreateInstance(Stream stream)
        {
            using (Graphics graphics = GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                Metafile metafile = new Metafile(stream, hdc);
                graphics.ReleaseHdc(hdc);
                return metafile;
            }
        }

        public static Metafile CreateInstance(RectangleF frameRect, MetafileFrameUnit frameUnit)
        {
            using (Graphics graphics = GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                Metafile metafile = new Metafile(hdc, frameRect, frameUnit);
                graphics.ReleaseHdc(hdc);
                return metafile;
            }
        }

        public static Metafile CreateInstance(Stream stream, EmfType type)
        {
            using (Graphics graphics = GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                Metafile metafile = new Metafile(stream, hdc, type);
                graphics.ReleaseHdc(hdc);
                return metafile;
            }
        }

        public static Metafile CreateInstance(RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type) => 
            CreateInstance(frameRect, frameUnit, type, false);

        public static Metafile CreateInstance(RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, bool createGraphicsFromImage)
        {
            using (Graphics graphics = createGraphicsFromImage ? GraphicsHelper.CreateGraphicsFromImage() : GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                Metafile metafile = new Metafile(hdc, frameRect, frameUnit, type);
                graphics.ReleaseHdc(hdc);
                return metafile;
            }
        }

        public static Metafile CreateInstance(Stream stream, int width, int height, MetafileFrameUnit frameUnit)
        {
            using (Graphics graphics = GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                Metafile metafile = new Metafile(stream, hdc, new Rectangle(0, 0, width, height), frameUnit);
                graphics.ReleaseHdc(hdc);
                return metafile;
            }
        }

        public static Metafile CreateInstance(Stream stream, int width, int height, MetafileFrameUnit frameUnit, EmfType type) => 
            CreateInstance(stream, width, height, frameUnit, type, false);

        public static Metafile CreateInstance(Stream stream, int width, int height, MetafileFrameUnit frameUnit, EmfType type, bool createGraphicsFromImage)
        {
            using (Graphics graphics = createGraphicsFromImage ? GraphicsHelper.CreateGraphicsFromImage() : GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                Metafile metafile = new Metafile(stream, hdc, new Rectangle(0, 0, width, height), frameUnit, type);
                graphics.ReleaseHdc(hdc);
                return metafile;
            }
        }
    }
}

