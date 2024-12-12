namespace DMEWorks.CMN
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Forms;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Control_CMN0702B : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0702B()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer6", DBNull.Value);
            this.set_Item("Answer7", DBNull.Value);
            this.set_Item("Answer8", DBNull.Value);
            this.set_Item("Answer12", DBNull.Value);
            this.set_Item("Answer13", DBNull.Value);
            this.set_Item("Answer14", DBNull.Value);
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
            this.pnlQuestions = new Panel();
            this.Label3 = new Label();
            this.lblQuestion5 = new Label();
            this.lblQuestion4 = new Label();
            this.lblQuestion3 = new Label();
            this.lblQuestion2 = new Label();
            this.lblQuestion1 = new Label();
            this.Label2 = new Label();
            this.TextBox1 = new TextBox();
            this.pnlAnswers = new Panel();
            this.rgAnswer14 = new RadioGroup();
            this.rgAnswer13 = new RadioGroup();
            this.rgAnswer12 = new RadioGroup();
            this.rgAnswer8 = new RadioGroup();
            this.rgAnswer7 = new RadioGroup();
            this.rgAnswer6 = new RadioGroup();
            this.Label1 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.Label3);
            this.pnlQuestions.Controls.Add(this.lblQuestion5);
            this.pnlQuestions.Controls.Add(this.lblQuestion4);
            this.pnlQuestions.Controls.Add(this.lblQuestion3);
            this.pnlQuestions.Controls.Add(this.lblQuestion2);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.Label2);
            this.pnlQuestions.Controls.Add(this.TextBox1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x70, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x2b0, 0xbc);
            this.pnlQuestions.TabIndex = 1;
            this.Label3.BackColor = Color.Transparent;
            this.Label3.BorderStyle = BorderStyle.FixedSingle;
            this.Label3.Dock = DockStyle.Top;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0, 160);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x2b0, 0x1c);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "14.  Does the patient's physical condition prevent a visit to a specialist in physical medicine, orthopedic surgery, neurology, or rheumatology?";
            this.lblQuestion5.BackColor = Color.Transparent;
            this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion5.Dock = DockStyle.Top;
            this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion5.Location = new Point(0, 0x84);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new Size(0x2b0, 0x1c);
            this.lblQuestion5.TabIndex = 6;
            this.lblQuestion5.Text = "13.  Is the patient more than one day's round trip from a specialist in physical medicine, orthopedic surgery, neurology, or rheumatology?";
            this.lblQuestion4.BackColor = Color.Transparent;
            this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion4.Dock = DockStyle.Top;
            this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion4.Location = new Point(0, 0x70);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new Size(0x2b0, 20);
            this.lblQuestion4.TabIndex = 5;
            this.lblQuestion4.Text = "12.  Is the physician signing this form a specialist in  physical medicine, orthopedic surgery, neurology, or rheumatology?";
            this.lblQuestion3.BackColor = Color.Transparent;
            this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion3.Dock = DockStyle.Top;
            this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion3.Location = new Point(0, 0x5c);
            this.lblQuestion3.Name = "lblQuestion3";
            this.lblQuestion3.Size = new Size(0x2b0, 20);
            this.lblQuestion3.TabIndex = 4;
            this.lblQuestion3.Text = "8.    Does the patient require a POV only for movement outside their residence?";
            this.lblQuestion2.BackColor = Color.Transparent;
            this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion2.Dock = DockStyle.Top;
            this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion2.Location = new Point(0, 0x48);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new Size(0x2b0, 20);
            this.lblQuestion2.TabIndex = 3;
            this.lblQuestion2.Text = "7.    Have all types of manual wheelchairs (including lightweights) been considered and ruled out?";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x34);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x2b0, 20);
            this.lblQuestion1.TabIndex = 2;
            this.lblQuestion1.Text = "6.    Does the patient require a POV to move around in their residence?";
            this.Label2.BackColor = Color.Transparent;
            this.Label2.BorderStyle = BorderStyle.FixedSingle;
            this.Label2.Dock = DockStyle.Top;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0, 0x20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x2b0, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "QUESTIONS 1 - 5, AND 9 - 11, ARE RESERVED FOR OTHER OR FUTURE USE.";
            this.TextBox1.AutoSize = false;
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
            this.TextBox1.Text = "ANSWER QUESTIONS 6 - 14 FOR POWER OPERATED VEHICLE (POV)\r\n(Circle Y for Yes, N for No, or D for Does Not Apply)";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer14);
            this.pnlAnswers.Controls.Add(this.rgAnswer13);
            this.pnlAnswers.Controls.Add(this.rgAnswer12);
            this.pnlAnswers.Controls.Add(this.rgAnswer8);
            this.pnlAnswers.Controls.Add(this.rgAnswer7);
            this.pnlAnswers.Controls.Add(this.rgAnswer6);
            this.pnlAnswers.Controls.Add(this.Label1);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x70, 0xbc);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer14.BackColor = SystemColors.Control;
            this.rgAnswer14.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer14.Dock = DockStyle.Top;
            this.rgAnswer14.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer14.Location = new Point(0, 160);
            this.rgAnswer14.Name = "rgAnswer14";
            this.rgAnswer14.Size = new Size(0x70, 0x1c);
            this.rgAnswer14.TabIndex = 7;
            this.rgAnswer14.Value = "";
            this.rgAnswer13.BackColor = SystemColors.Control;
            this.rgAnswer13.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer13.Dock = DockStyle.Top;
            this.rgAnswer13.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer13.Location = new Point(0, 0x84);
            this.rgAnswer13.Name = "rgAnswer13";
            this.rgAnswer13.Size = new Size(0x70, 0x1c);
            this.rgAnswer13.TabIndex = 6;
            this.rgAnswer13.Value = "";
            this.rgAnswer12.BackColor = SystemColors.Control;
            this.rgAnswer12.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer12.Dock = DockStyle.Top;
            this.rgAnswer12.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer12.Location = new Point(0, 0x70);
            this.rgAnswer12.Name = "rgAnswer12";
            this.rgAnswer12.Size = new Size(0x70, 20);
            this.rgAnswer12.TabIndex = 5;
            this.rgAnswer12.Value = "";
            this.rgAnswer8.BackColor = SystemColors.Control;
            this.rgAnswer8.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer8.Dock = DockStyle.Top;
            this.rgAnswer8.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer8.Location = new Point(0, 0x5c);
            this.rgAnswer8.Name = "rgAnswer8";
            this.rgAnswer8.Size = new Size(0x70, 20);
            this.rgAnswer8.TabIndex = 4;
            this.rgAnswer8.Value = "";
            this.rgAnswer7.BackColor = SystemColors.Control;
            this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7.Dock = DockStyle.Top;
            this.rgAnswer7.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer7.Location = new Point(0, 0x48);
            this.rgAnswer7.Name = "rgAnswer7";
            this.rgAnswer7.Size = new Size(0x70, 20);
            this.rgAnswer7.TabIndex = 3;
            this.rgAnswer7.Value = "";
            this.rgAnswer6.BackColor = SystemColors.Control;
            this.rgAnswer6.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer6.Dock = DockStyle.Top;
            this.rgAnswer6.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer6.Location = new Point(0, 0x34);
            this.rgAnswer6.Name = "rgAnswer6";
            this.rgAnswer6.Size = new Size(0x70, 20);
            this.rgAnswer6.TabIndex = 2;
            this.rgAnswer6.Value = "";
            this.Label1.BackColor = Color.Transparent;
            this.Label1.BorderStyle = BorderStyle.FixedSingle;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0x20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x70, 20);
            this.Label1.TabIndex = 1;
            this.Label1.TextAlign = ContentAlignment.TopCenter;
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
            base.Name = "Control_CMN0702B";
            base.Size = new Size(800, 0xbc);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer6", reader["Answer6"]);
            this.set_Item("Answer7", reader["Answer7"]);
            this.set_Item("Answer8", reader["Answer8"]);
            this.set_Item("Answer12", reader["Answer12"]);
            this.set_Item("Answer13", reader["Answer13"]);
            this.set_Item("Answer14", reader["Answer14"]);
        }

        private void rgAnswer12_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer13_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer14_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer6_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer7_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer8_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 5).Value = this.get_Item("Answer6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 5).Value = this.get_Item("Answer7");
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 5).Value = this.get_Item("Answer8");
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 5).Value = this.get_Item("Answer12");
            cmd.Parameters.Add("Answer13", MySqlType.VarChar, 5).Value = this.get_Item("Answer13");
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 5).Value = this.get_Item("Answer14");
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox1")]
        private TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer14")]
        private RadioGroup rgAnswer14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer13")]
        private RadioGroup rgAnswer13 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer12")]
        private RadioGroup rgAnswer12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer8")]
        private RadioGroup rgAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7")]
        private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer6")]
        private RadioGroup rgAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0702B;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object obj2;
            if (string.Compare(Index, "Answer12", true) == 0)
            {
                obj2 = this.rgAnswer12.Value;
            }
            else if (string.Compare(Index, "Answer13", true) == 0)
            {
                obj2 = this.rgAnswer13.Value;
            }
            else if (string.Compare(Index, "Answer14", true) == 0)
            {
                obj2 = this.rgAnswer14.Value;
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                obj2 = this.rgAnswer6.Value;
            }
            else if (string.Compare(Index, "Answer7", true) == 0)
            {
                obj2 = this.rgAnswer7.Value;
            }
            else
            {
                if (string.Compare(Index, "Answer8", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                obj2 = this.rgAnswer8.Value;
            }
            return obj2;
        }

        // Warning: Properties with arguments are not supported in C#. Setter of a Item property was decompiled as a method.
        protected void set_Item(string Index, object Value)
        {
            if (string.Compare(Index, "Answer12", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer12, Value);
            }
            else if (string.Compare(Index, "Answer13", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer13, Value);
            }
            else if (string.Compare(Index, "Answer14", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer14, Value);
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer6, Value);
            }
            else if (string.Compare(Index, "Answer7", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer7, Value);
            }
            else if (string.Compare(Index, "Answer8", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer8, Value);
            }
        }

    }
}

