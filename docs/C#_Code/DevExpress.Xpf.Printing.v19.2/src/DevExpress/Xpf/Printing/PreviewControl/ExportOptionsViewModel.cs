namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExportOptionsViewModel : ExportOptionsViewModelBase
    {
        private bool openFileAfterExport;
        private readonly List<ExportFormat> excludedFormats;

        protected ExportOptionsViewModel(PrintingSystemBase printingSystem) : base(printingSystem)
        {
            this.excludedFormats = new List<ExportFormat>();
            if (!printingSystem.Document.CanPerformContinuousExport)
            {
                ExportFormat[] collection = new ExportFormat[] { ExportFormat.Csv, ExportFormat.Txt };
                this.excludedFormats.AddRange(collection);
            }
        }

        public static ExportOptionsViewModel Create(PrintingSystemBase printingSystem, ExportFormat format, IEnumerable<ExportFormat> hiddenFormats = null)
        {
            ExportOptionsViewModel model1 = new ExportOptionsViewModel(printingSystem);
            IEnumerable<ExportFormat> enumerable1 = hiddenFormats;
            if (hiddenFormats == null)
            {
                IEnumerable<ExportFormat> local1 = hiddenFormats;
                enumerable1 = Enumerable.Empty<ExportFormat>();
            }
            model1.HiddenExportFormats = enumerable1;
            ExportOptionsViewModel local2 = model1;
            local2.ExportFormat = format;
            return local2;
        }

        protected override void OnExportFormatChanged()
        {
            base.OnExportFormatChanged();
            this.OpenFileAfterExport = this.CanUseActionAfterExport && (base.PreviewOptions.ActionAfterExport != ActionAfterExport.None);
        }

        public bool CanUseActionAfterExport
        {
            get
            {
                Func<ExportOptionsBase, bool> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<ExportOptionsBase, bool> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.UseActionAfterExportAndSaveModeValue;
                }
                return base.ExportOptions.Return<ExportOptionsBase, bool>(evaluator, (<>c.<>9__5_1 ??= () => false));
            }
        }

        public bool OpenFileAfterExport
        {
            get => 
                this.openFileAfterExport;
            set => 
                base.SetProperty<bool>(ref this.openFileAfterExport, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(ExportOptionsViewModel)), (MethodInfo) methodof(ExportOptionsViewModel.get_OpenFileAfterExport)), new ParameterExpression[0]));
        }

        public override IEnumerable<ExportFormat> AvailableExportFormats =>
            base.AvailableExportFormats.Except<ExportFormat>(this.excludedFormats);

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public override DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType SettingsType =>
            DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType.Export;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExportOptionsViewModel.<>c <>9 = new ExportOptionsViewModel.<>c();
            public static Func<ExportOptionsBase, bool> <>9__5_0;
            public static Func<bool> <>9__5_1;

            internal bool <get_CanUseActionAfterExport>b__5_0(ExportOptionsBase x) => 
                x.UseActionAfterExportAndSaveModeValue;

            internal bool <get_CanUseActionAfterExport>b__5_1() => 
                false;
        }
    }
}

