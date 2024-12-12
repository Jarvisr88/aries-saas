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

    public class Control_CMN0102B : Control_CMNBase
    {
        private IContainer components;
        public const string Ulcer1_Stage = "Answer21_Ulcer1_Stage";
        public const string Ulcer2_Stage = "Answer21_Ulcer2_Stage";
        public const string Ulcer3_Stage = "Answer21_Ulcer3_Stage";
        public const string Ulcer1_MaxLength = "Answer21_Ulcer1_MaxLength";
        public const string Ulcer2_MaxLength = "Answer21_Ulcer2_MaxLength";
        public const string Ulcer3_MaxLength = "Answer21_Ulcer3_MaxLength";
        public const string Ulcer1_MaxWidth = "Answer21_Ulcer1_MaxWidth";
        public const string Ulcer2_MaxWidth = "Answer21_Ulcer2_MaxWidth";
        public const string Ulcer3_MaxWidth = "Answer21_Ulcer3_MaxWidth";

        public Control_CMN0102B()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer12", DBNull.Value);
            this.set_Item("Answer13", DBNull.Value);
            this.set_Item("Answer14", DBNull.Value);
            this.set_Item("Answer15", DBNull.Value);
            this.set_Item("Answer16", DBNull.Value);
            this.set_Item("Answer19", DBNull.Value);
            this.set_Item("Answer20", DBNull.Value);
            this.set_Item("Answer22", DBNull.Value);
            this.set_Item("Answer21_Ulcer1_Stage", DBNull.Value);
            this.set_Item("Answer21_Ulcer1_MaxLength", DBNull.Value);
            this.set_Item("Answer21_Ulcer1_MaxWidth", DBNull.Value);
            this.set_Item("Answer21_Ulcer2_Stage", DBNull.Value);
            this.set_Item("Answer21_Ulcer2_MaxLength", DBNull.Value);
            this.set_Item("Answer21_Ulcer2_MaxWidth", DBNull.Value);
            this.set_Item("Answer21_Ulcer3_Stage", DBNull.Value);
            this.set_Item("Answer21_Ulcer3_MaxLength", DBNull.Value);
            this.set_Item("Answer21_Ulcer3_MaxWidth", DBNull.Value);
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
            this.rgAnswer22 = new RadioGroup();
            this.lblAnswer21 = new Label();
            this.rgAnswer20 = new RadioGroup();
            this.rgAnswer19 = new RadioGroup();
            this.rgAnswer16 = new RadioGroup();
            this.rgAnswer15 = new RadioGroup();
            this.rgAnswer14 = new RadioGroup();
            this.rgAnswer13 = new RadioGroup();
            this.rgAnswer12 = new RadioGroup();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions = new Panel();
            this.lblQuestion8 = new Label();
            this.pnlQuestion21 = new Panel();
            this.nmbUlcer3_MaxWidth = new NumericBox();
            this.nmbUlcer2_MaxWidth = new NumericBox();
            this.nmbUlcer1_MaxWidth = new NumericBox();
            this.nmbUlcer3_MaxLength = new NumericBox();
            this.nmbUlcer2_MaxLength = new NumericBox();
            this.nmbUlcer1_MaxLength = new NumericBox();
            this.Label5 = new Label();
            this.txtUlcer3_Stage = new TextBox();
            this.txtUlcer2_Stage = new TextBox();
            this.txtUlcer1_Stage = new TextBox();
            this.Label1 = new Label();
            this.Label9 = new Label();
            this.Label10 = new Label();
            this.Label11 = new Label();
            this.Label12 = new Label();
            this.Label13 = new Label();
            this.Label14 = new Label();
            this.lblQuestion20 = new Label();
            this.lblQuestion19 = new Label();
            this.lblQuestion16 = new Label();
            this.lblQuestion15 = new Label();
            this.lblQuestion14 = new Label();
            this.lblQuestion13 = new Label();
            this.lblQuestion12 = new Label();
            this.lblQuestionDescription3 = new Label();
            this.lblQuestionDescription2 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.pnlAnswers.SuspendLayout();
            this.pnlQuestions.SuspendLayout();
            this.pnlQuestion21.SuspendLayout();
            base.SuspendLayout();
            this.pnlAnswers.Controls.Add(this.rgAnswer22);
            this.pnlAnswers.Controls.Add(this.lblAnswer21);
            this.pnlAnswers.Controls.Add(this.rgAnswer20);
            this.pnlAnswers.Controls.Add(this.rgAnswer19);
            this.pnlAnswers.Controls.Add(this.rgAnswer16);
            this.pnlAnswers.Controls.Add(this.rgAnswer15);
            this.pnlAnswers.Controls.Add(this.rgAnswer14);
            this.pnlAnswers.Controls.Add(this.rgAnswer13);
            this.pnlAnswers.Controls.Add(this.rgAnswer12);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x88, 0x158);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer22.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer22.Dock = DockStyle.Top;
            this.rgAnswer22.Items = new string[] { "1", "2", "3" };
            this.rgAnswer22.Location = new Point(0, 0x144);
            this.rgAnswer22.Name = "rgAnswer22";
            this.rgAnswer22.Size = new Size(0x88, 20);
            this.rgAnswer22.TabIndex = 9;
            this.rgAnswer22.Value = "";
            this.lblAnswer21.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswer21.Dock = DockStyle.Top;
            this.lblAnswer21.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswer21.Location = new Point(0, 200);
            this.lblAnswer21.Name = "lblAnswer21";
            this.lblAnswer21.Size = new Size(0x88, 0x7c);
            this.lblAnswer21.TabIndex = 8;
            this.rgAnswer20.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer20.Dock = DockStyle.Top;
            this.rgAnswer20.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer20.Location = new Point(0, 180);
            this.rgAnswer20.Name = "rgAnswer20";
            this.rgAnswer20.Size = new Size(0x88, 20);
            this.rgAnswer20.TabIndex = 7;
            this.rgAnswer20.Value = "";
            this.rgAnswer19.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer19.Dock = DockStyle.Top;
            this.rgAnswer19.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer19.Location = new Point(0, 160);
            this.rgAnswer19.Name = "rgAnswer19";
            this.rgAnswer19.Size = new Size(0x88, 20);
            this.rgAnswer19.TabIndex = 6;
            this.rgAnswer19.Value = "";
            this.rgAnswer16.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer16.Dock = DockStyle.Top;
            this.rgAnswer16.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer16.Location = new Point(0, 140);
            this.rgAnswer16.Name = "rgAnswer16";
            this.rgAnswer16.Size = new Size(0x88, 20);
            this.rgAnswer16.TabIndex = 5;
            this.rgAnswer16.Value = "";
            this.rgAnswer15.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer15.Dock = DockStyle.Top;
            this.rgAnswer15.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer15.Location = new Point(0, 120);
            this.rgAnswer15.Name = "rgAnswer15";
            this.rgAnswer15.Size = new Size(0x88, 20);
            this.rgAnswer15.TabIndex = 4;
            this.rgAnswer15.Value = "";
            this.rgAnswer14.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer14.Dock = DockStyle.Top;
            this.rgAnswer14.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer14.Location = new Point(0, 100);
            this.rgAnswer14.Name = "rgAnswer14";
            this.rgAnswer14.Size = new Size(0x88, 20);
            this.rgAnswer14.TabIndex = 3;
            this.rgAnswer14.Value = "";
            this.rgAnswer13.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer13.Dock = DockStyle.Top;
            this.rgAnswer13.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer13.Location = new Point(0, 80);
            this.rgAnswer13.Name = "rgAnswer13";
            this.rgAnswer13.Size = new Size(0x88, 20);
            this.rgAnswer13.TabIndex = 2;
            this.rgAnswer13.Value = "";
            this.rgAnswer12.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer12.Dock = DockStyle.Top;
            this.rgAnswer12.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer12.Location = new Point(0, 60);
            this.rgAnswer12.Name = "rgAnswer12";
            this.rgAnswer12.Size = new Size(0x88, 20);
            this.rgAnswer12.TabIndex = 1;
            this.rgAnswer12.Value = "";
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x88, 60);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            this.pnlQuestions.Controls.Add(this.lblQuestion8);
            this.pnlQuestions.Controls.Add(this.pnlQuestion21);
            this.pnlQuestions.Controls.Add(this.lblQuestion20);
            this.pnlQuestions.Controls.Add(this.lblQuestion19);
            this.pnlQuestions.Controls.Add(this.lblQuestion16);
            this.pnlQuestions.Controls.Add(this.lblQuestion15);
            this.pnlQuestions.Controls.Add(this.lblQuestion14);
            this.pnlQuestions.Controls.Add(this.lblQuestion13);
            this.pnlQuestions.Controls.Add(this.lblQuestion12);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription3);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0x88, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x298, 0x158);
            this.pnlQuestions.TabIndex = 1;
            this.lblQuestion8.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion8.Dock = DockStyle.Top;
            this.lblQuestion8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion8.Location = new Point(0, 0x144);
            this.lblQuestion8.Name = "lblQuestion8";
            this.lblQuestion8.Size = new Size(0x298, 20);
            this.lblQuestion8.TabIndex = 11;
            this.lblQuestion8.Text = "22. Over the past month, the patient's ulcer(s) has/have:  1) Improved  2) Remained the same 3) Worsened?";
            this.pnlQuestion21.BorderStyle = BorderStyle.FixedSingle;
            this.pnlQuestion21.Controls.Add(this.nmbUlcer3_MaxWidth);
            this.pnlQuestion21.Controls.Add(this.nmbUlcer2_MaxWidth);
            this.pnlQuestion21.Controls.Add(this.nmbUlcer1_MaxWidth);
            this.pnlQuestion21.Controls.Add(this.nmbUlcer3_MaxLength);
            this.pnlQuestion21.Controls.Add(this.nmbUlcer2_MaxLength);
            this.pnlQuestion21.Controls.Add(this.nmbUlcer1_MaxLength);
            this.pnlQuestion21.Controls.Add(this.Label5);
            this.pnlQuestion21.Controls.Add(this.txtUlcer3_Stage);
            this.pnlQuestion21.Controls.Add(this.txtUlcer2_Stage);
            this.pnlQuestion21.Controls.Add(this.txtUlcer1_Stage);
            this.pnlQuestion21.Controls.Add(this.Label1);
            this.pnlQuestion21.Controls.Add(this.Label9);
            this.pnlQuestion21.Controls.Add(this.Label10);
            this.pnlQuestion21.Controls.Add(this.Label11);
            this.pnlQuestion21.Controls.Add(this.Label12);
            this.pnlQuestion21.Controls.Add(this.Label13);
            this.pnlQuestion21.Controls.Add(this.Label14);
            this.pnlQuestion21.Dock = DockStyle.Top;
            this.pnlQuestion21.Location = new Point(0, 200);
            this.pnlQuestion21.Name = "pnlQuestion21";
            this.pnlQuestion21.Size = new Size(0x298, 0x7c);
            this.pnlQuestion21.TabIndex = 10;
            this.nmbUlcer3_MaxWidth.BorderStyle = BorderStyle.FixedSingle;
            this.nmbUlcer3_MaxWidth.Location = new Point(360, 100);
            this.nmbUlcer3_MaxWidth.Name = "nmbUlcer3_MaxWidth";
            this.nmbUlcer3_MaxWidth.Size = new Size(0x70, 0x13);
            this.nmbUlcer3_MaxWidth.TabIndex = 0x10;
            this.nmbUlcer3_MaxWidth.TextAlign = HorizontalAlignment.Left;
            this.nmbUlcer2_MaxWidth.BorderStyle = BorderStyle.FixedSingle;
            this.nmbUlcer2_MaxWidth.Location = new Point(0xf4, 100);
            this.nmbUlcer2_MaxWidth.Name = "nmbUlcer2_MaxWidth";
            this.nmbUlcer2_MaxWidth.Size = new Size(0x70, 0x13);
            this.nmbUlcer2_MaxWidth.TabIndex = 12;
            this.nmbUlcer2_MaxWidth.TextAlign = HorizontalAlignment.Left;
            this.nmbUlcer1_MaxWidth.BorderStyle = BorderStyle.FixedSingle;
            this.nmbUlcer1_MaxWidth.Location = new Point(0x80, 100);
            this.nmbUlcer1_MaxWidth.Name = "nmbUlcer1_MaxWidth";
            this.nmbUlcer1_MaxWidth.Size = new Size(0x70, 0x13);
            this.nmbUlcer1_MaxWidth.TabIndex = 8;
            this.nmbUlcer1_MaxWidth.TextAlign = HorizontalAlignment.Left;
            this.nmbUlcer3_MaxLength.BorderStyle = BorderStyle.FixedSingle;
            this.nmbUlcer3_MaxLength.Location = new Point(360, 80);
            this.nmbUlcer3_MaxLength.Name = "nmbUlcer3_MaxLength";
            this.nmbUlcer3_MaxLength.Size = new Size(0x70, 0x13);
            this.nmbUlcer3_MaxLength.TabIndex = 15;
            this.nmbUlcer3_MaxLength.TextAlign = HorizontalAlignment.Left;
            this.nmbUlcer2_MaxLength.BorderStyle = BorderStyle.FixedSingle;
            this.nmbUlcer2_MaxLength.Location = new Point(0xf4, 80);
            this.nmbUlcer2_MaxLength.Name = "nmbUlcer2_MaxLength";
            this.nmbUlcer2_MaxLength.Size = new Size(0x70, 0x13);
            this.nmbUlcer2_MaxLength.TabIndex = 11;
            this.nmbUlcer2_MaxLength.TextAlign = HorizontalAlignment.Left;
            this.nmbUlcer1_MaxLength.BorderStyle = BorderStyle.FixedSingle;
            this.nmbUlcer1_MaxLength.Location = new Point(0x80, 80);
            this.nmbUlcer1_MaxLength.Name = "nmbUlcer1_MaxLength";
            this.nmbUlcer1_MaxLength.Size = new Size(0x70, 0x13);
            this.nmbUlcer1_MaxLength.TabIndex = 7;
            this.nmbUlcer1_MaxLength.TextAlign = HorizontalAlignment.Left;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(360, 40);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x70, 0x13);
            this.Label5.TabIndex = 13;
            this.Label5.Text = "Ulcer # 3";
            this.Label5.TextAlign = ContentAlignment.MiddleCenter;
            this.txtUlcer3_Stage.AutoSize = false;
            this.txtUlcer3_Stage.BorderStyle = BorderStyle.FixedSingle;
            this.txtUlcer3_Stage.Location = new Point(360, 60);
            this.txtUlcer3_Stage.Name = "txtUlcer3_Stage";
            this.txtUlcer3_Stage.Size = new Size(0x70, 0x13);
            this.txtUlcer3_Stage.TabIndex = 14;
            this.txtUlcer3_Stage.Text = "";
            this.txtUlcer2_Stage.AutoSize = false;
            this.txtUlcer2_Stage.BorderStyle = BorderStyle.FixedSingle;
            this.txtUlcer2_Stage.Location = new Point(0xf4, 60);
            this.txtUlcer2_Stage.Name = "txtUlcer2_Stage";
            this.txtUlcer2_Stage.Size = new Size(0x70, 0x13);
            this.txtUlcer2_Stage.TabIndex = 10;
            this.txtUlcer2_Stage.Text = "";
            this.txtUlcer1_Stage.AutoSize = false;
            this.txtUlcer1_Stage.BorderStyle = BorderStyle.FixedSingle;
            this.txtUlcer1_Stage.Location = new Point(0x80, 60);
            this.txtUlcer1_Stage.Name = "txtUlcer1_Stage";
            this.txtUlcer1_Stage.Size = new Size(0x70, 0x13);
            this.txtUlcer1_Stage.TabIndex = 6;
            this.txtUlcer1_Stage.Text = "";
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x296, 0x27);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "21. Provide the stage and size of each pressure ulcer necessitating the use of the overlay, mattress or bed.  If the patient is highly susceptible to decubitus ulcers, but currently has no ulcer present, place a \"9\" under ulcer #1.";
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(12, 60);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x70, 0x13);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "Stage:";
            this.Label9.TextAlign = ContentAlignment.MiddleLeft;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(12, 100);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(0x70, 0x13);
            this.Label10.TabIndex = 4;
            this.Label10.Text = "Max. Width (cm):";
            this.Label10.TextAlign = ContentAlignment.MiddleLeft;
            this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label11.Location = new Point(12, 80);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(0x70, 0x13);
            this.Label11.TabIndex = 3;
            this.Label11.Text = "Max. Length (cm):";
            this.Label11.TextAlign = ContentAlignment.MiddleLeft;
            this.Label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label12.Location = new Point(0x80, 40);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x70, 0x13);
            this.Label12.TabIndex = 5;
            this.Label12.Text = "Ulcer # 1";
            this.Label12.TextAlign = ContentAlignment.MiddleCenter;
            this.Label13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label13.Location = new Point(0xf4, 40);
            this.Label13.Name = "Label13";
            this.Label13.Size = new Size(0x70, 0x13);
            this.Label13.TabIndex = 9;
            this.Label13.Text = "Ulcer # 2";
            this.Label13.TextAlign = ContentAlignment.MiddleCenter;
            this.Label14.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label14.Location = new Point(12, 40);
            this.Label14.Name = "Label14";
            this.Label14.Size = new Size(0x70, 0x13);
            this.Label14.TabIndex = 1;
            this.Label14.Text = "Pressure Ulcer";
            this.Label14.TextAlign = ContentAlignment.MiddleLeft;
            this.lblQuestion20.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion20.Dock = DockStyle.Top;
            this.lblQuestion20.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion20.Location = new Point(0, 180);
            this.lblQuestion20.Name = "lblQuestion20";
            this.lblQuestion20.Size = new Size(0x298, 20);
            this.lblQuestion20.TabIndex = 9;
            this.lblQuestion20.Text = "20. Is there a trained full-time caregiver to assist the patient and manage all aspects involved with the use of the bed?";
            this.lblQuestion19.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion19.Dock = DockStyle.Top;
            this.lblQuestion19.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion19.Location = new Point(0, 160);
            this.lblQuestion19.Name = "lblQuestion19";
            this.lblQuestion19.Size = new Size(0x298, 20);
            this.lblQuestion19.TabIndex = 8;
            this.lblQuestion19.Text = "19. Are open, moist dressings used for the treatment of the patient?";
            this.lblQuestion16.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion16.Dock = DockStyle.Top;
            this.lblQuestion16.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion16.Location = new Point(0, 140);
            this.lblQuestion16.Name = "lblQuestion16";
            this.lblQuestion16.Size = new Size(0x298, 20);
            this.lblQuestion16.TabIndex = 7;
            this.lblQuestion16.Text = "16. Was a comprehensive assessment performed after failure of conservative treatment?";
            this.lblQuestion15.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion15.Dock = DockStyle.Top;
            this.lblQuestion15.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion15.Location = new Point(0, 120);
            this.lblQuestion15.Name = "lblQuestion15";
            this.lblQuestion15.Size = new Size(0x298, 20);
            this.lblQuestion15.TabIndex = 6;
            this.lblQuestion15.Text = "15. Has a conservative treatment program been tried without success?";
            this.lblQuestion14.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion14.Dock = DockStyle.Top;
            this.lblQuestion14.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion14.Location = new Point(0, 100);
            this.lblQuestion14.Name = "lblQuestion14";
            this.lblQuestion14.Size = new Size(0x298, 20);
            this.lblQuestion14.TabIndex = 5;
            this.lblQuestion14.Text = "14. Does the patient have coexisting pulmonary disease?";
            this.lblQuestion13.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion13.Dock = DockStyle.Top;
            this.lblQuestion13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion13.Location = new Point(0, 80);
            this.lblQuestion13.Name = "lblQuestion13";
            this.lblQuestion13.Size = new Size(0x298, 20);
            this.lblQuestion13.TabIndex = 4;
            this.lblQuestion13.Text = "13. Are you supervising the use of the device?";
            this.lblQuestion12.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion12.Dock = DockStyle.Top;
            this.lblQuestion12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion12.Location = new Point(0, 60);
            this.lblQuestion12.Name = "lblQuestion12";
            this.lblQuestion12.Size = new Size(0x298, 20);
            this.lblQuestion12.TabIndex = 3;
            this.lblQuestion12.Text = "12. Is the patient highly susceptible to decubitus ulcers?";
            this.lblQuestionDescription3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription3.Dock = DockStyle.Top;
            this.lblQuestionDescription3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription3.Location = new Point(0, 0x2c);
            this.lblQuestionDescription3.Name = "lblQuestionDescription3";
            this.lblQuestionDescription3.Size = new Size(0x298, 0x10);
            this.lblQuestionDescription3.TabIndex = 2;
            this.lblQuestionDescription3.Text = "QUESTIONS 1-11, 17 AND 18 ARE RESERVED FOR OTHER OR FUTURE USE.";
            this.lblQuestionDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription2.Dock = DockStyle.Top;
            this.lblQuestionDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription2.Location = new Point(0, 0x1c);
            this.lblQuestionDescription2.Name = "lblQuestionDescription2";
            this.lblQuestionDescription2.Size = new Size(0x298, 0x10);
            this.lblQuestionDescription2.TabIndex = 1;
            this.lblQuestionDescription2.Text = "(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x298, 0x1c);
            this.lblQuestionDescription1.TabIndex = 0;
            this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 12, 13, AND 21 FOR ALTERNATING PRESSURE PADS OR MATTRESSES; 13-22 FOR AIR FLUIDIZED BEDS";
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0102B";
            base.Size = new Size(800, 0x158);
            this.pnlAnswers.ResumeLayout(false);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlQuestion21.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer12", reader["Answer12"]);
            this.set_Item("Answer13", reader["Answer13"]);
            this.set_Item("Answer14", reader["Answer14"]);
            this.set_Item("Answer15", reader["Answer15"]);
            this.set_Item("Answer16", reader["Answer16"]);
            this.set_Item("Answer19", reader["Answer19"]);
            this.set_Item("Answer20", reader["Answer20"]);
            this.set_Item("Answer22", reader["Answer22"]);
        }

        private void nmbUlcer1_MaxLength_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbUlcer1_MaxWidth_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbUlcer2_MaxLength_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbUlcer2_MaxWidth_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbUlcer3_MaxLength_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbUlcer3_MaxWidth_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
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

        private void rgAnswer15_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer16_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer19_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer20_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer22_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 5).Value = this.get_Item("Answer12");
            cmd.Parameters.Add("Answer13", MySqlType.VarChar, 5).Value = this.get_Item("Answer13");
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 5).Value = this.get_Item("Answer14");
            cmd.Parameters.Add("Answer15", MySqlType.VarChar, 5).Value = this.get_Item("Answer15");
            cmd.Parameters.Add("Answer16", MySqlType.VarChar, 5).Value = this.get_Item("Answer16");
            cmd.Parameters.Add("Answer19", MySqlType.VarChar, 5).Value = this.get_Item("Answer19");
            cmd.Parameters.Add("Answer20", MySqlType.VarChar, 5).Value = this.get_Item("Answer20");
            cmd.Parameters.Add("Answer22", MySqlType.VarChar, 5).Value = this.get_Item("Answer22");
            cmd.Parameters.Add("Answer21_Ulcer1_Stage", MySqlType.VarChar, 30).Value = this.get_Item("Answer21_Ulcer1_Stage");
            cmd.Parameters.Add("Answer21_Ulcer1_MaxLength", MySqlType.Double).Value = this.get_Item("Answer21_Ulcer1_MaxLength");
            cmd.Parameters.Add("Answer21_Ulcer1_MaxWidth", MySqlType.Double).Value = this.get_Item("Answer21_Ulcer1_MaxWidth");
            cmd.Parameters.Add("Answer21_Ulcer2_Stage", MySqlType.VarChar, 30).Value = this.get_Item("Answer21_Ulcer2_Stage");
            cmd.Parameters.Add("Answer21_Ulcer2_MaxLength", MySqlType.Double).Value = this.get_Item("Answer21_Ulcer2_MaxLength");
            cmd.Parameters.Add("Answer21_Ulcer2_MaxWidth", MySqlType.Double).Value = this.get_Item("Answer21_Ulcer2_MaxWidth");
            cmd.Parameters.Add("Answer21_Ulcer3_Stage", MySqlType.VarChar, 30).Value = this.get_Item("Answer21_Ulcer3_Stage");
            cmd.Parameters.Add("Answer21_Ulcer3_MaxLength", MySqlType.Double).Value = this.get_Item("Answer21_Ulcer3_MaxLength");
            cmd.Parameters.Add("Answer21_Ulcer3_MaxWidth", MySqlType.Double).Value = this.get_Item("Answer21_Ulcer3_MaxWidth");
        }

        private void txtUlcer1_Stage_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtUlcer2_Stage_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtUlcer3_Stage_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription2")]
        private Label lblQuestionDescription2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription3")]
        private Label lblQuestionDescription3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription1")]
        private Label lblQuestionDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion8")]
        private Label lblQuestion8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label9")]
        private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label10")]
        private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label11")]
        private Label Label11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label12")]
        private Label Label12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label13")]
        private Label Label13 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label14")]
        private Label Label14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer22")]
        private RadioGroup rgAnswer22 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswer21")]
        private Label lblAnswer21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer20")]
        private RadioGroup rgAnswer20 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer16")]
        private RadioGroup rgAnswer16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer15")]
        private RadioGroup rgAnswer15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("txtUlcer3_Stage")]
        private TextBox txtUlcer3_Stage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUlcer2_Stage")]
        private TextBox txtUlcer2_Stage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUlcer1_Stage")]
        private TextBox txtUlcer1_Stage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestion21")]
        private Panel pnlQuestion21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion20")]
        private Label lblQuestion20 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion16")]
        private Label lblQuestion16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion15")]
        private Label lblQuestion15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion14")]
        private Label lblQuestion14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion13")]
        private Label lblQuestion13 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion12")]
        private Label lblQuestion12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbUlcer1_MaxLength")]
        private NumericBox nmbUlcer1_MaxLength { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbUlcer2_MaxLength")]
        private NumericBox nmbUlcer2_MaxLength { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbUlcer3_MaxLength")]
        private NumericBox nmbUlcer3_MaxLength { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbUlcer3_MaxWidth")]
        private NumericBox nmbUlcer3_MaxWidth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbUlcer2_MaxWidth")]
        private NumericBox nmbUlcer2_MaxWidth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion19")]
        private Label lblQuestion19 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer19")]
        private RadioGroup rgAnswer19 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbUlcer1_MaxWidth")]
        private NumericBox nmbUlcer1_MaxWidth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0102B;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object text;
            if (string.Compare(Index, "Answer12", true) == 0)
            {
                text = this.rgAnswer12.Value;
            }
            else if (string.Compare(Index, "Answer13", true) == 0)
            {
                text = this.rgAnswer13.Value;
            }
            else if (string.Compare(Index, "Answer14", true) == 0)
            {
                text = this.rgAnswer14.Value;
            }
            else if (string.Compare(Index, "Answer15", true) == 0)
            {
                text = this.rgAnswer15.Value;
            }
            else if (string.Compare(Index, "Answer16", true) == 0)
            {
                text = this.rgAnswer16.Value;
            }
            else if (string.Compare(Index, "Answer19", true) == 0)
            {
                text = this.rgAnswer19.Value;
            }
            else if (string.Compare(Index, "Answer20", true) == 0)
            {
                text = this.rgAnswer20.Value;
            }
            else if (string.Compare(Index, "Answer22", true) == 0)
            {
                text = this.rgAnswer22.Value;
            }
            else if (string.Compare(Index, "Answer21_Ulcer1_Stage", true) == 0)
            {
                text = this.txtUlcer1_Stage.Text;
            }
            else if (string.Compare(Index, "Answer21_Ulcer2_Stage", true) == 0)
            {
                text = this.txtUlcer2_Stage.Text;
            }
            else if (string.Compare(Index, "Answer21_Ulcer3_Stage", true) == 0)
            {
                text = this.txtUlcer3_Stage.Text;
            }
            else if (string.Compare(Index, "Answer21_Ulcer1_MaxLength", true) == 0)
            {
                text = this.nmbUlcer1_MaxLength.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Answer21_Ulcer2_MaxLength", true) == 0)
            {
                text = this.nmbUlcer2_MaxLength.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Answer21_Ulcer3_MaxLength", true) == 0)
            {
                text = this.nmbUlcer3_MaxLength.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Answer21_Ulcer1_MaxWidth", true) == 0)
            {
                text = this.nmbUlcer1_MaxWidth.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Answer21_Ulcer2_MaxWidth", true) == 0)
            {
                text = this.nmbUlcer2_MaxWidth.AsDouble.GetValueOrDefault(0.0);
            }
            else
            {
                if (string.Compare(Index, "Answer21_Ulcer3_MaxWidth", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                text = this.nmbUlcer3_MaxWidth.AsDouble.GetValueOrDefault(0.0);
            }
            return text;
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
            else if (string.Compare(Index, "Answer15", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer15, Value);
            }
            else if (string.Compare(Index, "Answer16", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer16, Value);
            }
            else if (string.Compare(Index, "Answer19", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer19, Value);
            }
            else if (string.Compare(Index, "Answer20", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer20, Value);
            }
            else if (string.Compare(Index, "Answer22", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer22, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer1_Stage", true) == 0)
            {
                Functions.SetTextBoxText(this.txtUlcer1_Stage, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer2_Stage", true) == 0)
            {
                Functions.SetTextBoxText(this.txtUlcer2_Stage, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer3_Stage", true) == 0)
            {
                Functions.SetTextBoxText(this.txtUlcer3_Stage, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer1_MaxLength", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbUlcer1_MaxLength, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer2_MaxLength", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbUlcer2_MaxLength, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer3_MaxLength", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbUlcer3_MaxLength, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer1_MaxWidth", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbUlcer1_MaxWidth, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer2_MaxWidth", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbUlcer2_MaxWidth, Value);
            }
            else if (string.Compare(Index, "Answer21_Ulcer3_MaxWidth", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbUlcer3_MaxWidth, Value);
            }
        }

    }
}

