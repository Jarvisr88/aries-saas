namespace DevExpress.Data.Summary
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class SummaryEditorUIItem : INotifyPropertyChanged
    {
        private SummaryItemsEditorController controller;
        private string fieldName;
        private string caption;
        private System.Type type;

        public event PropertyChangedEventHandler PropertyChanged;

        public SummaryEditorUIItem(SummaryItemsEditorController controller, string fieldName, string caption, System.Type type);
        public bool CanDoSummary(SummaryItemType summaryType);
        private void RaisePropertyChanged(string propertyName);
        public override string ToString();

        public bool this[SummaryItemType summaryType] { get; set; }

        public string FieldName { get; }

        public string Caption { get; }

        public System.Type Type { get; }

        public bool HasSummary { get; }

        protected SummaryItemsEditorController Controller { get; }
    }
}

