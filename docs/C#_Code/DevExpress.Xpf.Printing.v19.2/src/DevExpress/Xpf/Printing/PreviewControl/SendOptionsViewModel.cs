namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class SendOptionsViewModel : ExportOptionsViewModelBase
    {
        private readonly List<ExportFormat> excludedFormats;

        protected SendOptionsViewModel(PrintingSystemBase printingSystem) : base(printingSystem)
        {
            this.excludedFormats = new List<ExportFormat>();
            this.excludedFormats.Add(ExportFormat.Htm);
            if (!printingSystem.Document.CanPerformContinuousExport)
            {
                ExportFormat[] collection = new ExportFormat[] { ExportFormat.Csv, ExportFormat.Txt };
                this.excludedFormats.AddRange(collection);
            }
        }

        public static SendOptionsViewModel Create(PrintingSystemBase printingSystem, ExportFormat format, IEnumerable<ExportFormat> hiddenFormats = null)
        {
            SendOptionsViewModel model1 = new SendOptionsViewModel(printingSystem);
            IEnumerable<ExportFormat> enumerable1 = hiddenFormats;
            if (hiddenFormats == null)
            {
                IEnumerable<ExportFormat> local1 = hiddenFormats;
                enumerable1 = Enumerable.Empty<ExportFormat>();
            }
            model1.HiddenExportFormats = enumerable1;
            SendOptionsViewModel local2 = model1;
            local2.ExportFormat = format;
            return local2;
        }

        public DevExpress.XtraPrinting.EmailOptions EmailOptions =>
            base.Options.Email;

        public override IEnumerable<ExportFormat> AvailableExportFormats =>
            base.AvailableExportFormats.Except<ExportFormat>(this.excludedFormats);

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public override DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType SettingsType =>
            DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType.Send;
    }
}

