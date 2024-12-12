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

    public class Control_CMN0203A : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0203A()
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
            this.pnlQuestions = new Panel();
            this.lblQuestion7 = new Label();
            this.lblQuestion6 = new Label();
            this.lblQuestion5 = new Label();
            this.lblQuestion4 = new Label();
            this.lblQuestion3 = new Label();
            this.lblQuestion2 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestionDescription2 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.pnlAnswers = new Panel();
            this.rgAnswer7 = new RadioGroup();
            this.rgAnswer6 = new RadioGroup();
            this.Panel2 = new Panel();
            this.nmbAnswer5 = new NumericBox();
            this.rgAnswer4 = new RadioGroup();
            this.rgAnswer3 = new RadioGroup();
            this.rgAnswer2 = new RadioGroup();
            this.rgAnswer1 = new RadioGroup();
            this.lblAnswerDescription1 = new Label();
            this.Panel1 = new Panel();
            this.lblItem7 = new Label();
            this.lblItem6 = new Label();
            this.lblItem5 = new Label();
            this.lblItem4 = new Label();
            this.lblItem3 = new Label();
            this.lblItem2 = new Label();
            this.lblItem1 = new Label();
            this.Label9 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.lblQuestion7);
            this.pnlQuestions.Controls.Add(this.lblQuestion6);
            this.pnlQuestions.Controls.Add(this.lblQuestion5);
            this.pnlQuestions.Controls.Add(this.lblQuestion4);
            this.pnlQuestions.Controls.Add(this.lblQuestion3);
            this.pnlQuestions.Controls.Add(this.lblQuestion2);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x100, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x220, 240);
            this.pnlQuestions.TabIndex = 2;
            this.lblQuestion7.BackColor = Color.Transparent;
            this.lblQuestion7.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion7.Dock = DockStyle.Top;
            this.lblQuestion7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion7.Location = new Point(0, 220);
            this.lblQuestion7.Name = "lblQuestion7";
            this.lblQuestion7.Size = new Size(0x220, 20);
            this.lblQuestion7.TabIndex = 8;
            this.lblQuestion7.Text = "7.  Is the patient unable to operate any type of manual wheelchair?";
            this.lblQuestion6.BackColor = Color.Transparent;
            this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion6.Dock = DockStyle.Top;
            this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion6.Location = new Point(0, 0xc0);
            this.lblQuestion6.Name = "lblQuestion6";
            this.lblQuestion6.Size = new Size(0x220, 0x1c);
            this.lblQuestion6.TabIndex = 7;
            this.lblQuestion6.Text = "6.  Does the patient have severe weakness of the upper extremities due to a neurologic, muscular, or cardiopulmonary disease/condition?";
            this.lblQuestion5.BackColor = Color.Transparent;
            this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion5.Dock = DockStyle.Top;
            this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion5.Location = new Point(0, 0xa4);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new Size(0x220, 0x1c);
            this.lblQuestion5.TabIndex = 6;
            this.lblQuestion5.Text = "5.  How many hours per day does the patient usually spend in the wheelchair? (1-24) (Round up to the next hour)";
            this.lblQuestion4.BackColor = Color.Transparent;
            this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion4.Dock = DockStyle.Top;
            this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion4.Location = new Point(0, 0x90);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new Size(0x220, 20);
            this.lblQuestion4.TabIndex = 5;
            this.lblQuestion4.Text = "4.  Does the patient have a need for arm height different than that available using non-adjustable arms?";
            this.lblQuestion3.BackColor = Color.Transparent;
            this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion3.Dock = DockStyle.Top;
            this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion3.Location = new Point(0, 0x68);
            this.lblQuestion3.Name = "lblQuestion3";
            this.lblQuestion3.Size = new Size(0x220, 40);
            this.lblQuestion3.TabIndex = 4;
            this.lblQuestion3.Text = "3.  Does the patient have a cast, brace or musculoskeletal condition, which prevents 90 degree flexion of the knee, or does the patient have significant edema of the lower extremities that requires an elevating legrest, or is a reclining back ordered?";
            this.lblQuestion2.BackColor = Color.Transparent;
            this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion2.Dock = DockStyle.Top;
            this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion2.Location = new Point(0, 0x48);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new Size(0x220, 0x20);
            this.lblQuestion2.TabIndex = 3;
            this.lblQuestion2.Text = "2.  Does the patient have quadriplegia, a fixed hip angle, a trunk cast or brace, excessive extensor tone of the trunk muscles or a need to rest in a recumbent position two or more times during the day?";
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x2c);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x220, 0x1c);
            this.lblQuestion1.TabIndex = 2;
            this.lblQuestion1.Text = "1.  Does the patient require and use a wheelchair to move around in their residence?";
            this.lblQuestionDescription2.BackColor = Color.Transparent;
            this.lblQuestionDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription2.Dock = DockStyle.Top;
            this.lblQuestionDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription2.Location = new Point(0, 0x1c);
            this.lblQuestionDescription2.Name = "lblQuestionDescription2";
            this.lblQuestionDescription2.Size = new Size(0x220, 0x10);
            this.lblQuestionDescription2.TabIndex = 1;
            this.lblQuestionDescription2.Text = "(Circle Y for Yes, N for No, or D for Does Not Apply, unless otherwise noted)";
            this.lblQuestionDescription1.BackColor = Color.Transparent;
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x220, 0x1c);
            this.lblQuestionDescription1.TabIndex = 0;
            this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 1, 6, AND 7 FOR MOTORIZED WHEELCHAIR BASE, 1 - 5 FOR WHEELCHAIR OPTIONS/ACCESSORIES.";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer7);
            this.pnlAnswers.Controls.Add(this.rgAnswer6);
            this.pnlAnswers.Controls.Add(this.Panel2);
            this.pnlAnswers.Controls.Add(this.rgAnswer4);
            this.pnlAnswers.Controls.Add(this.rgAnswer3);
            this.pnlAnswers.Controls.Add(this.rgAnswer2);
            this.pnlAnswers.Controls.Add(this.rgAnswer1);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0x90, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x70, 240);
            this.pnlAnswers.TabIndex = 1;
            this.rgAnswer7.BackColor = SystemColors.Control;
            this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer7.Dock = DockStyle.Top;
            this.rgAnswer7.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer7.Location = new Point(0, 220);
            this.rgAnswer7.Name = "rgAnswer7";
            this.rgAnswer7.Size = new Size(0x70, 20);
            this.rgAnswer7.TabIndex = 7;
            this.rgAnswer7.Value = "";
            this.rgAnswer6.BackColor = SystemColors.Control;
            this.rgAnswer6.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer6.Dock = DockStyle.Top;
            this.rgAnswer6.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer6.Location = new Point(0, 0xc0);
            this.rgAnswer6.Name = "rgAnswer6";
            this.rgAnswer6.Size = new Size(0x70, 0x1c);
            this.rgAnswer6.TabIndex = 6;
            this.rgAnswer6.Value = "";
            this.Panel2.BackColor = Color.Transparent;
            this.Panel2.BorderStyle = BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.nmbAnswer5);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Location = new Point(0, 0xa4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(0x70, 0x1c);
            this.Panel2.TabIndex = 5;
            this.nmbAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer5.Location = new Point(8, 4);
            this.nmbAnswer5.Name = "nmbAnswer5";
            this.nmbAnswer5.Size = new Size(0x60, 20);
            this.nmbAnswer5.TabIndex = 0;
            this.nmbAnswer5.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer4.BackColor = SystemColors.Control;
            this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer4.Dock = DockStyle.Top;
            this.rgAnswer4.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer4.Location = new Point(0, 0x90);
            this.rgAnswer4.Name = "rgAnswer4";
            this.rgAnswer4.Size = new Size(0x70, 20);
            this.rgAnswer4.TabIndex = 4;
            this.rgAnswer4.Value = "";
            this.rgAnswer3.BackColor = SystemColors.Control;
            this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer3.Dock = DockStyle.Top;
            this.rgAnswer3.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer3.Location = new Point(0, 0x68);
            this.rgAnswer3.Name = "rgAnswer3";
            this.rgAnswer3.Size = new Size(0x70, 40);
            this.rgAnswer3.TabIndex = 3;
            this.rgAnswer3.Value = "";
            this.rgAnswer2.BackColor = SystemColors.Control;
            this.rgAnswer2.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer2.Dock = DockStyle.Top;
            this.rgAnswer2.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer2.Location = new Point(0, 0x48);
            this.rgAnswer2.Name = "rgAnswer2";
            this.rgAnswer2.Size = new Size(0x70, 0x20);
            this.rgAnswer2.TabIndex = 2;
            this.rgAnswer2.Value = "";
            this.rgAnswer1.BackColor = SystemColors.Control;
            this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer1.Dock = DockStyle.Top;
            this.rgAnswer1.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer1.Location = new Point(0, 0x2c);
            this.rgAnswer1.Name = "rgAnswer1";
            this.rgAnswer1.Size = new Size(0x70, 0x1c);
            this.rgAnswer1.TabIndex = 1;
            this.rgAnswer1.Value = "";
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x70, 0x2c);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            this.Panel1.BackColor = Color.Transparent;
            this.Panel1.Controls.Add(this.lblItem7);
            this.Panel1.Controls.Add(this.lblItem6);
            this.Panel1.Controls.Add(this.lblItem5);
            this.Panel1.Controls.Add(this.lblItem4);
            this.Panel1.Controls.Add(this.lblItem3);
            this.Panel1.Controls.Add(this.lblItem2);
            this.Panel1.Controls.Add(this.lblItem1);
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Dock = DockStyle.Left;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x90, 240);
            this.Panel1.TabIndex = 0;
            this.lblItem7.BackColor = Color.Transparent;
            this.lblItem7.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem7.Dock = DockStyle.Top;
            this.lblItem7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem7.Location = new Point(0, 220);
            this.lblItem7.Name = "lblItem7";
            this.lblItem7.Size = new Size(0x90, 20);
            this.lblItem7.TabIndex = 7;
            this.lblItem7.Text = "Motorized Whlchr Base";
            this.lblItem6.BackColor = Color.Transparent;
            this.lblItem6.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem6.Dock = DockStyle.Top;
            this.lblItem6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem6.Location = new Point(0, 0xc0);
            this.lblItem6.Name = "lblItem6";
            this.lblItem6.Size = new Size(0x90, 0x1c);
            this.lblItem6.TabIndex = 6;
            this.lblItem6.Text = "Motorized Whlchr Base";
            this.lblItem5.BackColor = Color.Transparent;
            this.lblItem5.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem5.Dock = DockStyle.Top;
            this.lblItem5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem5.Location = new Point(0, 0xa4);
            this.lblItem5.Name = "lblItem5";
            this.lblItem5.Size = new Size(0x90, 0x1c);
            this.lblItem5.TabIndex = 5;
            this.lblItem5.Text = "Reclining Back; Adjustable Height Armrest";
            this.lblItem4.BackColor = Color.Transparent;
            this.lblItem4.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem4.Dock = DockStyle.Top;
            this.lblItem4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem4.Location = new Point(0, 0x90);
            this.lblItem4.Name = "lblItem4";
            this.lblItem4.Size = new Size(0x90, 20);
            this.lblItem4.TabIndex = 4;
            this.lblItem4.Text = "Adjustable Height Armrest";
            this.lblItem3.BackColor = Color.Transparent;
            this.lblItem3.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem3.Dock = DockStyle.Top;
            this.lblItem3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem3.Location = new Point(0, 0x68);
            this.lblItem3.Name = "lblItem3";
            this.lblItem3.Size = new Size(0x90, 40);
            this.lblItem3.TabIndex = 3;
            this.lblItem3.Text = "Elevating Legrest";
            this.lblItem2.BackColor = Color.Transparent;
            this.lblItem2.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem2.Dock = DockStyle.Top;
            this.lblItem2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem2.Location = new Point(0, 0x48);
            this.lblItem2.Name = "lblItem2";
            this.lblItem2.Size = new Size(0x90, 0x20);
            this.lblItem2.TabIndex = 2;
            this.lblItem2.Text = "Reclining Back";
            this.lblItem1.BackColor = Color.Transparent;
            this.lblItem1.BorderStyle = BorderStyle.FixedSingle;
            this.lblItem1.Dock = DockStyle.Top;
            this.lblItem1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblItem1.Location = new Point(0, 0x2c);
            this.lblItem1.Name = "lblItem1";
            this.lblItem1.Size = new Size(0x90, 0x1c);
            this.lblItem1.TabIndex = 1;
            this.lblItem1.Text = "Motorized Whlchr Base and All Accessories";
            this.Label9.BackColor = Color.Transparent;
            this.Label9.BorderStyle = BorderStyle.FixedSingle;
            this.Label9.Dock = DockStyle.Top;
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(0, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x90, 0x2c);
            this.Label9.TabIndex = 0;
            this.Label9.Text = "ITEM ADDRESSED";
            this.Label9.TextAlign = ContentAlignment.TopCenter;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Controls.Add(this.Panel1);
            base.Name = "Control_CMN0203A";
            base.Size = new Size(800, 240);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
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

        private void nmbAnswer5_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer1_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer2_ValueChanged(object sender, EventArgs e)
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
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 5).Value = this.get_Item("Answer2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.get_Item("Answer3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.get_Item("Answer4");
            cmd.Parameters.Add("Answer5", MySqlType.Int, 4).Value = this.get_Item("Answer5");
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 50).Value = this.get_Item("Answer6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 50).Value = this.get_Item("Answer7");
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion6")]
        private Label lblQuestion6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion7")]
        private Label lblQuestion7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion5")]
        private Label lblQuestion5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription2")]
        private Label lblQuestionDescription2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion4")]
        private Label lblQuestion4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion3")]
        private Label lblQuestion3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem5")]
        private Label lblItem5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem6")]
        private Label lblItem6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem4")]
        private Label lblItem4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem1")]
        private Label lblItem1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem3")]
        private Label lblItem3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem2")]
        private Label lblItem2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label9")]
        private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion2")]
        private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem7")]
        private Label lblItem7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel2")]
        private Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer7")]
        private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer6")]
        private RadioGroup rgAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer4")]
        private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer3")]
        private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer2")]
        private RadioGroup rgAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer1")]
        private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer5")]
        internal virtual NumericBox nmbAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0203A;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object valueOrDefault;
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                valueOrDefault = this.rgAnswer1.Value;
            }
            else if (string.Compare(Index, "Answer2", true) == 0)
            {
                valueOrDefault = this.rgAnswer2.Value;
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                valueOrDefault = this.rgAnswer3.Value;
            }
            else if (string.Compare(Index, "Answer4", true) == 0)
            {
                valueOrDefault = this.rgAnswer4.Value;
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                valueOrDefault = this.nmbAnswer5.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer6", true) == 0)
            {
                valueOrDefault = this.rgAnswer6.Value;
            }
            else
            {
                if (string.Compare(Index, "Answer7", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                valueOrDefault = this.rgAnswer7.Value;
            }
            return valueOrDefault;
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
                Functions.SetRadioGroupValue(this.rgAnswer2, Value);
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
                Functions.SetNumericBoxValue(this.nmbAnswer5, Value);
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

