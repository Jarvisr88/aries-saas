namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlDocument : IXlDocument, IDisposable, IXlTableRepository
    {
        private IXlExport exporter;
        private int lastTableId;
        private readonly HashSet<string> uniqueTableNames = new HashSet<string>();

        public XlDocument(IXlExport exporter)
        {
            Guard.ArgumentNotNull(exporter, "exporter");
            this.exporter = exporter;
            this.Theme = XlDocumentTheme.Office2013;
        }

        public IXlSheet CreateSheet() => 
            new XlSheetProxy(this.exporter, this.exporter.BeginSheet());

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.exporter != null))
            {
                this.exporter.EndExport();
                this.exporter = null;
            }
        }

        public string GetUniqueTableName()
        {
            this.lastTableId++;
            string item = $"Table{this.lastTableId}";
            while (this.uniqueTableNames.Contains(item))
            {
                this.lastTableId++;
                item = $"Table{this.lastTableId}";
            }
            return item;
        }

        public void RegisterTableName(string name)
        {
            Guard.ArgumentIsNotNullOrEmpty(name, "name");
            if (this.uniqueTableNames.Contains(name))
            {
                throw new ArgumentException("The table with such name already exists.");
            }
            this.uniqueTableNames.Add(name);
        }

        public void SetSheetPosition(string name, int position)
        {
            IXlExportEx exporter = this.exporter as IXlExportEx;
            if (exporter != null)
            {
                exporter.SetWorksheetPosition(name, position);
            }
        }

        public void UnregisterTableName(string name)
        {
            if (!string.IsNullOrEmpty(name) && this.uniqueTableNames.Contains(name))
            {
                this.uniqueTableNames.Remove(name);
            }
        }

        public IXlDocumentOptions Options =>
            this.exporter.DocumentOptions;

        public XlDocumentProperties Properties =>
            this.exporter.DocumentProperties;

        public XlDocumentTheme Theme { get; set; }

        public XlDocumentView View { get; } = new XlDocumentView()
    }
}

