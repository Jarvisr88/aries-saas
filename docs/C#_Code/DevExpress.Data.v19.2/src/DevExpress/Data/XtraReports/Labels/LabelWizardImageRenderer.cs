namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public class LabelWizardImageRenderer
    {
        private static readonly Color backgroundColor;
        private static readonly Color pitchColor;
        private static readonly Color marginColor;
        private static readonly Color dimensionColor;
        private const int l1x = 0x56;
        private const int l1y = 0x30;
        private const int l2y = 0x91;
        private const int lh = 0x59;
        private const int pitchLineOffsetX = 0x18;
        private const int pitchLineOffsetY = 9;
        private const int pitchSizeOffsetX = 0x2b;
        private const int pitchSizeOffsetY = 0x13;
        private const int marginSizeOffsetY = 0x18;
        private const int arrowOffset = 2;
        private const int radius = 0x12;
        private const int baseControlHeight = 0xcc;
        private const int baseControlWidth = 0x156;
        private int lw;
        private int l2x;
        private int marginSizeOffsetX;
        private string verticalPitch;
        private string horizontalPitch;
        private string leftMargin;
        private string topMargin;
        private string width;
        private string height;
        private Size controlSize;

        static LabelWizardImageRenderer();
        public LabelWizardImageRenderer();
        private static GraphicsPath CreateRoundRectPath(float x, float y, float width, float height, float radius);
        private static Pen CreateSizePen(Color c);
        public void Draw(Graphics gr, Size controlSize);
        private static void DrawHorizontalLabel(Graphics gr, string[] lines, float midLineWidth, int offsetX, int offsetYCenter, int width, Brush bkBrush, Brush fgBrush, Font font, StringFormat sf);
        private static void DrawRoundedRectangle(Graphics gr, Brush br, Pen pen, RectangleF rect, float radius);
        private static void DrawVerticalLabel(Graphics gr, string[] lines, int offsetXCenter, int offsetY, int height, Brush bkBrush, Brush fgBrush, Font font, StringFormat sf);
        private string[] GetLinesFromText(string text);
        private Tuple<int, float> GetWidestLineInfo(Graphics gr, string[] horizontalLines, Font font, StringFormat sf);
        public void InitStrings(string verticalPitch, string horizontalPitch, string leftMargin, string topMargin, string width, string height);
        public void UpdateLayout(int deltaWidth);

        private Size ControlSize { get; set; }
    }
}

