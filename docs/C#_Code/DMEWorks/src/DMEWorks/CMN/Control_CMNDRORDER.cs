namespace DMEWorks.CMN
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Control_CMNDRORDER : Control_CMNBase
    {
        private IContainer components;

        public Control_CMNDRORDER()
        {
            base.Layout += new LayoutEventHandler(this.Control_CMNDRORDER_Layout);
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.txtPrognosis.Text = "";
            this.txtMedicalJustification.Text = "";
        }

        private void Control_CMNDRORDER_Layout(object sender, LayoutEventArgs e)
        {
            this.lblPrognosis.Location = new Point(8, 8);
            this.lblPrognosis.Size = new Size(0x48, 0x15);
            this.txtPrognosis.Location = new Point(0x58, 8);
            this.txtPrognosis.Size = new Size(Math.Max((base.Width - this.txtPrognosis.Left) - 8, 100), 0x15);
            this.lblMedicalJustification.Location = new Point(8, 0x20);
            this.lblMedicalJustification.Size = new Size(0x48, 0x1d);
            this.txtMedicalJustification.Location = new Point(0x58, 0x20);
            this.txtMedicalJustification.Size = new Size(Math.Max((base.Width - this.txtMedicalJustification.Left) - 8, 100), Math.Max((base.Height - this.txtMedicalJustification.Top) - 8, 100));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblPrognosis = new Label();
            this.lblMedicalJustification = new Label();
            this.txtPrognosis = new TextBox();
            this.txtMedicalJustification = new TextBox();
            this.Panel1 = new Panel();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblPrognosis.BackColor = Color.Transparent;
            this.lblPrognosis.Location = new Point(8, 8);
            this.lblPrognosis.Name = "lblPrognosis";
            this.lblPrognosis.Size = new Size(0x48, 0x15);
            this.lblPrognosis.TabIndex = 0;
            this.lblPrognosis.Text = "Prognosis";
            this.lblPrognosis.TextAlign = ContentAlignment.MiddleRight;
            this.lblMedicalJustification.BackColor = Color.Transparent;
            this.lblMedicalJustification.Location = new Point(8, 0x20);
            this.lblMedicalJustification.Name = "lblMedicalJustification";
            this.lblMedicalJustification.Size = new Size(0x48, 0x1d);
            this.lblMedicalJustification.TabIndex = 1;
            this.lblMedicalJustification.Text = "Medical Justification";
            this.lblMedicalJustification.TextAlign = ContentAlignment.MiddleRight;
            this.txtPrognosis.AutoSize = false;
            this.txtPrognosis.BorderStyle = BorderStyle.FixedSingle;
            this.txtPrognosis.Location = new Point(0x58, 8);
            this.txtPrognosis.Name = "txtPrognosis";
            this.txtPrognosis.Size = new Size(0x2c0, 20);
            this.txtPrognosis.TabIndex = 2;
            this.txtPrognosis.Text = "";
            this.txtMedicalJustification.AutoSize = false;
            this.txtMedicalJustification.BorderStyle = BorderStyle.FixedSingle;
            this.txtMedicalJustification.Location = new Point(0x58, 0x20);
            this.txtMedicalJustification.Multiline = true;
            this.txtMedicalJustification.Name = "txtMedicalJustification";
            this.txtMedicalJustification.Size = new Size(0x2c0, 0x98);
            this.txtMedicalJustification.TabIndex = 3;
            this.txtMedicalJustification.Text = "";
            this.Panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.lblMedicalJustification);
            this.Panel1.Controls.Add(this.txtMedicalJustification);
            this.Panel1.Controls.Add(this.lblPrognosis);
            this.Panel1.Controls.Add(this.txtPrognosis);
            this.Panel1.Dock = DockStyle.Fill;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(800, 0xc0);
            this.Panel1.TabIndex = 4;
            base.Controls.Add(this.Panel1);
            base.Name = "Control_CMNDRORDER";
            base.Size = new Size(800, 0xc0);
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.txtPrognosis.Text = NullableConvert.ToString(reader["Prognosis"]);
            this.txtMedicalJustification.Text = NullableConvert.ToString(reader["MedicalJustification"]);
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("MedicalJustification", MySqlType.Text, 0x7fffffff).Value = this.txtMedicalJustification.Text;
            cmd.Parameters.Add("Prognosis", MySqlType.VarChar, 50).Value = this.txtPrognosis.Text;
        }

        private void txtMedicalJustification_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtPrognosis_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("lblPrognosis")]
        private Label lblPrognosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMedicalJustification")]
        private Label lblMedicalJustification { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPrognosis")]
        private TextBox txtPrognosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMedicalJustification")]
        private TextBox txtMedicalJustification { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_DRORDER;
    }
}

