namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class TextUtils
    {
        public const string EllipsisString = "…";
        [ThreadStatic]
        private static FontsCache fontsCache;
        private static int tabStopSpacesCount = 4;
        private static int leftOffset;
        private static int rightOffset;
        private static int topOffset;
        private static int bottomOffset;
        private static bool useKerning;

        static TextUtils()
        {
            ResetOffsets();
        }

        public static void DrawSingleLineString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, StringFormat format)
        {
            Fonts.DrawSingleLineString(g, text, font, foreColor, drawBounds, format);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Point location)
        {
            DrawString(g, text, font, foreColor, location, Rectangle.Empty, StringFormat.GenericDefault);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds)
        {
            DrawString(g, text, font, foreColor, drawBounds, Rectangle.Empty, StringFormat.GenericDefault);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Point location, Rectangle clipBounds)
        {
            DrawString(g, text, font, foreColor, location, clipBounds, StringFormat.GenericDefault);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Point location, StringFormat stringFormat)
        {
            DrawString(g, text, font, foreColor, location, Rectangle.Empty, stringFormat);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, Rectangle clipBounds)
        {
            DrawString(g, text, font, foreColor, drawBounds, clipBounds, StringFormat.GenericDefault);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, StringFormat stringFormat)
        {
            DrawString(g, text, font, foreColor, drawBounds, Rectangle.Empty, stringFormat);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Point location, Rectangle clipBounds, StringFormat stringFormat)
        {
            DrawString(g, text, font, foreColor, new Rectangle(location, GetStringSize(g, text, font, stringFormat)), clipBounds, stringFormat);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat)
        {
            DrawString(g, text, font, foreColor, drawBounds, clipBounds, stringFormat, null);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, StringFormat stringFormat, TextHighLight highLight)
        {
            DrawString(g, text, font, foreColor, drawBounds, Rectangle.Empty, stringFormat, highLight);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight)
        {
            DrawString(g, text, font, foreColor, drawBounds, clipBounds, stringFormat, highLight, null);
        }

        public static void DrawString(Graphics g, string text, Font font, Color foreColor, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight, IWordBreakProvider wordBreakProvider)
        {
            Fonts.DrawString(g, text, font, foreColor, drawBounds, clipBounds, stringFormat, highLight, wordBreakProvider);
        }

        public static int GetFontAscentHeight(Graphics g, Font font) => 
            Fonts.GetFontAscentHeight(g, font);

        public static int GetFontHeight(Graphics g, Font font) => 
            Fonts.GetFontHeight(g, font);

        public static int GetFontInternalLeading(Graphics g, Font font) => 
            Fonts.GetFontInternalLeading(g, font);

        public static int[] GetMeasureString(Graphics g, string text, Font font) => 
            GetMeasureString(g, text, font, StringFormat.GenericDefault);

        public static int[] GetMeasureString(Graphics g, string text, Font font, StringFormat stringFormat) => 
            Fonts.GetMeasureString(g, text, font, stringFormat);

        public static int GetStringHeight(Graphics g, string text, Font font, int width) => 
            GetStringHeight(g, text, font, width, StringFormat.GenericDefault);

        public static int GetStringHeight(Graphics g, string text, Font font, int width, StringFormat stringFormat) => 
            Fonts.GetStringHeight(g, text, font, width, stringFormat);

        public static Size GetStringSize(Graphics g, string text, Font font) => 
            GetStringSize(g, text, font, StringFormat.GenericDefault);

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat) => 
            (text != null) ? Fonts.GetStringSize(g, text, font, stringFormat) : Size.Empty;

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat, int maxWidth) => 
            GetStringSize(g, text, font, stringFormat, maxWidth, (IWordBreakProvider) null);

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat, int maxWidth, IWordBreakProvider wordBreakProvider) => 
            (text != null) ? Fonts.GetStringSize(g, text, font, stringFormat, maxWidth, wordBreakProvider) : Size.Empty;

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight)
        {
            bool flag;
            return GetStringSize(g, text, font, stringFormat, maxWidth, maxHeight, null, out flag);
        }

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight, IWordBreakProvider wordBreakProvider)
        {
            bool flag;
            return GetStringSize(g, text, font, stringFormat, maxWidth, maxHeight, wordBreakProvider, out flag);
        }

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight, out bool isCropped) => 
            GetStringSize(g, text, font, stringFormat, maxWidth, maxHeight, null, out isCropped);

        public static Size GetStringSize(Graphics g, string text, Font font, StringFormat stringFormat, int maxWidth, int maxHeight, IWordBreakProvider wordBreakProvider, out bool isCropped)
        {
            isCropped = false;
            return ((text != null) ? Fonts.GetStringSize(g, text, font, stringFormat, maxWidth, maxHeight, wordBreakProvider, out isCropped) : Size.Empty);
        }

        public static bool IsMultilineTextFit(Graphics g, Font font, string text, Rectangle drawBounds, StringFormat format)
        {
            TextOutDraw draw = new TextOutDraw(Fonts[g, font], g, text, drawBounds, Rectangle.Empty, format, null, null);
            for (int i = 0; i < draw.TextLines.Count; i++)
            {
                if (draw.TextLines[i].HasEllipsis)
                {
                    return false;
                }
            }
            return true;
        }

        public static void ResetFontsCache(Graphics g, Font font)
        {
            if (fontsCache != null)
            {
                fontsCache.ResetFontCache(g.PageUnit, font);
            }
        }

        public static void ResetOffsets()
        {
            SetOffsets(0, 0, 0, 0);
        }

        public static void SetOffsets(int left, int top, int right, int bottom)
        {
            LeftOffset = left;
            TopOffset = top;
            RightOffset = right;
            BottomOffset = bottom;
        }

        public static int TabStopSpacesCount
        {
            get => 
                tabStopSpacesCount;
            set => 
                tabStopSpacesCount = value;
        }

        public static int LeftOffset
        {
            get => 
                leftOffset;
            set => 
                leftOffset = value;
        }

        public static int RightOffset
        {
            get => 
                rightOffset;
            set => 
                rightOffset = value;
        }

        public static int TopOffset
        {
            get => 
                topOffset;
            set => 
                topOffset = value;
        }

        public static int BottomOffset
        {
            get => 
                bottomOffset;
            set => 
                bottomOffset = value;
        }

        public static bool UseKerning
        {
            get => 
                useKerning;
            set => 
                useKerning = value;
        }

        private static FontsCache Fonts
        {
            get
            {
                fontsCache ??= new FontsCache();
                return fontsCache;
            }
        }
    }
}

