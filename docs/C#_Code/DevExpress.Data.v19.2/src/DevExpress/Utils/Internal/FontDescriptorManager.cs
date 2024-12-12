namespace DevExpress.Utils.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public class FontDescriptorManager
    {
        private readonly FontFamily fontFamily;
        private readonly Stream normalFontStream;
        private readonly Stream boldFontStream;
        private readonly Stream italicFontStream;
        private readonly Stream boldItalicFontStream;
        private readonly FontDescriptor[] fontDescriptors;
        private readonly bool isPredefined;

        public FontDescriptorManager(string fontFamilyName, Stream normalFontStream, Stream boldFontStream, Stream italicFontStream, Stream boldItalicFontStream, bool isPredefined)
        {
            Guard.ArgumentNotNull(normalFontStream, "normalFontStream");
            this.fontFamily = new FontFamily(fontFamilyName);
            this.fontDescriptors = new FontDescriptor[4];
            this.normalFontStream = normalFontStream;
            this.boldFontStream = boldFontStream;
            this.italicFontStream = italicFontStream;
            this.boldItalicFontStream = boldItalicFontStream;
            this.isPredefined = isPredefined;
        }

        private FontDescriptor CreateFontDescriptor(FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case FontStyle.Regular:
                    return this.CreateFontDescriptorCore(FontStyle.Regular, this.normalFontStream);

                case FontStyle.Bold:
                    return ((this.boldFontStream == null) ? this.GetFontDescriptor(FontStyle.Regular) : this.CreateFontDescriptorCore(FontStyle.Bold, this.boldFontStream));

                case FontStyle.Italic:
                    return ((this.italicFontStream == null) ? this.GetFontDescriptor(FontStyle.Regular) : this.CreateFontDescriptorCore(FontStyle.Italic, this.italicFontStream));

                case (FontStyle.Italic | FontStyle.Bold):
                    return ((this.boldItalicFontStream == null) ? ((this.boldFontStream == null) ? ((this.italicFontStream == null) ? this.GetFontDescriptor(FontStyle.Regular) : this.GetFontDescriptor(FontStyle.Italic)) : this.GetFontDescriptor(FontStyle.Bold)) : this.CreateFontDescriptorCore(FontStyle.Italic | FontStyle.Bold, this.boldItalicFontStream));
            }
            throw new ArgumentException("fontStyle");
        }

        private FontDescriptor CreateFontDescriptorCore(FontStyle fontStyle, Stream fontStream)
        {
            bool isPredefined = this.isPredefined;
            return new FontDescriptor(this.fontFamily, fontStyle, fontStream, null);
        }

        private FontDescriptor GetFontDescriptor(FontStyle fontStyle)
        {
            FontDescriptor descriptor = this.fontDescriptors[(int) fontStyle];
            if (descriptor == null)
            {
                descriptor = this.CreateFontDescriptor(fontStyle);
                this.fontDescriptors[(int) fontStyle] = descriptor;
            }
            return descriptor;
        }

        public FontDescriptor GetFontDescriptor(bool bold, bool italic) => 
            this.GetFontDescriptor(this.GetFontStyle(bold, italic));

        private FontStyle GetFontStyle(bool bold, bool italic)
        {
            FontStyle regular = FontStyle.Regular;
            if (bold)
            {
                regular |= FontStyle.Bold;
            }
            if (italic)
            {
                regular |= FontStyle.Italic;
            }
            return regular;
        }
    }
}

