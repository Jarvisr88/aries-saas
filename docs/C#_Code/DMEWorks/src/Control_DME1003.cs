using Devart.Data.MySql;
using DMEWorks;
using DMEWorks.CMN;
using DMEWorks.Controls;
using DMEWorks.Core;
using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[DesignerGenerated]
public class Control_DME1003 : Control_CMNBase
{
    private IContainer components;

    public Control_DME1003()
    {
        this.InitializeComponent();
    }

    public override void Clear()
    {
        Functions.SetRadioGroupValue(this.rgAnswer1, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer2, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer3a, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer3b, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer4a, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer4b, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer5, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer6, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer7, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8a, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8b, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8c, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8d, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8e, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8f, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8g, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer8h, DBNull.Value);
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

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_DME1003));
        this.rgAnswer9 = new RadioGroup();
        this.lblAnswer8 = new Label();
        this.nmbAnswer6 = new NumericBox();
        this.Label10 = new Label();
        this.Label11 = new Label();
        this.Label12 = new Label();
        this.Label9 = new Label();
        this.rgAnswer7 = new RadioGroup();
        this.Label1 = new Label();
        this.Label13 = new Label();
        this.lblAnswers = new Label();
        this.lblQuestion6 = new Label();
        this.lblQuestion7 = new Label();
        this.lblQuestions = new Label();
        this.Label5 = new Label();
        this.lblQuestion9 = new Label();
        this.nmbAnswer8h = new NumericBox();
        this.nmbAnswer8c = new NumericBox();
        this.nmbAnswer8g = new NumericBox();
        this.nmbAnswer8e = new NumericBox();
        this.nmbAnswer8b = new NumericBox();
        this.nmbAnswer8f = new NumericBox();
        this.pnlQuestions = new Panel();
        this.pnlQuestion8 = new Panel();
        this.nmbAnswer8d = new NumericBox();
        this.nmbAnswer8a = new NumericBox();
        this.Label8 = new Label();
        this.Label14 = new Label();
        this.Label3 = new Label();
        this.Label4 = new Label();
        this.Label6 = new Label();
        this.lblQuestion5 = new Label();
        this.lblQuestion4 = new Label();
        this.lblQuestion3 = new Label();
        this.lblQuestion2 = new Label();
        this.lblQuestion1 = new Label();
        this.pnlAnswers = new Panel();
        this.pnlAnswer6 = new Panel();
        this.rgAnswer5 = new RadioGroup();
        this.pnlAnswer4 = new Panel();
        this.nmbAnswer4b = new NumericBox();
        this.nmbAnswer4a = new NumericBox();
        this.Label23 = new Label();
        this.Label24 = new Label();
        this.pnlAnswer3 = new Panel();
        this.Label25 = new Label();
        this.Label26 = new Label();
        this.txtAnswer3b = new TextBox();
        this.txtAnswer3a = new TextBox();
        this.rgAnswer2 = new RadioGroup();
        this.rgAnswer1 = new RadioGroup();
        this.pnlQuestions.SuspendLayout();
        this.pnlQuestion8.SuspendLayout();
        this.pnlAnswers.SuspendLayout();
        this.pnlAnswer6.SuspendLayout();
        this.pnlAnswer4.SuspendLayout();
        this.pnlAnswer3.SuspendLayout();
        base.SuspendLayout();
        this.rgAnswer9.BackColor = SystemColors.Control;
        this.rgAnswer9.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer9.Dock = DockStyle.Top;
        this.rgAnswer9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer9.Items = new string[] { "1", "2", "3" };
        this.rgAnswer9.Location = new Point(0, 0x178);
        this.rgAnswer9.Name = "rgAnswer9";
        this.rgAnswer9.Size = new Size(0x98, 0x20);
        this.rgAnswer9.TabIndex = 9;
        this.rgAnswer9.Value = "";
        this.lblAnswer8.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswer8.Dock = DockStyle.Top;
        this.lblAnswer8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswer8.Location = new Point(0, 0x124);
        this.lblAnswer8.Name = "lblAnswer8";
        this.lblAnswer8.Size = new Size(0x98, 0x54);
        this.lblAnswer8.TabIndex = 8;
        this.nmbAnswer6.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.nmbAnswer6.Location = new Point(4, 2);
        this.nmbAnswer6.Name = "nmbAnswer6";
        this.nmbAnswer6.Size = new Size(140, 0x13);
        this.nmbAnswer6.TabIndex = 0;
        this.nmbAnswer6.TextAlign = HorizontalAlignment.Left;
        this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label10.Location = new Point(8, 60);
        this.Label10.Name = "Label10";
        this.Label10.Size = new Size(0x40, 0x13);
        this.Label10.TabIndex = 13;
        this.Label10.Text = "Lipids";
        this.Label10.TextAlign = ContentAlignment.MiddleLeft;
        this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label11.Location = new Point(8, 40);
        this.Label11.Name = "Label11";
        this.Label11.Size = new Size(0x40, 0x13);
        this.Label11.TabIndex = 8;
        this.Label11.Text = "Dextrose";
        this.Label11.TextAlign = ContentAlignment.MiddleLeft;
        this.Label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label12.Location = new Point(0x94, 20);
        this.Label12.Name = "Label12";
        this.Label12.Size = new Size(0x30, 0x13);
        this.Label12.TabIndex = 3;
        this.Label12.Text = "(ml/day)";
        this.Label12.TextAlign = ContentAlignment.MiddleLeft;
        this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label9.Location = new Point(8, 20);
        this.Label9.Name = "Label9";
        this.Label9.Size = new Size(0x40, 0x13);
        this.Label9.TabIndex = 1;
        this.Label9.Text = "Amino Acid";
        this.Label9.TextAlign = ContentAlignment.MiddleLeft;
        this.rgAnswer7.BackColor = SystemColors.Control;
        this.rgAnswer7.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer7.Dock = DockStyle.Top;
        this.rgAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer7.Items = new string[] { "Y", "N" };
        this.rgAnswer7.Location = new Point(0, 0xf8);
        this.rgAnswer7.Name = "rgAnswer7";
        this.rgAnswer7.Size = new Size(0x98, 0x2c);
        this.rgAnswer7.TabIndex = 7;
        this.rgAnswer7.Value = "";
        this.Label1.Dock = DockStyle.Top;
        this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label1.Location = new Point(0, 0);
        this.Label1.Name = "Label1";
        this.Label1.Size = new Size(0x286, 0x10);
        this.Label1.TabIndex = 0;
        this.Label1.Text = "8. Formula components:";
        this.Label13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label13.Location = new Point(0x94, 40);
        this.Label13.Name = "Label13";
        this.Label13.Size = new Size(0x30, 0x13);
        this.Label13.TabIndex = 10;
        this.Label13.Text = "(ml/day)";
        this.Label13.TextAlign = ContentAlignment.MiddleLeft;
        this.lblAnswers.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswers.Dock = DockStyle.Top;
        this.lblAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswers.Location = new Point(0, 0);
        this.lblAnswers.Name = "lblAnswers";
        this.lblAnswers.Size = new Size(0x98, 0x20);
        this.lblAnswers.TabIndex = 0;
        this.lblAnswers.Text = "ANSWERS";
        this.lblAnswers.TextAlign = ContentAlignment.TopCenter;
        this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion6.Dock = DockStyle.Top;
        this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion6.Location = new Point(0, 0xe0);
        this.lblQuestion6.Name = "lblQuestion6";
        this.lblQuestion6.Size = new Size(0x288, 0x18);
        this.lblQuestion6.TabIndex = 6;
        this.lblQuestion6.Text = "6. Days per week administered or infused (Enter 1 - 7)";
        this.lblQuestion7.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion7.Dock = DockStyle.Top;
        this.lblQuestion7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion7.Location = new Point(0, 0xf8);
        this.lblQuestion7.Name = "lblQuestion7";
        this.lblQuestion7.Size = new Size(0x288, 0x2c);
        this.lblQuestion7.TabIndex = 7;
        this.lblQuestion7.Text = manager.GetString("lblQuestion7.Text");
        this.lblQuestions.BackColor = SystemColors.Control;
        this.lblQuestions.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestions.Dock = DockStyle.Top;
        this.lblQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestions.Location = new Point(0, 0);
        this.lblQuestions.Name = "lblQuestions";
        this.lblQuestions.Size = new Size(0x288, 0x20);
        this.lblQuestions.TabIndex = 0;
        this.lblQuestions.Text = "ANSWER QUESTIONS 1 - 6 FOR ENTERAL NUTRITION, AND 6 - 9 FOR PARENTERAL NUTRITION\r\n    (Circle Y for Yes, N for No, Unless Otherwise Noted)";
        this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label5.Location = new Point(0x94, 60);
        this.Label5.Name = "Label5";
        this.Label5.Size = new Size(0x30, 0x13);
        this.Label5.TabIndex = 15;
        this.Label5.Text = "(ml/day)";
        this.Label5.TextAlign = ContentAlignment.MiddleLeft;
        this.lblQuestion9.BackColor = SystemColors.Control;
        this.lblQuestion9.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion9.Dock = DockStyle.Top;
        this.lblQuestion9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion9.Location = new Point(0, 0x178);
        this.lblQuestion9.Name = "lblQuestion9";
        this.lblQuestion9.Size = new Size(0x288, 0x20);
        this.lblQuestion9.TabIndex = 9;
        this.lblQuestion9.Text = "9. Circle the number for the route of administration.\r\n1 – Central Line (Including PICC)    2 – Hemodialysis Access Line    3 – Peritoneal Catheter";
        this.nmbAnswer8h.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8h.Location = new Point(0x16c, 60);
        this.nmbAnswer8h.Name = "nmbAnswer8h";
        this.nmbAnswer8h.Size = new Size(0x40, 0x13);
        this.nmbAnswer8h.TabIndex = 0x12;
        this.nmbAnswer8h.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer8c.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8c.Location = new Point(0x16c, 20);
        this.nmbAnswer8c.Name = "nmbAnswer8c";
        this.nmbAnswer8c.Size = new Size(0x40, 0x13);
        this.nmbAnswer8c.TabIndex = 6;
        this.nmbAnswer8c.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer8g.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8g.Location = new Point(200, 60);
        this.nmbAnswer8g.Name = "nmbAnswer8g";
        this.nmbAnswer8g.Size = new Size(0x40, 0x13);
        this.nmbAnswer8g.TabIndex = 0x10;
        this.nmbAnswer8g.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer8e.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8e.Location = new Point(200, 40);
        this.nmbAnswer8e.Name = "nmbAnswer8e";
        this.nmbAnswer8e.Size = new Size(0x40, 0x13);
        this.nmbAnswer8e.TabIndex = 11;
        this.nmbAnswer8e.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer8b.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8b.Location = new Point(200, 20);
        this.nmbAnswer8b.Name = "nmbAnswer8b";
        this.nmbAnswer8b.Size = new Size(0x40, 0x13);
        this.nmbAnswer8b.TabIndex = 4;
        this.nmbAnswer8b.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer8f.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8f.Location = new Point(0x4c, 60);
        this.nmbAnswer8f.Name = "nmbAnswer8f";
        this.nmbAnswer8f.Size = new Size(0x40, 0x13);
        this.nmbAnswer8f.TabIndex = 14;
        this.nmbAnswer8f.TextAlign = HorizontalAlignment.Left;
        this.pnlQuestions.Controls.Add(this.lblQuestion9);
        this.pnlQuestions.Controls.Add(this.pnlQuestion8);
        this.pnlQuestions.Controls.Add(this.lblQuestion7);
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
        this.pnlQuestions.Size = new Size(0x288, 0x198);
        this.pnlQuestions.TabIndex = 1;
        this.pnlQuestion8.BorderStyle = BorderStyle.FixedSingle;
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8h);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8c);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8g);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8e);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8b);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8f);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8d);
        this.pnlQuestion8.Controls.Add(this.nmbAnswer8a);
        this.pnlQuestion8.Controls.Add(this.Label8);
        this.pnlQuestion8.Controls.Add(this.Label14);
        this.pnlQuestion8.Controls.Add(this.Label3);
        this.pnlQuestion8.Controls.Add(this.Label4);
        this.pnlQuestion8.Controls.Add(this.Label6);
        this.pnlQuestion8.Controls.Add(this.Label5);
        this.pnlQuestion8.Controls.Add(this.Label1);
        this.pnlQuestion8.Controls.Add(this.Label9);
        this.pnlQuestion8.Controls.Add(this.Label10);
        this.pnlQuestion8.Controls.Add(this.Label11);
        this.pnlQuestion8.Controls.Add(this.Label12);
        this.pnlQuestion8.Controls.Add(this.Label13);
        this.pnlQuestion8.Dock = DockStyle.Top;
        this.pnlQuestion8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlQuestion8.Location = new Point(0, 0x124);
        this.pnlQuestion8.Name = "pnlQuestion8";
        this.pnlQuestion8.Size = new Size(0x288, 0x54);
        this.pnlQuestion8.TabIndex = 8;
        this.nmbAnswer8d.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8d.Location = new Point(0x4c, 40);
        this.nmbAnswer8d.Name = "nmbAnswer8d";
        this.nmbAnswer8d.Size = new Size(0x40, 0x13);
        this.nmbAnswer8d.TabIndex = 9;
        this.nmbAnswer8d.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer8a.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer8a.Location = new Point(0x4c, 20);
        this.nmbAnswer8a.Name = "nmbAnswer8a";
        this.nmbAnswer8a.Size = new Size(0x40, 0x13);
        this.nmbAnswer8a.TabIndex = 2;
        this.nmbAnswer8a.TextAlign = HorizontalAlignment.Left;
        this.Label8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label8.Location = new Point(0x1b4, 20);
        this.Label8.Name = "Label8";
        this.Label8.Size = new Size(0x58, 0x13);
        this.Label8.TabIndex = 7;
        this.Label8.Text = "gms protein/day";
        this.Label8.TextAlign = ContentAlignment.MiddleLeft;
        this.Label14.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label14.Location = new Point(0x1b4, 60);
        this.Label14.Name = "Label14";
        this.Label14.Size = new Size(0x58, 0x13);
        this.Label14.TabIndex = 0x13;
        this.Label14.Text = "concentration %";
        this.Label14.TextAlign = ContentAlignment.MiddleLeft;
        this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label3.Location = new Point(0x110, 60);
        this.Label3.Name = "Label3";
        this.Label3.Size = new Size(0x58, 0x13);
        this.Label3.TabIndex = 0x11;
        this.Label3.Text = "days/week";
        this.Label3.TextAlign = ContentAlignment.MiddleLeft;
        this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label4.Location = new Point(0x110, 20);
        this.Label4.Name = "Label4";
        this.Label4.Size = new Size(0x58, 0x13);
        this.Label4.TabIndex = 5;
        this.Label4.Text = "concentration %";
        this.Label4.TextAlign = ContentAlignment.MiddleLeft;
        this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label6.Location = new Point(0x110, 40);
        this.Label6.Name = "Label6";
        this.Label6.Size = new Size(0x58, 0x13);
        this.Label6.TabIndex = 12;
        this.Label6.Text = "concentration %";
        this.Label6.TextAlign = ContentAlignment.MiddleLeft;
        this.lblQuestion5.BackColor = SystemColors.Control;
        this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion5.Dock = DockStyle.Top;
        this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion5.Location = new Point(0, 0xc0);
        this.lblQuestion5.Name = "lblQuestion5";
        this.lblQuestion5.Size = new Size(0x288, 0x20);
        this.lblQuestion5.TabIndex = 5;
        this.lblQuestion5.Text = "5. Circle the number for method of administration?\r\n1 – Syringe    2 – Gravity    3 – Pump    4 – Oral (i.e. drinking)";
        this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion4.Dock = DockStyle.Top;
        this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion4.Location = new Point(0, 0x90);
        this.lblQuestion4.Name = "lblQuestion4";
        this.lblQuestion4.Size = new Size(0x288, 0x30);
        this.lblQuestion4.TabIndex = 4;
        this.lblQuestion4.Text = "4. Calories per day for each corresponding HCPCS code(s).";
        this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion3.Dock = DockStyle.Top;
        this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion3.Location = new Point(0, 0x60);
        this.lblQuestion3.Name = "lblQuestion3";
        this.lblQuestion3.Size = new Size(0x288, 0x30);
        this.lblQuestion3.TabIndex = 3;
        this.lblQuestion3.Text = "3. Print HCPCS code(s) of product.";
        this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion2.Dock = DockStyle.Top;
        this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion2.Location = new Point(0, 0x40);
        this.lblQuestion2.Name = "lblQuestion2";
        this.lblQuestion2.Size = new Size(0x288, 0x20);
        this.lblQuestion2.TabIndex = 2;
        this.lblQuestion2.Text = "2. Is the enteral nutrition being provided for administration via tube? (i.e., gastrostomy tube, jejunostomy tube, nasogastric tube)";
        this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion1.Dock = DockStyle.Top;
        this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion1.Location = new Point(0, 0x20);
        this.lblQuestion1.Name = "lblQuestion1";
        this.lblQuestion1.Size = new Size(0x288, 0x20);
        this.lblQuestion1.TabIndex = 1;
        this.lblQuestion1.Text = manager.GetString("lblQuestion1.Text");
        this.pnlAnswers.Controls.Add(this.rgAnswer9);
        this.pnlAnswers.Controls.Add(this.lblAnswer8);
        this.pnlAnswers.Controls.Add(this.rgAnswer7);
        this.pnlAnswers.Controls.Add(this.pnlAnswer6);
        this.pnlAnswers.Controls.Add(this.rgAnswer5);
        this.pnlAnswers.Controls.Add(this.pnlAnswer4);
        this.pnlAnswers.Controls.Add(this.pnlAnswer3);
        this.pnlAnswers.Controls.Add(this.rgAnswer2);
        this.pnlAnswers.Controls.Add(this.rgAnswer1);
        this.pnlAnswers.Controls.Add(this.lblAnswers);
        this.pnlAnswers.Dock = DockStyle.Left;
        this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswers.Location = new Point(0, 0);
        this.pnlAnswers.Name = "pnlAnswers";
        this.pnlAnswers.Size = new Size(0x98, 0x198);
        this.pnlAnswers.TabIndex = 0;
        this.pnlAnswer6.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer6.Controls.Add(this.nmbAnswer6);
        this.pnlAnswer6.Dock = DockStyle.Top;
        this.pnlAnswer6.Location = new Point(0, 0xe0);
        this.pnlAnswer6.Name = "pnlAnswer6";
        this.pnlAnswer6.Size = new Size(0x98, 0x18);
        this.pnlAnswer6.TabIndex = 6;
        this.rgAnswer5.BackColor = SystemColors.Control;
        this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer5.Dock = DockStyle.Top;
        this.rgAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer5.Items = new string[] { "1", "2", "3", "4" };
        this.rgAnswer5.Location = new Point(0, 0xc0);
        this.rgAnswer5.Name = "rgAnswer5";
        this.rgAnswer5.Size = new Size(0x98, 0x20);
        this.rgAnswer5.TabIndex = 5;
        this.rgAnswer5.Value = "";
        this.pnlAnswer4.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer4.Controls.Add(this.nmbAnswer4b);
        this.pnlAnswer4.Controls.Add(this.nmbAnswer4a);
        this.pnlAnswer4.Controls.Add(this.Label23);
        this.pnlAnswer4.Controls.Add(this.Label24);
        this.pnlAnswer4.Dock = DockStyle.Top;
        this.pnlAnswer4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer4.Location = new Point(0, 0x90);
        this.pnlAnswer4.Margin = new Padding(0);
        this.pnlAnswer4.Name = "pnlAnswer4";
        this.pnlAnswer4.Size = new Size(0x98, 0x30);
        this.pnlAnswer4.TabIndex = 4;
        this.nmbAnswer4b.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer4b.Location = new Point(0x1c, 0x18);
        this.nmbAnswer4b.Name = "nmbAnswer4b";
        this.nmbAnswer4b.Size = new Size(120, 20);
        this.nmbAnswer4b.TabIndex = 5;
        this.nmbAnswer4b.TextAlign = HorizontalAlignment.Left;
        this.nmbAnswer4a.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer4a.Location = new Point(0x1c, 2);
        this.nmbAnswer4a.Name = "nmbAnswer4a";
        this.nmbAnswer4a.Size = new Size(120, 20);
        this.nmbAnswer4a.TabIndex = 4;
        this.nmbAnswer4a.TextAlign = HorizontalAlignment.Left;
        this.Label23.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label23.Location = new Point(4, 0x18);
        this.Label23.Name = "Label23";
        this.Label23.Size = new Size(20, 20);
        this.Label23.TabIndex = 2;
        this.Label23.Text = "B.";
        this.Label23.TextAlign = ContentAlignment.MiddleRight;
        this.Label24.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label24.Location = new Point(4, 2);
        this.Label24.Name = "Label24";
        this.Label24.Size = new Size(20, 20);
        this.Label24.TabIndex = 0;
        this.Label24.Text = "A.";
        this.Label24.TextAlign = ContentAlignment.MiddleRight;
        this.pnlAnswer3.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer3.Controls.Add(this.Label25);
        this.pnlAnswer3.Controls.Add(this.Label26);
        this.pnlAnswer3.Controls.Add(this.txtAnswer3b);
        this.pnlAnswer3.Controls.Add(this.txtAnswer3a);
        this.pnlAnswer3.Dock = DockStyle.Top;
        this.pnlAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer3.Location = new Point(0, 0x60);
        this.pnlAnswer3.Margin = new Padding(0);
        this.pnlAnswer3.Name = "pnlAnswer3";
        this.pnlAnswer3.Size = new Size(0x98, 0x30);
        this.pnlAnswer3.TabIndex = 3;
        this.Label25.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label25.Location = new Point(4, 0x18);
        this.Label25.Name = "Label25";
        this.Label25.Size = new Size(20, 20);
        this.Label25.TabIndex = 2;
        this.Label25.Text = "B.";
        this.Label25.TextAlign = ContentAlignment.MiddleRight;
        this.Label26.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label26.Location = new Point(4, 2);
        this.Label26.Name = "Label26";
        this.Label26.Size = new Size(20, 20);
        this.Label26.TabIndex = 0;
        this.Label26.Text = "A.";
        this.Label26.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer3b.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer3b.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.txtAnswer3b.Location = new Point(0x1c, 0x18);
        this.txtAnswer3b.Margin = new Padding(0);
        this.txtAnswer3b.Name = "txtAnswer3b";
        this.txtAnswer3b.Size = new Size(120, 20);
        this.txtAnswer3b.TabIndex = 3;
        this.txtAnswer3a.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer3a.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.txtAnswer3a.Location = new Point(0x1c, 2);
        this.txtAnswer3a.Margin = new Padding(0);
        this.txtAnswer3a.Name = "txtAnswer3a";
        this.txtAnswer3a.Size = new Size(120, 20);
        this.txtAnswer3a.TabIndex = 1;
        this.rgAnswer2.BackColor = SystemColors.Control;
        this.rgAnswer2.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer2.Dock = DockStyle.Top;
        this.rgAnswer2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer2.Items = new string[] { "Y", "N" };
        this.rgAnswer2.Location = new Point(0, 0x40);
        this.rgAnswer2.Name = "rgAnswer2";
        this.rgAnswer2.Size = new Size(0x98, 0x20);
        this.rgAnswer2.TabIndex = 2;
        this.rgAnswer2.Value = "";
        this.rgAnswer1.BackColor = SystemColors.Control;
        this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer1.Dock = DockStyle.Top;
        this.rgAnswer1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer1.Items = new string[] { "Y", "N" };
        this.rgAnswer1.Location = new Point(0, 0x20);
        this.rgAnswer1.Name = "rgAnswer1";
        this.rgAnswer1.Size = new Size(0x98, 0x20);
        this.rgAnswer1.TabIndex = 1;
        this.rgAnswer1.Value = "";
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.Controls.Add(this.pnlQuestions);
        base.Controls.Add(this.pnlAnswers);
        base.Name = "Control_DME1003";
        base.Size = new Size(800, 0x198);
        this.pnlQuestions.ResumeLayout(false);
        this.pnlQuestion8.ResumeLayout(false);
        this.pnlAnswers.ResumeLayout(false);
        this.pnlAnswer6.ResumeLayout(false);
        this.pnlAnswer4.ResumeLayout(false);
        this.pnlAnswer3.ResumeLayout(false);
        this.pnlAnswer3.PerformLayout();
        base.ResumeLayout(false);
    }

    public override void LoadFromReader(MySqlDataReader reader)
    {
        Functions.SetRadioGroupValue(this.rgAnswer1, reader["Answer1"]);
        Functions.SetRadioGroupValue(this.rgAnswer2, reader["Answer2"]);
        Functions.SetTextBoxText(this.txtAnswer3a, reader["Answer3a"]);
        Functions.SetTextBoxText(this.txtAnswer3b, reader["Answer3b"]);
        Functions.SetNumericBoxValue(this.nmbAnswer4a, reader["Answer4a"]);
        Functions.SetNumericBoxValue(this.nmbAnswer4b, reader["Answer4b"]);
        Functions.SetRadioGroupValue(this.rgAnswer5, reader["Answer5"]);
        Functions.SetNumericBoxValue(this.nmbAnswer6, reader["Answer6"]);
        Functions.SetRadioGroupValue(this.rgAnswer7, reader["Answer7"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8a, reader["Answer8a"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8b, reader["Answer8b"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8c, reader["Answer8c"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8d, reader["Answer8d"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8e, reader["Answer8e"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8f, reader["Answer8f"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8g, reader["Answer8g"]);
        Functions.SetNumericBoxValue(this.nmbAnswer8h, reader["Answer8h"]);
        Functions.SetRadioGroupValue(this.rgAnswer9, reader["Answer9"]);
    }

    private void nmbAnswer4a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer6_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8b_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8c_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8d_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8e_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8f_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8g_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void nmbAnswer8h_ValueChanged(object sender, EventArgs e)
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

    private void rgAnswer5_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer7_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer9_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    public override void SaveToCommand(MySqlCommand cmd)
    {
        cmd.Parameters.Add("Answer1", MySqlType.VarChar, 5).Value = this.rgAnswer1.Value;
        cmd.Parameters.Add("Answer2", MySqlType.VarChar, 5).Value = this.rgAnswer2.Value;
        cmd.Parameters.Add("Answer3a", MySqlType.VarChar, 15).Value = this.txtAnswer3a.Text;
        cmd.Parameters.Add("Answer3b", MySqlType.VarChar, 15).Value = this.txtAnswer3b.Text;
        cmd.Parameters.Add("Answer4a", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer4a.AsInt32);
        cmd.Parameters.Add("Answer4b", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer4b.AsInt32);
        cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.rgAnswer5.Value;
        cmd.Parameters.Add("Answer6", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer6.AsInt32);
        cmd.Parameters.Add("Answer7", MySqlType.VarChar, 5).Value = this.rgAnswer7.Value;
        cmd.Parameters.Add("Answer8a", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8a.AsInt32);
        cmd.Parameters.Add("Answer8b", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8b.AsInt32);
        cmd.Parameters.Add("Answer8c", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8c.AsInt32);
        cmd.Parameters.Add("Answer8d", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8d.AsInt32);
        cmd.Parameters.Add("Answer8e", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8e.AsInt32);
        cmd.Parameters.Add("Answer8f", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8f.AsInt32);
        cmd.Parameters.Add("Answer8g", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8g.AsInt32);
        cmd.Parameters.Add("Answer8h", MySqlType.Int).Value = NullableConvert.ToDb(this.nmbAnswer8h.AsInt32);
        cmd.Parameters.Add("Answer9", MySqlType.VarChar, 5).Value = this.rgAnswer9.Value;
    }

    private void txtAnswer3a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer3b_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer4b_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    [field: AccessedThroughProperty("rgAnswer9")]
    private RadioGroup rgAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAnswer8")]
    private Label lblAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer6")]
    private NumericBox nmbAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

    [field: AccessedThroughProperty("Label9")]
    private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer7")]
    private RadioGroup rgAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label1")]
    private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label13")]
    private Label Label13 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAnswers")]
    private Label lblAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion6")]
    private Label lblQuestion6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion7")]
    private Label lblQuestion7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestions")]
    private Label lblQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label5")]
    private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion9")]
    private Label lblQuestion9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestions")]
    private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestion8")]
    private Panel pnlQuestion8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label8")]
    private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label14")]
    private Label Label14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label3")]
    private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label4")]
    private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label6")]
    private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswers")]
    private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

    [field: AccessedThroughProperty("lblQuestion2")]
    private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion1")]
    private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer5")]
    private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer4")]
    private Panel pnlAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label23")]
    private Label Label23 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label24")]
    private Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer3")]
    private Panel pnlAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label25")]
    private Label Label25 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label26")]
    private Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer3b")]
    private TextBox txtAnswer3b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer3a")]
    private TextBox txtAnswer3a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer2")]
    private RadioGroup rgAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer1")]
    private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer6")]
    private Panel pnlAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8h")]
    private NumericBox nmbAnswer8h { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8c")]
    private NumericBox nmbAnswer8c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8g")]
    private NumericBox nmbAnswer8g { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8e")]
    private NumericBox nmbAnswer8e { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8b")]
    private NumericBox nmbAnswer8b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8f")]
    private NumericBox nmbAnswer8f { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8d")]
    private NumericBox nmbAnswer8d { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer8a")]
    private NumericBox nmbAnswer8a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer4a")]
    internal virtual NumericBox nmbAnswer4a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer4b")]
    internal virtual NumericBox nmbAnswer4b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public override DmercType Type =>
        DmercType.DME_1003;
}

