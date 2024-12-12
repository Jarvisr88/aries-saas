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

    public class Control_CMN1002A : Control_CMNBase
    {
        private IContainer components;

        public Control_CMN1002A()
        {
            this.InitializeComponent();
        }

        public override void Clear()
        {
            this.set_Item("Answer1", DBNull.Value);
            this.set_Item("Answer3", DBNull.Value);
            this.set_Item("Concentration_AminoAcid", DBNull.Value);
            this.set_Item("Concentration_Dextrose", DBNull.Value);
            this.set_Item("Concentration_Lipids", DBNull.Value);
            this.set_Item("Dose_AminoAcid", DBNull.Value);
            this.set_Item("Dose_Dextrose", DBNull.Value);
            this.set_Item("Dose_Lipids", DBNull.Value);
            this.set_Item("DaysPerWeek_Lipids", DBNull.Value);
            this.set_Item("GmsPerDay_AminoAcid", DBNull.Value);
            this.set_Item("Answer5", DBNull.Value);
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
            this.TextBox4 = new TextBox();
            this.pnlQuestion7 = new Panel();
            this.nmbConcentration_Lipids = new NumericBox();
            this.nmbGmsPerDay_AminoAcid = new NumericBox();
            this.nmbDaysPerWeek_Lipids = new NumericBox();
            this.nmbConcentration_Dextrose = new NumericBox();
            this.nmbConcentration_AminoAcid = new NumericBox();
            this.nmbDose_Lipids = new NumericBox();
            this.nmbDose_Dextrose = new NumericBox();
            this.nmbDose_AminoAcid = new NumericBox();
            this.Label8 = new Label();
            this.Label14 = new Label();
            this.Label3 = new Label();
            this.Label4 = new Label();
            this.Label6 = new Label();
            this.Label5 = new Label();
            this.Label1 = new Label();
            this.Label9 = new Label();
            this.Label10 = new Label();
            this.Label11 = new Label();
            this.Label12 = new Label();
            this.Label13 = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestion0 = new Label();
            this.lblQuestionDescription1 = new Label();
            this.TextBox10 = new TextBox();
            this.pnlAnswers = new Panel();
            this.rgAnswer5 = new RadioGroup();
            this.lblAnswer7 = new Label();
            this.nmbAnswer3 = new NumericBox();
            this.rgAnswer1 = new RadioGroup();
            this.Label2 = new Label();
            this.lblAnswerDescription1 = new Label();
            this.pnlQuestions.SuspendLayout();
            this.pnlQuestion7.SuspendLayout();
            this.pnlAnswers.SuspendLayout();
            base.SuspendLayout();
            this.pnlQuestions.BackColor = Color.Transparent;
            this.pnlQuestions.Controls.Add(this.TextBox4);
            this.pnlQuestions.Controls.Add(this.pnlQuestion7);
            this.pnlQuestions.Controls.Add(this.lblQuestion1);
            this.pnlQuestions.Controls.Add(this.lblQuestion0);
            this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
            this.pnlQuestions.Controls.Add(this.TextBox10);
            this.pnlQuestions.Dock = DockStyle.Fill;
            this.pnlQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlQuestions.Location = new Point(0x70, 0);
            this.pnlQuestions.Name = "pnlQuestions";
            this.pnlQuestions.Size = new Size(0x2b0, 0xd4);
            this.pnlQuestions.TabIndex = 1;
            this.TextBox4.AutoSize = false;
            this.TextBox4.BackColor = SystemColors.Control;
            this.TextBox4.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox4.Dock = DockStyle.Top;
            this.TextBox4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox4.Location = new Point(0, 180);
            this.TextBox4.Multiline = true;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Size(0x2b0, 0x20);
            this.TextBox4.TabIndex = 5;
            this.TextBox4.Text = "5.  Circle the number for the route of administration.         2, 4, 5, 6-Reserved for other or future use.\r\n     1 -Central Line;    3 -Hemodialysis Access Line;    7 -Peripherally Inserted Catheter (PIC)";
            this.pnlQuestion7.BackColor = Color.Transparent;
            this.pnlQuestion7.BorderStyle = BorderStyle.FixedSingle;
            this.pnlQuestion7.Controls.Add(this.nmbConcentration_Lipids);
            this.pnlQuestion7.Controls.Add(this.nmbGmsPerDay_AminoAcid);
            this.pnlQuestion7.Controls.Add(this.nmbDaysPerWeek_Lipids);
            this.pnlQuestion7.Controls.Add(this.nmbConcentration_Dextrose);
            this.pnlQuestion7.Controls.Add(this.nmbConcentration_AminoAcid);
            this.pnlQuestion7.Controls.Add(this.nmbDose_Lipids);
            this.pnlQuestion7.Controls.Add(this.nmbDose_Dextrose);
            this.pnlQuestion7.Controls.Add(this.nmbDose_AminoAcid);
            this.pnlQuestion7.Controls.Add(this.Label8);
            this.pnlQuestion7.Controls.Add(this.Label14);
            this.pnlQuestion7.Controls.Add(this.Label3);
            this.pnlQuestion7.Controls.Add(this.Label4);
            this.pnlQuestion7.Controls.Add(this.Label6);
            this.pnlQuestion7.Controls.Add(this.Label5);
            this.pnlQuestion7.Controls.Add(this.Label1);
            this.pnlQuestion7.Controls.Add(this.Label9);
            this.pnlQuestion7.Controls.Add(this.Label10);
            this.pnlQuestion7.Controls.Add(this.Label11);
            this.pnlQuestion7.Controls.Add(this.Label12);
            this.pnlQuestion7.Controls.Add(this.Label13);
            this.pnlQuestion7.Dock = DockStyle.Top;
            this.pnlQuestion7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlQuestion7.Location = new Point(0, 0x60);
            this.pnlQuestion7.Name = "pnlQuestion7";
            this.pnlQuestion7.Size = new Size(0x2b0, 0x54);
            this.pnlQuestion7.TabIndex = 4;
            this.nmbConcentration_Lipids.BorderStyle = BorderStyle.FixedSingle;
            this.nmbConcentration_Lipids.Location = new Point(0x16c, 60);
            this.nmbConcentration_Lipids.Name = "nmbConcentration_Lipids";
            this.nmbConcentration_Lipids.Size = new Size(0x40, 0x13);
            this.nmbConcentration_Lipids.TabIndex = 0x12;
            this.nmbConcentration_Lipids.TextAlign = HorizontalAlignment.Left;
            this.nmbGmsPerDay_AminoAcid.BorderStyle = BorderStyle.FixedSingle;
            this.nmbGmsPerDay_AminoAcid.Location = new Point(0x16c, 20);
            this.nmbGmsPerDay_AminoAcid.Name = "nmbGmsPerDay_AminoAcid";
            this.nmbGmsPerDay_AminoAcid.Size = new Size(0x40, 0x13);
            this.nmbGmsPerDay_AminoAcid.TabIndex = 6;
            this.nmbGmsPerDay_AminoAcid.TextAlign = HorizontalAlignment.Left;
            this.nmbDaysPerWeek_Lipids.BorderStyle = BorderStyle.FixedSingle;
            this.nmbDaysPerWeek_Lipids.Location = new Point(200, 60);
            this.nmbDaysPerWeek_Lipids.Name = "nmbDaysPerWeek_Lipids";
            this.nmbDaysPerWeek_Lipids.Size = new Size(0x40, 0x13);
            this.nmbDaysPerWeek_Lipids.TabIndex = 0x10;
            this.nmbDaysPerWeek_Lipids.TextAlign = HorizontalAlignment.Left;
            this.nmbConcentration_Dextrose.BorderStyle = BorderStyle.FixedSingle;
            this.nmbConcentration_Dextrose.Location = new Point(200, 40);
            this.nmbConcentration_Dextrose.Name = "nmbConcentration_Dextrose";
            this.nmbConcentration_Dextrose.Size = new Size(0x40, 0x13);
            this.nmbConcentration_Dextrose.TabIndex = 11;
            this.nmbConcentration_Dextrose.TextAlign = HorizontalAlignment.Left;
            this.nmbConcentration_AminoAcid.BorderStyle = BorderStyle.FixedSingle;
            this.nmbConcentration_AminoAcid.Location = new Point(200, 20);
            this.nmbConcentration_AminoAcid.Name = "nmbConcentration_AminoAcid";
            this.nmbConcentration_AminoAcid.Size = new Size(0x40, 0x13);
            this.nmbConcentration_AminoAcid.TabIndex = 4;
            this.nmbConcentration_AminoAcid.TextAlign = HorizontalAlignment.Left;
            this.nmbDose_Lipids.BorderStyle = BorderStyle.FixedSingle;
            this.nmbDose_Lipids.Location = new Point(0x4c, 60);
            this.nmbDose_Lipids.Name = "nmbDose_Lipids";
            this.nmbDose_Lipids.Size = new Size(0x40, 0x13);
            this.nmbDose_Lipids.TabIndex = 14;
            this.nmbDose_Lipids.TextAlign = HorizontalAlignment.Left;
            this.nmbDose_Dextrose.BorderStyle = BorderStyle.FixedSingle;
            this.nmbDose_Dextrose.Location = new Point(0x4c, 40);
            this.nmbDose_Dextrose.Name = "nmbDose_Dextrose";
            this.nmbDose_Dextrose.Size = new Size(0x40, 0x13);
            this.nmbDose_Dextrose.TabIndex = 9;
            this.nmbDose_Dextrose.TextAlign = HorizontalAlignment.Left;
            this.nmbDose_AminoAcid.BorderStyle = BorderStyle.FixedSingle;
            this.nmbDose_AminoAcid.Location = new Point(0x4c, 20);
            this.nmbDose_AminoAcid.Name = "nmbDose_AminoAcid";
            this.nmbDose_AminoAcid.Size = new Size(0x40, 0x13);
            this.nmbDose_AminoAcid.TabIndex = 2;
            this.nmbDose_AminoAcid.TextAlign = HorizontalAlignment.Left;
            this.Label8.BackColor = Color.Transparent;
            this.Label8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label8.Location = new Point(0x1b4, 20);
            this.Label8.Name = "Label8";
            this.Label8.Size = new Size(0x58, 0x13);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "gms protein/day";
            this.Label8.TextAlign = ContentAlignment.MiddleLeft;
            this.Label14.BackColor = Color.Transparent;
            this.Label14.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label14.Location = new Point(0x1b4, 60);
            this.Label14.Name = "Label14";
            this.Label14.Size = new Size(0x58, 0x13);
            this.Label14.TabIndex = 0x13;
            this.Label14.Text = "concentration %";
            this.Label14.TextAlign = ContentAlignment.MiddleLeft;
            this.Label3.BackColor = Color.Transparent;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0x110, 60);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x58, 0x13);
            this.Label3.TabIndex = 0x11;
            this.Label3.Text = "days/week";
            this.Label3.TextAlign = ContentAlignment.MiddleLeft;
            this.Label4.BackColor = Color.Transparent;
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0x110, 20);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x58, 0x13);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "concentration %";
            this.Label4.TextAlign = ContentAlignment.MiddleLeft;
            this.Label6.BackColor = Color.Transparent;
            this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label6.Location = new Point(0x110, 40);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x58, 0x13);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "concentration %";
            this.Label6.TextAlign = ContentAlignment.MiddleLeft;
            this.Label5.BackColor = Color.Transparent;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(0x94, 60);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x30, 0x13);
            this.Label5.TabIndex = 15;
            this.Label5.Text = "(ml/day)";
            this.Label5.TextAlign = ContentAlignment.MiddleLeft;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.Dock = DockStyle.Top;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x2ae, 0x10);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "4.  Formula components:";
            this.Label9.BackColor = Color.Transparent;
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(8, 20);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x40, 0x13);
            this.Label9.TabIndex = 1;
            this.Label9.Text = "Amino Acid";
            this.Label9.TextAlign = ContentAlignment.MiddleLeft;
            this.Label10.BackColor = Color.Transparent;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(8, 60);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(0x40, 0x13);
            this.Label10.TabIndex = 13;
            this.Label10.Text = "Lipids";
            this.Label10.TextAlign = ContentAlignment.MiddleLeft;
            this.Label11.BackColor = Color.Transparent;
            this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label11.Location = new Point(8, 40);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(0x40, 0x13);
            this.Label11.TabIndex = 8;
            this.Label11.Text = "Dextrose";
            this.Label11.TextAlign = ContentAlignment.MiddleLeft;
            this.Label12.BackColor = Color.Transparent;
            this.Label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label12.Location = new Point(0x94, 20);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x30, 0x13);
            this.Label12.TabIndex = 3;
            this.Label12.Text = "(ml/day)";
            this.Label12.TextAlign = ContentAlignment.MiddleLeft;
            this.Label13.BackColor = Color.Transparent;
            this.Label13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label13.Location = new Point(0x94, 40);
            this.Label13.Name = "Label13";
            this.Label13.Size = new Size(0x30, 0x13);
            this.Label13.TabIndex = 10;
            this.Label13.Text = "(ml/day)";
            this.Label13.TextAlign = ContentAlignment.MiddleLeft;
            this.lblQuestion1.BackColor = Color.Transparent;
            this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion1.Dock = DockStyle.Top;
            this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion1.Location = new Point(0, 0x4c);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new Size(0x2b0, 20);
            this.lblQuestion1.TabIndex = 3;
            this.lblQuestion1.Text = "3.  Days per week infused?  (Enter 1-7).";
            this.lblQuestion0.BackColor = Color.Transparent;
            this.lblQuestion0.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestion0.Dock = DockStyle.Top;
            this.lblQuestion0.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestion0.Location = new Point(0, 0x30);
            this.lblQuestion0.Name = "lblQuestion0";
            this.lblQuestion0.Size = new Size(0x2b0, 0x1c);
            this.lblQuestion0.TabIndex = 2;
            this.lblQuestion0.Text = "1.  Does the patient have severe permanent disease of the gastrointestinal tract causing malabsorption severe enough to prevent maintenance of weight and strength commensurate with the patient's overall health status?";
            this.lblQuestionDescription1.BackColor = Color.Transparent;
            this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuestionDescription1.Dock = DockStyle.Top;
            this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblQuestionDescription1.Location = new Point(0, 0x20);
            this.lblQuestionDescription1.Name = "lblQuestionDescription1";
            this.lblQuestionDescription1.Size = new Size(0x2b0, 0x10);
            this.lblQuestionDescription1.TabIndex = 1;
            this.lblQuestionDescription1.Text = "QUESTION 2 RESERVED FOR OTHER OR FUTURE USE.";
            this.TextBox10.AutoSize = false;
            this.TextBox10.BackColor = SystemColors.Control;
            this.TextBox10.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox10.Dock = DockStyle.Top;
            this.TextBox10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.TextBox10.Location = new Point(0, 0);
            this.TextBox10.Multiline = true;
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Size(0x2b0, 0x20);
            this.TextBox10.TabIndex = 0;
            this.TextBox10.Text = "ANSWER QUESTIONS 1, AND 3 - 5 FOR PARENTERAL NUTRITION\r\n(Circle Y for Yes, N for No, or D for Does Not Apply, Unless Otherwise Noted)";
            this.pnlAnswers.BackColor = Color.Transparent;
            this.pnlAnswers.Controls.Add(this.rgAnswer5);
            this.pnlAnswers.Controls.Add(this.lblAnswer7);
            this.pnlAnswers.Controls.Add(this.nmbAnswer3);
            this.pnlAnswers.Controls.Add(this.rgAnswer1);
            this.pnlAnswers.Controls.Add(this.Label2);
            this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
            this.pnlAnswers.Dock = DockStyle.Left;
            this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.pnlAnswers.Location = new Point(0, 0);
            this.pnlAnswers.Name = "pnlAnswers";
            this.pnlAnswers.Size = new Size(0x70, 0xd4);
            this.pnlAnswers.TabIndex = 0;
            this.rgAnswer5.BackColor = SystemColors.Control;
            this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer5.Dock = DockStyle.Top;
            this.rgAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer5.Items = new string[] { "1", "3", "7" };
            this.rgAnswer5.Location = new Point(0, 180);
            this.rgAnswer5.Name = "rgAnswer5";
            this.rgAnswer5.Size = new Size(0x70, 0x20);
            this.rgAnswer5.TabIndex = 5;
            this.rgAnswer5.Value = "";
            this.lblAnswer7.BackColor = Color.Transparent;
            this.lblAnswer7.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswer7.Dock = DockStyle.Top;
            this.lblAnswer7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswer7.Location = new Point(0, 0x5f);
            this.lblAnswer7.Name = "lblAnswer7";
            this.lblAnswer7.Size = new Size(0x70, 0x55);
            this.lblAnswer7.TabIndex = 4;
            this.nmbAnswer3.BorderStyle = BorderStyle.FixedSingle;
            this.nmbAnswer3.Dock = DockStyle.Top;
            this.nmbAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.nmbAnswer3.Location = new Point(0, 0x4c);
            this.nmbAnswer3.Name = "nmbAnswer3";
            this.nmbAnswer3.Size = new Size(0x70, 0x13);
            this.nmbAnswer3.TabIndex = 3;
            this.nmbAnswer3.TextAlign = HorizontalAlignment.Left;
            this.rgAnswer1.BackColor = SystemColors.Control;
            this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
            this.rgAnswer1.Dock = DockStyle.Top;
            this.rgAnswer1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.rgAnswer1.Items = new string[] { "Y", "N", "D" };
            this.rgAnswer1.Location = new Point(0, 0x30);
            this.rgAnswer1.Name = "rgAnswer1";
            this.rgAnswer1.Size = new Size(0x70, 0x1c);
            this.rgAnswer1.TabIndex = 2;
            this.rgAnswer1.Value = "";
            this.Label2.BackColor = Color.Transparent;
            this.Label2.BorderStyle = BorderStyle.FixedSingle;
            this.Label2.Dock = DockStyle.Top;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0, 0x20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x70, 0x10);
            this.Label2.TabIndex = 1;
            this.Label2.TextAlign = ContentAlignment.TopCenter;
            this.lblAnswerDescription1.BackColor = Color.Transparent;
            this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnswerDescription1.Dock = DockStyle.Top;
            this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblAnswerDescription1.Location = new Point(0, 0);
            this.lblAnswerDescription1.Name = "lblAnswerDescription1";
            this.lblAnswerDescription1.Size = new Size(0x70, 0x20);
            this.lblAnswerDescription1.TabIndex = 0;
            this.lblAnswerDescription1.Text = "ANSWERS";
            this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
            base.Controls.Add(this.pnlQuestions);
            base.Controls.Add(this.pnlAnswers);
            base.Name = "Control_CMN1002A";
            base.Size = new Size(800, 0xd4);
            this.pnlQuestions.ResumeLayout(false);
            this.pnlQuestion7.ResumeLayout(false);
            this.pnlAnswers.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadFromReader(MySqlDataReader reader)
        {
            this.set_Item("Answer1", reader["Answer1"]);
            this.set_Item("Answer3", reader["Answer3"]);
            this.set_Item("Concentration_AminoAcid", reader["Concentration_AminoAcid"]);
            this.set_Item("Concentration_Dextrose", reader["Concentration_Dextrose"]);
            this.set_Item("Concentration_Lipids", reader["Concentration_Lipids"]);
            this.set_Item("Dose_AminoAcid", reader["Dose_AminoAcid"]);
            this.set_Item("Dose_Dextrose", reader["Dose_Dextrose"]);
            this.set_Item("Dose_Lipids", reader["Dose_Lipids"]);
            this.set_Item("DaysPerWeek_Lipids", reader["DaysPerWeek_Lipids"]);
            this.set_Item("DaysPerWeek_Lipids", reader["DaysPerWeek_Lipids"]);
            this.set_Item("GmsPerDay_AminoAcid", reader["GmsPerDay_AminoAcid"]);
            this.set_Item("Answer5", reader["Answer5"]);
        }

        private void nmbAnswer3_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbConcentration_AminoAcid_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbConcentration_Dextrose_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbConcentration_Lipids_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbDaysPerWeek_Lipids_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbDose_AminoAcid_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbDose_Dextrose_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbDose_Lipids_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void nmbGmsPerDay_AminoAcid_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer1_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void rgAnswer5_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        public override void SaveToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 5).Value = this.get_Item("Answer1");
            cmd.Parameters.Add("Answer3", MySqlType.Int).Value = this.get_Item("Answer3");
            cmd.Parameters.Add("Concentration_AminoAcid", MySqlType.Double).Value = this.get_Item("Concentration_AminoAcid");
            cmd.Parameters.Add("Concentration_Dextrose", MySqlType.Double).Value = this.get_Item("Concentration_Dextrose");
            cmd.Parameters.Add("Concentration_Lipids", MySqlType.Double).Value = this.get_Item("Concentration_Lipids");
            cmd.Parameters.Add("Dose_AminoAcid", MySqlType.Double).Value = this.get_Item("Dose_AminoAcid");
            cmd.Parameters.Add("Dose_Dextrose", MySqlType.Double).Value = this.get_Item("Dose_Dextrose");
            cmd.Parameters.Add("Dose_Lipids", MySqlType.Double).Value = this.get_Item("Dose_Lipids");
            cmd.Parameters.Add("DaysPerWeek_Lipids", MySqlType.Double).Value = this.get_Item("DaysPerWeek_Lipids");
            cmd.Parameters.Add("GmsPerDay_AminoAcid", MySqlType.Double).Value = this.get_Item("GmsPerDay_AminoAcid");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.get_Item("Answer5");
        }

        [field: AccessedThroughProperty("pnlQuestions")]
        private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlQuestion7")]
        private Panel pnlQuestion7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("lblQuestion1")]
        private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestion0")]
        private Label lblQuestion0 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuestionDescription1")]
        private Label lblQuestionDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAnswers")]
        private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswer7")]
        private Label lblAnswer7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAnswerDescription1")]
        private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox10")]
        private TextBox TextBox10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("Label8")]
        private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label14")]
        private Label Label14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox4")]
        private TextBox TextBox4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer5")]
        private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rgAnswer1")]
        private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDose_AminoAcid")]
        internal virtual NumericBox nmbDose_AminoAcid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDose_Dextrose")]
        internal virtual NumericBox nmbDose_Dextrose { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDose_Lipids")]
        internal virtual NumericBox nmbDose_Lipids { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDaysPerWeek_Lipids")]
        internal virtual NumericBox nmbDaysPerWeek_Lipids { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbConcentration_Dextrose")]
        internal virtual NumericBox nmbConcentration_Dextrose { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbConcentration_AminoAcid")]
        internal virtual NumericBox nmbConcentration_AminoAcid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbConcentration_Lipids")]
        internal virtual NumericBox nmbConcentration_Lipids { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbGmsPerDay_AminoAcid")]
        internal virtual NumericBox nmbGmsPerDay_AminoAcid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAnswer3")]
        private NumericBox nmbAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override DmercType Type =>
            DmercType.DMERC_1002A;

        // Warning: Properties with arguments are not supported in C#. Getter of a Item property was decompiled as a method.
        protected object get_Item(string Index)
        {
            object valueOrDefault;
            if (string.Compare(Index, "Answer1", true) == 0)
            {
                valueOrDefault = this.rgAnswer1.Value;
            }
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                valueOrDefault = this.nmbAnswer3.AsInt32.GetValueOrDefault(0);
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                valueOrDefault = this.rgAnswer5.Value;
            }
            else if (string.Compare(Index, "Concentration_AminoAcid", true) == 0)
            {
                valueOrDefault = this.nmbConcentration_AminoAcid.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Concentration_Dextrose", true) == 0)
            {
                valueOrDefault = this.nmbConcentration_Dextrose.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Concentration_Lipids", true) == 0)
            {
                valueOrDefault = this.nmbConcentration_Lipids.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Dose_AminoAcid", true) == 0)
            {
                valueOrDefault = this.nmbDose_AminoAcid.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Dose_Dextrose", true) == 0)
            {
                valueOrDefault = this.nmbDose_Dextrose.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "Dose_Lipids", true) == 0)
            {
                valueOrDefault = this.nmbDose_Lipids.AsDouble.GetValueOrDefault(0.0);
            }
            else if (string.Compare(Index, "DaysPerWeek_Lipids", true) == 0)
            {
                valueOrDefault = this.nmbDaysPerWeek_Lipids.AsDouble.GetValueOrDefault(0.0);
            }
            else
            {
                if (string.Compare(Index, "GmsPerDay_AminoAcid", true) != 0)
                {
                    throw new ArgumentOutOfRangeException("Index", "");
                }
                valueOrDefault = this.nmbGmsPerDay_AminoAcid.AsDouble.GetValueOrDefault(0.0);
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
            else if (string.Compare(Index, "Answer3", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbAnswer3, Value);
            }
            else if (string.Compare(Index, "Answer5", true) == 0)
            {
                Functions.SetRadioGroupValue(this.rgAnswer5, Value);
            }
            else if (string.Compare(Index, "Concentration_AminoAcid", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbConcentration_AminoAcid, Value);
            }
            else if (string.Compare(Index, "Concentration_Dextrose", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbConcentration_Dextrose, Value);
            }
            else if (string.Compare(Index, "Concentration_Lipids", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbConcentration_Lipids, Value);
            }
            else if (string.Compare(Index, "Dose_AminoAcid", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbDose_AminoAcid, Value);
            }
            else if (string.Compare(Index, "Dose_Dextrose", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbDose_Dextrose, Value);
            }
            else if (string.Compare(Index, "Dose_Lipids", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbDose_Lipids, Value);
            }
            else if (string.Compare(Index, "DaysPerWeek_Lipids", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbDaysPerWeek_Lipids, Value);
            }
            else if (string.Compare(Index, "GmsPerDay_AminoAcid", true) == 0)
            {
                Functions.SetNumericBoxValue(this.nmbGmsPerDay_AminoAcid, Value);
            }
        }

    }
}

