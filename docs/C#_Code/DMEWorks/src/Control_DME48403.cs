using Devart.Data.MySql;
using DMEWorks;
using DMEWorks.CMN;
using DMEWorks.Controls;
using DMEWorks.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[DesignerGenerated]
public class Control_DME48403 : Control_CMNBase
{
    private IContainer components;

    public Control_DME48403()
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
        Functions.SetRadioGroupValue(this.rgAnswer4, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer5, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer6a, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer6b, DBNull.Value);
        Functions.SetDateBoxValue(this.dtbAnswer6c, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer7, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer8, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer9, DBNull.Value);
    }

    [DebuggerNonUserCode]
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

    private void dtbAnswer6c_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_DME48403));
        this.Label18 = new Label();
        this.dtbAnswer1c = new UltraDateTimeEditor();
        this.Label2 = new Label();
        this.Label8 = new Label();
        this.Label7 = new Label();
        this.Label24 = new Label();
        this.nmbAnswer1b = new NumericBox();
        this.rgAnswer3 = new RadioGroup();
        this.rgAnswer2 = new RadioGroup();
        this.nmbAnswer1a = new NumericBox();
        this.pnlAnswer1 = new Panel();
        this.lblAnswers = new Label();
        this.rgAnswer9 = new RadioGroup();
        this.Panel8 = new Panel();
        this.rgAnswer8 = new RadioGroup();
        this.rgAnswer7 = new RadioGroup();
        this.lblQuestion9 = new Label();
        this.lblQuestion8 = new Label();
        this.Panel5 = new Panel();
        this.Panel6 = new Panel();
        this.lblQuestion7 = new Label();
        this.Label26 = new Label();
        this.pnlAnswers = new Panel();
        this.pnlAnswer6 = new Panel();
        this.nmbAnswer6b = new NumericBox();
        this.nmbAnswer6a = new NumericBox();
        this.Label9 = new Label();
        this.Label10 = new Label();
        this.dtbAnswer6c = new UltraDateTimeEditor();
        this.Label27 = new Label();
        this.Label28 = new Label();
        this.Label29 = new Label();
        this.pnlAnswer5 = new Panel();
        this.Label32 = new Label();
        this.txtAnswer5 = new TextBox();
        this.rgAnswer4 = new RadioGroup();
        this.Panel1 = new Panel();
        this.pnlQuestions = new Panel();
        this.lblQuestion6 = new Label();
        this.lblQuestion5 = new Label();
        this.lblQuestion4 = new Label();
        this.lblQuestion3 = new Label();
        this.lblQuestion2 = new Label();
        this.lblQuestion1 = new Label();
        this.lblQuestions = new Label();
        this.pnlAnswer1.SuspendLayout();
        this.Panel8.SuspendLayout();
        this.Panel5.SuspendLayout();
        this.Panel6.SuspendLayout();
        this.pnlAnswers.SuspendLayout();
        this.pnlAnswer6.SuspendLayout();
        this.pnlAnswer5.SuspendLayout();
        this.Panel1.SuspendLayout();
        this.pnlQuestions.SuspendLayout();
        base.SuspendLayout();
        this.Label18.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label18.Location = new Point(0x6a, 2);
        this.Label18.Name = "Label18";
        this.Label18.Size = new Size(0x2a, 0x13);
        this.Label18.TabIndex = 2;
        this.Label18.Text = "mm Hg";
        this.Label18.TextAlign = ContentAlignment.MiddleCenter;
        this.dtbAnswer1c.BorderStyle = UIElementBorderStyle.Solid;
        this.dtbAnswer1c.Location = new Point(0x1c, 0x2a);
        this.dtbAnswer1c.Margin = new Padding(0);
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
        this.Label24.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label24.Location = new Point(0x6a, 0x16);
        this.Label24.Name = "Label24";
        this.Label24.Size = new Size(0x2a, 0x13);
        this.Label24.TabIndex = 5;
        this.Label24.Text = "%";
        this.Label24.TextAlign = ContentAlignment.MiddleCenter;
        this.nmbAnswer1b.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer1b.Location = new Point(0x1c, 0x16);
        this.nmbAnswer1b.Margin = new Padding(0);
        this.nmbAnswer1b.Name = "nmbAnswer1b";
        this.nmbAnswer1b.Size = new Size(0x4e, 0x13);
        this.nmbAnswer1b.TabIndex = 4;
        this.nmbAnswer1b.TextAlign = HorizontalAlignment.Left;
        this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer3.Dock = DockStyle.Top;
        this.rgAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer3.Items = new string[] { "1", "2", "3" };
        this.rgAnswer3.Location = new Point(0, 120);
        this.rgAnswer3.Name = "rgAnswer3";
        this.rgAnswer3.Size = new Size(0x98, 0x18);
        this.rgAnswer3.TabIndex = 3;
        this.rgAnswer3.Value = "";
        this.rgAnswer2.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer2.Dock = DockStyle.Top;
        this.rgAnswer2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer2.Items = new string[] { "1", "2", "3" };
        this.rgAnswer2.Location = new Point(0, 0x58);
        this.rgAnswer2.Name = "rgAnswer2";
        this.rgAnswer2.Size = new Size(0x98, 0x20);
        this.rgAnswer2.TabIndex = 2;
        this.rgAnswer2.Value = "";
        this.nmbAnswer1a.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer1a.Location = new Point(0x1c, 2);
        this.nmbAnswer1a.Margin = new Padding(0);
        this.nmbAnswer1a.Name = "nmbAnswer1a";
        this.nmbAnswer1a.Size = new Size(0x4e, 0x13);
        this.nmbAnswer1a.TabIndex = 1;
        this.nmbAnswer1a.TextAlign = HorizontalAlignment.Left;
        this.pnlAnswer1.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer1.Controls.Add(this.nmbAnswer1b);
        this.pnlAnswer1.Controls.Add(this.nmbAnswer1a);
        this.pnlAnswer1.Controls.Add(this.Label24);
        this.pnlAnswer1.Controls.Add(this.Label18);
        this.pnlAnswer1.Controls.Add(this.dtbAnswer1c);
        this.pnlAnswer1.Controls.Add(this.Label2);
        this.pnlAnswer1.Controls.Add(this.Label8);
        this.pnlAnswer1.Controls.Add(this.Label7);
        this.pnlAnswer1.Dock = DockStyle.Top;
        this.pnlAnswer1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer1.Location = new Point(0, 0x18);
        this.pnlAnswer1.Name = "pnlAnswer1";
        this.pnlAnswer1.Size = new Size(0x98, 0x40);
        this.pnlAnswer1.TabIndex = 1;
        this.lblAnswers.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswers.Dock = DockStyle.Top;
        this.lblAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswers.Location = new Point(0, 0);
        this.lblAnswers.Name = "lblAnswers";
        this.lblAnswers.Size = new Size(0x98, 0x18);
        this.lblAnswers.TabIndex = 0;
        this.lblAnswers.Text = "ANSWERS";
        this.lblAnswers.TextAlign = ContentAlignment.TopCenter;
        this.rgAnswer9.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer9.Dock = DockStyle.Top;
        this.rgAnswer9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer9.Items = new string[] { "Y", "N" };
        this.rgAnswer9.Location = new Point(0, 0x38);
        this.rgAnswer9.Name = "rgAnswer9";
        this.rgAnswer9.Size = new Size(0x98, 0x18);
        this.rgAnswer9.TabIndex = 2;
        this.rgAnswer9.Value = "";
        this.Panel8.Controls.Add(this.rgAnswer9);
        this.Panel8.Controls.Add(this.rgAnswer8);
        this.Panel8.Controls.Add(this.rgAnswer7);
        this.Panel8.Dock = DockStyle.Left;
        this.Panel8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Panel8.Location = new Point(0, 0);
        this.Panel8.Name = "Panel8";
        this.Panel8.Size = new Size(0x98, 80);
        this.Panel8.TabIndex = 0;
        this.rgAnswer8.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer8.Dock = DockStyle.Top;
        this.rgAnswer8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer8.Items = new string[] { "Y", "N" };
        this.rgAnswer8.Location = new Point(0, 0x18);
        this.rgAnswer8.Name = "rgAnswer8";
        this.rgAnswer8.Size = new Size(0x98, 0x20);
        this.rgAnswer8.TabIndex = 1;
        this.rgAnswer8.Value = "";
        this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer7.Dock = DockStyle.Top;
        this.rgAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer7.Items = new string[] { "Y", "N" };
        this.rgAnswer7.Location = new Point(0, 0);
        this.rgAnswer7.Name = "rgAnswer7";
        this.rgAnswer7.Size = new Size(0x98, 0x18);
        this.rgAnswer7.TabIndex = 0;
        this.rgAnswer7.Value = "";
        this.lblQuestion9.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion9.Dock = DockStyle.Top;
        this.lblQuestion9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion9.Location = new Point(0, 0x38);
        this.lblQuestion9.Name = "lblQuestion9";
        this.lblQuestion9.Size = new Size(0x288, 0x18);
        this.lblQuestion9.TabIndex = 2;
        this.lblQuestion9.Text = "9.   Does the patient have a hermatocrit greater than 56%?";
        this.lblQuestion8.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion8.Dock = DockStyle.Top;
        this.lblQuestion8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion8.Location = new Point(0, 0x18);
        this.lblQuestion8.Name = "lblQuestion8";
        this.lblQuestion8.Size = new Size(0x288, 0x20);
        this.lblQuestion8.TabIndex = 1;
        this.lblQuestion8.Text = "8.   Does the patient have cor pulmonale or pulmonary hypertension documented by P pulmonale on an EKG or by an echocardiogram, gated blood pool scan or direct pulmonary artery pressure measurement?";
        this.Panel5.Controls.Add(this.Panel6);
        this.Panel5.Controls.Add(this.Panel8);
        this.Panel5.Dock = DockStyle.Top;
        this.Panel5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Panel5.Location = new Point(0, 0x117);
        this.Panel5.Name = "Panel5";
        this.Panel5.Size = new Size(800, 80);
        this.Panel5.TabIndex = 2;
        this.Panel6.BackColor = SystemColors.Control;
        this.Panel6.Controls.Add(this.lblQuestion9);
        this.Panel6.Controls.Add(this.lblQuestion8);
        this.Panel6.Controls.Add(this.lblQuestion7);
        this.Panel6.Dock = DockStyle.Fill;
        this.Panel6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Panel6.Location = new Point(0x98, 0);
        this.Panel6.Name = "Panel6";
        this.Panel6.Size = new Size(0x288, 80);
        this.Panel6.TabIndex = 1;
        this.lblQuestion7.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion7.Dock = DockStyle.Top;
        this.lblQuestion7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion7.Location = new Point(0, 0);
        this.lblQuestion7.Name = "lblQuestion7";
        this.lblQuestion7.Size = new Size(0x288, 0x18);
        this.lblQuestion7.TabIndex = 0;
        this.lblQuestion7.Text = "7.   Does the patient have dependent edema due to congestive heart failure?";
        this.Label26.BorderStyle = BorderStyle.FixedSingle;
        this.Label26.Dock = DockStyle.Top;
        this.Label26.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
        this.Label26.Location = new Point(0, 0x100);
        this.Label26.Name = "Label26";
        this.Label26.Size = new Size(800, 0x17);
        this.Label26.TabIndex = 1;
        this.Label26.Text = "ANSWER QUESTIONS 7-9 ONLY IF PO2 = 56–59 OR OXYGEN SATURATION = 89 IN QUESTION 1";
        this.Label26.TextAlign = ContentAlignment.MiddleCenter;
        this.pnlAnswers.Controls.Add(this.pnlAnswer6);
        this.pnlAnswers.Controls.Add(this.pnlAnswer5);
        this.pnlAnswers.Controls.Add(this.rgAnswer4);
        this.pnlAnswers.Controls.Add(this.rgAnswer3);
        this.pnlAnswers.Controls.Add(this.rgAnswer2);
        this.pnlAnswers.Controls.Add(this.pnlAnswer1);
        this.pnlAnswers.Controls.Add(this.lblAnswers);
        this.pnlAnswers.Dock = DockStyle.Left;
        this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswers.Location = new Point(0, 0);
        this.pnlAnswers.Name = "pnlAnswers";
        this.pnlAnswers.Size = new Size(0x98, 0x100);
        this.pnlAnswers.TabIndex = 0;
        this.pnlAnswer6.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer6.Controls.Add(this.nmbAnswer6b);
        this.pnlAnswer6.Controls.Add(this.nmbAnswer6a);
        this.pnlAnswer6.Controls.Add(this.Label9);
        this.pnlAnswer6.Controls.Add(this.Label10);
        this.pnlAnswer6.Controls.Add(this.dtbAnswer6c);
        this.pnlAnswer6.Controls.Add(this.Label27);
        this.pnlAnswer6.Controls.Add(this.Label28);
        this.pnlAnswer6.Controls.Add(this.Label29);
        this.pnlAnswer6.Dock = DockStyle.Top;
        this.pnlAnswer6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer6.Location = new Point(0, 0xc0);
        this.pnlAnswer6.Name = "pnlAnswer6";
        this.pnlAnswer6.Size = new Size(0x98, 0x40);
        this.pnlAnswer6.TabIndex = 6;
        this.nmbAnswer6b.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer6b.Location = new Point(0x1c, 0x16);
        this.nmbAnswer6b.Margin = new Padding(0);
        this.nmbAnswer6b.Name = "nmbAnswer6b";
        this.nmbAnswer6b.Size = new Size(0x4e, 0x13);
        this.nmbAnswer6b.TabIndex = 4;
        this.nmbAnswer6b.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer6a.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer6a.Location = new Point(0x1c, 2);
        this.nmbAnswer6a.Margin = new Padding(0);
        this.nmbAnswer6a.Name = "nmbAnswer6a";
        this.nmbAnswer6a.Size = new Size(0x4e, 0x13);
        this.nmbAnswer6a.TabIndex = 1;
        this.nmbAnswer6a.TextAlign = HorizontalAlignment.Left;
        this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label9.Location = new Point(0x6a, 0x16);
        this.Label9.Name = "Label9";
        this.Label9.Size = new Size(0x2a, 0x13);
        this.Label9.TabIndex = 5;
        this.Label9.Text = "%";
        this.Label9.TextAlign = ContentAlignment.MiddleCenter;
        this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label10.Location = new Point(0x6a, 2);
        this.Label10.Name = "Label10";
        this.Label10.Size = new Size(0x2a, 0x13);
        this.Label10.TabIndex = 2;
        this.Label10.Text = "mm Hg";
        this.Label10.TextAlign = ContentAlignment.MiddleCenter;
        this.dtbAnswer6c.BorderStyle = UIElementBorderStyle.Solid;
        this.dtbAnswer6c.Location = new Point(0x1c, 0x2a);
        this.dtbAnswer6c.Margin = new Padding(0);
        this.dtbAnswer6c.Name = "dtbAnswer6c";
        this.dtbAnswer6c.Size = new Size(120, 0x13);
        this.dtbAnswer6c.TabIndex = 7;
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
        this.pnlAnswer5.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer5.Controls.Add(this.Label32);
        this.pnlAnswer5.Controls.Add(this.txtAnswer5);
        this.pnlAnswer5.Dock = DockStyle.Top;
        this.pnlAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer5.Location = new Point(0, 0xa8);
        this.pnlAnswer5.Name = "pnlAnswer5";
        this.pnlAnswer5.Size = new Size(0x98, 0x18);
        this.pnlAnswer5.TabIndex = 5;
        this.Label32.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label32.Location = new Point(0x6a, 2);
        this.Label32.Name = "Label32";
        this.Label32.Size = new Size(0x2a, 0x13);
        this.Label32.TabIndex = 1;
        this.Label32.Text = "LPM";
        this.Label32.TextAlign = ContentAlignment.MiddleCenter;
        this.txtAnswer5.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.txtAnswer5.Location = new Point(2, 1);
        this.txtAnswer5.Margin = new Padding(0);
        this.txtAnswer5.Name = "txtAnswer5";
        this.txtAnswer5.Size = new Size(0x68, 20);
        this.txtAnswer5.TabIndex = 0;
        this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer4.Dock = DockStyle.Top;
        this.rgAnswer4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer4.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer4.Location = new Point(0, 0x90);
        this.rgAnswer4.Name = "rgAnswer4";
        this.rgAnswer4.Size = new Size(0x98, 0x18);
        this.rgAnswer4.TabIndex = 4;
        this.rgAnswer4.Value = "";
        this.Panel1.Controls.Add(this.pnlQuestions);
        this.Panel1.Controls.Add(this.pnlAnswers);
        this.Panel1.Dock = DockStyle.Top;
        this.Panel1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Panel1.Location = new Point(0, 0);
        this.Panel1.Name = "Panel1";
        this.Panel1.Size = new Size(800, 0x100);
        this.Panel1.TabIndex = 0;
        this.pnlQuestions.Controls.Add(this.lblQuestion6);
        this.pnlQuestions.Controls.Add(this.lblQuestion5);
        this.pnlQuestions.Controls.Add(this.lblQuestion4);
        this.pnlQuestions.Controls.Add(this.lblQuestion3);
        this.pnlQuestions.Controls.Add(this.lblQuestion2);
        this.pnlQuestions.Controls.Add(this.lblQuestion1);
        this.pnlQuestions.Controls.Add(this.lblQuestions);
        this.pnlQuestions.Dock = DockStyle.Fill;
        this.pnlQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlQuestions.Location = new Point(0x98, 0);
        this.pnlQuestions.Name = "pnlQuestions";
        this.pnlQuestions.Size = new Size(0x288, 0x100);
        this.pnlQuestions.TabIndex = 1;
        this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion6.Dock = DockStyle.Top;
        this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion6.Location = new Point(0, 0xc0);
        this.lblQuestion6.Name = "lblQuestion6";
        this.lblQuestion6.Size = new Size(0x288, 0x40);
        this.lblQuestion6.TabIndex = 6;
        this.lblQuestion6.Text = manager.GetString("lblQuestion6.Text");
        this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion5.Dock = DockStyle.Top;
        this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion5.Location = new Point(0, 0xa8);
        this.lblQuestion5.Name = "lblQuestion5";
        this.lblQuestion5.Size = new Size(0x288, 0x18);
        this.lblQuestion5.TabIndex = 5;
        this.lblQuestion5.Text = "5.   Enter the highest oxygen flow rate ordered for this patient in liters per minute. If less than 1 LPM, enter a \"X\".";
        this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion4.Dock = DockStyle.Top;
        this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion4.Location = new Point(0, 0x90);
        this.lblQuestion4.Name = "lblQuestion4";
        this.lblQuestion4.Size = new Size(0x288, 0x18);
        this.lblQuestion4.TabIndex = 4;
        this.lblQuestion4.Text = "4.   If you are ordering portable oxygen, is the patient mobile within the home? If you are not ordering portable oxygen, circle D.";
        this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion3.Dock = DockStyle.Top;
        this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion3.Location = new Point(0, 120);
        this.lblQuestion3.Name = "lblQuestion3";
        this.lblQuestion3.Size = new Size(0x288, 0x18);
        this.lblQuestion3.TabIndex = 3;
        this.lblQuestion3.Text = "3.   Circle the one number for the condition of the test in Question 1: (1) At Rest; (2) During Exercise; (3) During Sleep";
        this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion2.Dock = DockStyle.Top;
        this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion2.Location = new Point(0, 0x58);
        this.lblQuestion2.Name = "lblQuestion2";
        this.lblQuestion2.Size = new Size(0x288, 0x20);
        this.lblQuestion2.TabIndex = 2;
        this.lblQuestion2.Text = manager.GetString("lblQuestion2.Text");
        this.lblQuestion1.BackColor = SystemColors.Control;
        this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion1.Dock = DockStyle.Top;
        this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion1.Location = new Point(0, 0x18);
        this.lblQuestion1.Name = "lblQuestion1";
        this.lblQuestion1.Size = new Size(0x288, 0x40);
        this.lblQuestion1.TabIndex = 1;
        this.lblQuestion1.Text = "1.  Enter the result of most recent test taken on or before the certification date listed in Section A. \r\nEnter (a) arterial blood gas PO2 and/or (b) oxygen saturation test. Enter date of test (c).";
        this.lblQuestions.BackColor = SystemColors.Control;
        this.lblQuestions.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestions.Dock = DockStyle.Top;
        this.lblQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestions.Location = new Point(0, 0);
        this.lblQuestions.Name = "lblQuestions";
        this.lblQuestions.Size = new Size(0x288, 0x18);
        this.lblQuestions.TabIndex = 0;
        this.lblQuestions.Text = "ANSWER QUESTIONS 1-9. (Circle Y for Yes, N for No, or D for Does Not Apply, unless otherwise noted.)";
        base.Controls.Add(this.Panel5);
        base.Controls.Add(this.Label26);
        base.Controls.Add(this.Panel1);
        base.Name = "Control_DME48403";
        base.Size = new Size(800, 360);
        this.pnlAnswer1.ResumeLayout(false);
        this.Panel8.ResumeLayout(false);
        this.Panel5.ResumeLayout(false);
        this.Panel6.ResumeLayout(false);
        this.pnlAnswers.ResumeLayout(false);
        this.pnlAnswer6.ResumeLayout(false);
        this.pnlAnswer5.ResumeLayout(false);
        this.pnlAnswer5.PerformLayout();
        this.Panel1.ResumeLayout(false);
        this.pnlQuestions.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    public override void LoadFromReader(MySqlDataReader reader)
    {
        Functions.SetNumericBoxValue(this.nmbAnswer1a, reader["Answer1a"]);
        Functions.SetNumericBoxValue(this.nmbAnswer1b, reader["Answer1b"]);
        Functions.SetDateBoxValue(this.dtbAnswer1c, reader["Answer1c"]);
        Functions.SetRadioGroupValue(this.rgAnswer2, reader["Answer2"]);
        Functions.SetRadioGroupValue(this.rgAnswer3, reader["Answer3"]);
        Functions.SetRadioGroupValue(this.rgAnswer4, reader["Answer4"]);
        Functions.SetTextBoxText(this.txtAnswer5, reader["Answer5"]);
        Functions.SetNumericBoxValue(this.nmbAnswer6a, reader["Answer6a"]);
        Functions.SetNumericBoxValue(this.nmbAnswer6b, reader["Answer6b"]);
        Functions.SetDateBoxValue(this.dtbAnswer6c, reader["Answer6c"]);
        Functions.SetRadioGroupValue(this.rgAnswer7, reader["Answer7"]);
        Functions.SetRadioGroupValue(this.rgAnswer8, reader["Answer8"]);
        Functions.SetRadioGroupValue(this.rgAnswer9, reader["Answer9"]);
    }

    private void nmbAnswer1a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer1b_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer6a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer6b_ValueChanged(object sender, EventArgs e)
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

    private void rgAnswer7_ValueChanged(object sender, EventArgs e)
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
        cmd.Parameters.Add("Answer1a", MySqlType.Int).Value = this.nmbAnswer1a.AsInt32.GetValueOrDefault(0);
        cmd.Parameters.Add("Answer1b", MySqlType.Int).Value = this.nmbAnswer1b.AsInt32.GetValueOrDefault(0);
        cmd.Parameters.Add("Answer1c", MySqlType.Date, 4).Value = Functions.GetDateBoxValue(this.dtbAnswer1c);
        cmd.Parameters.Add("Answer2", MySqlType.VarChar, 5).Value = this.rgAnswer2.Value;
        cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.rgAnswer3.Value;
        cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.rgAnswer4.Value;
        cmd.Parameters.Add("Answer5", MySqlType.VarChar, 10).Value = this.txtAnswer5.Text;
        cmd.Parameters.Add("Answer6a", MySqlType.Int).Value = this.nmbAnswer6a.AsInt32.GetValueOrDefault(0);
        cmd.Parameters.Add("Answer6b", MySqlType.Int).Value = this.nmbAnswer6b.AsInt32.GetValueOrDefault(0);
        cmd.Parameters.Add("Answer6c", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbAnswer6c);
        cmd.Parameters.Add("Answer7", MySqlType.VarChar).Value = this.rgAnswer7.Value;
        cmd.Parameters.Add("Answer8", MySqlType.VarChar).Value = this.rgAnswer8.Value;
        cmd.Parameters.Add("Answer9", MySqlType.VarChar).Value = this.rgAnswer9.Value;
    }

    private void txtAnswer5_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    [field: AccessedThroughProperty("dtbAnswer1c")]
    private UltraDateTimeEditor dtbAnswer1c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dtbAnswer6c")]
    private UltraDateTimeEditor dtbAnswer6c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label10")]
    private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label18")]
    private Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label2")]
    private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label24")]
    private Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label26")]
    private Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

    [field: AccessedThroughProperty("Label32")]
    private Label Label32 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label7")]
    private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label8")]
    private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label9")]
    private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAnswers")]
    private Label lblAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion1")]
    private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion2")]
    private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

    [field: AccessedThroughProperty("lblQuestion8")]
    private Label lblQuestion8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion9")]
    private Label lblQuestion9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestions")]
    private Label lblQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer1a")]
    private NumericBox nmbAnswer1a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer1b")]
    private NumericBox nmbAnswer1b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer6a")]
    private NumericBox nmbAnswer6a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer6b")]
    private NumericBox nmbAnswer6b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Panel1")]
    private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Panel5")]
    private Panel Panel5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Panel6")]
    private Panel Panel6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Panel8")]
    private Panel Panel8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer1")]
    private Panel pnlAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer5")]
    private Panel pnlAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer6")]
    private Panel pnlAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswers")]
    private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestions")]
    private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer2")]
    private RadioGroup rgAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer3")]
    private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer4")]
    private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer7")]
    private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer8")]
    private RadioGroup rgAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer9")]
    private RadioGroup rgAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer5")]
    private TextBox txtAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public override DmercType Type =>
        DmercType.DME_48403;
}

