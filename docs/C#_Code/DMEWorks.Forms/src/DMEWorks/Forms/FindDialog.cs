namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FindDialog : Component
    {
        private static readonly object EVENT_INITDIALOG = new object();
        private FormSelector _dialog;

        public event InitDialogEventHandler InitDialog
        {
            add
            {
                base.Events.AddHandler(EVENT_INITDIALOG, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_INITDIALOG, value);
            }
        }

        protected virtual void OnInitDialog(InitDialogEventArgs e)
        {
            InitDialogEventHandler handler = (InitDialogEventHandler) base.Events[EVENT_INITDIALOG];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public DialogResult ShowDialog()
        {
            DataTable dataSource = this.DataSource;
            if (dataSource == null)
            {
                return DialogResult.Cancel;
            }
            if (this._dialog == null)
            {
                this._dialog = new FormSelector();
                this._dialog.StartPosition = FormStartPosition.CenterScreen;
            }
            DataTableGridSource source = this._dialog.DataSource as DataTableGridSource;
            if ((source == null) || !ReferenceEquals(source.Table, dataSource))
            {
                this._dialog.DataSource = dataSource.ToGridSource();
                InitDialogEventArgs e = new InitDialogEventArgs(this._dialog.GridAppearance) {
                    Caption = this._dialog.Text
                };
                this.OnInitDialog(e);
                this._dialog.Text = e.Caption;
            }
            this._dialog.ClearFilter();
            DataRow selectedRow = this.SelectedRow;
            this._dialog.SetSelectedRow(r => ReferenceEquals(r.GetDataRow(), selectedRow));
            this._dialog.SelectFilter();
            DialogResult result1 = this._dialog.ShowDialog();
            if (result1 == DialogResult.OK)
            {
                this.SelectedRow = this._dialog.SelectedRow.GetDataRow();
            }
            return result1;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DataRow SelectedRow { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DataTable DataSource { get; set; }
    }
}

