namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;

    public class ProgressMaster
    {
        private ProgressMode progressMode;
        private ProgressReflector progressReflector;

        public ProgressMaster(ProgressReflector progressReflector, ExportOptionsBase exportOptions)
        {
            this.progressReflector = progressReflector;
            this.progressMode = GetProgressMode(exportOptions.ExportModeBase);
        }

        public void AllObjectsExported()
        {
            if (this.progressMode == ProgressMode.ByObjects)
            {
                this.progressReflector.MaximizeRange();
            }
        }

        public void AllPagesExported()
        {
            if (this.progressMode == ProgressMode.ByPages)
            {
                this.progressReflector.MaximizeRange();
            }
        }

        private static ProgressMode GetProgressMode(ExportModeBase exportMode) => 
            (exportMode == ExportModeBase.SingleFile) ? ProgressMode.ByObjects : ((exportMode == ExportModeBase.SingleFilePageByPage) ? ProgressMode.ByPages : ProgressMode.None);

        public void InitializeRangeByObjects(int count)
        {
            if (this.progressMode == ProgressMode.ByObjects)
            {
                this.progressReflector.InitializeRange(count);
            }
        }

        public void InitializeRangeByPages(int count)
        {
            if (this.progressMode == ProgressMode.ByPages)
            {
                this.progressReflector.InitializeRange(count);
            }
        }

        public void ObjectExported()
        {
            if (this.progressMode == ProgressMode.ByObjects)
            {
                this.progressReflector.RangeValue++;
            }
        }

        public void PageExported()
        {
            if (this.progressMode == ProgressMode.ByPages)
            {
                this.progressReflector.RangeValue++;
            }
        }

        private enum ProgressMode
        {
            None,
            ByObjects,
            ByPages
        }
    }
}

