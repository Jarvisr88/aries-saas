namespace DevExpress.Office.Services
{
    using System;

    public interface IFontCharacterSetService
    {
        void BeginProcessing(string fontName);
        bool ContainsChar(char ch);
        void EndProcessing();
    }
}

