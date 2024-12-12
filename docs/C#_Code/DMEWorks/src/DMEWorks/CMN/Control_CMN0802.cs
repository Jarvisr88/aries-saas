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

    public class Control_CMN0802 : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN0802()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            Functions.SetTextBoxText(this.txtAnswer1_HCPCS, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer1_MG, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer1_Times, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer2_HCPCS, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer2_MG, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer2_Times, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer3_HCPCS, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer3_MG, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer3_Times, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer4, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer5_1, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer5_2, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer5_3, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer8, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer9, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer10, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbAnswer11, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer12, DBNull.Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dtbAnswer11_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.pnlAnswers = new Panel();
            this.rgAnswer12 = new RadioGroup();
            this.pnlAnswer11 = new Panel();
            this.pnlAnswer10 = new Panel();
            this.txtAnswer10 = new TextBox();
            this.pnlAnswer9 = new Panel();
            this.txtAnswer9 = new TextBox();
            this.pnlAnswer8 = new Panel();
            this.txtAnswer8 = new TextBox();
            this.pnlAnswer5 = new Panel();
            this.txtAnswer5_3 = new TextBox();
            this.txtAnswer5_2 = new TextBox();
            this.txtAnswer5_1 = new TextBox();
            this.rgAnswer4 = new RadioGroup();
            this.lblAnswer123 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions = new Panel();
            this.lblQuestion12 = new Label();
            this.lblQuestion11 = new Label();
            this.lblQuestion10 = new Label();
            this.lblQuestion9 = new Label();
            this.lblQuestion8 = new Label();
            this.Panel1 = new Panel();
            this.Label2 = new Label();
            this.Label8 = new Label();
            this.lblQuestion5 = new Label();
            this.Label4 = new Label();
            this.Label6 = new Label();
            this.Label7 = new Label();
            this.lblQuestion4 = new Label();
            this.pnlQuestion123 = new Panel();
            this.nmbAnswer3_Times = new NumericBox();
            this.nmbAnswer3_MG = new NumericBox();
            this.nmbAnswer1_MG = new NumericBox();
            this.nmbAnswer2_Times = new NumericBox();
            this.nmbAnswer2_MG = new NumericBox();
            this.nmbAnswer1_Times = new NumericBox();
            this.Label5 = new Label();
            this.txtAnswer3_HCPCS = new TextBox();
            this.txtAnswer2_HCPCS = new TextBox();
            this.txtAnswer1_HCPCS = new TextBox();
            this.Label1 = new Label();
            this.Label9 = new Label();
            this.Label10 = new Label();
            this.Label11 = new Label();
            this.Label12 = new Label();
            this.Label13 = new Label();
            this.lblQuestionDescription3 = new Label();
            this.lblQuestionDescription2 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.dtbAnswer11 = new UltraDateTimeEditor();
            this.pnlAnswers.SuspendLayout();
            this.pnlAnswer11.SuspendLayout();
            this.pnlAnswer10.SuspendLayout();
            this.pnlAnswer9.SuspendLayout();
            this.pnlAnswer8.SuspendLayout();
            this.pnlAnswer5.SuspendLayout();
            this.pnlQuestions.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlQuestion123.SuspendLayout();
            base.SuspendLayout();
            this.pnlAnswers.Controls.Add(this.rgAnswer12);
            this.pnlAnswers.Controls.Add(this.pnlAnswer11);
            this.pnlAnswers.Controls.Add(this.pnlAnswer10);
            this.pnlAnswers.Controls.Add(this.pnlAnswer9);
            this.pnlAnswers.Controls.Add(this.pnlAnswer8);
            this.pnlAnswers.Controls.Add(this.pnlAnswer5);
            this.pnlAnswers.Controls.Add(this.rgAnswer4);
            this.pnlAnswers.Controls.Add(this.lblAnswer123);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0xb8, 0x1b0);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer12.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer12.Dock = DockStyle.Top;
            this.rgAnswer12.Items = new string[] { "Y", "N" };
            this.rgAnswer12.Location = new Point(0, 0x194);
            this.rgAnswer12.Name = "rgAnswer12";
            this.rgAnswer12.Size = new Size(0xb8, 0x1c);
            this.rgAnswer12.TabIndex = 8;
            this.rgAnswer12.Value = "";
            this.pnlAnswer11.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAnswer11.Controls.Add(this.dtbAnswer11);
            this.pnlAnswer11.Dock = DockStyle.Top;
            this.pnlAnswer11.Location = new Point(0, 0x178);
            this.pnlAnswer11.Name = "pnlAnswer11";
            this.pnlAnswer11.Size = new Size(0xb8, 0x1c);
            this.pnlAnswer11.TabIndex = 7;
            this.pnlAnswer10.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAnswer10.Controls.Add(this.txtAnswer10);
            this.pnlAnswer10.Dock = DockStyle.Top;
            this.pnlAnswer10.Location = new Point(0, 0x15c);
            this.pnlAnswer10.Name = "pnlAnswer10";
            this.pnlAnswer10.Size = new Size(0xb8, 0x1c);
            this.pnlAnswer10.TabIndex = 6;
            this.txtAnswer10.AutoSize = false;
            this.txtAnswer10.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer10.Location = new Point(0x24, 4);
            this.txtAnswer10.MaxLength = 2;
            this.txtAnswer10.Name = "txtAnswer10";
            this.txtAnswer10.Size = new Size(0x70, 0x13);
            this.txtAnswer10.TabIndex = 0;
            this.txtAnswer10.Text = "";
            this.pnlAnswer9.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAnswer9.Controls.Add(this.txtAnswer9);
            this.pnlAnswer9.Dock = DockStyle.Top;
            this.pnlAnswer9.Location = new Point(0, 320);
            this.pnlAnswer9.Name = "pnlAnswer9";
            this.pnlAnswer9.Size = new Size(0xb8, 0x1c);
            this.pnlAnswer9.TabIndex = 5;
            this.txtAnswer9.AutoSize = false;
            this.txtAnswer9.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer9.Location = new Point(0x24, 4);
            this.txtAnswer9.MaxLength = 20;
            this.txtAnswer9.Name = "txtAnswer9";
            this.txtAnswer9.Size = new Size(0x70, 0x13);
            this.txtAnswer9.TabIndex = 0;
            this.txtAnswer9.Text = "";
            this.pnlAnswer8.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAnswer8.Controls.Add(this.txtAnswer8);
            this.pnlAnswer8.Dock = DockStyle.Top;
            this.pnlAnswer8.Location = new Point(0, 0x124);
            this.pnlAnswer8.Name = "pnlAnswer8";
            this.pnlAnswer8.Size = new Size(0xb8, 0x1c);
            this.pnlAnswer8.TabIndex = 4;
            this.txtAnswer8.AutoSize = false;
            this.txtAnswer8.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer8.Location = new Point(4, 4);
            this.txtAnswer8.MaxLength = 60;
            this.txtAnswer8.Name = "txtAnswer8";
            this.txtAnswer8.Size = new Size(0xac, 0x13);
            this.txtAnswer8.TabIndex = 0;
            this.txtAnswer8.Text = "";
            this.pnlAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAnswer5.Controls.Add(this.txtAnswer5_3);
            this.pnlAnswer5.Controls.Add(this.txtAnswer5_2);
            this.pnlAnswer5.Controls.Add(this.txtAnswer5_1);
            this.pnlAnswer5.Dock = DockStyle.Top;
            this.pnlAnswer5.Location = new Point(0, 0xb8);
            this.pnlAnswer5.Name = "pnlAnswer5";
            this.pnlAnswer5.Size = new Size(0xb8, 0x6c);
            this.pnlAnswer5.TabIndex = 3;
            this.txtAnswer5_3.AutoSize = false;
            this.txtAnswer5_3.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer5_3.Location = new Point(0x24, 0x40);
            this.txtAnswer5_3.Name = "txtAnswer5_3";
            this.txtAnswer5_3.Size = new Size(0x70, 0x13);
            this.txtAnswer5_3.TabIndex = 2;
            this.txtAnswer5_3.Text = "";
            this.txtAnswer5_2.AutoSize = false;
            this.txtAnswer5_2.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer5_2.Location = new Point(0x24, 0x2c);
            this.txtAnswer5_2.Name = "txtAnswer5_2";
            this.txtAnswer5_2.Size = new Size(0x70, 0x13);
            this.txtAnswer5_2.TabIndex = 1;
            this.txtAnswer5_2.Text = "";
            this.txtAnswer5_1.AutoSize = false;
            this.txtAnswer5_1.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer5_1.Location = new Point(0x24, 0x18);
            this.txtAnswer5_1.Name = "txtAnswer5_1";
            this.txtAnswer5_1.Size = new Size(0x70, 0x13);
            this.txtAnswer5_1.TabIndex = 0;
            this.txtAnswer5_1.Text = "";
            this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer4.Dock = DockStyle.Top;
            this.rgAnswer4.Items = new string[] { "Y", "N" };
            this.rgAnswer4.Location = new Point(0, 0x9c);
            this.rgAnswer4.Name = "rgAnswer4";
            this.rgAnswer4.Size = new Size(0xb8, 0x1c);
            this.rgAnswer4.TabIndex = 2;
            this.rgAnswer4.Value = "";
            this.lblAnswer123.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswer123.Dock = DockStyle.Top;
            this.lblAnswer123.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswer123.Location = new Point(0, 0x34);
            this.lblAnswer123.Name = "lblAnswer123";
            this.lblAnswer123.Size = new Size(0xb8, 0x68);
            this.lblAnswer123.TabIndex = 1;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0xb8, 0x34);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            this.pnlQuestions.Controls.Add(this.lblQuestion12);
            this.pnlQuestions.Controls.Add(this.lblQuestion11);
            this.pnlQuestions.Controls.Add(this.lblQuestion10);
            this.pnlQuestions.Controls.Add(this.lblQuestion9);
            this.pnlQuestions.Controls.Add(this.lblQuestion8);
            this.pnlQuestions.Controls.Add(this.Panel1);
            this.pnlQuestions.Controls.Add(this.lblQuestion4);
            this.pnlQuestions.Controls.Add(this.pnlQuestion123);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription3);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription2);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Location = new Point(0xb8, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x268, 0x1b0);
            this.pnlQuestions.TabIndex = 1;
            this.lblQuestion12.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion12.Dock = DockStyle.Top;
            this.lblQuestion12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion12.Location = new Point(0, 0x194);
            this.lblQuestion12.Name = "lblQuestion12";
            this.lblQuestion12.Size = new Size(0x268, 0x1c);
            this.lblQuestion12.TabIndex = 10;
            this.lblQuestion12.Text = "12. Was there a prior transplant failure of this same organ?";
            this.lblQuestion11.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion11.Dock = DockStyle.Top;
            this.lblQuestion11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion11.Location = new Point(0, 0x178);
            this.lblQuestion11.Name = "lblQuestion11";
            this.lblQuestion11.Size = new Size(0x268, 0x1c);
            this.lblQuestion11.TabIndex = 9;
            this.lblQuestion11.Text = "11. On what date was the patient discharged from the hospital following this transplant surgery?";
            this.lblQuestion10.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion10.Dock = DockStyle.Top;
            this.lblQuestion10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion10.Location = new Point(0, 0x15c);
            this.lblQuestion10.Name = "lblQuestion10";
            this.lblQuestion10.Size = new Size(0x268, 0x1c);
            this.lblQuestion10.TabIndex = 8;
            this.lblQuestion10.Text = "10. State where facility is located.";
            this.lblQuestion9.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion9.Dock = DockStyle.Top;
            this.lblQuestion9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion9.Location = new Point(0, 320);
            this.lblQuestion9.Name = "lblQuestion9";
            this.lblQuestion9.Size = new Size(0x268, 0x1c);
            this.lblQuestion9.TabIndex = 7;
            this.lblQuestion9.Text = "9. City where facility is located.";
            this.lblQuestion8.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion8.Dock = DockStyle.Top;
            this.lblQuestion8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion8.Location = new Point(0, 0x124);
            this.lblQuestion8.Name = "lblQuestion8";
            this.lblQuestion8.Size = new Size(0x268, 0x1c);
            this.lblQuestion8.TabIndex = 6;
            this.lblQuestion8.Text = "8. Name of facility where transplant was performed.";
            this.Panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label8);
            this.Panel1.Controls.Add(this.lblQuestion5);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.Label7);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0xb8);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x268, 0x6c);
            this.Panel1.TabIndex = 5;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0x60, 0x54);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x88, 0x13);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "5   -   Lung";
            this.Label2.TextAlign = ContentAlignment.MiddleLeft;
            this.Label8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label8.Location = new Point(0x60, 0x44);
            this.Label8.Name = "Label8";
            this.Label8.Size = new Size(0x88, 0x13);
            this.Label8.TabIndex = 4;
            this.Label8.Text = "4   -   Bone Marrow";
            this.Label8.TextAlign = ContentAlignment.MiddleLeft;
            this.lblQuestion5.Dock = DockStyle.Top;
            this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion5.Location = new Point(0, 0);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new Size(0x266, 20);
            this.lblQuestion5.TabIndex = 0;
            this.lblQuestion5.Text = "5. Which organ(s) have been transplanted?  (List most recent transplant) (May enter up to three dirrerent organs).";
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0x60, 20);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x88, 0x13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "1   -   Heart";
            this.Label4.TextAlign = ContentAlignment.MiddleLeft;
            this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label6.Location = new Point(0x60, 0x34);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x88, 0x13);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "3   -   Kidney";
            this.Label6.TextAlign = ContentAlignment.MiddleLeft;
            this.Label7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(0x60, 0x24);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(0x88, 0x13);
            this.Label7.TabIndex = 2;
            this.Label7.Text = "2   -   Liver";
            this.Label7.TextAlign = ContentAlignment.MiddleLeft;
            this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion4.Dock = DockStyle.Top;
            this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion4.Location = new Point(0, 0x9c);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new Size(0x268, 0x1c);
            this.lblQuestion4.TabIndex = 4;
            this.lblQuestion4.Text = "4. Has the patient had an organ transplant that was covered by Medicate?";
            this.pnlQuestion123.BorderStyle = BorderStyle.FixedSingle;
            this.pnlQuestion123.Controls.Add(this.nmbAnswer3_Times);
            this.pnlQuestion123.Controls.Add(this.nmbAnswer3_MG);
            this.pnlQuestion123.Controls.Add(this.nmbAnswer1_MG);
            this.pnlQuestion123.Controls.Add(this.nmbAnswer2_Times);
            this.pnlQuestion123.Controls.Add(this.nmbAnswer2_MG);
            this.pnlQuestion123.Controls.Add(this.nmbAnswer1_Times);
            this.pnlQuestion123.Controls.Add(this.Label5);
            this.pnlQuestion123.Controls.Add(this.txtAnswer3_HCPCS);
            this.pnlQuestion123.Controls.Add(this.txtAnswer2_HCPCS);
            this.pnlQuestion123.Controls.Add(this.txtAnswer1_HCPCS);
            this.pnlQuestion123.Controls.Add(this.Label1);
            this.pnlQuestion123.Controls.Add(this.Label9);
            this.pnlQuestion123.Controls.Add(this.Label10);
            this.pnlQuestion123.Controls.Add(this.Label11);
            this.pnlQuestion123.Controls.Add(this.Label12);
            this.pnlQuestion123.Controls.Add(this.Label13);
            this.pnlQuestion123.Dock = DockStyle.Top;
            this.pnlQuestion123.Location = new Point(0, 0x34);
            this.pnlQuestion123.Name = "pnlQuestion123";
            this.pnlQuestion123.Size = new Size(0x268, 0x68);
            this.pnlQuestion123.TabIndex = 3;
            this.nmbAnswer3_Times.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer3_Times.Location = new Point(0x110, 80);
            this.nmbAnswer3_Times.Name = "nmbAnswer3_Times";
            this.nmbAnswer3_Times.Size = new Size(0x70, 0x13);
            this.nmbAnswer3_Times.TabIndex = 15;
            this.nmbAnswer3_Times.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer3_MG.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer3_MG.Location = new Point(0x9c, 80);
            this.nmbAnswer3_MG.Name = "nmbAnswer3_MG";
            this.nmbAnswer3_MG.Size = new Size(0x70, 0x13);
            this.nmbAnswer3_MG.TabIndex = 14;
            this.nmbAnswer3_MG.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer1_MG.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer1_MG.Location = new Point(0x9c, 40);
            this.nmbAnswer1_MG.Name = "nmbAnswer1_MG";
            this.nmbAnswer1_MG.Size = new Size(0x70, 0x13);
            this.nmbAnswer1_MG.TabIndex = 6;
            this.nmbAnswer1_MG.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer2_Times.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer2_Times.Location = new Point(0x110, 60);
            this.nmbAnswer2_Times.Name = "nmbAnswer2_Times";
            this.nmbAnswer2_Times.Size = new Size(0x70, 0x13);
            this.nmbAnswer2_Times.TabIndex = 11;
            this.nmbAnswer2_Times.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer2_MG.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer2_MG.Location = new Point(0x9c, 60);
            this.nmbAnswer2_MG.Name = "nmbAnswer2_MG";
            this.nmbAnswer2_MG.Size = new Size(0x70, 0x13);
            this.nmbAnswer2_MG.TabIndex = 10;
            this.nmbAnswer2_MG.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer1_Times.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer1_Times.Location = new Point(0x110, 40);
            this.nmbAnswer1_Times.Name = "nmbAnswer1_Times";
            this.nmbAnswer1_Times.Size = new Size(0x70, 0x13);
            this.nmbAnswer1_Times.TabIndex = 7;
            this.nmbAnswer1_Times.TextAlign = HorizontalAlignment.Left;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(0x110, 20);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x70, 0x13);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "TIMES PER DAY";
            this.Label5.TextAlign = ContentAlignment.MiddleCenter;
            this.txtAnswer3_HCPCS.AutoSize = false;
            this.txtAnswer3_HCPCS.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer3_HCPCS.Location = new Point(40, 80);
            this.txtAnswer3_HCPCS.Name = "txtAnswer3_HCPCS";
            this.txtAnswer3_HCPCS.Size = new Size(0x70, 0x13);
            this.txtAnswer3_HCPCS.TabIndex = 13;
            this.txtAnswer3_HCPCS.Text = "";
            this.txtAnswer2_HCPCS.AutoSize = false;
            this.txtAnswer2_HCPCS.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer2_HCPCS.Location = new Point(40, 60);
            this.txtAnswer2_HCPCS.Name = "txtAnswer2_HCPCS";
            this.txtAnswer2_HCPCS.Size = new Size(0x70, 0x13);
            this.txtAnswer2_HCPCS.TabIndex = 9;
            this.txtAnswer2_HCPCS.Text = "";
            this.txtAnswer1_HCPCS.AutoSize = false;
            this.txtAnswer1_HCPCS.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer1_HCPCS.Location = new Point(40, 40);
            this.txtAnswer1_HCPCS.Name = "txtAnswer1_HCPCS";
            this.txtAnswer1_HCPCS.Size = new Size(0x70, 0x13);
            this.txtAnswer1_HCPCS.TabIndex = 5;
            this.txtAnswer1_HCPCS.Text = "";
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x266, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "What are the drug(s) prescribed and the dosage and frequency of administration of each?";
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(12, 40);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x18, 0x13);
            this.Label9.TabIndex = 4;
            this.Label9.Text = "1.";
            this.Label9.TextAlign = ContentAlignment.MiddleLeft;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(12, 80);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(0x18, 0x13);
            this.Label10.TabIndex = 12;
            this.Label10.Text = "3.";
            this.Label10.TextAlign = ContentAlignment.MiddleLeft;
            this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label11.Location = new Point(12, 60);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(0x18, 0x13);
            this.Label11.TabIndex = 8;
            this.Label11.Text = "2.";
            this.Label11.TextAlign = ContentAlignment.MiddleLeft;
            this.Label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label12.Location = new Point(40, 20);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x70, 0x13);
            this.Label12.TabIndex = 1;
            this.Label12.Text = "HCPCS ";
            this.Label12.TextAlign = ContentAlignment.MiddleCenter;
            this.Label13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label13.Location = new Point(0x9c, 20);
            this.Label13.Name = "Label13";
            this.Label13.Size = new Size(0x70, 0x13);
            this.Label13.TabIndex = 2;
            this.Label13.Text = "MG";
            this.Label13.TextAlign = ContentAlignment.MiddleCenter;
            this.lblQuestionDescription3.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription3.Dock = DockStyle.Top;
            this.lblQuestionDescription3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription3.Location = new Point(0, 0x24);
            this.lblQuestionDescription3.Name = "lblQuestionDescription3";
            this.lblQuestionDescription3.Size = new Size(0x268, 0x10);
            this.lblQuestionDescription3.TabIndex = 2;
            this.lblQuestionDescription3.Text = "Questions 6 and 7, reserved for other or future use.";
            this.lblQuestionDescription2.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription2.Dock = DockStyle.Top;
            this.lblQuestionDescription2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription2.Location = new Point(0, 20);
            this.lblQuestionDescription2.Name = "lblQuestionDescription2";
            this.lblQuestionDescription2.Size = new Size(0x268, 0x10);
            this.lblQuestionDescription2.TabIndex = 1;
            this.lblQuestionDescription2.Text = "(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x268, 20);
            this.lblQuestionDescription1.TabIndex = 0;
            this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 1 - 5 AND 8 - 12 FOR IMMUNOSUPPRESSIVE DRUGS";
            this.dtbAnswer11.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer11.Location = new Point(0x24, 4);
            this.dtbAnswer11.Name = "dtbAnswer11";
            this.dtbAnswer11.Size = new Size(0x70, 0x15);
            this.dtbAnswer11.TabIndex = 0;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN0802";
            base.Size = new Size(800, 0x1b0);
            this.pnlAnswers.ResumeLayout(false);
            this.pnlAnswer11.ResumeLayout(false);
            this.pnlAnswer10.ResumeLayout(false);
            this.pnlAnswer9.ResumeLayout(false);
            this.pnlAnswer8.ResumeLayout(false);
            this.pnlAnswer5.ResumeLayout(false);
            this.pnlQuestions.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.pnlQuestion123.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            Functions.SetTextBoxText(this.txtAnswer1_HCPCS, reader["Answer1_HCPCS"]);
            Functions.SetNumericBoxValue(this.nmbAnswer1_MG, reader["Answer1_MG"]);
            Functions.SetNumericBoxValue(this.nmbAnswer1_Times, reader["Answer1_Times"]);
            Functions.SetTextBoxText(this.txtAnswer2_HCPCS, reader["Answer2_HCPCS"]);
            Functions.SetNumericBoxValue(this.nmbAnswer2_MG, reader["Answer2_MG"]);
            Functions.SetNumericBoxValue(this.nmbAnswer2_Times, reader["Answer2_Times"]);
            Functions.SetTextBoxText(this.txtAnswer3_HCPCS, reader["Answer3_HCPCS"]);
            Functions.SetNumericBoxValue(this.nmbAnswer3_MG, reader["Answer3_MG"]);
            Functions.SetNumericBoxValue(this.nmbAnswer3_Times, reader["Answer3_Times"]);
            Functions.SetRadioGroupValue(this.rgAnswer4, reader["Answer4"]);
            Functions.SetTextBoxText(this.txtAnswer5_1, reader["Answer5_1"]);
            Functions.SetTextBoxText(this.txtAnswer5_2, reader["Answer5_2"]);
            Functions.SetTextBoxText(this.txtAnswer5_3, reader["Answer5_3"]);
            Functions.SetTextBoxText(this.txtAnswer8, reader["Answer8"]);
            Functions.SetTextBoxText(this.txtAnswer9, reader["Answer9"]);
            Functions.SetTextBoxText(this.txtAnswer10, reader["Answer10"]);
            Functions.SetDateBoxValue(this.dtbAnswer11, reader["Answer11"]);
            Functions.SetRadioGroupValue(this.rgAnswer12, reader["Answer12"]);
        }

        private void nmbAnswer1_MG_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer1_Times_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer2_MG_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer2_Times_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer3_MG_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer3_Times_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer12_ValueChanged1(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer4_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer1_HCPCS", MySqlType.VarChar, 5).Value = this.txtAnswer1_HCPCS.Text;
            cmd.Parameters.Add("Answer1_MG", MySqlType.Int).Value = this.nmbAnswer1_MG.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer1_Times", MySqlType.Int).Value = this.nmbAnswer1_Times.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer2_HCPCS", MySqlType.VarChar, 5).Value = this.txtAnswer2_HCPCS.Text;
            cmd.Parameters.Add("Answer2_MG", MySqlType.Int).Value = this.nmbAnswer2_MG.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer2_Times", MySqlType.Int).Value = this.nmbAnswer2_Times.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer3_HCPCS", MySqlType.VarChar, 5).Value = this.txtAnswer3_HCPCS.Text;
            cmd.Parameters.Add("Answer3_MG", MySqlType.Int).Value = this.nmbAnswer3_MG.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer3_Times", MySqlType.Int).Value = this.nmbAnswer3_Times.AsInt32.GetValueOrDefault(0);
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 1).Value = this.rgAnswer4.Value;
            cmd.Parameters.Add("Answer5_1", MySqlType.VarChar, 1).Value = this.txtAnswer5_1.Text;
            cmd.Parameters.Add("Answer5_2", MySqlType.VarChar, 1).Value = this.txtAnswer5_2.Text;
            cmd.Parameters.Add("Answer5_3", MySqlType.VarChar, 1).Value = this.txtAnswer5_3.Text;
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 60).Value = this.txtAnswer8.Text;
            cmd.Parameters.Add("Answer9", MySqlType.VarChar, 20).Value = this.txtAnswer9.Text;
            cmd.Parameters.Add("Answer10", MySqlType.VarChar, 2).Value = this.txtAnswer10.Text;
            cmd.Parameters.Add("Answer11", MySqlType.Date, 8).Value = Functions.GetDateBoxValue(this.dtbAnswer11);
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 1).Value = this.rgAnswer12.Value;
        }

        private void txtAnswer1_HCPCS_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer2_HCPCS_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnswer3_HCPCS_TextChanged(object sender, EventArgs e)
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

        [field: AccessedThroughProperty("lblAnswer123")]
        private Label lblAnswer123 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestion123")]
        private Panel pnlQuestion123 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer4")]
        private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion4")]
        private Label lblQuestion4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer3_Times")]
        private NumericBox nmbAnswer3_Times { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer3_MG")]
        private NumericBox nmbAnswer3_MG { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer1_MG")]
        private NumericBox nmbAnswer1_MG { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer2_Times")]
        private NumericBox nmbAnswer2_Times { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer2_MG")]
        private NumericBox nmbAnswer2_MG { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer1_Times")]
        private NumericBox nmbAnswer1_Times { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer3_HCPCS")]
        private TextBox txtAnswer3_HCPCS { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer2_HCPCS")]
        private TextBox txtAnswer2_HCPCS { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer1_HCPCS")]
        private TextBox txtAnswer1_HCPCS { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label6")]
        private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label8")]
        private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswer5")]
        private Panel pnlAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer5_1")]
        private TextBox txtAnswer5_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer5_2")]
        private TextBox txtAnswer5_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion5")]
        private Label lblQuestion5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion8")]
        private Label lblQuestion8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion9")]
        private Label lblQuestion9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion10")]
        private Label lblQuestion10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion11")]
        private Label lblQuestion11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswer8")]
        private Panel pnlAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer8")]
        private TextBox txtAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswer9")]
        private Panel pnlAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer9")]
        private TextBox txtAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswer10")]
        private Panel pnlAnswer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer10")]
        private TextBox txtAnswer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswer11")]
        private Panel pnlAnswer11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer12")]
        private RadioGroup rgAnswer12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion12")]
        private Label lblQuestion12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer5_3")]
        private TextBox txtAnswer5_3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer11")]
        private UltraDateTimeEditor dtbAnswer11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_0102B;
    }
}

