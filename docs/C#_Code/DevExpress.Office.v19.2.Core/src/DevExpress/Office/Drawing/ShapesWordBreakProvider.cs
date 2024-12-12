namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils.Text;
    using System;

    public class ShapesWordBreakProvider : IWordBreakProvider
    {
        bool IWordBreakProvider.IsWordBreakChar(char ch) => 
            char.IsWhiteSpace(ch) || (ch == '-');
    }
}

