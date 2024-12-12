namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Import;
    using DevExpress.Office.Localization;
    using DevExpress.Utils.CommonDialogs;
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public abstract class ImportHelper<TFormat, TResult> : ImportExportHelper
    {
        protected ImportHelper(IDocumentModel documentModel) : base(documentModel)
        {
        }

        protected internal abstract void ApplyEncoding(IImporterOptions options, Encoding encoding);
        public IImporter<TFormat, TResult> AutodetectImporter(string fileName, IImportManagerService<TFormat, TResult> importManagerService) => 
            this.AutodetectImporter(fileName, importManagerService, true);

        public IImporter<TFormat, TResult> AutodetectImporter(string fileName, IImportManagerService<TFormat, TResult> importManagerService, bool useFormatFallback)
        {
            List<IImporter<TFormat, TResult>> importers = importManagerService.GetImporters();
            return ((importers.Count != 0) ? this.ChooseImporter(fileName, -1, importers, useFormatFallback) : null);
        }

        protected internal IImporter<TFormat, TResult> ChooseImporter(string fileName, int filterIndex, List<IImporter<TFormat, TResult>> importers, bool useFormatFallback)
        {
            IImporter<TFormat, TResult> importer = this.ChooseImporterByFileName(fileName, importers);
            importer ??= this.ChooseImporterByFilterIndex(filterIndex, importers);
            if (importer == null)
            {
                TFormat format = useFormatFallback ? this.FallbackFormat : this.UndefinedFormat;
                importer = this.ChooseImporterByFormat(format, importers);
            }
            return importer;
        }

        protected IImporter<TFormat, TResult> ChooseImporterByFileName(string fileName, List<IImporter<TFormat, TResult>> importers)
        {
            char[] trimChars = new char[] { '.' };
            string str = Path.GetExtension(fileName).TrimStart(trimChars).ToLower();
            if (!string.IsNullOrEmpty(str))
            {
                int count = importers.Count;
                for (int i = 0; i < count; i++)
                {
                    FileExtensionCollection extensions = importers[i].Filter.Extensions;
                    if (extensions.IndexOf(str) >= 0)
                    {
                        return importers[i];
                    }
                }
            }
            return null;
        }

        protected IImporter<TFormat, TResult> ChooseImporterByFilterIndex(int filterIndex, List<IImporter<TFormat, TResult>> importers) => 
            ((filterIndex < 0) || (filterIndex >= importers.Count)) ? null : importers[filterIndex];

        protected IImporter<TFormat, TResult> ChooseImporterByFormat(TFormat format, List<IImporter<TFormat, TResult>> importers)
        {
            if (!format.Equals(this.UndefinedFormat))
            {
                int count = importers.Count;
                for (int i = 0; i < count; i++)
                {
                    if (format.Equals(importers[i].Format))
                    {
                        return importers[i];
                    }
                }
            }
            return null;
        }

        protected internal virtual FileDialogFilterCollection CreateImportFilters(FileDialogFilterCollection filters)
        {
            FileDialogFilter item = new FileDialogFilter {
                Description = OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_AllSupportedFiles)
            };
            FileDialogFilterCollection filters2 = new FileDialogFilterCollection();
            filters2.Add(FileDialogFilter.AllFiles);
            filters2.Add(item);
            int count = filters.Count;
            for (int i = 0; i < count; i++)
            {
                FileDialogFilter filter2 = filters[i];
                if (filter2.Extensions.Count > 0)
                {
                    filters2.Add(filter2);
                    item.Extensions.AddRange(filter2.Extensions);
                }
            }
            return filters2;
        }

        protected internal virtual OpenFileDialog CreateOpenFileDialog(FileDialogFilterCollection filters)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            this.InitializeOpenFileDialog(dialog, filters);
            return dialog;
        }

        protected internal virtual FileDialogFilterCollection GetLoadDocumentDialogFileFilters(IImportManagerService<TFormat, TResult> importManagerService)
        {
            FileDialogFilterCollection filters = new FileDialogFilterCollection();
            foreach (IImporter<TFormat, TResult> importer in importManagerService.GetImporters())
            {
                filters.Add(importer.Filter);
            }
            return filters;
        }

        protected internal abstract IImporterOptions GetPredefinedOptions(TFormat format);
        public TResult Import(Stream stream, string sourceUri, IImporter<TFormat, TResult> importer, Encoding encoding) => 
            this.Import(stream, sourceUri, importer, encoding, true);

        public TResult Import(Stream stream, TFormat format, string sourceUri, IImportManagerService<TFormat, TResult> importManagerService) => 
            this.Import(stream, format, sourceUri, importManagerService, true);

        public TResult Import(Stream stream, string sourceUri, IImporter<TFormat, TResult> importer, Encoding encoding, bool leaveOpen)
        {
            if (importer == null)
            {
                this.ThrowUnsupportedFormatException();
            }
            IImporterOptions options = importer.SetupLoading();
            IImporterOptions predefinedOptions = this.GetPredefinedOptions(importer.Format);
            if (predefinedOptions != null)
            {
                options.CopyFrom(predefinedOptions);
            }
            options.SourceUri = sourceUri;
            if (encoding != null)
            {
                this.ApplyEncoding(options, encoding);
            }
            return importer.LoadDocument(base.DocumentModel, stream, options, leaveOpen);
        }

        public TResult Import(Stream stream, TFormat format, string sourceUri, IImportManagerService<TFormat, TResult> importManagerService, bool leaveOpen) => 
            this.Import(stream, format, sourceUri, importManagerService, null, leaveOpen);

        public TResult Import(Stream stream, TFormat format, string sourceUri, IImportManagerService<TFormat, TResult> importManagerService, Encoding encoding) => 
            this.Import(stream, format, sourceUri, importManagerService, encoding, true);

        public TResult Import(Stream stream, TFormat format, string sourceUri, IImportManagerService<TFormat, TResult> importManagerService, Encoding encoding, bool leaveOpen)
        {
            IImporter<TFormat, TResult> importer = importManagerService.GetImporter(format);
            return this.Import(stream, sourceUri, importer, encoding, leaveOpen);
        }

        public TResult ImportFromFileAutodetectFormat(string fileName, IImportManagerService<TFormat, TResult> importManagerService)
        {
            IImporter<TFormat, TResult> importer = this.AutodetectImporter(fileName, importManagerService);
            if (importer == null)
            {
                return default(TResult);
            }
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return this.Import(stream, importer.Format, fileName, importManagerService);
            }
        }

        protected internal virtual void InitializeOpenFileDialog(IOpenFileDialog dialog, FileDialogFilterCollection filters)
        {
            dialog.Filter = base.CreateFilterString(filters);
            dialog.FilterIndex = 2;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.SupportMultiDottedExtensions = true;
            dialog.AddExtension = false;
            dialog.DereferenceLinks = true;
            dialog.ValidateNames = true;
        }

        protected internal virtual void InitializeOpenFileDialog(OpenFileDialog dialog, FileDialogFilterCollection filters)
        {
            dialog.Filter = base.CreateFilterString(filters);
            dialog.FilterIndex = 2;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.SupportMultiDottedExtensions = true;
            dialog.AddExtension = false;
            dialog.DereferenceLinks = true;
            dialog.ValidateNames = true;
        }

        public ImportSource<TFormat, TResult> InvokeImportDialog(IWin32Window parent, IImportManagerService<TFormat, TResult> importManagerService)
        {
            if (importManagerService == null)
            {
                this.ThrowUnsupportedFormatException();
            }
            List<IImporter<TFormat, TResult>> importers = importManagerService.GetImporters();
            if (importers.Count <= 0)
            {
                this.ThrowUnsupportedFormatException();
            }
            FileDialogFilterCollection filters = this.CreateImportFilters(this.GetLoadDocumentDialogFileFilters(importManagerService));
            if (!this.ShowOpenFileDialog(filters, parent))
            {
                return null;
            }
            IImporter<TFormat, TResult> importer = this.ChooseImporter(base.FileName, base.FilterIndex, importers, true);
            if (importer == null)
            {
                this.ThrowUnsupportedFormatException();
            }
            return new ImportSource<TFormat, TResult>(base.FileName, base.FileName, importer);
        }

        protected virtual bool ShowOpenFileDialog(FileDialogFilterCollection filters, IWin32Window parent)
        {
            bool flag;
            if (base.DialogProviderService == null)
            {
                OpenFileDialog dialog = this.CreateOpenFileDialog(filters);
                flag = this.ShowOpenFileDialog(dialog, parent);
                if (flag)
                {
                    base.ApplyFileDialogSettings(dialog.FileName, dialog.FilterIndex - 1);
                }
            }
            else
            {
                IOpenFileDialog dialog = base.DialogProviderService.CreateDefaultOpenFileDialog();
                this.InitializeOpenFileDialog(dialog, filters);
                flag = this.ShowOpenFileDialog(dialog, parent);
                if (flag)
                {
                    base.ApplyFileDialogSettings(dialog.FileName, dialog.FilterIndex - 1);
                }
            }
            return flag;
        }

        protected internal virtual bool ShowOpenFileDialog(IOpenFileDialog dialog, IWin32Window parent) => 
            dialog.ShowDialog(parent) == DevExpress.Utils.CommonDialogs.Internal.DialogResult.OK;

        protected internal virtual bool ShowOpenFileDialog(OpenFileDialog dialog, IWin32Window parent) => 
            dialog.ShowDialog(parent) == System.Windows.Forms.DialogResult.OK;

        protected internal abstract TFormat UndefinedFormat { get; }

        protected internal abstract TFormat FallbackFormat { get; }
    }
}

