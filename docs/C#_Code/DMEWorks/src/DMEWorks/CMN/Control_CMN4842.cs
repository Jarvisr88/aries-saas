namespace DMEWorks.CMN
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Control_CMN4842 : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN4842()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            Functions.SetNumericBoxValue(this.nmbAnswer1a, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer1b, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbAnswer1c, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer2, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer3, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhysicianAddress, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhysicianCity, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhysicianState, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhysicianZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhysicianName, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer5, DBNull.Value);
            Functions.SetTextBoxText(this.txtAnswer6, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer7a, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAnswer7b, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbAnswer7c, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer8, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer9, DBNull.Value);
            Functions.SetRadioGroupValue(this.rgAnswer10, DBNull.Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dtbAnswer1c_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void dtbAnswer7c_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Panel1 = new Panel();
            this.pnlQuestions = new Panel();
            this.Label25 = new Label();
            this.Label5 = new Label();
            this.Label1 = new Label();
            this.Panel2 = new Panel();
            this.Label3 = new Label();
            this.Label4 = new Label();
            this.txtPhysicianAddress = new TextBox();
            this.txtPhysicianName = new TextBox();
            this.Label6 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestion0 = new Label();
            this.TextBox9 = new TextBox();
            this.TextBox10 = new TextBox();
            this.pnlAnswers = new Panel();
            this.Panel4 = new Panel();
            this.nmbAnswer7b = new NumericBox();
            this.nmbAnswer7a = new NumericBox();
            this.Label9 = new Label();
            this.Label10 = new Label();
            this.dtbAnswer7c = new UltraDateTimeEditor();
            this.Label27 = new Label();
            this.Label28 = new Label();
            this.Label29 = new Label();
            this.Panel11 = new Panel();
            this.Label32 = new Label();
            this.txtAnswer6 = new TextBox();
            this.rgAnswer5 = new RadioGroup();
            this.lblAnswer7 = new Label();
            this.rgAnswer3 = new RadioGroup();
            this.rgAnswer2 = new RadioGroup();
            this.Panel3 = new Panel();
            this.nmbAnswer1b = new NumericBox();
            this.nmbAnswer1a = new NumericBox();
            this.Label24 = new Label();
            this.Label18 = new Label();
            this.dtbAnswer1c = new UltraDateTimeEditor();
            this.Label2 = new Label();
            this.Label8 = new Label();
            this.Label7 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.Panel5 = new Panel();
            this.Panel6 = new Panel();
            this.Label15 = new Label();
            this.Label16 = new Label();
            this.Label17 = new Label();
            this.Panel8 = new Panel();
            this.rgAnswer10 = new RadioGroup();
            this.rgAnswer9 = new RadioGroup();
            this.rgAnswer8 = new RadioGroup();
            this.Label26 = new Label();
            this.Label31 = new Label();
            this.Label11 = new Label();
            this.txtPhysicianCity = new TextBox();
            this.txtPhysicianState = new TextBox();
            this.txtPhysicianZip = new TextBox();
            this.Panel1.SuspendLayout();
            this.pnlQuestions.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel11.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel8.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Controls.Add(this.pnlQuestions);
            this.Panel1.Controls.Add(this.pnlAnswers);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(800, 320);
            this.Panel1.TabIndex = 0;
            this.pnlQuestions.Controls.Add(this.Label25);
            this.pnlQuestions.Controls.Add(this.Label5);
            this.pnlQuestions.Controls.Add(this.Label1);
            this.pnlQuestions.Controls.Add(this.Panel2);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestion0);
            this.pnlQuestions.Controls.Add(this.TextBox9);
            this.pnlQuestions.Controls.Add(this.TextBox10);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlQuestions.Location = new Point(0x98, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x288, 320);
            this.pnlQuestions.TabIndex = 1;
            this.Label25.BorderStyle = BorderStyle.FixedSingle;
            this.Label25.Dock = DockStyle.Top;
            this.Label25.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label25.Location = new Point(0, 0x100);
            this.Label25.Name = "Label25";
            this.Label25.Size = new Size(0x288, 0x40);
            this.Label25.TabIndex = 7;
            this.Label25.Text = "7.   If greater than 4 LPM is prescribed, enter results of most recent test taken on 4LPM.This may be an (a) arterial blood gas PO2 and/or (b) oxygen saturation test with patient in a chronic stable state. Enter date of test (c).";
            this.Label5.BorderStyle = BorderStyle.FixedSingle;
            this.Label5.Dock = DockStyle.Top;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(0, 0xe8);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x288, 0x18);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "6.   Enter the highest oxygen flow rate ordered for this patient in liters per minute. If less than 1 LPM, enter a \"X\".";
            this.Label1.BorderStyle = BorderStyle.FixedSingle;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0xd4);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x288, 20);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "5.   If you are ordering portable oxygen, is the patient mobile within the home? If you are not ordering portable oxygen, circle D.";
            this.Panel2.BorderStyle = BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.txtPhysicianZip);
            this.Panel2.Controls.Add(this.txtPhysicianState);
            this.Panel2.Controls.Add(this.txtPhysicianCity);
            this.Panel2.Controls.Add(this.Label11);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.txtPhysicianAddress);
            this.Panel2.Controls.Add(this.txtPhysicianName);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel2.Location = new Point(0, 0x90);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(0x288, 0x44);
            this.Panel2.TabIndex = 4;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0x128, 20);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x54, 20);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "ADDRESS:";
            this.Label3.TextAlign = ContentAlignment.MiddleLeft;
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(4, 20);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(40, 20);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "NAME:";
            this.Label4.TextAlign = ContentAlignment.MiddleLeft;
            this.txtPhysicianAddress.AutoSize = false;
            this.txtPhysicianAddress.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhysicianAddress.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtPhysicianAddress.Location = new Point(0x180, 20);
            this.txtPhysicianAddress.Name = "txtPhysicianAddress";
            this.txtPhysicianAddress.Size = new Size(0x100, 20);
            this.txtPhysicianAddress.TabIndex = 4;
            this.txtPhysicianAddress.Text = "";
            this.txtPhysicianName.AutoSize = false;
            this.txtPhysicianName.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhysicianName.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtPhysicianName.Location = new Point(0x2c, 20);
            this.txtPhysicianName.Name = "txtPhysicianName";
            this.txtPhysicianName.Size = new Size(0xf8, 20);
            this.txtPhysicianName.TabIndex = 2;
            this.txtPhysicianName.Text = "";
            this.Label6.Dock = DockStyle.Top;
            this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label6.Location = new Point(0, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x286, 0x10);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "4.   Physician/provider performing test in Question 1 (and, if applicable, Question 7). Print/type name and address below:";
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x7c);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x288, 20);
            this.lblQuestion1.TabIndex = 3;
            this.lblQuestion1.Text = "3.   Circle the one number for the condition of the test in Question 1: (1) At Rest; (2) During Exercise; (3) During Sleep";
            this.lblQuestion0.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion0.Dock = DockStyle.Top;
            this.lblQuestion0.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion0.Location = new Point(0, 0x60);
            this.lblQuestion0.Name = "lblQuestion0";
            this.lblQuestion0.Size = new Size(0x288, 0x1c);
            this.lblQuestion0.TabIndex = 2;
            this.lblQuestion0.Text = "2.  Was the test in Question 1 performed EITHER with the patient in a chronic stable state as an outpatient OR within two days prior to discharge from an inpatient facility to home?";
            this.TextBox9.AutoSize = false;
            this.TextBox9.BackColor = SystemColors.Control;
            this.TextBox9.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox9.Dock = DockStyle.Top;
            this.TextBox9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox9.Location = new Point(0, 0x20);
            this.TextBox9.Multiline = true;
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Size = new Size(0x288, 0x40);
            this.TextBox9.TabIndex = 1;
            this.TextBox9.TabStop = false;
            this.TextBox9.Text = "1.  Enter the result of most recent test taken on or before the certification date listed in Section A. \r\nEnter (a) arterial blood gas PO2 and/or (b) oxygen saturation test. Enter date of test (c).";
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
            this.TextBox10.TabStop = false;
            this.TextBox10.Text = "ANSWER QUESTIONS 1-10.\r\n(Circle Y for Yes, N for No, or D for Does Not Apply, unless otherwise noted.)";
            this.pnlAnswers.Controls.Add(this.Panel4);
            this.pnlAnswers.Controls.Add(this.Panel11);
            this.pnlAnswers.Controls.Add(this.rgAnswer5);
            this.pnlAnswers.Controls.Add(this.lblAnswer7);
            this.pnlAnswers.Controls.Add(this.rgAnswer3);
            this.pnlAnswers.Controls.Add(this.rgAnswer2);
            this.pnlAnswers.Controls.Add(this.Panel3);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x98, 320);
            this.pnlAnswers.TabIndex = 0;
            this.Panel4.BorderStyle = BorderStyle.FixedSingle;
            this.Panel4.Controls.Add(this.nmbAnswer7b);
            this.Panel4.Controls.Add(this.nmbAnswer7a);
            this.Panel4.Controls.Add(this.Label9);
            this.Panel4.Controls.Add(this.Label10);
            this.Panel4.Controls.Add(this.dtbAnswer7c);
            this.Panel4.Controls.Add(this.Label27);
            this.Panel4.Controls.Add(this.Label28);
            this.Panel4.Controls.Add(this.Label29);
            this.Panel4.Dock = DockStyle.Top;
            this.Panel4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel4.Location = new Point(0, 0x100);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new Size(0x98, 0x40);
            this.Panel4.TabIndex = 7;
            this.nmbAnswer7b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer7b.Location = new Point(0x1c, 0x16);
            this.nmbAnswer7b.Name = "nmbAnswer7b";
            this.nmbAnswer7b.Size = new Size(0x4e, 0x13);
            this.nmbAnswer7b.TabIndex = 4;
            this.nmbAnswer7b.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer7a.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer7a.Location = new Point(0x1c, 2);
            this.nmbAnswer7a.Name = "nmbAnswer7a";
            this.nmbAnswer7a.Size = new Size(0x4e, 0x13);
            this.nmbAnswer7a.TabIndex = 1;
            this.nmbAnswer7a.TextAlign = HorizontalAlignment.Left;
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(0x6a, 0x16);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x2a, 0x13);
            this.Label9.TabIndex = 5;
            this.Label9.Text = "mm Hg";
            this.Label9.TextAlign = ContentAlignment.MiddleCenter;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(0x6a, 2);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(0x2a, 0x13);
            this.Label10.TabIndex = 2;
            this.Label10.Text = "mm Hg";
            this.Label10.TextAlign = ContentAlignment.MiddleCenter;
            this.dtbAnswer7c.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer7c.Location = new Point(0x1c, 0x2a);
            this.dtbAnswer7c.Name = "dtbAnswer7c";
            this.dtbAnswer7c.Size = new Size(120, 0x13);
            this.dtbAnswer7c.TabIndex = 7;
            this.Label27.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label27.Location = new Point(4, 0x2a);
            this.Label27.Name = "Label27";
            this.Label27.Size = new Size(20, 0x13);
            this.Label27.TabIndex = 6;
            this.Label27.Text = "C.";
            this.Label28.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label28.Location = new Point(4, 0x16);
            this.Label28.Name = "Label28";
            this.Label28.Size = new Size(20, 0x13);
            this.Label28.TabIndex = 3;
            this.Label28.Text = "B.";
            this.Label29.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label29.Location = new Point(4, 2);
            this.Label29.Name = "Label29";
            this.Label29.Size = new Size(20, 0x13);
            this.Label29.TabIndex = 0;
            this.Label29.Text = "A.";
            this.Panel11.BorderStyle = BorderStyle.FixedSingle;
            this.Panel11.Controls.Add(this.Label32);
            this.Panel11.Controls.Add(this.txtAnswer6);
            this.Panel11.Dock = DockStyle.Top;
            this.Panel11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel11.Location = new Point(0, 0xe8);
            this.Panel11.Name = "Panel11";
            this.Panel11.Size = new Size(0x98, 0x18);
            this.Panel11.TabIndex = 6;
            this.Label32.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label32.Location = new Point(0x6a, 2);
            this.Label32.Name = "Label32";
            this.Label32.Size = new Size(0x2a, 0x13);
            this.Label32.TabIndex = 1;
            this.Label32.Text = "LPM";
            this.Label32.TextAlign = ContentAlignment.MiddleCenter;
            this.txtAnswer6.AutoSize = false;
            this.txtAnswer6.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnswer6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnswer6.Location = new Point(2, 2);
            this.txtAnswer6.Name = "txtAnswer6";
            this.txtAnswer6.Size = new Size(100, 0x13);
            this.txtAnswer6.TabIndex = 0;
            this.txtAnswer6.Text = "";
            this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer5.Dock = DockStyle.Top;
            this.rgAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer5.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer5.Location = new Point(0, 0xd4);
            this.rgAnswer5.Name = "rgAnswer5";
            this.rgAnswer5.Size = new Size(0x98, 20);
            this.rgAnswer5.TabIndex = 5;
            this.rgAnswer5.Value = "";
            this.lblAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswer7.Dock = DockStyle.Top;
            this.lblAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswer7.Location = new Point(0, 0x90);
            this.lblAnswer7.Name = "lblAnswer7";
            this.lblAnswer7.Size = new Size(0x98, 0x44);
            this.lblAnswer7.TabIndex = 4;
            this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer3.Dock = DockStyle.Top;
            this.rgAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer3.Items = new string[] { "1", "2", "3" };
            this.rgAnswer3.Location = new Point(0, 0x7c);
            this.rgAnswer3.Name = "rgAnswer3";
            this.rgAnswer3.Size = new Size(0x98, 20);
            this.rgAnswer3.TabIndex = 3;
            this.rgAnswer3.Value = "";
            this.rgAnswer2.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer2.Dock = DockStyle.Top;
            this.rgAnswer2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer2.Items = new string[] { "Y", "N" };
            this.rgAnswer2.Location = new Point(0, 0x60);
            this.rgAnswer2.Name = "rgAnswer2";
            this.rgAnswer2.Size = new Size(0x98, 0x1c);
            this.rgAnswer2.TabIndex = 2;
            this.rgAnswer2.Value = "";
            this.Panel3.BorderStyle = BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.nmbAnswer1b);
            this.Panel3.Controls.Add(this.nmbAnswer1a);
            this.Panel3.Controls.Add(this.Label24);
            this.Panel3.Controls.Add(this.Label18);
            this.Panel3.Controls.Add(this.dtbAnswer1c);
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Dock = DockStyle.Top;
            this.Panel3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel3.Location = new Point(0, 0x20);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new Size(0x98, 0x40);
            this.Panel3.TabIndex = 1;
            this.nmbAnswer1b.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer1b.Location = new Point(0x1c, 0x16);
            this.nmbAnswer1b.Name = "nmbAnswer1b";
            this.nmbAnswer1b.Size = new Size(0x4e, 0x13);
            this.nmbAnswer1b.TabIndex = 4;
            this.nmbAnswer1b.TextAlign = HorizontalAlignment.Left;
            this.nmbAnswer1a.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer1a.Location = new Point(0x1c, 2);
            this.nmbAnswer1a.Name = "nmbAnswer1a";
            this.nmbAnswer1a.Size = new Size(0x4e, 0x13);
            this.nmbAnswer1a.TabIndex = 1;
            this.nmbAnswer1a.TextAlign = HorizontalAlignment.Left;
            this.Label24.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label24.Location = new Point(0x6a, 0x16);
            this.Label24.Name = "Label24";
            this.Label24.Size = new Size(0x2a, 0x13);
            this.Label24.TabIndex = 5;
            this.Label24.Text = "mm Hg";
            this.Label24.TextAlign = ContentAlignment.MiddleCenter;
            this.Label18.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label18.Location = new Point(0x6a, 2);
            this.Label18.Name = "Label18";
            this.Label18.Size = new Size(0x2a, 0x13);
            this.Label18.TabIndex = 2;
            this.Label18.Text = "mm Hg";
            this.Label18.TextAlign = ContentAlignment.MiddleCenter;
            this.dtbAnswer1c.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbAnswer1c.Location = new Point(0x1c, 0x2a);
            this.dtbAnswer1c.Name = "dtbAnswer1c";
            this.dtbAnswer1c.Size = new Size(120, 0x13);
            this.dtbAnswer1c.TabIndex = 7;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(4, 0x2a);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(20, 0x13);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "C.";
            this.Label8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label8.Location = new Point(4, 0x16);
            this.Label8.Name = "Label8";
            this.Label8.Size = new Size(20, 0x13);
            this.Label8.TabIndex = 3;
            this.Label8.Text = "B.";
            this.Label7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(4, 2);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(20, 0x13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "A.";
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x98, 0x20);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            this.Panel5.Controls.Add(this.Panel6);
            this.Panel5.Controls.Add(this.Panel8);
            this.Panel5.Dock = DockStyle.Top;
            this.Panel5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel5.Location = new Point(0, 0x157);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new Size(800, 0x45);
            this.Panel5.TabIndex = 2;
            this.Panel6.BackColor = Color.Transparent;
            this.Panel6.Controls.Add(this.Label15);
            this.Panel6.Controls.Add(this.Label16);
            this.Panel6.Controls.Add(this.Label17);
            this.Panel6.Dock = DockStyle.Fill;
            this.Panel6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel6.Location = new Point(0x98, 0);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new Size(0x288, 0x45);
            this.Panel6.TabIndex = 1;
            this.Label15.BorderStyle = BorderStyle.FixedSingle;
            this.Label15.Dock = DockStyle.Top;
            this.Label15.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label15.Location = new Point(0, 0x30);
            this.Label15.Name = "Label15";
            this.Label15.Size = new Size(0x288, 20);
            this.Label15.TabIndex = 2;
            this.Label15.Text = "10. Does the patient have a hermatocrit greater than 56%?";
            this.Label16.BorderStyle = BorderStyle.FixedSingle;
            this.Label16.Dock = DockStyle.Top;
            this.Label16.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label16.Location = new Point(0, 20);
            this.Label16.Name = "Label16";
            this.Label16.Size = new Size(0x288, 0x1c);
            this.Label16.TabIndex = 1;
            this.Label16.Text = "9.   Does the patient have cor pulmonale or pulmonary hypertension documented by P pulmonale on an EKG or by an echocardiogram, gated blood pool scan or direct pulmonary artery pressure measurement?";
            this.Label17.BorderStyle = BorderStyle.FixedSingle;
            this.Label17.Dock = DockStyle.Top;
            this.Label17.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label17.Location = new Point(0, 0);
            this.Label17.Name = "Label17";
            this.Label17.Size = new Size(0x288, 20);
            this.Label17.TabIndex = 0;
            this.Label17.Text = "8.   Does the patient have dependent edema due to congestive heart failure?";
            this.Panel8.Controls.Add(this.rgAnswer10);
            this.Panel8.Controls.Add(this.rgAnswer9);
            this.Panel8.Controls.Add(this.rgAnswer8);
            this.Panel8.Dock = DockStyle.Left;
            this.Panel8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel8.Location = new Point(0, 0);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new Size(0x98, 0x45);
            this.Panel8.TabIndex = 0;
            this.rgAnswer10.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer10.Dock = DockStyle.Top;
            this.rgAnswer10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer10.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer10.Location = new Point(0, 0x30);
            this.rgAnswer10.Name = "rgAnswer10";
            this.rgAnswer10.Size = new Size(0x98, 20);
            this.rgAnswer10.TabIndex = 2;
            this.rgAnswer10.Value = "";
            this.rgAnswer9.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer9.Dock = DockStyle.Top;
            this.rgAnswer9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer9.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer9.Location = new Point(0, 20);
            this.rgAnswer9.Name = "rgAnswer9";
            this.rgAnswer9.Size = new Size(0x98, 0x1c);
            this.rgAnswer9.TabIndex = 1;
            this.rgAnswer9.Value = "";
            this.rgAnswer8.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer8.Dock = DockStyle.Top;
            this.rgAnswer8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer8.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer8.Location = new Point(0, 0);
            this.rgAnswer8.Name = "rgAnswer8";
            this.rgAnswer8.Size = new Size(0x98, 20);
            this.rgAnswer8.TabIndex = 0;
            this.rgAnswer8.Value = "";
            this.Label26.BorderStyle = BorderStyle.FixedSingle;
            this.Label26.Dock = DockStyle.Top;
            this.Label26.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label26.Location = new Point(0, 320);
            this.Label26.Name = "Label26";
            this.Label26.Size = new Size(800, 0x17);
            this.Label26.TabIndex = 1;
            this.Label26.Text = "IF PO2 = 56-59 OR OXYGEN SATURATION = 89%, AT LEAST ONE OF THE FOLLOWING CRITERIA MUST BE MET.";
            this.Label26.TextAlign = ContentAlignment.MiddleCenter;
            this.Label31.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label31.Location = new Point(0x6a, 2);
            this.Label31.Name = "Label31";
            this.Label31.Size = new Size(0x2a, 0x13);
            this.Label31.TabIndex = 0x19;
            this.Label31.Text = "mm Hg";
            this.Label31.TextAlign = ContentAlignment.MiddleCenter;
            this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label11.Location = new Point(0x128, 0x2c);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(0x54, 20);
            this.Label11.TabIndex = 5;
            this.Label11.Text = "City, State, Zip:";
            this.Label11.TextAlign = ContentAlignment.MiddleLeft;
            this.txtPhysicianCity.AutoSize = false;
            this.txtPhysicianCity.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhysicianCity.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtPhysicianCity.Location = new Point(0x180, 0x2c);
            this.txtPhysicianCity.Name = "txtPhysicianCity";
            this.txtPhysicianCity.Size = new Size(0x88, 20);
            this.txtPhysicianCity.TabIndex = 6;
            this.txtPhysicianCity.Text = "";
            this.txtPhysicianState.AutoSize = false;
            this.txtPhysicianState.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhysicianState.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtPhysicianState.Location = new Point(0x20c, 0x2c);
            this.txtPhysicianState.Name = "txtPhysicianState";
            this.txtPhysicianState.Size = new Size(0x24, 20);
            this.txtPhysicianState.TabIndex = 7;
            this.txtPhysicianState.Text = "WW";
            this.txtPhysicianZip.AutoSize = false;
            this.txtPhysicianZip.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhysicianZip.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtPhysicianZip.Location = new Point(0x234, 0x2c);
            this.txtPhysicianZip.Name = "txtPhysicianZip";
            this.txtPhysicianZip.Size = new Size(0x4c, 20);
            this.txtPhysicianZip.TabIndex = 8;
            this.txtPhysicianZip.Text = "12345-1234";
            base.Controls.Add(this.Panel5);
            base.Controls.Add(this.Label26);
            base.Controls.Add(this.Panel1);
            base.Name = "Control_CMN4842";
            base.Size = new Size(800, 0x19c);
            this.Panel1.ResumeLayout(false);
            this.pnlQuestions.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel11.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.Panel6.ResumeLayout(false);
            this.Panel8.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            Functions.SetNumericBoxValue(this.nmbAnswer1a, reader["Answer1a"]);
            Functions.SetNumericBoxValue(this.nmbAnswer1b, reader["Answer1b"]);
            Functions.SetDateBoxValue(this.dtbAnswer1c, reader["Answer1c"]);
            Functions.SetRadioGroupValue(this.rgAnswer2, reader["Answer2"]);
            Functions.SetRadioGroupValue(this.rgAnswer3, reader["Answer3"]);
            Functions.SetTextBoxText(this.txtPhysicianAddress, reader["PhysicianAddress"]);
            Functions.SetTextBoxText(this.txtPhysicianCity, reader["PhysicianCity"]);
            Functions.SetTextBoxText(this.txtPhysicianState, reader["PhysicianState"]);
            Functions.SetTextBoxText(this.txtPhysicianZip, reader["PhysicianZip"]);
            Functions.SetTextBoxText(this.txtPhysicianName, reader["PhysicianName"]);
            Functions.SetRadioGroupValue(this.rgAnswer5, reader["Answer5"]);
            Functions.SetTextBoxText(this.txtAnswer6, reader["Answer6"]);
            Functions.SetNumericBoxValue(this.nmbAnswer7a, reader["Answer7a"]);
            Functions.SetNumericBoxValue(this.nmbAnswer7b, reader["Answer7b"]);
            Functions.SetDateBoxValue(this.dtbAnswer7c, reader["Answer7c"]);
            Functions.SetRadioGroupValue(this.rgAnswer8, reader["Answer8"]);
            Functions.SetRadioGroupValue(this.rgAnswer9, reader["Answer9"]);
            Functions.SetRadioGroupValue(this.rgAnswer10, reader["Answer10"]);
        }

        private void nmbAnswer1a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer1b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer7a_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbAnswer7b_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer10_ValueChanged(object sender, EventArgs e)
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

        private void rgAnswer5_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer8_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer9_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer1a", MySqlType.Int, 4).Value = NullableConvert.ToInt32(this.nmbAnswer1a.AsInt32);
            cmd.Parameters.Add("Answer1b", MySqlType.Int, 4).Value = NullableConvert.ToInt32(this.nmbAnswer1b.AsInt32);
            cmd.Parameters.Add("Answer1c", MySqlType.Date, 4).Value = Functions.GetDateBoxValue(this.dtbAnswer1c);
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 5).Value = this.rgAnswer2.Value;
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.rgAnswer3.Value;
            cmd.Parameters.Add("PhysicianAddress", MySqlType.VarChar, 50).Value = this.txtPhysicianAddress.Text;
            cmd.Parameters.Add("PhysicianCity", MySqlType.VarChar, 50).Value = this.txtPhysicianCity.Text;
            cmd.Parameters.Add("PhysicianState", MySqlType.VarChar, 50).Value = this.txtPhysicianState.Text;
            cmd.Parameters.Add("PhysicianZip", MySqlType.VarChar, 50).Value = this.txtPhysicianZip.Text;
            cmd.Parameters.Add("PhysicianName", MySqlType.VarChar, 50).Value = this.txtPhysicianName.Text;
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.rgAnswer5.Value;
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 10).Value = this.txtAnswer6.Text;
            cmd.Parameters.Add("Answer7a", MySqlType.Int).Value = NullableConvert.ToInt32(this.nmbAnswer7a.AsInt32);
            cmd.Parameters.Add("Answer7b", MySqlType.Int).Value = NullableConvert.ToInt32(this.nmbAnswer7b.AsInt32);
            cmd.Parameters.Add("Answer7c", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbAnswer7c);
            cmd.Parameters.Add("Answer8", MySqlType.VarChar).Value = this.rgAnswer8.Value;
            cmd.Parameters.Add("Answer9", MySqlType.VarChar).Value = this.rgAnswer9.Value;
            cmd.Parameters.Add("Answer10", MySqlType.VarChar).Value = this.rgAnswer10.Value;
        }

        private void txtAnswer6_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtPhysicianAddress_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtPhysicianCity_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtPhysicianName_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtPhysicianState_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtPhysicianZip_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel2")]
        private Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label6")]
        private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("lblAnswer7")]
        private Label lblAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel3")]
        private Panel Panel3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label8")]
        private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel5")]
        private Panel Panel5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel6")]
        private Panel Panel6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label15")]
        private Label Label15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label16")]
        private Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label17")]
        private Label Label17 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel8")]
        private Panel Panel8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label26")]
        private Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox9")]
        private TextBox TextBox9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label18")]
        private Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label24")]
        private Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label25")]
        private Label Label25 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("Label27")]
        private Label Label27 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label28")]
        private Label Label28 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label29")]
        private Label Label29 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label31")]
        private Label Label31 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel11")]
        private Panel Panel11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label32")]
        private Label Label32 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhysicianAddress")]
        private TextBox txtPhysicianAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhysicianName")]
        private TextBox txtPhysicianName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer7c")]
        private UltraDateTimeEditor dtbAnswer7c { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnswer6")]
        private TextBox txtAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer3")]
        private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer2")]
        private RadioGroup rgAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAnswer1c")]
        private UltraDateTimeEditor dtbAnswer1c { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer10")]
        private RadioGroup rgAnswer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer9")]
        private RadioGroup rgAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer8")]
        private RadioGroup rgAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer1a")]
        private NumericBox nmbAnswer1a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer1b")]
        private NumericBox nmbAnswer1b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer7b")]
        private NumericBox nmbAnswer7b { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer7a")]
        private NumericBox nmbAnswer7a { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer5")]
        private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label11")]
        private Label Label11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhysicianCity")]
        private TextBox txtPhysicianCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhysicianState")]
        private TextBox txtPhysicianState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhysicianZip")]
        private TextBox txtPhysicianZip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_4842;
    }
}

