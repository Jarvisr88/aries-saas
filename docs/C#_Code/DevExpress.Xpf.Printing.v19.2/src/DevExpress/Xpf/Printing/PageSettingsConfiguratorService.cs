namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Printing;
    using System.Windows;

    public class PageSettingsConfiguratorService : IPageSettingsConfiguratorService
    {
        private static void AssignPageSettingsFromModel(XtraPageSettingsBase pageSettings, LegacyPageSetupViewModel model)
        {
            SizeF sizeF = model.GetSizeF(300f);
            PageData val = new PageData(model.GetMargins(300f), model.PaperKind, model.Landscape);
            if (model.PaperKind == PaperKind.Custom)
            {
                val.PageSize = sizeF;
            }
            pageSettings.Assign(val);
        }

        private static void AssignPageSettingsToModel(XtraPageSettingsBase pageSettings, LegacyPageSetupViewModel model)
        {
            PageData data = pageSettings.Data;
            model.Landscape = pageSettings.Landscape;
            model.PaperKind = pageSettings.PaperKind;
            model.SetPageSize(data.PageSize, 300f);
            model.SetMargins(data.MarginsF.Left, data.MarginsF.Top, data.MarginsF.Right, data.MarginsF.Bottom, 300f);
        }

        public bool? Configure(XtraPageSettingsBase pageSettings, Window ownerWindow)
        {
            try
            {
                PageSetupWindow window1 = new PageSetupWindow();
                window1.Owner = ownerWindow;
                window1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                PageSetupWindow window = window1;
                if (ownerWindow != null)
                {
                    window.FlowDirection = ownerWindow.FlowDirection;
                }
                LegacyPageSetupViewModel model = window.Model;
                AssignPageSettingsToModel(pageSettings, model);
                bool? nullable = window.ShowDialog();
                bool flag = true;
                if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
                {
                    AssignPageSettingsFromModel(pageSettings, model);
                }
                return window.DialogResult;
            }
            catch (PrintServerException)
            {
                MessageBoxHelper.Show(MessageBoxButton.OK, MessageBoxImage.Hand, PreviewStringId.Msg_NeedPrinter, new object[0]);
                return false;
            }
        }
    }
}

