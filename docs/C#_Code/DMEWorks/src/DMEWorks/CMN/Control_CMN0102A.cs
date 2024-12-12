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

    public class Control_CMN0102A : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0102A()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer1", DBNull.Value);
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
            this.pnlAnswers = new Panel();
            this.rgAnswer7 = new RadioGroup();
            this.rgAnswer6 = new RadioGroup();
            this.rgAnswer5 = new RadioGroup();
            this.rgAnswer4 = new RadioGroup();
            this.rgAnswer3 = new RadioGroup();
            this.rgAnswer1 = new RadioGroup();
            this.lblAnswerDescription2 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions = new Panel();
            this.lblQuestion7 = new Label();
            this.lblQuestion6 = new Label();
            this.lblQuestion5 = new Label();
            this.lblQuestion4 = new Label();
            this.lblQuestion3 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestionDescription3 = new Label();
            this.lblQuestionDescription2 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.pnlAnswers.SuspendLayout();
            this.pnlQuestions.SuspendLayout();
            base.SuspendLayout();
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer7);
            this.pnlAnswers.Controls.Add(this.rgAnswer6);
            this.pnlAnswers.Controls.Add(this.rgAnswer5);
            this.pnlAnswers.Controls.Add(this.rgAnswer4);
            this.pnlAnswers.Controls.Add(this.rgAnswer3);
            this.pnlAnswers.Controls.Add(this.rgAnswer1);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription2);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x88, 0xd8);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer7.BackColor = SystemColors.Control;
            this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7.Dock = DockStyle.Top;
            this.rgAnswer7.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer7.Location = new Point(0, 0xbc);
            this.rgAnswer7.Name = "rgAnswer7";
            this.rgAnswer7.Size = new Size(0x88, 0x1c);
            this.rgAnswer7.TabIndex = 8;
            this.rgAnswer6.BackColor = SystemColors.Control;
            this.rgAnswer6.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer6.Dock = DockStyle.Top;
            this.rgAnswer6.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer6.Location = new Point(0, 160);
            this.rgAnswer6.Name = "rgAnswer6";
            this.rgAnswer6.Size = new Size(0x88, 0x1c);
            this.rgAnswer6.TabIndex = 7;
            this.rgAnswer5.BackColor = SystemColors.Control;
            this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer5.Dock = DockStyle.Top;
            this.rgAnswer5.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer5.Location = new Point(0, 0x84);
            this.rgAnswer5.Name = "rgAnswer5";
            this.rgAnswer5.Size = new Size(0x88, 0x1c);
            this.rgAnswer5.TabIndex = 6;
            this.rgAnswer4.BackColor = SystemColors.Control;
            this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer4.Dock = DockStyle.Top;
            this.rgAnswer4.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer4.Location = new Point(0, 0x68);
            this.rgAnswer4.Name = "rgAnswer4";
            this.rgAnswer4.Size = new Size(0x88, 0x1c);
            this.rgAnswer4.TabIndex = 5;
            this.rgAnswer3.BackColor = SystemColors.Control;
            this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer3.Dock = DockStyle.Top;
            this.rgAnswer3.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer3.Location = new Point(0, 0x4c);
            this.rgAnswer3.Name = "rgAnswer3";
            this.rgAnswer3.Size = new Size(0x88, 0x1c);
            this.rgAnswer3.TabIndex = 4;
            this.rgAnswer1.BackColor = SystemColors.Control;
            this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer1.Dock = DockStyle.Top;
            this.rgAnswer1.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer1.Location = new Point(0, 0x30);
            this.rgAnswer1.Name = "rgAnswer1";
            this.rgAnswer1.Size = new Size(0x88, 0x1c);
            this.rgAnswer1.TabIndex = 3;
            this.lblAnswerDescription2.BackColor = Color.Transparent;
            this.lblAnswerDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription2.Dock = DockStyle.Top;
            this.lblAnswerDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription2.Location = new Point(0, 0x20);
            this.lblAnswerDescription2.Name = "lblAnswerDescription2";
            this.lblAnswerDescription2.Size = new Size(0x88, 0x10);
            this.lblAnswerDescription2.TabIndex = 1;
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x88, 0x20);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.lblQuestion7);
            this.pnlQuestions.Controls.Add(this.lblQuestion6);
            this.pnlQuestions.Controls.Add(this.lblQuestion5);
            this.pnlQuestions.Controls.Add(this.lblQuestion4);
            this.pnlQuestions.Controls.Add(this.lblQuestion3);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription3);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x88, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x298, 0xd8);
            this.pnlQuestions.TabIndex = 2;
            this.lblQuestion7.BackColor = Color.Transparent;
            this.lblQuestion7.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion7.Dock = DockStyle.Top;
            this.lblQuestion7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion7.Location = new Point(0, 0xbc);
            this.lblQuestion7.Name = "lblQuestion7";
            this.lblQuestion7.Size = new Size(0x298, 0x1c);
            this.lblQuestion7.TabIndex = 8;
            this.lblQuestion7.Text = "7.   Does the patient require frequent changes in body position and/or have an immediate need for a change in body position.";
            this.lblQuestion6.BackColor = Color.Transparent;
            this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion6.Dock = DockStyle.Top;
            this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion6.Location = new Point(0, 160);
            this.lblQuestion6.Name = "lblQuestion6";
            this.lblQuestion6.Size = new Size(0x298, 0x1c);
            this.lblQuestion6.TabIndex = 7;
            this.lblQuestion6.Text = "6.   Does the patient require a bed height different than a fixed height hospital bed to permit transfers to chair, wheelchair, or standing position?";
            this.lblQuestion5.BackColor = Color.Transparent;
            this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion5.Dock = DockStyle.Top;
            this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion5.Location = new Point(0, 0x84);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new Size(0x298, 0x1c);
            this.lblQuestion5.TabIndex = 6;
            this.lblQuestion5.Text = "5.   Does the patient require traction which can only be attached to a hospital bed?";
            this.lblQuestion4.BackColor = Color.Transparent;
            this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion4.Dock = DockStyle.Top;
            this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion4.Location = new Point(0, 0x68);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new Size(0x298, 0x1c);
            this.lblQuestion4.TabIndex = 5;
            this.lblQuestion4.Text = "4.   Does the patient require the head of the bed to be elevated more than 30 degrees most of the time due to conjestive heart failure, chronic pulmonary disease or aspiration.";
            this.lblQuestion3.BackColor = Color.Transparent;
            this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion3.Dock = DockStyle.Top;
            this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion3.Location = new Point(0, 0x4c);
            this.lblQuestion3.Name = "lblQuestion3";
            this.lblQuestion3.Size = new Size(0x298, 0x1c);
            this.lblQuestion3.TabIndex = 4;
            this.lblQuestion3.Text = "3.  Does the patient require, for the alleviation of pain, positioning of the body in ways not feasible with an ordinary bed?";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x30);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x298, 0x1c);
            this.lblQuestion1.TabIndex = 3;
            this.lblQuestion1.Text = "1.  Does the patient require positioning of the body in ways not feasible with an ordinary bed due to a medical condition which is expected to last at least one month?";
            this.lblQuestionDescription3.BackColor = Color.Transparent;
            this.lblQuestionDescription3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription3.Dock = DockStyle.Top;
            this.lblQuestionDescription3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription3.Location = new Point(0, 0x20);
            this.lblQuestionDescription3.Name = "lblQuestionDescription3";
            this.lblQuestionDescription3.Size = new Size(0x298, 0x10);
            this.lblQuestionDescription3.TabIndex = 2;
            this.lblQuestionDescription3.Text = "QUESTION 2 RESERVED FOR OTHER OR FUTURE USE.";
            this.lblQuestionDescription2.BackColor = Color.Transparent;
            this.lblQuestionDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription2.Dock = DockStyle.Top;
            this.lblQuestionDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription2.Location = new Point(0, 0x10);
            this.lblQuestionDescription2.Name = "lblQuestionDescription2";
            this.lblQuestionDescription2.Size = new Size(0x298, 0x10);
            this.lblQuestionDescription2.TabIndex = 1;
            this.lblQuestionDescription2.Text = "        (Circle Y for Yes, N for No, or D for Does Not Apply)";
            this.lblQuestionDescription1.BackColor = Color.Transparent;
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x298, 0x10);
            this.lblQuestionDescription1.TabIndex = 0;
            this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 1, AND 3-7 FOR HOSPITAL BEDS";
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0102A";
            base.Size = new Size(800, 0xd8);
            this.pnlAnswers.ResumeLayout(false);
            this.pnlQuestions.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer1", reader["Answer1"]);
            this.set_Item("Answer3", reader["Answer3"]);
            this.set_Item("Answer4", reader["Answer4"]);
            this.set_Item("Answer5", reader["Answer5"]);
            this.set_Item("Answer6", reader["Answer6"]);
            this.set_Item("Answer7", reader["Answer7"]);
        }

        private void rgAnswer1_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer3_ValueChanged(object sender, EventArgs e)
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
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.get_Item("Answer3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.get_Item("Answer4");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.get_Item("Answer5");
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 5).Value = this.get_Item("Answer6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 5).Value = this.get_Item("Answer7");
        }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription2")]
        private Label lblAnswerDescription2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription1")]
        private Label lblQuestionDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription2")]
        private Label lblQuestionDescription2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription3")]
        private Label lblQuestionDescription3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion3")]
        private Label lblQuestion3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion4")]
        private Label lblQuestion4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion5")]
        private Label lblQuestion5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion6")]
        private Label lblQuestion6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion7")]
        private Label lblQuestion7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7")]
        private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer6")]
        private RadioGroup rgAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer5")]
        private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer4")]
        private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer3")]
        private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer1")]
        private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0102A;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object obj2;
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                obj2 = this.rgAnswer1.Value;
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                obj2 = this.rgAnswer3.Value;
            }
            else if (string.Compare(Index, "Answer4", true) == 0)
            {
                obj2 = this.rgAnswer4.Value;
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                obj2 = this.rgAnswer5.Value;
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                obj2 = this.rgAnswer6.Value;
            }
            else
            {
                if (string.Compare(Index, "Answer7", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                obj2 = this.rgAnswer7.Value;
            }
            return obj2;
        }

        // Warning: Properties with arguments are not supported in C#. Setter of a Item property was decompiled as a method.
        protected void set_Item(string Index, object Value)
        {
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer1, Value);
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer3, Value);
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
                Functions.SetRadioGroupValue(this.rgAnswer6, Value);
            }
            else if (string.Compare(Index, "Answer7", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer7, Value);
            }
        }

    }
}

