namespace DevExpress.Utils.About
{
    using DevExpress.Utils;
    using System;

    public class ProductInfoHelper
    {
        public const string PlatformFreeOffer = "Free Controls";
        public const string PlatformWinForms = "WinForms Subscription";
        public const string PlatformDashboard = "DevExpress Dashboard";
        public const string PlatformUniversal = "Universal Subscription";
        public const string WinGrid = "The XtraGrid Suite";
        public const string WinEditors = "The XtraEditors Suite";
        public const string WinDiagram = "The XtraDiagram Suite";
        public const string WinVGrid = "The XtraVerticalGrid Suite";
        public const string WinRichEdit = "The XtraRichEdit Suite";
        public const string WinSpellChecker = "The XtraSpellChecker";
        public const string WinScheduler = "The XtraScheduler Suite";
        public const string WinSnap = "Snap Reports";
        public const string WinCharts = "The XtraCharts Suite";
        public const string WinTreeList = "The XtraTreeList Suite";
        public const string WinPivotGrid = "The XtraPivotGrid Suite";
        public const string WinXtraBars = "The XtraBars Suite";
        public const string WinMaps = "The XtraMaps Suite";
        public const string WinSpreadsheet = "The XtraSpreadsheet Suite";
        public const string WinNavBar = "The XtraNavBar Suite";
        public const string WinLayoutControl = "The XtraLayout Suite";
        public const string WinPrinting = "The XtraPrinting Library";
        public const string WinGauge = "The XtraGauge Suite";
        public const string WinPdfViewer = "PDF Viewer";
        public const string WinWizard = "XtraWizard";
        public const string WinReports = "Reporting Solution";
        public const string WinXPO = "eXpress Persistent Objects";
        public const string WinMVVM = "Application Infrastructural MVVM Solution";
        public const string WinTreeMap = "The XtraTreeMap Suite";

        public static ProductStringInfo GetProductInfo(ProductKind kind)
        {
            if (LocalizationHelper.IsJapanese)
            {
                return new ProductStringInfo("XtraGrid for WinForms", "Japanese Edition");
            }
            string platform = "DXperience";
            string str2 = "DXperience Universal";
            if (kind <= (ProductKind.Default | ProductKind.DXperienceSliverlight))
            {
                if (kind <= (ProductKind.Default | ProductKind.XPO))
                {
                    if (kind <= (ProductKind.Default | ProductKind.XtraReports))
                    {
                        if (kind == (ProductKind.Default | ProductKind.DXperienceWin))
                        {
                            return new ProductStringInfo("WinForms Subscription", "DXperience WinForms");
                        }
                        if (kind == (ProductKind.Default | ProductKind.XtraReports))
                        {
                            return new ProductStringInfo(platform, "Cross-Platform Reporting Solution");
                        }
                    }
                    else
                    {
                        if (kind == (ProductKind.Default | ProductKind.DemoWin))
                        {
                            return new ProductStringInfo(platform, "Demo for Windows Forms");
                        }
                        if (kind == (ProductKind.Default | ProductKind.XPO))
                        {
                            return new ProductStringInfo(platform, "eXpress Persistent Objects");
                        }
                    }
                }
                else if (kind <= (ProductKind.Default | ProductKind.XAF))
                {
                    if (kind == (ProductKind.Default | ProductKind.DXperienceASP))
                    {
                        return new ProductStringInfo(platform, "DXperience ASP.NET");
                    }
                    if (kind == (ProductKind.Default | ProductKind.XAF))
                    {
                        return new ProductStringInfo(str2, "eXpressApp Framework");
                    }
                }
                else
                {
                    if (kind == (ProductKind.Default | ProductKind.DXperienceWPF))
                    {
                        return new ProductStringInfo("WPF Subscription", "DXperience WPF");
                    }
                    if (kind == (ProductKind.Default | ProductKind.DXperienceSliverlight))
                    {
                        return new ProductStringInfo("Silverlight Subscription", "DXperience Silverlight");
                    }
                }
            }
            else if (kind <= (ProductKind.Default | ProductKind.Snap))
            {
                if (kind <= ProductKind.Dashboard)
                {
                    if (kind == (ProductKind.Default | ProductKind.LightSwitchReports))
                    {
                        return new ProductStringInfo(platform, "Reporting for LightSwitch");
                    }
                    if (kind == ProductKind.Dashboard)
                    {
                        return new ProductStringInfo("Universal Subscription", "DevExpress Dashboard");
                    }
                }
                else
                {
                    if (kind == ProductKind.CodedUIWin)
                    {
                        return new ProductStringInfo(str2, "Coded UI Support for WinForms Controls");
                    }
                    if (kind == (ProductKind.Default | ProductKind.Snap))
                    {
                        return new ProductStringInfo(platform, "Snap by DevExpress");
                    }
                }
            }
            else if (kind <= (ProductKind.Default | ProductKind.Docs))
            {
                if (kind == (ProductKind.Default | ProductKind.DXperiencePro))
                {
                    return new ProductStringInfo(platform, "professional");
                }
                if (kind == (ProductKind.Default | ProductKind.Docs))
                {
                    return new ProductStringInfo(str2, "Document Server");
                }
            }
            else
            {
                if (kind == (ProductKind.Default | ProductKind.Docs | ProductKind.DXperienceASP | ProductKind.DXperiencePro | ProductKind.DXperienceSliverlight | ProductKind.DXperienceWPF | ProductKind.XPO))
                {
                    return new ProductStringInfo(platform, "enterprise");
                }
                if (kind == (ProductKind.Dashboard | ProductKind.Docs | ProductKind.DXperienceASP | ProductKind.DXperiencePro | ProductKind.DXperienceSliverlight | ProductKind.DXperienceWPF | ProductKind.XAF | ProductKind.XPO))
                {
                    return new ProductStringInfo(platform, "universal");
                }
                if (kind == (ProductKind.Default | ProductKind.FreeOffer))
                {
                    return new ProductStringInfo(platform, "Free Offer");
                }
            }
            return new ProductStringInfo($"{kind}");
        }
    }
}

