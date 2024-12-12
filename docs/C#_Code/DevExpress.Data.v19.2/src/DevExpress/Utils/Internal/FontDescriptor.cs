namespace DevExpress.Utils.Internal
{
    using System;
    using System.Drawing;
    using System.IO;

    public class FontDescriptor
    {
        private TTFontInfo fontInfo;
        private readonly System.Drawing.FontFamily fontFamily;
        private readonly object fontSource;
        private readonly Stream fontStream;
        private readonly System.Drawing.FontStyle fontStyle;
        private static readonly object locker = new object();

        public FontDescriptor(System.Drawing.FontFamily fontFamily, System.Drawing.FontStyle fontStyle, Stream fontStream, object fontSource)
        {
            this.fontFamily = fontFamily;
            this.fontStyle = fontStyle;
            this.fontSource = fontSource;
            this.fontStream = fontStream;
        }

        public System.Drawing.FontFamily FontFamily =>
            this.fontFamily;

        public TTFontInfo FontInfo
        {
            get
            {
                if (this.fontInfo == null)
                {
                    object locker = FontDescriptor.locker;
                    lock (locker)
                    {
                        if (this.fontInfo == null)
                        {
                            this.FontStream.Position = 0L;
                            try
                            {
                                this.fontInfo = new TTFontInfo(this.FontStream);
                            }
                            catch (NotSupportedException exception)
                            {
                                if (this.fontFamily != null)
                                {
                                    throw new NotSupportedException("Unable to load TTFontInfo, fontFamily.Name=" + this.fontFamily.Name, exception);
                                }
                            }
                        }
                    }
                }
                return this.fontInfo;
            }
        }

        public object FontSource =>
            this.fontSource;

        public Stream FontStream =>
            this.fontStream;

        public System.Drawing.FontStyle FontStyle =>
            this.fontStyle;
    }
}

