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

    public class Control_CMN0403C : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0403C()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer6a", DBNull.Value);
            this.set_Item("Answer6b", DBNull.Value);
            this.set_Item("Answer7a", DBNull.Value);
            this.set_Item("Answer7b", DBNull.Value);
            this.set_Item("Answer8", DBNull.Value);
            this.set_Item("Answer9a", DBNull.Value);
            this.set_Item("Answer9b", DBNull.Value);
            this.set_Item("Answer10a", DBNull.Value);
            this.set_Item("Answer10b", DBNull.Value);
            this.set_Item("Answer10c", DBNull.Value);
            this.set_Item("Answer11a", DBNull.Value);
            this.set_Item("Answer11b", DBNull.Value);
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
            this.Label1 = new Label();
            this.Label3 = new Label();
            this.Label4 = new Label();
            this.Label5 = new Label();
            this.Label6 = new Label();
            this.Label7 = new Label();
            this.lblQuestion6 = new Label();
            this.lblQuestion5 = new Label();
            this.lblQuestion4 = new Label();
            this.lblQuestion3 = new Label();
            this.lblQuestion2 = new Label();
            this.lblQuestion1 = new Label();
            this.Label2 = new Label();
            this.lblQuestionDescription2 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.pnlAnswers = new Panel();
            this.nmbAnswer11b = new NumericBox();
            this.rgAnswer11a = new RadioGroup();
            this.nmbAnswer10c = new NumericBox();
            this.nmbAnswer10b = new NumericBox();
            this.rgAnswer10a = new RadioGroup();
            this.nmbAnswer9b = new NumericBox();
            this.rgAnswer9a = new RadioGroup();
            this.rgAnswer8 = new RadioGroup();
            this.nmbAnswer7b = new NumericBox();
            this.rgAnswer7a = new RadioGroup();
            this.nmbAnswer6b = new NumericBox();
            this.rgAnswer6a = new RadioGroup();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.Label1);
            this.pnlQuestions.Controls.Add(this.Label3);
            this.pnlQuestions.Controls.Add(this.Label4);
            this.pnlQuestions.Controls.Add(this.Label5);
            this.pnlQuestions.Controls.Add(this.Label6);
            this.pnlQuestions.Controls.Add(this.Label7);
            this.pnlQuestions.Controls.Add(this.lblQuestion6);
            this.pnlQuestions.Controls.Add(this.lblQuestion5);
            this.pnlQuestions.Controls.Add(this.lblQuestion4);
            this.pnlQuestions.Controls.Add(this.lblQuestion3);
            this.pnlQuestions.Controls.Add(this.lblQuestion2);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.Label2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x70, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x2b0, 0x128);
            this.pnlQuestions.TabIndex = 1;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.BorderStyle = BorderStyle.FixedSingle;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0x114);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x2b0, 20);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "       (b) How many months prior to ordering the device did the patient have the multi-level fusion?";
            this.Label3.BackColor = Color.Transparent;
            this.Label3.BorderStyle = BorderStyle.FixedSingle;
            this.Label3.Dock = DockStyle.Top;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0, 0x100);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x2b0, 20);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "11.  (a) Is the device being ordered as an adjunct to recent spinal fusion surgery in a patient who has had a multi-level fusion?";
            this.Label4.BackColor = Color.Transparent;
            this.Label4.BorderStyle = BorderStyle.FixedSingle;
            this.Label4.Dock = DockStyle.Top;
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0, 0xec);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x2b0, 20);
            this.Label4.TabIndex = 12;
            this.Label4.Text = "       (c) How many months prior to ordering the device did the patient have the previously failed fusion?";
            this.Label5.BackColor = Color.Transparent;
            this.Label5.BorderStyle = BorderStyle.FixedSingle;
            this.Label5.Dock = DockStyle.Top;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(0, 0xd8);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x2b0, 20);
            this.Label5.TabIndex = 11;
            this.Label5.Text = "       (b) How many months prior to ordering the device did the patient have the repeat fusion?";
            this.Label6.BackColor = Color.Transparent;
            this.Label6.BorderStyle = BorderStyle.FixedSingle;
            this.Label6.Dock = DockStyle.Top;
            this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label6.Location = new Point(0, 0xbc);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x2b0, 0x1c);
            this.Label6.TabIndex = 10;
            this.Label6.Text = "10.  (a) Is the device being ordered as an adjunct to repeat spinal fusion surgery in a patient with a previously failed spinal fusion at the same level(s)?";
            this.Label7.BackColor = Color.Transparent;
            this.Label7.BorderStyle = BorderStyle.FixedSingle;
            this.Label7.Dock = DockStyle.Top;
            this.Label7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(0, 0xa8);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(0x2b0, 20);
            this.Label7.TabIndex = 9;
            this.Label7.Text = "     (b) How many months prior to ordering the device did the patient have the fusion?";
            this.lblQuestion6.BackColor = Color.Transparent;
            this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion6.Dock = DockStyle.Top;
            this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion6.Location = new Point(0, 0x94);
            this.lblQuestion6.Name = "lblQuestion6";
            this.lblQuestion6.Size = new Size(0x2b0, 20);
            this.lblQuestion6.TabIndex = 8;
            this.lblQuestion6.Text = "9.  (a) Is the device being ordered as a treatment of a failed spinal fusion in a patient who has not had a recent repeat fusion?";
            this.lblQuestion5.BackColor = Color.Transparent;
            this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion5.Dock = DockStyle.Top;
            this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion5.Location = new Point(0, 0x80);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new Size(0x2b0, 20);
            this.lblQuestion5.TabIndex = 7;
            this.lblQuestion5.Text = "8.   Does the patient have a congenital pseudoarthrosis?";
            this.lblQuestion4.BackColor = Color.Transparent;
            this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion4.Dock = DockStyle.Top;
            this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion4.Location = new Point(0, 0x6c);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new Size(0x2b0, 20);
            this.lblQuestion4.TabIndex = 6;
            this.lblQuestion4.Text = "     (b) How many months prior to ordering the device did the patient have the fusion?";
            this.lblQuestion3.BackColor = Color.Transparent;
            this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion3.Dock = DockStyle.Top;
            this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion3.Location = new Point(0, 0x58);
            this.lblQuestion3.Name = "lblQuestion3";
            this.lblQuestion3.Size = new Size(0x2b0, 20);
            this.lblQuestion3.TabIndex = 5;
            this.lblQuestion3.Text = "7.  (a) Does the patient have a failed fusion of a joint other than the spine?";
            this.lblQuestion2.BackColor = Color.Transparent;
            this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion2.Dock = DockStyle.Top;
            this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion2.Location = new Point(0, 0x44);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new Size(0x2b0, 20);
            this.lblQuestion2.TabIndex = 4;
            this.lblQuestion2.Text = "    (b) How many months prior to ordering the device did the patient sustain the fracture?";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x30);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x2b0, 20);
            this.lblQuestion1.TabIndex = 3;
            this.lblQuestion1.Text = "6.  (a) Does the patient have a non-union of a long-bone fracture?";
            this.Label2.BackColor = Color.Transparent;
            this.Label2.BorderStyle = BorderStyle.FixedSingle;
            this.Label2.Dock = DockStyle.Top;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0, 0x20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x2b0, 0x10);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "(CircleY for Yes,N for No,or D for Does Not Apply. For questions about months,enter 1-99 or D. If less than one month,enter 1.)";
            this.lblQuestionDescription2.BackColor = Color.Transparent;
            this.lblQuestionDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription2.Dock = DockStyle.Top;
            this.lblQuestionDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription2.Location = new Point(0, 0x10);
            this.lblQuestionDescription2.Name = "lblQuestionDescription2";
            this.lblQuestionDescription2.Size = new Size(0x2b0, 0x10);
            this.lblQuestionDescription2.TabIndex = 1;
            this.lblQuestionDescription2.Text = "ANSWER QUESTIONS 9-11 FOR SPINAL OSTEOGENISIS STIMULATOR.";
            this.lblQuestionDescription1.BackColor = Color.Transparent;
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x2b0, 0x10);
            this.lblQuestionDescription1.TabIndex = 0;
            this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 6-8 FOR NONSPINAL OSTEOGENISIS STIMULATOR.";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.nmbAnswer11b);
            this.pnlAnswers.Controls.Add(this.rgAnswer11a);
            this.pnlAnswers.Controls.Add(this.nmbAnswer10c);
            this.pnlAnswers.Controls.Add(this.nmbAnswer10b);
            this.pnlAnswers.Controls.Add(this.rgAnswer10a);
            this.pnlAnswers.Controls.Add(this.nmbAnswer9b);
            this.pnlAnswers.Controls.Add(this.rgAnswer9a);
            this.pnlAnswers.Controls.Add(this.rgAnswer8);
            this.pnlAnswers.Controls.Add(this.nmbAnswer7b);
            this.pnlAnswers.Controls.Add(this.rgAnswer7a);
            this.pnlAnswers.Controls.Add(this.nmbAnswer6b);
            this.pnlAnswers.Controls.Add(this.rgAnswer6a);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x70, 0x128);
            this.pnlAnswers.TabIndex = 0;
            this.nmbAnswer11b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer11b.Dock = DockStyle.Top;
            this.nmbAnswer11b.Location = new Point(0, 0x114);
            this.nmbAnswer11b.Name = "nmbAnswer11b";
            this.nmbAnswer11b.Size = new Size(0x70, 20);
            this.nmbAnswer11b.TabIndex = 12;
            this.nmbAnswer11b.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer11a.BackColor = SystemColors.Control;
            this.rgAnswer11a.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer11a.Dock = DockStyle.Top;
            this.rgAnswer11a.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer11a.Location = new Point(0, 0x100);
            this.rgAnswer11a.Name = "rgAnswer11a";
            this.rgAnswer11a.Size = new Size(0x70, 20);
            this.rgAnswer11a.TabIndex = 11;
            this.rgAnswer11a.Value = "";
            this.nmbAnswer10c.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer10c.Dock = DockStyle.Top;
            this.nmbAnswer10c.Location = new Point(0, 0xec);
            this.nmbAnswer10c.Name = "nmbAnswer10c";
            this.nmbAnswer10c.Size = new Size(0x70, 20);
            this.nmbAnswer10c.TabIndex = 10;
            this.nmbAnswer10c.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer10b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer10b.Dock = DockStyle.Top;
            this.nmbAnswer10b.Location = new Point(0, 0xd8);
            this.nmbAnswer10b.Name = "nmbAnswer10b";
            this.nmbAnswer10b.Size = new Size(0x70, 20);
            this.nmbAnswer10b.TabIndex = 9;
            this.nmbAnswer10b.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer10a.BackColor = SystemColors.Control;
            this.rgAnswer10a.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer10a.Dock = DockStyle.Top;
            this.rgAnswer10a.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer10a.Location = new Point(0, 0xbc);
            this.rgAnswer10a.Name = "rgAnswer10a";
            this.rgAnswer10a.Size = new Size(0x70, 0x1c);
            this.rgAnswer10a.TabIndex = 8;
            this.rgAnswer10a.Value = "";
            this.nmbAnswer9b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer9b.Dock = DockStyle.Top;
            this.nmbAnswer9b.Location = new Point(0, 0xa8);
            this.nmbAnswer9b.Name = "nmbAnswer9b";
            this.nmbAnswer9b.Size = new Size(0x70, 20);
            this.nmbAnswer9b.TabIndex = 7;
            this.nmbAnswer9b.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer9a.BackColor = SystemColors.Control;
            this.rgAnswer9a.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer9a.Dock = DockStyle.Top;
            this.rgAnswer9a.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer9a.Location = new Point(0, 0x94);
            this.rgAnswer9a.Name = "rgAnswer9a";
            this.rgAnswer9a.Size = new Size(0x70, 20);
            this.rgAnswer9a.TabIndex = 6;
            this.rgAnswer9a.Value = "";
            this.rgAnswer8.BackColor = SystemColors.Control;
            this.rgAnswer8.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer8.Dock = DockStyle.Top;
            this.rgAnswer8.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer8.Location = new Point(0, 0x80);
            this.rgAnswer8.Name = "rgAnswer8";
            this.rgAnswer8.Size = new Size(0x70, 20);
            this.rgAnswer8.TabIndex = 5;
            this.rgAnswer8.Value = "";
            this.nmbAnswer7b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer7b.Dock = DockStyle.Top;
            this.nmbAnswer7b.Location = new Point(0, 0x6c);
            this.nmbAnswer7b.Name = "nmbAnswer7b";
            this.nmbAnswer7b.Size = new Size(0x70, 20);
            this.nmbAnswer7b.TabIndex = 4;
            this.nmbAnswer7b.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer7a.BackColor = SystemColors.Control;
            this.rgAnswer7a.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7a.Dock = DockStyle.Top;
            this.rgAnswer7a.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer7a.Location = new Point(0, 0x58);
            this.rgAnswer7a.Name = "rgAnswer7a";
            this.rgAnswer7a.Size = new Size(0x70, 20);
            this.rgAnswer7a.TabIndex = 3;
            this.rgAnswer7a.Value = "";
            this.nmbAnswer6b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer6b.Dock = DockStyle.Top;
            this.nmbAnswer6b.Location = new Point(0, 0x44);
            this.nmbAnswer6b.Name = "nmbAnswer6b";
            this.nmbAnswer6b.Size = new Size(0x70, 20);
            this.nmbAnswer6b.TabIndex = 2;
            this.nmbAnswer6b.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer6a.BackColor = SystemColors.Control;
            this.rgAnswer6a.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer6a.Dock = DockStyle.Top;
            this.rgAnswer6a.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer6a.Location = new Point(0, 0x30);
            this.rgAnswer6a.Name = "rgAnswer6a";
            this.rgAnswer6a.Size = new Size(0x70, 20);
            this.rgAnswer6a.TabIndex = 1;
            this.rgAnswer6a.Value = "";
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x70, 0x30);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0403C";
            base.Size = new Size(800, 0x128);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer6a", reader["Answer6a"]);
            this.set_Item("Answer6b", reader["Answer6b"]);
            this.set_Item("Answer7a", reader["Answer7a"]);
            this.set_Item("Answer7b", reader["Answer7b"]);
            this.set_Item("Answer8", reader["Answer8"]);
            this.set_Item("Answer9a", reader["Answer9a"]);
            this.set_Item("Answer9b", reader["Answer9b"]);
            this.set_Item("Answer10a", reader["Answer10a"]);
            this.set_Item("Answer10b", reader["Answer10b"]);
            this.set_Item("Answer10c", reader["Answer10c"]);
            this.set_Item("Answer11a", reader["Answer11a"]);
            this.set_Item("Answer11b", reader["Answer11b"]);
        }

        private void nmbAnswer10b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer10c_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer11b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer6b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer7b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer9b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer10a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer11a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer6a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer7a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer8_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer9a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer6a", MySqlType.VarChar, 5).Value = this.get_Item("Answer6a");
            cmd.Parameters.Add("Answer6b", MySqlType.Int, 0).Value = this.get_Item("Answer6b");
            cmd.Parameters.Add("Answer7a", MySqlType.VarChar, 5).Value = this.get_Item("Answer7a");
            cmd.Parameters.Add("Answer7b", MySqlType.Int, 0).Value = this.get_Item("Answer7b");
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 5).Value = this.get_Item("Answer8");
            cmd.Parameters.Add("Answer9a", MySqlType.VarChar, 5).Value = this.get_Item("Answer9a");
            cmd.Parameters.Add("Answer9b", MySqlType.Int, 0).Value = this.get_Item("Answer9b");
            cmd.Parameters.Add("Answer10a", MySqlType.VarChar, 5).Value = this.get_Item("Answer10a");
            cmd.Parameters.Add("Answer10b", MySqlType.Int, 0).Value = this.get_Item("Answer10b");
            cmd.Parameters.Add("Answer10c", MySqlType.Int, 0).Value = this.get_Item("Answer10c");
            cmd.Parameters.Add("Answer11a", MySqlType.VarChar, 5).Value = this.get_Item("Answer11a");
            cmd.Parameters.Add("Answer11b", MySqlType.Int, 0).Value = this.get_Item("Answer11b");
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion6")]
        private Label lblQuestion6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription2")]
        private Label lblQuestionDescription2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription1")]
        private Label lblQuestionDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion2")]
        private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("Label6")]
        private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer8")]
        private RadioGroup rgAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer6a")]
        private RadioGroup rgAnswer6a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer11a")]
        private RadioGroup rgAnswer11a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer9a")]
        private RadioGroup rgAnswer9a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7a")]
        private RadioGroup rgAnswer7a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer10a")]
        private RadioGroup rgAnswer10a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer11b")]
        private NumericBox nmbAnswer11b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer10c")]
        private NumericBox nmbAnswer10c { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer10b")]
        private NumericBox nmbAnswer10b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer9b")]
        private NumericBox nmbAnswer9b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer7b")]
        private NumericBox nmbAnswer7b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer6b")]
        private NumericBox nmbAnswer6b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0403C;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object valueOrDefault;
            if (string.Compare(Index, "Answer10a", true) == 0)
            {
                valueOrDefault = this.rgAnswer10a.Value;
            }
            else if (string.Compare(Index, "Answer11a", true) == 0)
            {
                valueOrDefault = this.rgAnswer11a.Value;
            }
            else if (string.Compare(Index, "Answer6a", true) == 0)
            {
                valueOrDefault = this.rgAnswer6a.Value;
            }
            else if (string.Compare(Index, "Answer7a", true) == 0)
            {
                valueOrDefault = this.rgAnswer7a.Value;
            }
            else if (string.Compare(Index, "Answer8", true) == 0)
            {
                valueOrDefault = this.rgAnswer8.Value;
            }
            else if (string.Compare(Index, "Answer9a", true) == 0)
            {
                valueOrDefault = this.rgAnswer9a.Value;
            }
            else if (string.Compare(Index, "Answer10b", true) == 0)
            {
                valueOrDefault = this.nmbAnswer10b.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer10c", true) == 0)
            {
                valueOrDefault = this.nmbAnswer10c.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer11b", true) == 0)
            {
                valueOrDefault = this.nmbAnswer11b.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer6b", true) == 0)
            {
                valueOrDefault = this.nmbAnswer6b.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer7b", true) == 0)
            {
                valueOrDefault = this.nmbAnswer7b.AsInt32.GetValueOrDefault(0);
            }
            else
            {
                if (string.Compare(Index, "Answer9b", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                valueOrDefault = this.nmbAnswer9b.AsInt32.GetValueOrDefault(0);
            }
            return valueOrDefault;
        }

        // Warning: Properties with arguments are not supported in C#. Setter of a Item property was decompiled as a method.
        protected void set_Item(string Index, object Value)
        {
            if (string.Compare(Index, "Answer10a", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer10a, Value);
            }
            else if (string.Compare(Index, "Answer11a", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer11a, Value);
            }
            else if (string.Compare(Index, "Answer6a", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer6a, Value);
            }
            else if (string.Compare(Index, "Answer7a", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer7a, Value);
            }
            else if (string.Compare(Index, "Answer8", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer8, Value);
            }
            else if (string.Compare(Index, "Answer9a", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer9a, Value);
            }
            else if (string.Compare(Index, "Answer10b", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer10b, Value);
            }
            else if (string.Compare(Index, "Answer10c", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer10c, Value);
            }
            else if (string.Compare(Index, "Answer11b", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer11b, Value);
            }
            else if (string.Compare(Index, "Answer6b", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer6b, Value);
            }
            else if (string.Compare(Index, "Answer7b", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer7b, Value);
            }
            else if (string.Compare(Index, "Answer9b", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer9b, Value);
            }
        }

    }
}

