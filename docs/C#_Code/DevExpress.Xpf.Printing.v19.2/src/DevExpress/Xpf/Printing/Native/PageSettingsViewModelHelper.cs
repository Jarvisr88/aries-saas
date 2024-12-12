namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;

    public static class PageSettingsViewModelHelper
    {
        public static void AssignPageDataFromModel(PageSetupViewModel model, Page page, PrintingSystemBase printingSystem)
        {
            PaperSize paperSize = GetPaperSize(model);
            IPageSettingsService service = printingSystem.GetService<IPageSettingsService>();
            if (service != null)
            {
                service.Assign(page, GetMargins(model), page.PageData.MinMarginsF, paperSize.Kind, PageData.ToSize(paperSize), model.Landscape, paperSize.PaperName);
            }
        }

        public static MarginsF GetMargins(PageSetupViewModel model) => 
            new MarginsFloat(GraphicsUnitConverter.Convert(model.LeftMargin, model.Unit, GraphicsUnit.Document), GraphicsUnitConverter.Convert(model.RightMargin, model.Unit, GraphicsUnit.Document), GraphicsUnitConverter.Convert(model.TopMargin, model.Unit, GraphicsUnit.Document), GraphicsUnitConverter.Convert(model.BottomMargin, model.Unit, GraphicsUnit.Document));

        public static PaperSize GetPaperSize(PageSetupViewModel model)
        {
            PaperSize size = model.PaperSizes.FirstOrDefault<PaperSize>(a => a.Kind == model.PaperKind);
            if (size.Kind == PaperKind.Custom)
            {
                size.Width = (int) GraphicsUnitConverter.Convert(model.PaperWidth, GraphicsDpi.UnitToDpi(model.Unit), (float) 100f);
                size.Height = (int) GraphicsUnitConverter.Convert(model.PaperHeight, GraphicsDpi.UnitToDpi(model.Unit), (float) 100f);
                if (model.Landscape)
                {
                    size.Width = size.Height;
                    size.Height = size.Width;
                }
            }
            return size;
        }
    }
}

