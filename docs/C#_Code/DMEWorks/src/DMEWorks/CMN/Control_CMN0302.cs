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

    public class Control_CMN0302 : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0302()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            Functions.SetNumericBoxValue(this.nmbAnswer12, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer14, DBNull.Value);
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
            this.lblQuestion2 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestionDescription3 = new Label();
            this.lblQuestionDescription2 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.pnlAnswers = new Panel();
            this.rgAnswer14 = new RadioGroup();
            this.pnlAnswer1 = new Panel();
            this.nmbAnswer12 = new NumericBox();
            this.Label1 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            this.pnlAnswer1.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.lblQuestion2);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription3);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x70, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x2b0, 0x70);
            this.pnlQuestions.TabIndex = 1;
            this.lblQuestion2.BackColor = Color.Transparent;
            this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion2.Dock = DockStyle.Top;
            this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion2.Location = new Point(0, 0x58);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new Size(0x2b0, 0x18);
            this.lblQuestion2.TabIndex = 4;
            this.lblQuestion2.Text = "14. Does the patient have obstructive sleep apnea?";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x38);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x2b0, 0x20);
            this.lblQuestion1.TabIndex = 3;
            this.lblQuestion1.Text = "12. How many episodes of apnea lasting greater than 10 seconds does the patient have during 6-7 hours of recorded sleep?  (Number of episodes) (If greater than 99, enter 99.)";
            this.lblQuestionDescription3.BackColor = Color.Transparent;
            this.lblQuestionDescription3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription3.Dock = DockStyle.Top;
            this.lblQuestionDescription3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription3.Location = new Point(0, 0x24);
            this.lblQuestionDescription3.Name = "lblQuestionDescription3";
            this.lblQuestionDescription3.Size = new Size(0x2b0, 20);
            this.lblQuestionDescription3.TabIndex = 2;
            this.lblQuestionDescription3.Text = "QUESTIONS 1-11,  AND 13 ARE RESERVED FOR OTHER OR FUTURE USE.";
            this.lblQuestionDescription2.BackColor = Color.Transparent;
            this.lblQuestionDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription2.Dock = DockStyle.Top;
            this.lblQuestionDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription2.Location = new Point(0, 0x10);
            this.lblQuestionDescription2.Name = "lblQuestionDescription2";
            this.lblQuestionDescription2.Size = new Size(0x2b0, 20);
            this.lblQuestionDescription2.TabIndex = 1;
            this.lblQuestionDescription2.Text = "(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            this.lblQuestionDescription1.BackColor = Color.Transparent;
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x2b0, 0x10);
            this.lblQuestionDescription1.TabIndex = 0;
            this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 12 AND 14 FOR CPAP";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer14);
            this.pnlAnswers.Controls.Add(this.pnlAnswer1);
            this.pnlAnswers.Controls.Add(this.Label1);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x70, 0x70);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer14.BackColor = SystemColors.Control;
            this.rgAnswer14.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer14.Dock = DockStyle.Top;
            this.rgAnswer14.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer14.Location = new Point(0, 0x58);
            this.rgAnswer14.Name = "rgAnswer14";
            this.rgAnswer14.Size = new Size(0x70, 0x18);
            this.rgAnswer14.TabIndex = 3;
            this.rgAnswer14.Value = "";
            this.pnlAnswer1.BackColor = Color.Transparent;
            this.pnlAnswer1.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAnswer1.Controls.Add(this.nmbAnswer12);
            this.pnlAnswer1.Dock = DockStyle.Top;
            this.pnlAnswer1.Location = new Point(0, 0x38);
            this.pnlAnswer1.Name = "pnlAnswer1";
            this.pnlAnswer1.Size = new Size(0x70, 0x20);
            this.pnlAnswer1.TabIndex = 2;
            this.nmbAnswer12.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer12.Location = new Point(8, 4);
            this.nmbAnswer12.Name = "nmbAnswer12";
            this.nmbAnswer12.Size = new Size(0x60, 20);
            this.nmbAnswer12.TabIndex = 0;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.BorderStyle = BorderStyle.FixedSingle;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0x24);
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
            this.lblAnswerDescription1.Size = new Size(0x70, 0x24);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0302";
            base.Size = new Size(800, 0x70);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            this.pnlAnswer1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            Functions.SetNumericBoxValue(this.nmbAnswer12, reader["Answer12"]);
            Functions.SetRadioGroupValue(this.rgAnswer14, reader["Answer14"]);
        }

        private void nmbAnswer12_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer14_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer12", MySqlType.Int).Value = this.nmbAnswer12.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 5).Value = this.rgAnswer14.Value;
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion2")]
        private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription3")]
        private Label lblQuestionDescription3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("pnlAnswer1")]
        private Panel pnlAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer14")]
        private RadioGroup rgAnswer14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer12")]
        private NumericBox nmbAnswer12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0302;
    }
}

