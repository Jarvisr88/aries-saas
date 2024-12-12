namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ShowValueEditorEventArgs : RoutedEventArgs
    {
        private ClauseNode node;
        private DevExpress.Data.Filtering.OperandValue value;
        private BaseEdit editor;

        public ShowValueEditorEventArgs(BaseEdit editor, ClauseNode node, DevExpress.Data.Filtering.OperandValue value)
        {
            this.editor = editor;
            this.node = node;
            this.value = value;
        }

        public virtual BaseEdit Editor =>
            this.editor;

        public ClauseNode CurrentNode =>
            this.node;

        public DevExpress.Data.Filtering.OperandValue OperandValue =>
            this.value;

        public ClauseType Operation =>
            this.CurrentNode.Operation;

        public BaseEditSettings CustomEditSettings { get; set; }
    }
}

