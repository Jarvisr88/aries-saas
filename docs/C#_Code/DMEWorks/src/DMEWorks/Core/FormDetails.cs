namespace DMEWorks.Core
{
    using DMEWorks.Controls;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class FormDetails : DmeForm
    {
        private IContainer components;
        protected readonly ControlDetails F_Parent;
        private readonly DMEWorks.Controls.ChangesTracker m_changesTracker;
        private bool FChanged;
        private AllowStateEnum FAllowState;
        protected const string CrLf = "\r\n";
        private object FSelectedRowID;

        public FormDetails() : this(null)
        {
        }

        public FormDetails(ControlDetails Parent)
        {
            this.FAllowState = AllowStateEnum.AllowAll;
            this.InitializeComponent();
            this.F_Parent = Parent;
            this.m_changesTracker = new DMEWorks.Controls.ChangesTracker(new EventHandler(this.HandleControlChanged));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.PrivateValidateObject())
                {
                    this.Commit();
                    base.Hide();
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Error In input data");
                ProjectData.ClearProjectError();
            }
        }

        protected virtual void Clear()
        {
        }

        private static void ClearErrors(Control parent, ErrorProvider provider)
        {
            int num = parent.Controls.Count - 1;
            for (int i = 0; i <= num; i++)
            {
                Control control = parent.Controls[i];
                provider.SetError(control, "");
                ClearErrors(control, provider);
            }
        }

        protected internal bool Commit() => 
            Versioned.IsNumeric(this.PrivateCommitItem(this.FSelectedRowID));

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected internal bool Edit(object RowID)
        {
            this.FSelectedRowID = this.PrivateEditItem(RowID);
            return Versioned.IsNumeric(this.FSelectedRowID);
        }

        private void HandleControlChanged(object sender, EventArgs args)
        {
            this.FChanged = true;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormDetails));
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.ValidationErrors = new ErrorProvider(this.components);
            this.ValidationWarnings = new ErrorProvider(this.components);
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(280, 0xe8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x19);
            this.btnCancel.TabIndex = 0x23;
            this.btnCancel.Text = "Cancel";
            this.btnOK.Location = new Point(200, 0xe8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x48, 0x19);
            this.btnOK.TabIndex = 0x22;
            this.btnOK.Text = "OK";
            this.ValidationErrors.ContainerControl = this;
            this.ValidationWarnings.ContainerControl = this;
            this.ValidationWarnings.DataMember = "";
            this.ValidationWarnings.Icon = (Icon) manager.GetObject("ValidationWarnings.Icon");
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(360, 0x105);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "FormDetails";
            this.Text = "FormDetails";
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            ((ISupportInitialize) this.ValidationWarnings).EndInit();
            base.ResumeLayout(false);
        }

        public virtual void LoadComboBoxes()
        {
        }

        protected virtual void LoadFromRow(DataRow Row)
        {
        }

        private void PrivateClearValidationErrors()
        {
            ClearErrors(this, this.ValidationErrors);
            ClearErrors(this, this.ValidationWarnings);
        }

        private object PrivateCommitItem(object RowID)
        {
            object obj2;
            if ((this.AllowState & AllowStateEnum.AllowEdit) != AllowStateEnum.AllowNone)
            {
                if (base.Visible)
                {
                    if (this.F_Parent != null)
                    {
                        ControlDetails.TableDetails details = this.F_Parent.F_TableDetails;
                        details.BeginUpdate();
                        try
                        {
                            if (Versioned.IsNumeric(RowID))
                            {
                                DataRow[] rowArray = details.Select($"RowID = {Conversions.ToInteger(RowID)}");
                                if (rowArray.Length == 1)
                                {
                                    DataRow row2 = rowArray[0];
                                    this.SaveToRow(row2);
                                    this.F_Parent.EditingCompleted(row2);
                                    return row2[details.Col_RowID];
                                }
                            }
                            DataRow row = details.NewRow();
                            this.SaveToRow(row);
                            details.Rows.Add(row);
                            this.F_Parent.EditingCompleted(row);
                            obj2 = row[details.Col_RowID];
                        }
                        finally
                        {
                            details.EndUpdate();
                        }
                    }
                    else
                    {
                        obj2 = DBNull.Value;
                    }
                }
                else
                {
                    obj2 = DBNull.Value;
                }
            }
            else
            {
                obj2 = DBNull.Value;
            }
            return obj2;
        }

        private object PrivateEditItem(object RowID)
        {
            if (this.F_Parent != null)
            {
                ControlDetails.TableDetails details = this.F_Parent.F_TableDetails;
                if (Versioned.IsNumeric(RowID))
                {
                    DataRow[] rowArray = details.Select($"RowID = {Conversions.ToInteger(RowID)}");
                    if (rowArray.Length == 1)
                    {
                        this.LoadFromRow(rowArray[0]);
                        return Conversions.ToInteger(RowID);
                    }
                }
            }
            this.Clear();
            return DBNull.Value;
        }

        private bool PrivateValidateObject()
        {
            bool flag;
            this.PrivateClearValidationErrors();
            this.ValidateObject();
            StringBuilder builder = new StringBuilder("There are some errors in the input data");
            if (0 < Functions.EnumerateErrors(this, this.ValidationErrors, builder))
            {
                builder.Append("\r\n");
                builder.Append("\r\n");
                builder.Append("Cannot proceed.");
                MessageBox.Show(builder.ToString(), this.Text + " - validation errors", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            else
            {
                StringBuilder builder2 = new StringBuilder("There are some warnings in the input data");
                if (0 >= Functions.EnumerateErrors(this, this.ValidationWarnings, builder2))
                {
                    flag = true;
                }
                else
                {
                    builder2.Append("\r\n");
                    builder2.Append("\r\n");
                    builder2.Append("Do you want to proceed?");
                    flag = MessageBox.Show(builder2.ToString(), this.Text + " - validation warnings", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }
            }
            return flag;
        }

        protected virtual void SaveToRow(DataRow Row)
        {
        }

        protected virtual void ValidateObject()
        {
        }

        [field: AccessedThroughProperty("btnCancel")]
        protected virtual Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        protected virtual Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ValidationErrors")]
        protected virtual ErrorProvider ValidationErrors { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected DMEWorks.Controls.ChangesTracker ChangesTracker =>
            this.m_changesTracker;

        public virtual AllowStateEnum AllowState
        {
            get => 
                this.FAllowState;
            set => 
                this.FAllowState = value;
        }

        [field: AccessedThroughProperty("ValidationWarnings")]
        protected virtual ErrorProvider ValidationWarnings { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

