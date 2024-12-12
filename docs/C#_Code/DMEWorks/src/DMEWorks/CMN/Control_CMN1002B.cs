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

    public class Control_CMN1002B : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN1002B()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer7", DBNull.Value);
            this.set_Item("Answer8", DBNull.Value);
            this.set_Item("Answer10a", DBNull.Value);
            this.set_Item("Answer10b", DBNull.Value);
            this.set_Item("Answer11a", DBNull.Value);
            this.set_Item("Answer11b", DBNull.Value);
            this.set_Item("Answer12", DBNull.Value);
            this.set_Item("Answer13", DBNull.Value);
            this.set_Item("Answer14", DBNull.Value);
            this.set_Item("Answer15", DBNull.Value);
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
            this.pnlAnswers = new Panel();
            this.lblAnswer7 = new Label();
            this.rgAnswer14 = new RadioGroup();
            this.rgAnswer13 = new RadioGroup();
            this.nmbAnswer12 = new NumericBox();
            this.Panel4 = new Panel();
            this.Label9 = new Label();
            this.Label10 = new Label();
            this.txtAnswer11b = new TextBox();
            this.txtAnswer11a = new TextBox();
            this.Panel2 = new Panel();
            this.Label8 = new Label();
            this.Label7 = new Label();
            this.txtAnswer10b = new TextBox();
            this.txtAnswer10a = new TextBox();
            this.rgAnswer8 = new RadioGroup();
            this.rgAnswer7 = new RadioGroup();
            this.Label2 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions = new Panel();
            this.Panel1 = new Panel();
            this.Label6 = new Label();
            this.txtAnswer15 = new TextBox();
            this.Label5 = new Label();
            this.TextBox4 = new TextBox();
            this.Label4 = new Label();
            this.Label3 = new Label();
            this.Label1 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestion0 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.TextBox10 = new TextBox();
            this.pnlAnswers.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlQuestions.SuspendLayout();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.lblAnswer7);
            this.pnlAnswers.Controls.Add(this.rgAnswer14);
            this.pnlAnswers.Controls.Add(this.rgAnswer13);
            this.pnlAnswers.Controls.Add(this.nmbAnswer12);
            this.pnlAnswers.Controls.Add(this.Panel4);
            this.pnlAnswers.Controls.Add(this.Panel2);
            this.pnlAnswers.Controls.Add(this.rgAnswer8);
            this.pnlAnswers.Controls.Add(this.rgAnswer7);
            this.pnlAnswers.Controls.Add(this.Label2);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x98, 0x13c);
            this.pnlAnswers.TabIndex = 0;
            this.lblAnswer7.BackColor = Color.Transparent;
            this.lblAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswer7.Dock = DockStyle.Top;
            this.lblAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswer7.Location = new Point(0, 0x110);
            this.lblAnswer7.Name = "lblAnswer7";
            this.lblAnswer7.Size = new Size(0x98, 0x2c);
            this.lblAnswer7.TabIndex = 9;
            this.rgAnswer14.BackColor = SystemColors.Control;
            this.rgAnswer14.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer14.Dock = DockStyle.Top;
            this.rgAnswer14.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer14.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer14.Location = new Point(0, 0xfc);
            this.rgAnswer14.Name = "rgAnswer14";
            this.rgAnswer14.Size = new Size(0x98, 20);
            this.rgAnswer14.TabIndex = 8;
            this.rgAnswer14.Value = "";
            this.rgAnswer13.BackColor = SystemColors.Control;
            this.rgAnswer13.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer13.Dock = DockStyle.Top;
            this.rgAnswer13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer13.Items = new string[] { "1", "2", "3", "4" };
            this.rgAnswer13.Location = new Point(0, 220);
            this.rgAnswer13.Name = "rgAnswer13";
            this.rgAnswer13.Size = new Size(0x98, 0x20);
            this.rgAnswer13.TabIndex = 7;
            this.rgAnswer13.Value = "";
            this.nmbAnswer12.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer12.Dock = DockStyle.Top;
            this.nmbAnswer12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.nmbAnswer12.Location = new Point(0, 200);
            this.nmbAnswer12.Name = "nmbAnswer12";
            this.nmbAnswer12.Size = new Size(0x98, 20);
            this.nmbAnswer12.TabIndex = 6;
            this.nmbAnswer12.TextAlign = HorizontalAlignment.Left;
            this.Panel4.BorderStyle = BorderStyle.FixedSingle;
            this.Panel4.Controls.Add(this.Label9);
            this.Panel4.Controls.Add(this.Label10);
            this.Panel4.Controls.Add(this.txtAnswer11b);
            this.Panel4.Controls.Add(this.txtAnswer11a);
            this.Panel4.Dock = DockStyle.Top;
            this.Panel4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel4.Location = new Point(0, 0x98);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new Size(0x98, 0x30);
            this.Panel4.TabIndex = 5;
            this.Label9.BackColor = Color.Transparent;
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(4, 0x18);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(20, 20);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "B.";
            this.Label10.BackColor = Color.Transparent;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(4, 4);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(20, 20);
            this.Label10.TabIndex = 0;
            this.Label10.Text = "A.";
            this.txtAnswer11b.AutoSize = false;
            this.txtAnswer11b.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer11b.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnswer11b.Location = new Point(0x1c, 0x18);
            this.txtAnswer11b.Name = "txtAnswer11b";
            this.txtAnswer11b.Size = new Size(120, 20);
            this.txtAnswer11b.TabIndex = 3;
            this.txtAnswer11b.Text = "";
            this.txtAnswer11a.AutoSize = false;
            this.txtAnswer11a.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer11a.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnswer11a.Location = new Point(0x1c, 4);
            this.txtAnswer11a.Name = "txtAnswer11a";
            this.txtAnswer11a.Size = new Size(120, 20);
            this.txtAnswer11a.TabIndex = 1;
            this.txtAnswer11a.Text = "";
            this.Panel2.BorderStyle = BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.txtAnswer10b);
            this.Panel2.Controls.Add(this.txtAnswer10a);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel2.Location = new Point(0, 0x68);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(0x98, 0x30);
            this.Panel2.TabIndex = 4;
            this.Label8.BackColor = Color.Transparent;
            this.Label8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label8.Location = new Point(4, 0x18);
            this.Label8.Name = "Label8";
            this.Label8.Size = new Size(20, 0x13);
            this.Label8.TabIndex = 2;
            this.Label8.Text = "B.";
            this.Label7.BackColor = Color.Transparent;
            this.Label7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(4, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(20, 0x13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "A.";
            this.txtAnswer10b.AutoSize = false;
            this.txtAnswer10b.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer10b.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnswer10b.Location = new Point(0x1c, 0x18);
            this.txtAnswer10b.Name = "txtAnswer10b";
            this.txtAnswer10b.Size = new Size(120, 0x13);
            this.txtAnswer10b.TabIndex = 3;
            this.txtAnswer10b.Text = "";
            this.txtAnswer10a.AutoSize = false;
            this.txtAnswer10a.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer10a.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnswer10a.Location = new Point(0x1c, 4);
            this.txtAnswer10a.Name = "txtAnswer10a";
            this.txtAnswer10a.Size = new Size(120, 0x13);
            this.txtAnswer10a.TabIndex = 1;
            this.txtAnswer10a.Text = "";
            this.rgAnswer8.BackColor = SystemColors.Control;
            this.rgAnswer8.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer8.Dock = DockStyle.Top;
            this.rgAnswer8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer8.Items = new string[] { "Y", "N" };
            this.rgAnswer8.Location = new Point(0, 0x4c);
            this.rgAnswer8.Name = "rgAnswer8";
            this.rgAnswer8.Size = new Size(0x98, 0x1c);
            this.rgAnswer8.TabIndex = 3;
            this.rgAnswer8.Value = "";
            this.rgAnswer7.BackColor = SystemColors.Control;
            this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7.Dock = DockStyle.Top;
            this.rgAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer7.Items = new string[] { "Y", "N" };
            this.rgAnswer7.Location = new Point(0, 0x30);
            this.rgAnswer7.Name = "rgAnswer7";
            this.rgAnswer7.Size = new Size(0x98, 0x1c);
            this.rgAnswer7.TabIndex = 2;
            this.rgAnswer7.Value = "";
            this.Label2.BackColor = Color.Transparent;
            this.Label2.BorderStyle = BorderStyle.FixedSingle;
            this.Label2.Dock = DockStyle.Top;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0, 0x20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x98, 0x10);
            this.Label2.TabIndex = 1;
            this.Label2.TextAlign = ContentAlignment.TopCenter;
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x98, 0x20);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.Panel1);
            this.pnlQuestions.Controls.Add(this.Label5);
            this.pnlQuestions.Controls.Add(this.TextBox4);
            this.pnlQuestions.Controls.Add(this.Label4);
            this.pnlQuestions.Controls.Add(this.Label3);
            this.pnlQuestions.Controls.Add(this.Label1);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestion0);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Controls.Add(this.TextBox10);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlQuestions.Location = new Point(0x98, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x288, 0x13c);
            this.pnlQuestions.TabIndex = 1;
            this.Panel1.BackColor = Color.Transparent;
            this.Panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.txtAnswer15);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel1.Location = new Point(0, 0x110);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x288, 0x2c);
            this.Panel1.TabIndex = 9;
            this.Label6.BackColor = Color.Transparent;
            this.Label6.Dock = DockStyle.Top;
            this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label6.Location = new Point(0, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x286, 20);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "15. Additional information when required by policy:";
            this.txtAnswer15.AutoSize = false;
            this.txtAnswer15.BorderStyle = BorderStyle.None;
            this.txtAnswer15.Dock = DockStyle.Bottom;
            this.txtAnswer15.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnswer15.Location = new Point(0, 0x18);
            this.txtAnswer15.Name = "txtAnswer15";
            this.txtAnswer15.Size = new Size(0x286, 0x12);
            this.txtAnswer15.TabIndex = 1;
            this.txtAnswer15.Text = "";
            this.Label5.BackColor = Color.Transparent;
            this.Label5.BorderStyle = BorderStyle.FixedSingle;
            this.Label5.Dock = DockStyle.Top;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(0, 0xfc);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x288, 20);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "14.  Does the patient have a documented allergy or intolerance to semi-synthetic nutrients?";
            this.TextBox4.AutoSize = false;
            this.TextBox4.BackColor = SystemColors.Control;
            this.TextBox4.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox4.Dock = DockStyle.Top;
            this.TextBox4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox4.Location = new Point(0, 220);
            this.TextBox4.Multiline = true;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Size(0x288, 0x20);
            this.TextBox4.TabIndex = 7;
            this.TextBox4.Text = "13.  Circle the number for method of administration?\r\n              1 -Syringe   2 -Gravity   3 -Pump  4 -Does not apply";
            this.Label4.BackColor = Color.Transparent;
            this.Label4.BorderStyle = BorderStyle.FixedSingle;
            this.Label4.Dock = DockStyle.Top;
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0, 200);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x288, 20);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "12.  Days per week administered?  (Enter 1-7)";
            this.Label3.BackColor = Color.Transparent;
            this.Label3.BorderStyle = BorderStyle.FixedSingle;
            this.Label3.Dock = DockStyle.Top;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0, 0x98);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x288, 0x30);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "11.  Calories per day for each product?";
            this.Label1.BackColor = Color.Transparent;
            this.Label1.BorderStyle = BorderStyle.FixedSingle;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0x68);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x288, 0x30);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "10.  Print product name(s).";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x4c);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x288, 0x1c);
            this.lblQuestion1.TabIndex = 3;
            this.lblQuestion1.Text = "8.    Does the patient require tube feedings to provide sufficient nutrients to maintain weight and strength commensurate with the patient's overall health status?";
            this.lblQuestion0.BackColor = Color.Transparent;
            this.lblQuestion0.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion0.Dock = DockStyle.Top;
            this.lblQuestion0.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion0.Location = new Point(0, 0x30);
            this.lblQuestion0.Name = "lblQuestion0";
            this.lblQuestion0.Size = new Size(0x288, 0x1c);
            this.lblQuestion0.TabIndex = 2;
            this.lblQuestion0.Text = "7.    Does the patient have permanent non-function or disease of the structures that normally permit food to reach or be absorbed from the small bowel?";
            this.lblQuestionDescription1.BackColor = Color.Transparent;
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0x20);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x288, 0x10);
            this.lblQuestionDescription1.TabIndex = 1;
            this.lblQuestionDescription1.Text = "QUESTIONS  1 - 6, AND 9, ARE RESERVED FOR OTHER OR FUTURE USE.";
            this.TextBox10.AutoSize = false;
            this.TextBox10.BackColor = SystemColors.Control;
            this.TextBox10.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox10.Dock = DockStyle.Top;
            this.TextBox10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox10.Location = new Point(0, 0);
            this.TextBox10.Multiline = true;
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Size(0x288, 0x20);
            this.TextBox10.TabIndex = 0;
            this.TextBox10.Text = "ANSWER QUESTIONS 7, 8, AND 10 - 15 FOR ENTERAL NUTRITION\r\n(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN1002B";
            base.Size = new Size(800, 0x13c);
            this.pnlAnswers.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.pnlQuestions.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer7", reader["Answer7"]);
            this.set_Item("Answer8", reader["Answer8"]);
            this.set_Item("Answer10a", reader["Answer10a"]);
            this.set_Item("Answer10b", reader["Answer10b"]);
            this.set_Item("Answer11a", reader["Answer11a"]);
            this.set_Item("Answer11b", reader["Answer11b"]);
            this.set_Item("Answer12", reader["Answer12"]);
            this.set_Item("Answer13", reader["Answer13"]);
            this.set_Item("Answer14", reader["Answer14"]);
            this.set_Item("Answer15", reader["Answer15"]);
        }

        private void nmbAnswer12_ValueChanged(object sender, EventArgs e)
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
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 5).Value = this.get_Item("Answer7");
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 5).Value = this.get_Item("Answer8");
            cmd.Parameters.Add("Answer10a", MySqlType.VarChar, 50).Value = this.get_Item("Answer10a");
            cmd.Parameters.Add("Answer10b", MySqlType.VarChar, 50).Value = this.get_Item("Answer10b");
            cmd.Parameters.Add("Answer11a", MySqlType.VarChar, 50).Value = this.get_Item("Answer11a");
            cmd.Parameters.Add("Answer11b", MySqlType.VarChar, 50).Value = this.get_Item("Answer11b");
            cmd.Parameters.Add("Answer12", MySqlType.Int).Value = this.get_Item("Answer12");
            cmd.Parameters.Add("Answer13", MySqlType.VarChar, 5).Value = this.get_Item("Answer13");
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 5).Value = this.get_Item("Answer14");
            cmd.Parameters.Add("Answer15", MySqlType.VarChar, 50).Value = this.get_Item("Answer15");
        }

        private void txtAnswer10a_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer10b_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer11a_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer11b_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer15_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswer7")]
        private Label lblAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox4")]
        private TextBox TextBox4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion0")]
        private Label lblQuestion0 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription1")]
        private Label lblQuestionDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox10")]
        private TextBox TextBox10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel2")]
        private Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label8")]
        private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel4")]
        private Panel Panel4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label9")]
        private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label10")]
        private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label6")]
        private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer14")]
        private RadioGroup rgAnswer14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer13")]
        private RadioGroup rgAnswer13 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer11b")]
        private TextBox txtAnswer11b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer11a")]
        private TextBox txtAnswer11a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer10b")]
        private TextBox txtAnswer10b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer10a")]
        private TextBox txtAnswer10a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer8")]
        private RadioGroup rgAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7")]
        private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer15")]
        private TextBox txtAnswer15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer12")]
        private NumericBox nmbAnswer12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_1002B;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object text;
            if (string.Compare(Index, "Answer7", true) == 0)
            {
                text = this.rgAnswer7.Value;
            }
            else if (string.Compare(Index, "Answer8", true) == 0)
            {
                text = this.rgAnswer8.Value;
            }
            else if (string.Compare(Index, "Answer13", true) == 0)
            {
                text = this.rgAnswer13.Value;
            }
            else if (string.Compare(Index, "Answer14", true) == 0)
            {
                text = this.rgAnswer14.Value;
            }
            else if (string.Compare(Index, "Answer10a", true) == 0)
            {
                text = this.txtAnswer10a.Text;
            }
            else if (string.Compare(Index, "Answer10b", true) == 0)
            {
                text = this.txtAnswer10b.Text;
            }
            else if (string.Compare(Index, "Answer11a", true) == 0)
            {
                text = this.txtAnswer11a.Text;
            }
            else if (string.Compare(Index, "Answer11b", true) == 0)
            {
                text = this.txtAnswer11b.Text;
            }
            else if (string.Compare(Index, "Answer12", true) == 0)
            {
                text = this.nmbAnswer12.AsInt32.GetValueOrDefault(0);
            }
            else
            {
                if (string.Compare(Index, "Answer15", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                text = this.txtAnswer15.Text;
            }
            return text;
        }

        // Warning: Properties with arguments are not supported in C#. Setter of a Item property was decompiled as a method.
        protected void set_Item(string Index, object Value)
        {
            if (string.Compare(Index, "Answer7", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer7, Value);
            }
            else if (string.Compare(Index, "Answer8", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer8, Value);
            }
            else if (string.Compare(Index, "Answer13", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer13, Value);
            }
            else if (string.Compare(Index, "Answer14", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer14, Value);
            }
            else if (string.Compare(Index, "Answer10a", true) == 0)
            {
                this.txtAnswer10a.Text = NullableConvert.ToString(Value);
            }
            else if (string.Compare(Index, "Answer10b", true) == 0)
            {
                this.txtAnswer10b.Text = NullableConvert.ToString(Value);
            }
            else if (string.Compare(Index, "Answer11a", true) == 0)
            {
                this.txtAnswer11a.Text = NullableConvert.ToString(Value);
            }
            else if (string.Compare(Index, "Answer11b", true) == 0)
            {
                this.txtAnswer11b.Text = NullableConvert.ToString(Value);
            }
            else if (string.Compare(Index, "Answer12", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer12, Value);
            }
            else if (string.Compare(Index, "Answer15", true) == 0)
            {
                this.txtAnswer15.Text = NullableConvert.ToString(Value);
            }
        }

    }
}

