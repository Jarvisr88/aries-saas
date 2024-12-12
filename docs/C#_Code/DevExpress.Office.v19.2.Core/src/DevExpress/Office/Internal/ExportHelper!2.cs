namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Export;
    using DevExpress.Utils.CommonDialogs;
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public abstract class ExportHelper<TFormat, TResult> : ImportExportHelper, IDisposable
    {
        protected ExportHelper(IDocumentModel documentModel) : base(documentModel)
        {
        }

        protected internal abstract void ApplyParameters(IExporterOptions options, ExportParameters<TFormat, TResult> parameters);
        protected internal virtual int CalculateCurrentFilterIndex(List<IExporter<TFormat, TResult>> exporters) => 
            this.CalculateCurrentFilterIndex(exporters, null);

        internal virtual int CalculateCurrentFilterIndex(List<IExporter<TFormat, TResult>> exporters, IDocumentSaveOptions<TFormat> options)
        {
            TFormat objB = (options != null) ? this.GetCurrentDocumentFormat(options) : this.GetCurrentDocumentFormat();
            int count = exporters.Count;
            for (int i = 0; i < count; i++)
            {
                if (Equals(exporters[i].Format, objB))
                {
                    return i;
                }
            }
            return 0;
        }

        protected IExporter<TFormat, TResult> ChooseExporter(string fileName, int filterIndex, List<IExporter<TFormat, TResult>> exporters)
        {
            IExporter<TFormat, TResult> exporter = this.ChooseExporterByFileName(fileName, exporters);
            return this.ChooseExporterByFilterIndex(filterIndex, exporters);
        }

        protected IExporter<TFormat, TResult> ChooseExporterByFileName(string fileName, List<IExporter<TFormat, TResult>> exporters)
        {
            char[] trimChars = new char[] { '.' };
            string str = Path.GetExtension(fileName).TrimStart(trimChars).ToLower();
            if (!string.IsNullOrEmpty(str))
            {
                int count = exporters.Count;
                for (int i = 0; i < count; i++)
                {
                    FileExtensionCollection extensions = exporters[i].Filter.Extensions;
                    if (extensions.IndexOf(str) >= 0)
                    {
                        return exporters[i];
                    }
                }
            }
            return null;
        }

        protected IExporter<TFormat, TResult> ChooseExporterByFilterIndex(int filterIndex, List<IExporter<TFormat, TResult>> exporters) => 
            ((filterIndex < 0) || (filterIndex >= exporters.Count)) ? null : exporters[filterIndex];

        protected internal virtual FileDialogFilterCollection CreateExportFilters(List<IExporter<TFormat, TResult>> exporters)
        {
            FileDialogFilterCollection filters = new FileDialogFilterCollection();
            int count = exporters.Count;
            for (int i = 0; i < count; i++)
            {
                FileDialogFilter item = exporters[i].Filter;
                if (item.Extensions.Count > 0)
                {
                    filters.Add(item);
                }
            }
            return filters;
        }

        protected internal virtual SaveFileDialog CreateSaveFileDialog(FileDialogFilterCollection filters, int currentFilterIndex) => 
            this.CreateSaveFileDialog(filters, currentFilterIndex, null);

        internal virtual SaveFileDialog CreateSaveFileDialog(FileDialogFilterCollection filters, int currentFilterIndex, IDocumentSaveOptions<TFormat> options)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            this.InitializeSaveFileDialog(dialog, filters, currentFilterIndex);
            return dialog;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public TResult Export(Stream stream, ExportParameters<TFormat, TResult> parameters)
        {
            IExporter<TFormat, TResult> exporter = parameters.ExportManagerService.GetExporter(parameters.DocumentFormat);
            if (exporter == null)
            {
                this.ThrowUnsupportedFormatException();
            }
            IExporterOptions options = exporter.SetupSaving();
            IExporterOptions predefinedOptions = this.GetPredefinedOptions(parameters.DocumentFormat);
            if (predefinedOptions != null)
            {
                options.CopyFrom(predefinedOptions);
            }
            options.TargetUri = parameters.TargetUri;
            this.ApplyParameters(options, parameters);
            this.PreprocessContentBeforeExport(parameters.DocumentFormat);
            return exporter.SaveDocument(base.DocumentModel, stream, options);
        }

        public TResult Export(Stream stream, TFormat format, string targetUri, IExportManagerService<TFormat, TResult> exportManagerService) => 
            this.Export(stream, format, targetUri, exportManagerService, null);

        public TResult Export(Stream stream, TFormat format, string targetUri, IExportManagerService<TFormat, TResult> exportManagerService, Encoding encoding)
        {
            ExportParameters<TFormat, TResult> parameters = new ExportParameters<TFormat, TResult>(format, targetUri, encoding) {
                ExportManagerService = exportManagerService
            };
            return this.Export(stream, parameters);
        }

        protected internal abstract TFormat GetCurrentDocumentFormat();
        protected internal virtual TFormat GetCurrentDocumentFormat(IDocumentSaveOptions<TFormat> options) => 
            this.GetCurrentDocumentFormat();

        protected internal virtual string GetDirectoryName(string fileName) => 
            !string.IsNullOrEmpty(fileName) ? (!string.IsNullOrEmpty(Path.GetDirectoryName(fileName)) ? Path.GetFullPath(Path.GetDirectoryName(fileName)) : string.Empty) : string.Empty;

        protected internal virtual string GetFileName(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            return fileName;
        }

        protected internal abstract string GetFileNameForSaving();
        protected internal virtual string GetFileNameForSaving(IDocumentSaveOptions<TFormat> options) => 
            this.GetFileNameForSaving();

        protected internal abstract IExporterOptions GetPredefinedOptions(TFormat format);
        protected internal virtual void InitializeSaveFileDialog(ISaveFileDialog dialog, FileDialogFilterCollection filters, int currentFilterIndex)
        {
            this.InitializeSaveFileDialog(dialog, filters, currentFilterIndex, null);
        }

        protected internal virtual void InitializeSaveFileDialog(SaveFileDialog dialog, FileDialogFilterCollection filters, int currentFilterIndex)
        {
            this.InitializeSaveFileDialog(dialog, filters, currentFilterIndex, null);
        }

        protected internal virtual void InitializeSaveFileDialog(ISaveFileDialog dialog, FileDialogFilterCollection filters, int currentFilterIndex, IDocumentSaveOptions<TFormat> options)
        {
            dialog.Filter = base.CreateFilterString(filters);
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.OverwritePrompt = true;
            dialog.DereferenceLinks = true;
            dialog.ValidateNames = true;
            dialog.AddExtension = false;
            dialog.FilterIndex = 1;
            if ((filters.Count > 0) && (currentFilterIndex < filters.Count))
            {
                dialog.FilterIndex = 1 + currentFilterIndex;
                if (filters[currentFilterIndex].Extensions.Count > 0)
                {
                    dialog.DefaultExt = filters[currentFilterIndex].Extensions[0];
                    dialog.AddExtension = true;
                }
            }
            string fileName = (options != null) ? this.GetFileNameForSaving(options) : this.GetFileNameForSaving();
            string directoryName = this.GetDirectoryName(fileName);
            fileName = this.GetFileName(fileName);
            this.SetFileName(dialog, fileName);
            this.SetDirectoryName(dialog, directoryName);
        }

        protected internal virtual void InitializeSaveFileDialog(SaveFileDialog dialog, FileDialogFilterCollection filters, int currentFilterIndex, IDocumentSaveOptions<TFormat> options)
        {
            dialog.Filter = base.CreateFilterString(filters);
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.OverwritePrompt = true;
            dialog.DereferenceLinks = true;
            dialog.ValidateNames = true;
            dialog.AddExtension = false;
            dialog.FilterIndex = 1;
            if ((filters.Count > 0) && (currentFilterIndex < filters.Count))
            {
                dialog.FilterIndex = 1 + currentFilterIndex;
                if (filters[currentFilterIndex].Extensions.Count > 0)
                {
                    dialog.DefaultExt = filters[currentFilterIndex].Extensions[0];
                    dialog.AddExtension = true;
                }
            }
            string fileName = (options != null) ? this.GetFileNameForSaving(options) : this.GetFileNameForSaving();
            string directoryName = this.GetDirectoryName(fileName);
            fileName = this.GetFileName(fileName);
            this.SetFileName(dialog, fileName);
            this.SetDirectoryName(dialog, directoryName);
        }

        public ExportTarget<TFormat, TResult> InvokeExportDialog(IWin32Window parent, IExportManagerService<TFormat, TResult> exportManagerService) => 
            this.InvokeExportDialog(parent, exportManagerService, null);

        public ExportTarget<TFormat, TResult> InvokeExportDialog(IWin32Window parent, IExportManagerService<TFormat, TResult> exportManagerService, ExportersCalculator<TFormat, TResult> exportersCollector) => 
            this.InvokeExportDialog(parent, exportManagerService, exportersCollector, null);

        public ExportTarget<TFormat, TResult> InvokeExportDialog(IWin32Window parent, IExportManagerService<TFormat, TResult> exportManagerService, ExportersCalculator<TFormat, TResult> exportersCollector, IDocumentSaveOptions<TFormat> options)
        {
            if (exportManagerService == null)
            {
                this.ThrowUnsupportedFormatException();
            }
            List<IExporter<TFormat, TResult>> exporters = (exportersCollector != null) ? exportersCollector(exportManagerService) : exportManagerService.GetExporters();
            if (exporters.Count <= 0)
            {
                this.ThrowUnsupportedFormatException();
            }
            FileDialogFilterCollection filters = this.CreateExportFilters(exporters);
            int currentFilterIndex = this.CalculateCurrentFilterIndex(exporters, options);
            if (!this.ShowSaveFileDialog(filters, parent, options, currentFilterIndex))
            {
                return null;
            }
            IExporter<TFormat, TResult> exporter = this.ChooseExporter(base.FileName, base.FilterIndex, exporters);
            if (exporter == null)
            {
                this.ThrowUnsupportedFormatException();
            }
            return new ExportTarget<TFormat, TResult>(base.FileName, exporter);
        }

        protected internal abstract void PreprocessContentBeforeExport(TFormat format);
        private void SetDirectoryName(ISaveFileDialog dialog, string directoryName)
        {
            dialog.InitialDirectory = directoryName;
        }

        private void SetDirectoryName(SaveFileDialog dialog, string directoryName)
        {
            dialog.InitialDirectory = directoryName;
        }

        private void SetFileName(ISaveFileDialog dialog, string fileName)
        {
            dialog.FileName = fileName;
        }

        private void SetFileName(SaveFileDialog dialog, string fileName)
        {
            dialog.FileName = fileName;
        }

        protected internal virtual bool ShowSaveFileDialog(ISaveFileDialog dialog, IWin32Window parent) => 
            dialog.ShowDialog(parent) == DevExpress.Utils.CommonDialogs.Internal.DialogResult.OK;

        protected internal virtual bool ShowSaveFileDialog(SaveFileDialog dialog, IWin32Window parent) => 
            dialog.ShowDialog(parent) == System.Windows.Forms.DialogResult.OK;

        protected virtual bool ShowSaveFileDialog(FileDialogFilterCollection filters, IWin32Window parent, IDocumentSaveOptions<TFormat> options, int currentFilterIndex)
        {
            bool flag;
            if (base.DialogProviderService == null)
            {
                SaveFileDialog dialog = this.CreateSaveFileDialog(filters, currentFilterIndex, options);
                flag = this.ShowSaveFileDialog(dialog, parent);
                if (flag)
                {
                    base.ApplyFileDialogSettings(dialog.FileName, dialog.FilterIndex - 1);
                }
            }
            else
            {
                ISaveFileDialog dialog = base.DialogProviderService.CreateDefaultSaveFileDialog();
                this.InitializeSaveFileDialog(dialog, filters, currentFilterIndex, options);
                flag = this.ShowSaveFileDialog(dialog, parent);
                if (flag)
                {
                    base.ApplyFileDialogSettings(dialog.FileName, dialog.FilterIndex - 1);
                }
            }
            return flag;
        }
    }
}

