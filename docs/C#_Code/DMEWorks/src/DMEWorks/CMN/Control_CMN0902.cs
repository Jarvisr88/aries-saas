namespace DMEWorks.CMN
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Control_CMN0902 : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0902()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer1", DBNull.Value);
            this.set_Item("Answer2", DBNull.Value);
            this.set_Item("Answer3", DBNull.Value);
            this.set_Item("Answer4", DBNull.Value);
            this.set_Item("Answer5", DBNull.Value);
            this.set_Item("Answer6", DBNull.Value);
            this.set_Item("Answer7", DBNull.Value);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_CMN0902));
            this.pnlQuestions = new Panel();
            this.Label3 = new Label();
            this.lblQuestion5 = new Label();
            this.lblQuestion4 = new Label();
            this.lblQuestion3 = new Label();
            this.Label2 = new Label();
            this.lblQuestion2 = new Label();
            this.lblQuestion1 = new Label();
            this.TextBox1 = new TextBox();
            this.pnlAnswers = new Panel();
            this.rgAnswer7 = new RadioGroup();
            this.nmbAnswer6 = new NumericBox();
            this.rgAnswer5 = new RadioGroup();
            this.rgAnswer4 = new RadioGroup();
            this.txtAnswer3 = new TextBox();
            this.Panel1 = new Panel();
            this.Label1 = new Label();
            this.txtAnswer2 = new TextBox();
            this.rgAnswer1 = new RadioGroup();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.Label3);
            this.pnlQuestions.Controls.Add(this.lblQuestion5);
            this.pnlQuestions.Controls.Add(this.lblQuestion4);
            this.pnlQuestions.Controls.Add(this.lblQuestion3);
            this.pnlQuestions.Controls.Add(this.Label2);
            this.pnlQuestions.Controls.Add(this.lblQuestion2);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.TextBox1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x70, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x2b0, 0xd4);
            this.pnlQuestions.TabIndex = 1;
            this.Label3.BackColor = Color.Transparent;
            this.Label3.BorderStyle = BorderStyle.FixedSingle;
            this.Label3.Dock = DockStyle.Top;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0, 180);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x2b0, 0x20);
            this.Label3.TabIndex = 7;
            this.Label3.Text = manager.GetString("Label3.Text");
            this.lblQuestion5.BackColor = Color.Transparent;
            this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion5.Dock = DockStyle.Top;
            this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion5.Location = new Point(0, 160);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new Size(0x2b0, 20);
            this.lblQuestion5.TabIndex = 6;
            this.lblQuestion5.Text = "6.  What is the total duration of drug infusion per 24 hours? (1 - 24)";
            this.lblQuestion4.BackColor = Color.Transparent;
            this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion4.Dock = DockStyle.Top;
            this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion4.Location = new Point(0, 140);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new Size(0x2b0, 20);
            this.lblQuestion4.TabIndex = 5;
            this.lblQuestion4.Text = "5.  Circle number for method of adminstration?  1 - Continuous; 2 - Intermittent; 3 – Bolus";
            this.lblQuestion3.BackColor = Color.Transparent;
            this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion3.Dock = DockStyle.Top;
            this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion3.Location = new Point(0, 120);
            this.lblQuestion3.Name = "lblQuestion3";
            this.lblQuestion3.Size = new Size(0x2b0, 20);
            this.lblQuestion3.TabIndex = 4;
            this.lblQuestion3.Text = "4.  Circle number for route of administration?  1 - Intravenous;  2 - Reserved for other or future use;  3 - Epidural;  4 - Subcutaneous";
            this.Label2.BackColor = Color.Transparent;
            this.Label2.BorderStyle = BorderStyle.FixedSingle;
            this.Label2.Dock = DockStyle.Top;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0, 100);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x2b0, 20);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "3.  If non-specific code was used to answer questions, print name of drug.";
            this.lblQuestion2.BackColor = Color.Transparent;
            this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion2.Dock = DockStyle.Top;
            this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion2.Location = new Point(0, 0x40);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new Size(0x2b0, 0x24);
            this.lblQuestion2.TabIndex = 2;
            this.lblQuestion2.Text = "2.  Provide the HCPCS code for the drug that requires the use of the pump.";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x20);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x2b0, 0x20);
            this.lblQuestion1.TabIndex = 1;
            this.lblQuestion1.Text = manager.GetString("lblQuestion1.Text");
            this.TextBox1.BackColor = SystemColors.Control;
            this.TextBox1.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox1.Dock = DockStyle.Top;
            this.TextBox1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox1.Location = new Point(0, 0);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new Size(0x2b0, 0x20);
            this.TextBox1.TabIndex = 0;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "ANSWER QUESTIONS 1 - 7 FOR EXTERNAL INFUSION PUMP\r\n(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer7);
            this.pnlAnswers.Controls.Add(this.nmbAnswer6);
            this.pnlAnswers.Controls.Add(this.rgAnswer5);
            this.pnlAnswers.Controls.Add(this.rgAnswer4);
            this.pnlAnswers.Controls.Add(this.txtAnswer3);
            this.pnlAnswers.Controls.Add(this.Panel1);
            this.pnlAnswers.Controls.Add(this.rgAnswer1);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x70, 0xd4);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer7.BackColor = SystemColors.Control;
            this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7.Dock = DockStyle.Top;
            this.rgAnswer7.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer7.Location = new Point(0, 180);
            this.rgAnswer7.Name = "rgAnswer7";
            this.rgAnswer7.Size = new Size(0x70, 0x20);
            this.rgAnswer7.TabIndex = 7;
            this.nmbAnswer6.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer6.Dock = DockStyle.Top;
            this.nmbAnswer6.Location = new Point(0, 160);
            this.nmbAnswer6.Name = "nmbAnswer6";
            this.nmbAnswer6.Size = new Size(0x70, 20);
            this.nmbAnswer6.TabIndex = 6;
            this.nmbAnswer6.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer5.BackColor = SystemColors.Control;
            this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer5.Dock = DockStyle.Top;
            this.rgAnswer5.Items = new string[] { "1", "2", "3" };
            this.rgAnswer5.Location = new Point(0, 140);
            this.rgAnswer5.Name = "rgAnswer5";
            this.rgAnswer5.Size = new Size(0x70, 20);
            this.rgAnswer5.TabIndex = 5;
            this.rgAnswer4.BackColor = SystemColors.Control;
            this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer4.Dock = DockStyle.Top;
            this.rgAnswer4.Items = new string[] { "1", "3", "4" };
            this.rgAnswer4.Location = new Point(0, 120);
            this.rgAnswer4.Name = "rgAnswer4";
            this.rgAnswer4.Size = new Size(0x70, 20);
            this.rgAnswer4.TabIndex = 4;
            this.txtAnswer3.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer3.Dock = DockStyle.Top;
            this.txtAnswer3.Location = new Point(0, 100);
            this.txtAnswer3.Name = "txtAnswer3";
            this.txtAnswer3.Size = new Size(0x70, 20);
            this.txtAnswer3.TabIndex = 3;
            this.Panel1.BackColor = Color.Transparent;
            this.Panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.txtAnswer2);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0x40);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x70, 0x24);
            this.Panel1.TabIndex = 2;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.Dock = DockStyle.Fill;
            this.Label1.Location = new Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(110, 0x10);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "HCPCS CODE:";
            this.Label1.TextAlign = ContentAlignment.MiddleCenter;
            this.txtAnswer2.BorderStyle = BorderStyle.None;
            this.txtAnswer2.Dock = DockStyle.Bottom;
            this.txtAnswer2.Location = new Point(0, 0x10);
            this.txtAnswer2.Name = "txtAnswer2";
            this.txtAnswer2.Size = new Size(110, 0x12);
            this.txtAnswer2.TabIndex = 1;
            this.rgAnswer1.BackColor = SystemColors.Control;
            this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer1.Dock = DockStyle.Top;
            this.rgAnswer1.Items = new string[] { "1", "3", "4" };
            this.rgAnswer1.Location = new Point(0, 0x20);
            this.rgAnswer1.Name = "rgAnswer1";
            this.rgAnswer1.Size = new Size(0x70, 0x20);
            this.rgAnswer1.TabIndex = 1;
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x70, 0x20);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0902";
            base.Size = new Size(800, 0xd4);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer1", reader["Answer1"]);
            this.set_Item("Answer2", reader["Answer2"]);
            this.set_Item("Answer3", reader["Answer3"]);
            this.set_Item("Answer4", reader["Answer4"]);
            this.set_Item("Answer5", reader["Answer5"]);
            this.set_Item("Answer6", reader["Answer6"]);
            this.set_Item("Answer7", reader["Answer7"]);
        }

        private void nmbAnswer6_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer1_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer4_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer5_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer7_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 5).Value = this.get_Item("Answer1");
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 50).Value = this.get_Item("Answer2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 50).Value = this.get_Item("Answer3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.get_Item("Answer4");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.get_Item("Answer5");
            cmd.Parameters.Add("Answer6", MySqlType.Int, 0).Value = this.get_Item("Answer6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 5).Value = this.get_Item("Answer7");
        }

        private void txtAnswer2_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer3_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion5")]
        private Label lblQuestion5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion4")]
        private Label lblQuestion4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion3")]
        private Label lblQuestion3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion2")]
        private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox1")]
        private TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7")]
        private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer5")]
        private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer4")]
        private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer3")]
        private TextBox txtAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer2")]
        private TextBox txtAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer1")]
        private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer6")]
        private NumericBox nmbAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0902;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object text;
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                text = this.rgAnswer1.Value;
            }
            else if (string.Compare(Index, "Answer2", true) == 0)
            {
                text = this.txtAnswer2.Text;
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                text = this.txtAnswer3.Text;
            }
            else if (string.Compare(Index, "Answer4", true) == 0)
            {
                text = this.rgAnswer4.Value;
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                text = this.rgAnswer5.Value;
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                text = this.nmbAnswer6.AsInt32.GetValueOrDefault(0);
            }
            else
            {
                if (string.Compare(Index, "Answer7", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                text = this.rgAnswer7.Value;
            }
            return text;
        }

        // Warning: Properties with arguments are not supported in C#. Setter of a Item property was decompiled as a method.
        protected void set_Item(string Index, object Value)
        {
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer1, Value);
            }
            else if (string.Compare(Index, "Answer2", true) == 0)
            {
                this.txtAnswer2.Text = NullableConvert.ToString(Value);
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                this.txtAnswer3.Text = NullableConvert.ToString(Value);
            }
            else if (string.Compare(Index, "Answer4", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer4, Value);
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer5, Value);
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer6, Value);
            }
            else if (string.Compare(Index, "Answer7", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer7, Value);
            }
        }

    }
}

