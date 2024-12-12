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

    public class Control_CMNURO : Control_CMNBase
    {
        private IContainer components;

        public Control_CMNURO()
        {
            base.Layout += new LayoutEventHandler(this.Control_CMNDRORDER_Layout);
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.txtPrognosis.Text = "";
        }

        private void Control_CMNDRORDER_Layout(object sender, LayoutEventArgs e)
        {
            this.lblPrognosis.Location = new Point(8, 8);
            this.lblPrognosis.Size = new Size(0x48, 0x15);
            this.txtPrognosis.Location = new Point(0x58, 8);
            this.txtPrognosis.Size = new Size(Math.Max((base.Width - this.txtPrognosis.Left) - 8, 100), 0x15);
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
            this.txtPrognosis = new TextBox();
            this.Panel1 = new Panel();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblPrognosis.BackColor = Color.Transparent;
            this.lblPrognosis.Location = new Point(8, 8);
            this.lblPrognosis.Name = "lblPrognosis";
            this.lblPrognosis.Size = new Size(0x48, 0x15);
            this.lblPrognosis.TabIndex = 0;
            this.lblPrognosis.Text = "Prognosis";
            this.lblPrognosis.TextAlign = ContentAlignment.TopRight;
            this.txtPrognosis.AutoSize = false;
            this.txtPrognosis.BorderStyle = BorderStyle.FixedSingle;
            this.txtPrognosis.Location = new Point(0x58, 8);
            this.txtPrognosis.Name = "txtPrognosis";
            this.txtPrognosis.Size = new Size(0x2c0, 20);
            this.txtPrognosis.TabIndex = 2;
            this.txtPrognosis.Text = "";
            this.Panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.lblPrognosis);
            this.Panel1.Controls.Add(this.txtPrognosis);
            this.Panel1.Dock = DockStyle.Fill;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(800, 40);
            this.Panel1.TabIndex = 3;
            base.Controls.Add(this.Panel1);
            base.Name = "Control_CMNURO";
            base.Size = new Size(800, 40);
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.txtPrognosis.Text = NullableConvert.ToString(reader["Prognosis"]);
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Prognosis", MySqlType.VarChar, 50).Value = this.txtPrognosis.Text;
        }

        private void txtPrognosis_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("lblPrognosis")]
        private Label lblPrognosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPrognosis")]
        private TextBox txtPrognosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_URO;
    }
}

