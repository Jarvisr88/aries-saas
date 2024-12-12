namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Reflection;
    using System.Security;

    public class PageSettingsHelper
    {
        private static PageSettingsHelper instance;
        private PageSettings defaultSettings;

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public static void ChangePageSettings(PageSettings pageSettings, PaperSize paperSize, ReadonlyPageData pageData)
        {
            try
            {
                if (paperSize != null)
                {
                    pageSettings.PaperSize = paperSize;
                }
                pageSettings.Landscape = pageData.Landscape;
                pageSettings.Margins = pageData.Margins;
            }
            catch
            {
            }
        }

        private static Margins ConvertToLandscape(Margins margins) => 
            new Margins(margins.Top, margins.Bottom, margins.Right, margins.Left);

        [SecuritySafeCritical]
        private static IntPtr CreateDC(string printerName)
        {
            try
            {
                if (!string.IsNullOrEmpty(printerName))
                {
                    return Win32.CreateDC(null, printerName, IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (AccessViolationException)
            {
            }
            return IntPtr.Zero;
        }

        public static PaperSize CreateLetterPaperSize() => 
            new PaperSize("Custom", 850, 0x44c);

        private static FieldInfo GetFieldCore(Type type, string baseFieldName) => 
            type.GetField(baseFieldName, BindingFlags.NonPublic | BindingFlags.Instance) ?? type.GetField("_" + baseFieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        private static object GetFieldValue(string fieldName, PageSettings pageSettings) => 
            GetFieldCore(typeof(PageSettings), fieldName)?.GetValue(pageSettings);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object GetLandscapeFieldValue(PageSettings pageSettings) => 
            GetFieldValue("landscape", pageSettings);

        [SecuritySafeCritical]
        public static Margins GetMinMargins(PageSettings pageSettings)
        {
            Margins margins2;
            IntPtr hdc = CreateDC(pageSettings.PrinterSettings.PrinterName);
            if (!(hdc != IntPtr.Zero))
            {
                return XtraPageSettingsBase.DefaultMinMargins;
            }
            try
            {
                using (Graphics graphics = Graphics.FromHdc(hdc))
                {
                    Margins minMargins = DeviceCaps.GetMinMargins(graphics);
                    margins2 = pageSettings.Landscape ? ConvertToLandscape(minMargins) : minMargins;
                }
            }
            finally
            {
                Win32.DeleteDC(hdc);
            }
            return margins2;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object GetPaperSizeFieldValue(PageSettings pageSettings) => 
            GetFieldValue("paperSize", pageSettings);

        public static void ResetUserSetPageSettings(PrintDocument printDocument)
        {
            FieldInfo fieldCore = GetFieldCore(typeof(PrintDocument), "userSetPageSettings");
            if (fieldCore != null)
            {
                fieldCore.SetValue(printDocument, false);
            }
        }

        public static void SetDefaultPageSettings(PrinterSettings printerSettings, PageSettings pageSettings)
        {
            FieldInfo fieldCore = GetFieldCore(typeof(PrinterSettings), "defaultPageSettings");
            if (fieldCore != null)
            {
                fieldCore.SetValue(printerSettings, pageSettings);
            }
        }

        private static void SetFieldValue(string fieldName, PageSettings pageSettings, object value)
        {
            FieldInfo field = typeof(PageSettings).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(pageSettings, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetLandscapeFieldValue(PageSettings pageSettings, object value)
        {
            SetFieldValue("landscape", pageSettings, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetPaperSizeFieldValue(PageSettings pageSettings, object value)
        {
            SetFieldValue("paperSize", pageSettings, value);
        }

        public static void SetPrinterName(PrinterSettings sets, string printerName)
        {
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                if (Equals(str, printerName))
                {
                    sets.PrinterName = printerName;
                    break;
                }
            }
        }

        public static PageSettingsHelper Instance
        {
            get
            {
                instance ??= new PageSettingsHelper();
                return instance;
            }
            set => 
                instance = value;
        }

        public static PageSettings DefaultPageSettings =>
            Instance.DefaultSettings;

        public static bool PrinterExists
        {
            get
            {
                try
                {
                    return (PrinterSettings.InstalledPrinters.Count > 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        public virtual PageSettings DefaultSettings
        {
            get
            {
                if (this.defaultSettings == null)
                {
                    this.defaultSettings = new PrintDocument().DefaultPageSettings;
                    try
                    {
                        PaperSize paperSize = this.defaultSettings.PaperSize;
                    }
                    catch
                    {
                        this.defaultSettings.PaperSize = CreateLetterPaperSize();
                        this.defaultSettings.Margins = XtraPageSettingsBase.DefaultMargins;
                    }
                }
                return this.defaultSettings;
            }
        }
    }
}

