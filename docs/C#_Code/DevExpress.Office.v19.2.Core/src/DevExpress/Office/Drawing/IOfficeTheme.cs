namespace DevExpress.Office.Drawing
{
    using System;

    public interface IOfficeTheme : IDisposable
    {
        void Clear();
        IOfficeTheme Clone();
        void CopyFrom(IOfficeTheme sourceObj);

        ThemeDrawingColorCollection Colors { get; }

        ThemeFontScheme FontScheme { get; }

        ThemeFormatScheme FormatScheme { get; }

        string Name { get; set; }

        bool IsValidate { get; }
    }
}

