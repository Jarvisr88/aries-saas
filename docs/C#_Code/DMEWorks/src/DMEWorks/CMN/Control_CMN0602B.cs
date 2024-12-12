namespace DMEWorks.CMN
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Control_CMN0602B : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0602B()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer1", DBNull.Value);
            this.set_Item("Answer2", DBNull.Value);
            this.set_Item("Answer3", DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer4, DBNull.Value);
            this.set_Item("Answer5", DBNull.Value);
            this.set_Item("Answer6", DBNull.Value);
            this.set_Item("Answer7", DBNull.Value);
            this.set_Item("Answer8_begun", DBNull.Value);
            this.set_Item("Answer8_ended", DBNull.Value);
            this.set_Item("Answer9", DBNull.Value);
            this.set_Item("Answer10", DBNull.Value);
            this.set_Item("Answer11", DBNull.Value);
            this.set_Item("Answer12", DBNull.Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dtbAnswer2_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void dtbAnswer8_begun_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void dtbAnswer8_ended_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void dtbAnswer9_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_CMN0602B));
            this.pnlQuestions = new Panel();
            this.TextBox2 = new TextBox();
            this.Label11 = new Label();
            this.TextBox4 = new TextBox();
            this.Label12 = new Label();
            this.Label2 = new Label();
            this.Label5 = new Label();
            this.Label4 = new Label();
            this.TextBox1 = new TextBox();
            this.Label3 = new Label();
            this.Label1 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestion0 = new Label();
            this.TextBox10 = new TextBox();
            this.pnlAnswers = new Panel();
            this.rgAnswer12 = new RadioGroup();
            this.rgAnswer11 = new RadioGroup();
            this.rgAnswer10 = new RadioGroup();
            this.dtbAnswer9 = new UltraDateTimeEditor();
            this.Panel2 = new Panel();
            this.dtbAnswer8_ended = new UltraDateTimeEditor();
            this.dtbAnswer8_begun = new UltraDateTimeEditor();
            this.Label7 = new Label();
            this.rgAnswer7 = new RadioGroup();
            this.rgAnswer6 = new RadioGroup();
            this.rgAnswer5 = new RadioGroup();
            this.Panel4 = new Panel();
            this.nmbAnswer4 = new NumericBox();
            this.Label10 = new Label();
            this.rgAnswer3 = new RadioGroup();
            this.dtbAnswer2 = new UltraDateTimeEditor();
            this.rgAnswer1 = new RadioGroup();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel4.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.TextBox2);
            this.pnlQuestions.Controls.Add(this.Label11);
            this.pnlQuestions.Controls.Add(this.TextBox4);
            this.pnlQuestions.Controls.Add(this.Label12);
            this.pnlQuestions.Controls.Add(this.Label2);
            this.pnlQuestions.Controls.Add(this.Label5);
            this.pnlQuestions.Controls.Add(this.Label4);
            this.pnlQuestions.Controls.Add(this.TextBox1);
            this.pnlQuestions.Controls.Add(this.Label3);
            this.pnlQuestions.Controls.Add(this.Label1);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestion0);
            this.pnlQuestions.Controls.Add(this.TextBox10);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlQuestions.Location = new Point(0xa8, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x278, 380);
            this.pnlQuestions.TabIndex = 1;
            this.TextBox2.BackColor = SystemColors.Control;
            this.TextBox2.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox2.Dock = DockStyle.Top;
            this.TextBox2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox2.Location = new Point(0, 0x15c);
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new Size(0x278, 0x20);
            this.TextBox2.TabIndex = 12;
            this.TextBox2.Text = "12.  Numbers of TENS leads (i.e., separate electrodes) routinely needed and used by the patient at any one time:\r\n             (Circle appropriate number)    2 = 2 leads    4 = 4 leads";
            this.Label11.BackColor = Color.Transparent;
            this.Label11.BorderStyle = BorderStyle.FixedSingle;
            this.Label11.Dock = DockStyle.Top;
            this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label11.Location = new Point(0, 0x13c);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(0x278, 0x20);
            this.Label11.TabIndex = 11;
            this.Label11.Text = "11.  Do you and the patient agree that there has been a significant improvement in the pain and that long term use of a TENS is warranted?";
            this.TextBox4.BackColor = SystemColors.Control;
            this.TextBox4.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox4.Dock = DockStyle.Top;
            this.TextBox4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox4.Location = new Point(0, 0x11c);
            this.TextBox4.Multiline = true;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Size(0x278, 0x20);
            this.TextBox4.TabIndex = 10;
            this.TextBox4.Text = "10.  How often has the patient been using the TENS?  (Circle appropriate number)\r\n            1 = Daily   2 = 3 to 6 days per week    3 = 2 or less days per week";
            this.Label12.BackColor = Color.Transparent;
            this.Label12.BorderStyle = BorderStyle.FixedSingle;
            this.Label12.Dock = DockStyle.Top;
            this.Label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label12.Location = new Point(0, 0x108);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x278, 20);
            this.Label12.TabIndex = 9;
            this.Label12.Text = "9.    What is the date that you reevaluated the patient at the end of the trial period?";
            this.Label2.BackColor = Color.Transparent;
            this.Label2.BorderStyle = BorderStyle.FixedSingle;
            this.Label2.Dock = DockStyle.Top;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0, 200);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x278, 0x40);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "8.    What are the dates that trail of TENS unit began and ended?";
            this.Label5.BackColor = Color.Transparent;
            this.Label5.BorderStyle = BorderStyle.FixedSingle;
            this.Label5.Dock = DockStyle.Top;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(0, 180);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x278, 20);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "7.    Has the patient received a TENS trial?";
            this.Label4.BackColor = Color.Transparent;
            this.Label4.BorderStyle = BorderStyle.FixedSingle;
            this.Label4.Dock = DockStyle.Top;
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0, 0x98);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x278, 0x1c);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "6.    Is there documentation in the medical record of multiple medications and/or other therapies that have been tried and failed?";
            this.TextBox1.BackColor = SystemColors.Control;
            this.TextBox1.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox1.Dock = DockStyle.Top;
            this.TextBox1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox1.Location = new Point(0, 120);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Size(0x278, 0x20);
            this.TextBox1.TabIndex = 5;
            this.TextBox1.Text = manager.GetString("TextBox1.Text");
            this.Label3.BackColor = Color.Transparent;
            this.Label3.BorderStyle = BorderStyle.FixedSingle;
            this.Label3.Dock = DockStyle.Top;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0, 0x5c);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x278, 0x1c);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "4.    How long has the patient had intractable pain?  (Enter number of months, 1-99.)";
            this.Label1.BackColor = Color.Transparent;
            this.Label1.BorderStyle = BorderStyle.FixedSingle;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0x48);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x278, 20);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "3.    Does the patient have chronic, intractable pain?";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x34);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x278, 20);
            this.lblQuestion1.TabIndex = 2;
            this.lblQuestion1.Text = "2.    What is the date of surgery resulting in acute post-operative pain?";
            this.lblQuestion0.BackColor = Color.Transparent;
            this.lblQuestion0.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion0.Dock = DockStyle.Top;
            this.lblQuestion0.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion0.Location = new Point(0, 0x20);
            this.lblQuestion0.Name = "lblQuestion0";
            this.lblQuestion0.Size = new Size(0x278, 20);
            this.lblQuestion0.TabIndex = 1;
            this.lblQuestion0.Text = "1.    Does the patient have acute post-operative pain?";
            this.TextBox10.BackColor = SystemColors.Control;
            this.TextBox10.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox10.Dock = DockStyle.Top;
            this.TextBox10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox10.Location = new Point(0, 0);
            this.TextBox10.Multiline = true;
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Size(0x278, 0x20);
            this.TextBox10.TabIndex = 0;
            this.TextBox10.Text = "ANSWER QUESTIONS 1 - 6 FOR RENTAL OF TENS, AND 3 - 12 FOR PURCHASE OF TENS\r\n(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer12);
            this.pnlAnswers.Controls.Add(this.rgAnswer11);
            this.pnlAnswers.Controls.Add(this.rgAnswer10);
            this.pnlAnswers.Controls.Add(this.dtbAnswer9);
            this.pnlAnswers.Controls.Add(this.Panel2);
            this.pnlAnswers.Controls.Add(this.rgAnswer7);
            this.pnlAnswers.Controls.Add(this.rgAnswer6);
            this.pnlAnswers.Controls.Add(this.rgAnswer5);
            this.pnlAnswers.Controls.Add(this.Panel4);
            this.pnlAnswers.Controls.Add(this.rgAnswer3);
            this.pnlAnswers.Controls.Add(this.dtbAnswer2);
            this.pnlAnswers.Controls.Add(this.rgAnswer1);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0xa8, 380);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer12.BackColor = SystemColors.Control;
            this.rgAnswer12.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer12.Dock = DockStyle.Top;
            this.rgAnswer12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer12.Items = new string[] { "2", "4" };
            this.rgAnswer12.Location = new Point(0, 0x15c);
            this.rgAnswer12.Name = "rgAnswer12";
            this.rgAnswer12.Size = new Size(0xa8, 0x20);
            this.rgAnswer12.TabIndex = 12;
            this.rgAnswer11.BackColor = SystemColors.Control;
            this.rgAnswer11.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer11.Dock = DockStyle.Top;
            this.rgAnswer11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer11.Items = new string[] { "Y", "N" };
            this.rgAnswer11.Location = new Point(0, 0x13c);
            this.rgAnswer11.Name = "rgAnswer11";
            this.rgAnswer11.Size = new Size(0xa8, 0x20);
            this.rgAnswer11.TabIndex = 11;
            this.rgAnswer10.BackColor = SystemColors.Control;
            this.rgAnswer10.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer10.Dock = DockStyle.Top;
            this.rgAnswer10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer10.Items = new string[] { "1", "2", "3" };
            this.rgAnswer10.Location = new Point(0, 0x11c);
            this.rgAnswer10.Name = "rgAnswer10";
            this.rgAnswer10.Size = new Size(0xa8, 0x20);
            this.rgAnswer10.TabIndex = 10;
            this.dtbAnswer9.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer9.Dock = DockStyle.Top;
            this.dtbAnswer9.Location = new Point(0, 0x108);
            this.dtbAnswer9.Name = "dtbAnswer9";
            this.dtbAnswer9.Size = new Size(0xa8, 20);
            this.dtbAnswer9.TabIndex = 9;
            this.Panel2.BorderStyle = BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.dtbAnswer8_ended);
            this.Panel2.Controls.Add(this.dtbAnswer8_begun);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel2.Location = new Point(0, 200);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(0xa8, 0x40);
            this.Panel2.TabIndex = 8;
            this.dtbAnswer8_ended.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer8_ended.Location = new Point(0x1c, 40);
            this.dtbAnswer8_ended.Name = "dtbAnswer8_ended";
            this.dtbAnswer8_ended.Size = new Size(0x60, 0x13);
            this.dtbAnswer8_ended.TabIndex = 2;
            this.dtbAnswer8_begun.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer8_begun.Location = new Point(0x1c, 20);
            this.dtbAnswer8_begun.Name = "dtbAnswer8_begun";
            this.dtbAnswer8_begun.Size = new Size(0x60, 0x13);
            this.dtbAnswer8_begun.TabIndex = 1;
            this.Label7.BackColor = Color.Transparent;
            this.Label7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(0x1c, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(0x60, 0x13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "begun/ended";
            this.Label7.TextAlign = ContentAlignment.MiddleCenter;
            this.rgAnswer7.BackColor = SystemColors.Control;
            this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7.Dock = DockStyle.Top;
            this.rgAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer7.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer7.Location = new Point(0, 180);
            this.rgAnswer7.Name = "rgAnswer7";
            this.rgAnswer7.Size = new Size(0xa8, 20);
            this.rgAnswer7.TabIndex = 7;
            this.rgAnswer6.BackColor = SystemColors.Control;
            this.rgAnswer6.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer6.Dock = DockStyle.Top;
            this.rgAnswer6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer6.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer6.Location = new Point(0, 0x98);
            this.rgAnswer6.Name = "rgAnswer6";
            this.rgAnswer6.Size = new Size(0xa8, 0x1c);
            this.rgAnswer6.TabIndex = 6;
            this.rgAnswer5.BackColor = SystemColors.Control;
            this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer5.Dock = DockStyle.Top;
            this.rgAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer5.Items = new string[] { "1", "2", "3", "4", "5" };
            this.rgAnswer5.Location = new Point(0, 120);
            this.rgAnswer5.Name = "rgAnswer5";
            this.rgAnswer5.Size = new Size(0xa8, 0x20);
            this.rgAnswer5.Spacing = 0;
            this.rgAnswer5.TabIndex = 5;
            this.Panel4.BorderStyle = BorderStyle.FixedSingle;
            this.Panel4.Controls.Add(this.nmbAnswer4);
            this.Panel4.Controls.Add(this.Label10);
            this.Panel4.Dock = DockStyle.Top;
            this.Panel4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel4.Location = new Point(0, 0x5c);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new Size(0xa8, 0x1c);
            this.Panel4.TabIndex = 4;
            this.nmbAnswer4.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer4.Location = new Point(8, 4);
            this.nmbAnswer4.Name = "nmbAnswer4";
            this.nmbAnswer4.Size = new Size(0x68, 20);
            this.nmbAnswer4.TabIndex = 0;
            this.nmbAnswer4.TextAlign = HorizontalAlignment.Left;
            this.Label10.BackColor = Color.Transparent;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(120, 4);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(40, 20);
            this.Label10.TabIndex = 1;
            this.Label10.Text = "month";
            this.rgAnswer3.BackColor = SystemColors.Control;
            this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer3.Dock = DockStyle.Top;
            this.rgAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer3.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer3.Location = new Point(0, 0x48);
            this.rgAnswer3.Name = "rgAnswer3";
            this.rgAnswer3.Size = new Size(0xa8, 20);
            this.rgAnswer3.TabIndex = 3;
            this.dtbAnswer2.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer2.Dock = DockStyle.Top;
            this.dtbAnswer2.Location = new Point(0, 0x34);
            this.dtbAnswer2.Name = "dtbAnswer2";
            this.dtbAnswer2.Size = new Size(0xa8, 20);
            this.dtbAnswer2.TabIndex = 2;
            this.rgAnswer1.BackColor = SystemColors.Control;
            this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer1.Dock = DockStyle.Top;
            this.rgAnswer1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer1.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer1.Location = new Point(0, 0x20);
            this.rgAnswer1.Name = "rgAnswer1";
            this.rgAnswer1.Size = new Size(0xa8, 20);
            this.rgAnswer1.TabIndex = 1;
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0xa8, 0x20);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0602B";
            base.Size = new Size(800, 380);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer1", reader["Answer1"]);
            this.set_Item("Answer2", reader["Answer2"]);
            this.set_Item("Answer3", reader["Answer3"]);
            Functions.SetNumericBoxValue(this.nmbAnswer4, reader["Answer4"]);
            this.set_Item("Answer5", reader["Answer5"]);
            this.set_Item("Answer6", reader["Answer6"]);
            this.set_Item("Answer7", reader["Answer7"]);
            this.set_Item("Answer8_begun", reader["Answer8_begun"]);
            this.set_Item("Answer8_ended", reader["Answer8_ended"]);
            this.set_Item("Answer9", reader["Answer9"]);
            this.set_Item("Answer10", reader["Answer10"]);
            this.set_Item("Answer11", reader["Answer11"]);
            this.set_Item("Answer12", reader["Answer12"]);
        }

        private void nmbAnswer4_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer1_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer10_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer11_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer12_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer3_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer5_ValueChanged(object sender, EventArgs e)
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

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 5).Value = this.get_Item("Answer1");
            cmd.Parameters.Add("Answer2", MySqlType.Date, 0).Value = this.get_Item("Answer2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.get_Item("Answer3");
            cmd.Parameters.Add("Answer4", MySqlType.Int).Value = this.nmbAnswer4.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.get_Item("Answer5");
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 5).Value = this.get_Item("Answer6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 5).Value = this.get_Item("Answer7");
            cmd.Parameters.Add("Answer8_begun", MySqlType.Date, 0).Value = this.get_Item("Answer8_begun");
            cmd.Parameters.Add("Answer8_ended", MySqlType.Date, 0).Value = this.get_Item("Answer8_ended");
            cmd.Parameters.Add("Answer9", MySqlType.Date, 0).Value = this.get_Item("Answer9");
            cmd.Parameters.Add("Answer10", MySqlType.VarChar, 5).Value = this.get_Item("Answer10");
            cmd.Parameters.Add("Answer11", MySqlType.VarChar, 5).Value = this.get_Item("Answer11");
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 5).Value = this.get_Item("Answer12");
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox4")]
        private TextBox TextBox4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion0")]
        private Label lblQuestion0 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox10")]
        private TextBox TextBox10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel4")]
        private Panel Panel4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label10")]
        private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel2")]
        private Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox1")]
        private TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label11")]
        private Label Label11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label12")]
        private Label Label12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox2")]
        private TextBox TextBox2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer8_ended")]
        private UltraDateTimeEditor dtbAnswer8_ended { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer8_begun")]
        private UltraDateTimeEditor dtbAnswer8_begun { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer12")]
        private RadioGroup rgAnswer12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer11")]
        private RadioGroup rgAnswer11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer10")]
        private RadioGroup rgAnswer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7")]
        private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer9")]
        private UltraDateTimeEditor dtbAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer6")]
        private RadioGroup rgAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer5")]
        private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer3")]
        private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer2")]
        private UltraDateTimeEditor dtbAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer1")]
        private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer4")]
        private NumericBox nmbAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0602B;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object dateBoxValue;
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                dateBoxValue = this.rgAnswer1.Value;
            }
            else if (string.Compare(Index, "Answer2", true) == 0)
            {
                dateBoxValue = Functions.GetDateBoxValue(this.dtbAnswer2);
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                dateBoxValue = this.rgAnswer3.Value;
            }
            else if (string.Compare(Index, "Answer4", true) == 0)
            {
                dateBoxValue = this.nmbAnswer4.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                dateBoxValue = this.rgAnswer5.Value;
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                dateBoxValue = this.rgAnswer6.Value;
            }
            else if (string.Compare(Index, "Answer7", true) == 0)
            {
                dateBoxValue = this.rgAnswer7.Value;
            }
            else if (string.Compare(Index, "Answer8_begun", true) == 0)
            {
                dateBoxValue = Functions.GetDateBoxValue(this.dtbAnswer8_begun);
            }
            else if (string.Compare(Index, "Answer8_ended", true) == 0)
            {
                dateBoxValue = Functions.GetDateBoxValue(this.dtbAnswer8_ended);
            }
            else if (string.Compare(Index, "Answer9", true) == 0)
            {
                dateBoxValue = Functions.GetDateBoxValue(this.dtbAnswer9);
            }
            else if (string.Compare(Index, "Answer10", true) == 0)
            {
                dateBoxValue = this.rgAnswer10.Value;
            }
            else if (string.Compare(Index, "Answer11", true) == 0)
            {
                dateBoxValue = this.rgAnswer11.Value;
            }
            else
            {
                if (string.Compare(Index, "Answer12", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                dateBoxValue = this.rgAnswer12.Value;
            }
            return dateBoxValue;
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
                Functions.SetDateBoxValue(this.dtbAnswer2, Value);
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer3, Value);
            }
            else if (string.Compare(Index, "Answer4", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer4, Value);
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer5, Value);
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer6, Value);
            }
            else if (string.Compare(Index, "Answer7", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer7, Value);
            }
            else if (string.Compare(Index, "Answer8_begun", true) == 0)
            {
                Functions.SetDateBoxValue(this.dtbAnswer8_begun, Value);
            }
            else if (string.Compare(Index, "Answer8_ended", true) == 0)
            {
                Functions.SetDateBoxValue(this.dtbAnswer8_ended, Value);
            }
            else if (string.Compare(Index, "Answer9", true) == 0)
            {
                Functions.SetDateBoxValue(this.dtbAnswer9, Value);
            }
            else if (string.Compare(Index, "Answer10", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer10, Value);
            }
            else if (string.Compare(Index, "Answer11", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer11, Value);
            }
            else if (string.Compare(Index, "Answer12", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer12, Value);
            }
        }

    }
}

