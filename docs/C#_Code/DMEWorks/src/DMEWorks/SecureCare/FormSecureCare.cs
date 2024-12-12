namespace DMEWorks.SecureCare
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormSecureCare : DmeForm
    {
        private IContainer components;
        private static readonly CultureInfo _eng = new CultureInfo("en-US", false);

        public FormSecureCare()
        {
            this.InitializeComponent();
            this.MySqlConnection.ConnectionString = ClassGlobalObjects.ConnectionString_MySql;
            this.tbbDump.Visible = false;
            this.InitializeGrid(this.Grid.Appearance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private static string dmerc2hcfa(string value) => 
            (string.Compare(value, "DMERC 01.02A", true) != 0) ? ((string.Compare(value, "DMERC 01.02B", true) != 0) ? ((string.Compare(value, "DMERC 02.03A", true) != 0) ? ((string.Compare(value, "DMERC 02.03B", true) != 0) ? ((string.Compare(value, "DMERC 03.02", true) != 0) ? ((string.Compare(value, "DMERC 04.03B", true) != 0) ? ((string.Compare(value, "DMERC 04.03C", true) != 0) ? ((string.Compare(value, "DMERC 06.02B", true) != 0) ? ((string.Compare(value, "DMERC 07.02A", true) != 0) ? ((string.Compare(value, "DMERC 07.02B", true) != 0) ? ((string.Compare(value, "DMERC 09.02", true) != 0) ? ((string.Compare(value, "DMERC 10.02A", true) != 0) ? ((string.Compare(value, "DMERC 10.02B", true) != 0) ? ((string.Compare(value, "DMERC 484.2", true) != 0) ? "" : "HCFA484") : "HCFA853") : "HCFA852") : "HCFA851") : "HCFA850") : "HCFA849") : "HCFA848") : "HCFA847") : "HCFA846") : "HCFA845") : "HCFA844") : "HCFA843") : "HCFA842") : "HCFA841";

        private void DumpDataset()
        {
            this.DatasetExport.AcceptChanges();
            if (this.SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DumpDataset(this.DatasetExport, this.SaveFileDialog.FileName);
            }
        }

        private static void DumpDataset(DatasetSecureCareExport2 dataset, string FileName)
        {
            dataset.WriteXml(FileName, XmlWriteMode.IgnoreSchema);
        }

        private void ExportDataset()
        {
            this.DatasetExport.AcceptChanges();
            if (this.SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportDataset(this.DatasetExport, this.SaveFileDialog.FileName);
            }
        }

        private static void ExportDataset(DatasetSecureCareExport2 dataset, string FileName)
        {
            DatasetSecureCareExport2.CMNFormDataTable cMNForm = dataset.CMNForm;
            DatasetSecureCareExport2.DetailsDataTable details = dataset.Details;
            XmlTextWriter writer = new XmlTextWriter(FileName, Encoding.UTF8);
            try
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("CMNs");
                DataRow[] rowArray = cMNForm.Select("([Checked] = true)", "CMNForm_ID", DataViewRowState.ModifiedOriginal | DataViewRowState.CurrentRows);
                int upperBound = rowArray.GetUpperBound(0);
                int lowerBound = rowArray.GetLowerBound(0);
                while (true)
                {
                    if (lowerBound > upperBound)
                    {
                        writer.WriteEndElement();
                        break;
                    }
                    DataRow row = rowArray[lowerBound];
                    writer.WriteStartElement("CMN");
                    writer.WriteAttributeString("Version", "1.000");
                    writer.WriteAttributeString("Form", ToXmlString(row[cMNForm.CMNForm_HCFATypeColumn]));
                    writer.WriteAttributeString("DocumentKey", ToXmlString(row[cMNForm.CMNForm_IDColumn]));
                    writer.WriteStartElement("CoverPage");
                    writer.WriteElementString("Message", "TODO");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Section_A");
                    writer.WriteStartElement("Certification-Type-Date");
                    writer.WriteElementString("Initial", ToXmlString(row[cMNForm.CMNForm_InitialDateColumn]));
                    writer.WriteElementString("Revised", ToXmlString(row[cMNForm.CMNForm_RevisedDateColumn]));
                    writer.WriteElementString("Recertification", ToXmlString(row[cMNForm.CMNForm_RecertificationDateColumn]));
                    writer.WriteEndElement();
                    writer.WriteStartElement("Patient");
                    writer.WriteAttributeString("SSN", ToXmlString(row[cMNForm.Customer_SSNColumn]));
                    writer.WriteElementString("HIC-Number", ToXmlString(row[cMNForm.Customer_HIC_NumberColumn]));
                    writer.WriteStartElement("Name");
                    writer.WriteElementString("Whole_Name", ToXmlString(row[cMNForm.Customer_WholeNameColumn]));
                    writer.WriteElementString("First", ToXmlString(row[cMNForm.Customer_FirstNameColumn]));
                    writer.WriteElementString("Middle", ToXmlString(row[cMNForm.Customer_MiddleNameColumn]));
                    writer.WriteElementString("Last", ToXmlString(row[cMNForm.Customer_LastNameColumn]));
                    writer.WriteEndElement();
                    writer.WriteStartElement("Address");
                    writer.WriteElementString("Street", ToXmlString(row[cMNForm.Customer_Address1Column]));
                    writer.WriteElementString("City", ToXmlString(row[cMNForm.Customer_CityColumn]));
                    writer.WriteElementString("State", ToXmlString(row[cMNForm.Customer_StateColumn]));
                    writer.WriteElementString("Zip", ToXmlString(row[cMNForm.Customer_ZipColumn]));
                    writer.WriteEndElement();
                    writer.WriteElementString("Telephone", ToXmlString(row[cMNForm.Customer_PhoneColumn]));
                    writer.WriteElementString("DOB", ToXmlString(row[cMNForm.Customer_DOBColumn]));
                    writer.WriteElementString("Gender", ToXmlString(row[cMNForm.Customer_GenderColumn]));
                    writer.WriteElementString("Height", ToXmlString(row[cMNForm.Customer_HeightColumn]));
                    writer.WriteElementString("Weight", ToXmlString(row[cMNForm.Customer_WeightColumn]));
                    writer.WriteEndElement();
                    writer.WriteStartElement("Supplier");
                    writer.WriteElementString("Name", ToXmlString(row[cMNForm.Company_NameColumn]));
                    writer.WriteStartElement("Address");
                    writer.WriteElementString("Street", ToXmlString(row[cMNForm.Company_Address1Column]));
                    writer.WriteElementString("City", ToXmlString(row[cMNForm.Company_CityColumn]));
                    writer.WriteElementString("State", ToXmlString(row[cMNForm.Company_StateColumn]));
                    writer.WriteElementString("Zip", ToXmlString(row[cMNForm.Company_ZipColumn]));
                    writer.WriteEndElement();
                    writer.WriteElementString("Telephone", ToXmlString(row[cMNForm.Company_PhoneColumn]));
                    writer.WriteElementString("NSC", ToXmlString(row[cMNForm.Company_NSCColumn]));
                    writer.WriteEndElement();
                    if (row.IsNull(cMNForm.Facility_IDColumn))
                    {
                        writer.WriteStartElement("Place-of-Service");
                        writer.WriteElementString("Code", "12");
                        writer.WriteElementString("Name", "");
                        writer.WriteStartElement("Address");
                        writer.WriteElementString("Street", "");
                        writer.WriteElementString("City", "");
                        writer.WriteElementString("State", "");
                        writer.WriteElementString("Zip", "");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    else
                    {
                        writer.WriteStartElement("Place-of-Service");
                        writer.WriteElementString("Code", ToXmlString(row[cMNForm.Facility_CodeColumn]));
                        writer.WriteElementString("Name", ToXmlString(row[cMNForm.Facility_NameColumn]));
                        writer.WriteStartElement("Address");
                        writer.WriteElementString("Street", ToXmlString(row[cMNForm.Facility_Address1Column]));
                        writer.WriteElementString("City", ToXmlString(row[cMNForm.Facility_CityColumn]));
                        writer.WriteElementString("State", ToXmlString(row[cMNForm.Facility_StateColumn]));
                        writer.WriteElementString("Zip", ToXmlString(row[cMNForm.Facility_ZipColumn]));
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteStartElement("Physician");
                    writer.WriteElementString("UPIN", ToXmlString(row[cMNForm.Doctor_UPINColumn]));
                    writer.WriteStartElement("Name");
                    writer.WriteElementString("Whole_Name", ToXmlString(row[cMNForm.Doctor_WholeNameColumn]));
                    writer.WriteElementString("First", ToXmlString(row[cMNForm.Doctor_FirstNameColumn]));
                    writer.WriteElementString("Middle", ToXmlString(row[cMNForm.Doctor_MiddleNameColumn]));
                    writer.WriteElementString("Last", ToXmlString(row[cMNForm.Doctor_LastNameColumn]));
                    writer.WriteEndElement();
                    writer.WriteStartElement("Address");
                    writer.WriteElementString("Street", ToXmlString(row[cMNForm.Doctor_Address1Column]));
                    writer.WriteElementString("City", ToXmlString(row[cMNForm.Doctor_CityColumn]));
                    writer.WriteElementString("State", ToXmlString(row[cMNForm.Doctor_StateColumn]));
                    writer.WriteElementString("Zip", ToXmlString(row[cMNForm.Customer_ZipColumn]));
                    writer.WriteEndElement();
                    writer.WriteElementString("Telephone", ToXmlString(row[cMNForm.Doctor_PhoneColumn]));
                    writer.WriteEndElement();
                    DataRow[] rowArray2 = details.Select($"CMNFormID = {row[cMNForm.CMNForm_IDColumn]}");
                    writer.WriteStartElement("HCPCS-Code");
                    int num3 = rowArray2.GetUpperBound(0);
                    int index = rowArray2.GetLowerBound(0);
                    while (true)
                    {
                        if (index > num3)
                        {
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteStartElement("Section_C");
                            writer.WriteElementString("Narrative", GenerateNarrative(details, row[cMNForm.CMNForm_IDColumn]));
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                            lowerBound++;
                            break;
                        }
                        DataRow row2 = rowArray2[index];
                        writer.WriteElementString("Code", ToXmlString(row2[details.BillingCodeColumn]));
                        index++;
                    }
                }
            }
            finally
            {
                writer.Close();
            }
        }

        private static string GenerateNarrative(DatasetSecureCareExport2.DetailsDataTable details, object CMNFormID)
        {
            string str;
            DataRow[] rowArray = details.Select($"CMNFormID = {CMNFormID}");
            string[,] strArray = new string[rowArray.Length + 1, 5];
            strArray[0, 0] = "HCPCS";
            strArray[0, 1] = "Item Description";
            strArray[0, 2] = "Qty";
            strArray[0, 3] = "Supplier Charges";
            strArray[0, 4] = "Medicare Allowable";
            int num = rowArray.Length - 1;
            for (int i = 0; i <= num; i++)
            {
                DataRow row = rowArray[i];
                strArray[i + 1, 0] = Conversions.ToString(row[details.BillingCodeColumn]);
                strArray[i + 1, 1] = Conversions.ToString(row[details.ItemDescriptionColumn]);
                if (!(row[details.QuantityColumn] is DBNull))
                {
                    strArray[i + 1, 2] = Conversions.ToString(row[details.QuantityColumn]);
                }
                strArray[i + 1, 3] = "$" + row[details.BillablePriceColumn] + "/" + row[details.UnitsColumn];
                strArray[i + 1, 4] = "$" + row[details.AllowablePriceColumn] + "/" + row[details.UnitsColumn];
            }
            int[] numArray = new int[strArray.GetUpperBound(1) + 1];
            int upperBound = strArray.GetUpperBound(1);
            for (int j = strArray.GetLowerBound(1); j <= upperBound; j++)
            {
                numArray[j] = strArray[0, j].Length;
            }
            int num5 = strArray.GetUpperBound(0);
            int num6 = strArray.GetLowerBound(0) + 1;
            while (num6 <= num5)
            {
                int num7 = strArray.GetUpperBound(1);
                int lowerBound = strArray.GetLowerBound(1);
                while (true)
                {
                    if (lowerBound > num7)
                    {
                        num6++;
                        break;
                    }
                    numArray[lowerBound] = Math.Max(strArray[num6, lowerBound].Length, numArray[lowerBound]);
                    lowerBound++;
                }
            }
            StringWriter writer = new StringWriter();
            try
            {
                writer.NewLine = "\r\n";
                int num9 = strArray.GetUpperBound(0);
                int lowerBound = strArray.GetLowerBound(0);
                while (true)
                {
                    if (lowerBound > num9)
                    {
                        str = writer.ToString();
                        break;
                    }
                    int num11 = strArray.GetUpperBound(1);
                    int index = strArray.GetLowerBound(1);
                    while (true)
                    {
                        if (index > num11)
                        {
                            writer.WriteLine();
                            lowerBound++;
                            break;
                        }
                        string str2 = strArray[lowerBound, index];
                        writer.Write(str2);
                        writer.Write(new string(' ', (numArray[index] + 1) - str2.Length));
                        index++;
                    }
                }
            }
            finally
            {
                writer.Close();
            }
            return str;
        }

        private static string GetEnumAnswer(XmlNode node, string Question)
        {
            XmlNode node2 = node.SelectSingleNode($"Answer[@Number='{Question}']/SubAnswer[.='1']/@Id");
            return ((node2 != null) ? node2.Value : "");
        }

        private static string GetSubAnswer(XmlNode node, string Question) => 
            GetSubAnswer(node, Question, 0);

        private static string GetSubAnswer(XmlNode node, string Question, int Index)
        {
            XmlNodeList list = node.SelectNodes($"Answer[@Number='{Question}']/SubAnswer");
            return (((0 > Index) || (Index >= list.Count)) ? "" : list[Index].InnerText);
        }

        private static string GetSubAnswer(XmlNode node, string Question, string SubQuestion)
        {
            XmlNode node2 = node.SelectSingleNode($"Answer[@Number='{Question}']/SubAnswer[@Id='{SubQuestion}']");
            return ((node2 != null) ? node2.InnerText : "");
        }

        private static string GetWholeName(object FirstName, object MiddleName, object LastName) => 
            GetWholeName(ToXmlString(FirstName), ToXmlString(MiddleName), ToXmlString(LastName));

        private static string GetWholeName(string FirstName, string MiddleName, string LastName)
        {
            StringBuilder builder = new StringBuilder();
            if ((FirstName != null) && (FirstName.Trim() != ""))
            {
                builder.Append(FirstName.Trim());
            }
            if ((MiddleName != null) && (MiddleName.Trim() != ""))
            {
                if (0 < builder.Length)
                {
                    builder.Append(' ');
                }
                builder.Append(MiddleName.Trim());
                builder.Append('.');
            }
            if ((LastName != null) && (LastName.Trim() != ""))
            {
                if (0 < builder.Length)
                {
                    builder.Append(' ');
                }
                builder.Append(LastName.Trim());
            }
            return builder.ToString();
        }

        private static string GetYNDAnswer(XmlNode node, string Question)
        {
            IEnumerator enumerator;
            XmlNodeList list = node.SelectNodes($"Answer[@Number='{Question}']/SubAnswer");
            try
            {
                enumerator = list.GetEnumerator();
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    XmlNode current = (XmlNode) enumerator.Current;
                    if (node.InnerText == "1")
                    {
                        XmlNode node2 = node.Attributes["Id"];
                        if (node2 != null)
                        {
                            string str;
                            if (string.Compare(node2.Value, "Yes", true) == 0)
                            {
                                str = "Y";
                            }
                            else if (string.Compare(node2.Value, "No", true) == 0)
                            {
                                str = "N";
                            }
                            else
                            {
                                if (string.Compare(node2.Value, "DNA", true) != 0)
                                {
                                    continue;
                                }
                                str = "D";
                            }
                            return str;
                        }
                    }
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            return "";
        }

        private static string hcfa2dmerc(string value) => 
            (string.Compare(value, "HCFA841", true) != 0) ? ((string.Compare(value, "HCFA842", true) != 0) ? ((string.Compare(value, "HCFA843", true) != 0) ? ((string.Compare(value, "HCFA844", true) != 0) ? ((string.Compare(value, "HCFA845", true) != 0) ? ((string.Compare(value, "HCFA846", true) != 0) ? ((string.Compare(value, "HCFA847", true) != 0) ? ((string.Compare(value, "HCFA848", true) != 0) ? ((string.Compare(value, "HCFA849", true) != 0) ? ((string.Compare(value, "HCFA850", true) != 0) ? ((string.Compare(value, "HCFA851", true) != 0) ? ((string.Compare(value, "HCFA852", true) != 0) ? ((string.Compare(value, "HCFA853", true) != 0) ? ((string.Compare(value, "HCFA484", true) != 0) ? "" : "DMERC 484.2") : "DMERC 10.02B") : "DMERC 10.02A") : "DMERC 09.02") : "DMERC 07.02B") : "DMERC 07.02A") : "DMERC 06.02B") : "DMERC 04.03C") : "DMERC 04.03B") : "DMERC 03.02") : "DMERC 02.03B") : "DMERC 02.03A") : "DMERC 01.02B") : "DMERC 01.02A";

        private static void ImportAnswers(Devart.Data.MySql.MySqlConnection cnn, MySqlTransaction tran, int ID, string CMNType, XmlNode node)
        {
            using (MySqlCommand command = new MySqlCommand("", cnn, tran))
            {
                string str;
                if (string.Compare(CMNType, "DMERC 01.02A", true) == 0)
                {
                    PrepareCommand_0102a(command, node);
                    str = "tbl_cmnform_0102a";
                }
                else if (string.Compare(CMNType, "DMERC 01.02B", true) == 0)
                {
                    PrepareCommand_0102b(command, node);
                    str = "tbl_cmnform_0102b";
                }
                else if (string.Compare(CMNType, "DMERC 02.03A", true) == 0)
                {
                    PrepareCommand_0203a(command, node);
                    str = "tbl_cmnform_0203a";
                }
                else if (string.Compare(CMNType, "DMERC 02.03B", true) == 0)
                {
                    PrepareCommand_0203b(command, node);
                    str = "tbl_cmnform_0203b";
                }
                else if (string.Compare(CMNType, "DMERC 03.02", true) == 0)
                {
                    PrepareCommand_0302(command, node);
                    str = "tbl_cmnform_0302";
                }
                else if (string.Compare(CMNType, "DMERC 04.03B", true) == 0)
                {
                    PrepareCommand_0403b(command, node);
                    str = "tbl_cmnform_0403b";
                }
                else if (string.Compare(CMNType, "DMERC 04.03C", true) == 0)
                {
                    PrepareCommand_0403c(command, node);
                    str = "tbl_cmnform_0403c";
                }
                else if (string.Compare(CMNType, "DMERC 06.02B", true) == 0)
                {
                    PrepareCommand_0602b(command, node);
                    str = "tbl_cmnform_0602b";
                }
                else if (string.Compare(CMNType, "DMERC 07.02A", true) == 0)
                {
                    PrepareCommand_0702a(command, node);
                    str = "tbl_cmnform_0702a";
                }
                else if (string.Compare(CMNType, "DMERC 07.02B", true) == 0)
                {
                    PrepareCommand_0702b(command, node);
                    str = "tbl_cmnform_0702b";
                }
                else if (string.Compare(CMNType, "DMERC 09.02", true) == 0)
                {
                    PrepareCommand_0902(command, node);
                    str = "tbl_cmnform_0902";
                }
                else if (string.Compare(CMNType, "DMERC 10.02A", true) == 0)
                {
                    PrepareCommand_1002a(command, node);
                    str = "tbl_cmnform_1002a";
                }
                else if (string.Compare(CMNType, "DMERC 10.02B", true) == 0)
                {
                    PrepareCommand_1002b(command, node);
                    str = "tbl_cmnform_1002b";
                }
                else
                {
                    if (string.Compare(CMNType, "DMERC 484.2", true) != 0)
                    {
                        throw new Exception("Unsupported form");
                    }
                    PrepareCommand_4842(command, node);
                    str = "tbl_cmnform_4842";
                }
                command.Parameters.Add("CMNFormID", MySqlType.Int, 4, "ID").Value = ID;
                try
                {
                    string[] whereParameters = new string[] { "CMNFormID" };
                    if ((command.ExecuteUpdate(str, whereParameters) == 0) && (command.ExecuteInsert(str) == 0))
                    {
                        throw new Exception("");
                    }
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    TraceHelper.TraceInfo("Exception when working with " + CMNType);
                    throw;
                }
            }
        }

        private static void ImportCMNForm(Devart.Data.MySql.MySqlConnection cnn, MySqlTransaction tran, XmlNode node)
        {
            XmlNode node2 = node.SelectSingleNode("Section_A/Certification-Type-Date");
            XmlElement element1 = node["Section_A"];
            XmlNode node3 = node["Section_B"];
            XmlNodeList list = node3.SelectNodes("Diagnoses/Diagnosis");
            XmlNode node4 = node3["Answerer"];
            XmlNode node5 = node3["Answers"];
            int iD = int.Parse(node.Attributes["DocumentKey"].Value);
            string cMNType = hcfa2dmerc(node.Attributes["Form"].Value);
            using (MySqlCommand command = new MySqlCommand("", cnn, tran))
            {
                command.Parameters.Add("Customer_ICD9_1", MySqlType.VarChar, 6, "Customer_ICD9_1").Value = DBNull.Value;
                command.Parameters.Add("Customer_ICD9_2", MySqlType.VarChar, 6, "Customer_ICD9_2").Value = DBNull.Value;
                command.Parameters.Add("Customer_ICD9_3", MySqlType.VarChar, 6, "Customer_ICD9_3").Value = DBNull.Value;
                command.Parameters.Add("Customer_ICD9_4", MySqlType.VarChar, 6, "Customer_ICD9_4").Value = DBNull.Value;
                command.Parameters.Add("InitialDate", MySqlType.Date, 0, "InitialDate").Value = DBNull.Value;
                command.Parameters.Add("RevisedDate", MySqlType.Date, 0, "RevisedDate").Value = DBNull.Value;
                command.Parameters.Add("RecertificationDate", MySqlType.Date, 0, "RecertificationDate").Value = DBNull.Value;
                command.Parameters.Add("EstimatedLengthOfNeed", MySqlType.Int, 0, "EstimatedLengthOfNeed").Value = 0;
                command.Parameters.Add("AnsweringName", MySqlType.VarChar, 50, "AnsweringName").Value = "";
                command.Parameters.Add("AnsweringTitle", MySqlType.VarChar, 50, "AnsweringTitle").Value = "";
                command.Parameters.Add("AnsweringEmployer", MySqlType.VarChar, 50, "AnsweringEmployer").Value = "";
                command.Parameters.Add("Signature_Date", MySqlType.Date, 0, "Signature_Date").Value = DBNull.Value;
                try
                {
                    command.Parameters["EstimatedLengthOfNeed"].Value = int.Parse(node3["Length_of_Need"].InnerText);
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
                goto TR_0027;
            TR_0009:
                try
                {
                    command.Parameters["AnsweringEmployer"].Value = node4["Employer"].InnerText;
                }
                catch (Exception exception12)
                {
                    ProjectData.SetProjectError(exception12);
                    ProjectData.ClearProjectError();
                }
                command.Parameters.Add("ID", MySqlType.Int, 4, "ID").Value = iD;
                command.Parameters.Add("CMNType", MySqlType.VarChar, 50, "CMNType").Value = cMNType;
                string[] whereParameters = new string[] { "ID", "CMNFormID" };
                if (command.ExecuteUpdate("tbl_cmnform", whereParameters) == 1)
                {
                    ImportAnswers(cnn, tran, iD, cMNType, node5);
                }
                return;
            TR_000C:
                try
                {
                    command.Parameters["AnsweringTitle"].Value = node4["Title"].InnerText;
                }
                catch (Exception exception11)
                {
                    ProjectData.SetProjectError(exception11);
                    ProjectData.ClearProjectError();
                }
                goto TR_0009;
            TR_000F:
                try
                {
                    command.Parameters["AnsweringName"].Value = node4["Name"].InnerText;
                }
                catch (Exception exception10)
                {
                    ProjectData.SetProjectError(exception10);
                    ProjectData.ClearProjectError();
                }
                goto TR_000C;
            TR_0012:
                try
                {
                    command.Parameters["Signature_Date"].Value = DateTime.Parse(node["Date-Signed"].InnerText, _eng);
                }
                catch (Exception exception9)
                {
                    ProjectData.SetProjectError(exception9);
                    ProjectData.ClearProjectError();
                }
                goto TR_000F;
            TR_0015:
                try
                {
                    command.Parameters["Customer_ICD9_4"].Value = list[3]["ICD-Code"].InnerText;
                }
                catch (Exception exception8)
                {
                    ProjectData.SetProjectError(exception8);
                    ProjectData.ClearProjectError();
                }
                goto TR_0012;
            TR_0018:
                try
                {
                    command.Parameters["Customer_ICD9_3"].Value = list[2]["ICD-Code"].InnerText;
                }
                catch (Exception exception7)
                {
                    ProjectData.SetProjectError(exception7);
                    ProjectData.ClearProjectError();
                }
                goto TR_0015;
            TR_001B:
                try
                {
                    command.Parameters["Customer_ICD9_2"].Value = list[1]["ICD-Code"].InnerText;
                }
                catch (Exception exception6)
                {
                    ProjectData.SetProjectError(exception6);
                    ProjectData.ClearProjectError();
                }
                goto TR_0018;
            TR_001E:
                try
                {
                    command.Parameters["Customer_ICD9_1"].Value = list[0]["ICD-Code"].InnerText;
                }
                catch (Exception exception5)
                {
                    ProjectData.SetProjectError(exception5);
                    ProjectData.ClearProjectError();
                }
                goto TR_001B;
            TR_0021:
                try
                {
                    command.Parameters["RecertificationDate"].Value = DateTime.Parse(node2["Revised"].InnerText, _eng);
                }
                catch (Exception exception4)
                {
                    ProjectData.SetProjectError(exception4);
                    ProjectData.ClearProjectError();
                }
                goto TR_001E;
            TR_0024:
                try
                {
                    command.Parameters["RevisedDate"].Value = DateTime.Parse(node2["Revised"].InnerText, _eng);
                }
                catch (Exception exception3)
                {
                    ProjectData.SetProjectError(exception3);
                    ProjectData.ClearProjectError();
                }
                goto TR_0021;
            TR_0027:
                try
                {
                    command.Parameters["InitialDate"].Value = DateTime.Parse(node2["Initial"].InnerText, _eng);
                }
                catch (Exception exception2)
                {
                    ProjectData.SetProjectError(exception2);
                    ProjectData.ClearProjectError();
                }
                goto TR_0024;
            }
        }

        private void ImportData()
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.OpenFileDialog.FileName;
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                int num = 0;
                int num2 = 0;
                if (this.MySqlConnection.State == ConnectionState.Closed)
                {
                    this.MySqlConnection.Open();
                }
                try
                {
                    XmlNodeList list = document.SelectNodes("Documents/CMNs/CMN");
                    int num3 = list.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        num2++;
                        MySqlTransaction tran = this.MySqlConnection.BeginTransaction();
                        try
                        {
                            ImportCMNForm(this.MySqlConnection, tran, list[i]);
                            num++;
                            tran.Commit();
                        }
                        catch (Exception exception1)
                        {
                            Exception ex = exception1;
                            ProjectData.SetProjectError(ex);
                            Exception exception = ex;
                            tran.Rollback();
                            TraceHelper.TraceInfo(exception.ToString());
                            ProjectData.ClearProjectError();
                        }
                    }
                }
                finally
                {
                    bool flag;
                    if (flag)
                    {
                        this.MySqlConnection.Close();
                    }
                }
                MessageBox.Show($"DMEWorks! imported {num} of {num2} forms from file {fileName}.", "SecureCare Import", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private static int ImportData(Devart.Data.MySql.MySqlConnection cnn, string FileName)
        {
            int num = 0;
            XmlDocument document = new XmlDocument();
            document.Load(FileName);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                XmlNodeList list = document.SelectNodes("Documents/CMNs/CMN");
                int num2 = list.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    MySqlTransaction tran = cnn.BeginTransaction();
                    try
                    {
                        ImportCMNForm(cnn, tran, list[i]);
                        num++;
                        tran.Rollback();
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception exception = ex;
                        tran.Rollback();
                        TraceHelper.TraceInfo(exception.ToString());
                        ProjectData.ClearProjectError();
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
            return num;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ResourceManager manager = new ResourceManager(typeof(FormSecureCare));
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MySqlConnection = new Devart.Data.MySql.MySqlConnection();
            this.Grid = new FilteredGrid();
            this.tbMain = new ToolBar();
            this.tbbLoad = new ToolBarButton();
            this.tbbImport = new ToolBarButton();
            this.tbbExport = new ToolBarButton();
            this.tbbDump = new ToolBarButton();
            this.tbbClose = new ToolBarButton();
            this.imglButtons = new ImageList(this.components);
            this.DatasetExport = new DatasetSecureCareExport2();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((ISupportInitialize) this.Grid).BeginInit();
            this.DatasetExport.BeginInit();
            base.SuspendLayout();
            this.SaveFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x29);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x240, 300);
            this.Grid.TabIndex = 5;
            this.tbMain.Appearance = ToolBarAppearance.Flat;
            this.tbMain.BorderStyle = BorderStyle.FixedSingle;
            ToolBarButton[] buttons = new ToolBarButton[] { this.tbbLoad, this.tbbImport, this.tbbExport, this.tbbDump, this.tbbClose };
            this.tbMain.Buttons.AddRange(buttons);
            this.tbMain.Divider = false;
            this.tbMain.DropDownArrows = true;
            this.tbMain.ImageList = this.imglButtons;
            this.tbMain.Location = new Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.ShowToolTips = true;
            this.tbMain.Size = new Size(0x240, 0x29);
            this.tbMain.TabIndex = 6;
            this.tbMain.Wrappable = false;
            this.tbbLoad.ImageIndex = 11;
            this.tbbLoad.Text = "Load";
            this.tbbImport.ImageIndex = 14;
            this.tbbImport.Text = "Import";
            this.tbbExport.ImageIndex = 0x12;
            this.tbbExport.Text = "Export";
            this.tbbDump.ImageIndex = 0x12;
            this.tbbDump.Text = "Dump";
            this.tbbDump.ToolTipText = "Dump dataset for testing purpose";
            this.tbbClose.ImageIndex = 0;
            this.tbbClose.Text = "Close";
            this.imglButtons.ImageSize = new Size(0x10, 0x10);
            this.imglButtons.ImageStream = (ImageListStreamer) manager.GetObject("imglButtons.ImageStream");
            this.imglButtons.TransparentColor = Color.Magenta;
            this.DatasetExport.DataSetName = "DatasetSecureCareExport2";
            this.DatasetExport.Locale = new CultureInfo("en-US");
            this.OpenFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x240, 0x155);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.tbMain);
            base.Name = "FormSecureCare";
            this.Text = "SecureCare";
            ((ISupportInitialize) this.Grid).EndInit();
            this.DatasetExport.EndInit();
            base.ResumeLayout(false);
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AllowEdit = true;
            Appearance.AddBoolColumn("Checked", "...", 40).ReadOnly = false;
            Appearance.AddTextColumn("CMNForm_ID", "ID", 40);
            Appearance.AddTextColumn("Customer_WholeName", "Customer", 100);
            Appearance.AddTextColumn("Doctor_WholeName", "Doctor", 100);
            Appearance.AddTextColumn("CMNType", "CMNType", 80);
            Appearance.AddTextColumn("CMNForm_InitialDate", "InitialDate", 80, Appearance.DateStyle());
        }

        private void LoadDataset()
        {
            LoadDataset(this.MySqlConnection, this.DatasetExport);
            this.Grid.GridSource = this.DatasetExport.CMNForm.ToGridSource();
        }

        private static void LoadDataset(Devart.Data.MySql.MySqlConnection cnn, DatasetSecureCareExport2 dataset)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("", cnn))
                {
                    Guid guid = Guid.NewGuid();
                    Guid guid2 = Guid.NewGuid();
                    command.CommandText = $"CREATE TEMPORARY TABLE `{guid}`
(
  CustomerID INT NOT NULL,
  `Rank` INT NOT NULL,
  PRIMARY KEY (CustomerID)
)";
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT INTO `{guid}` (CustomerID, `Rank`)
SELECT CustomerID, Min(`Rank`) as `Rank`
FROM tbl_customer_insurance
GROUP BY CustomerID";
                    command.ExecuteNonQuery();
                    command.CommandText = $"CREATE TEMPORARY TABLE `{guid2}`
(
  CustomerID INT NOT NULL,
  CustomerInsuranceID INT NOT NULL,
  PRIMARY KEY (CustomerID)
)";
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("INSERT INTO `{1}` (CustomerID, CustomerInsuranceID)\r\nSELECT `{0}`.CustomerID, Min(tbl_customer_insurance.ID) as CustomerInsuranceID\r\nFROM tbl_customer_insurance\r\n     INNER JOIN `{0}` ON `{0}`.CustomerID = tbl_customer_insurance.CustomerID\r\n                     AND `{0}`.`Rank` = tbl_customer_insurance.`Rank`\r\nGROUP BY `{0}`.CustomerID", guid, guid2);
                    command.ExecuteNonQuery();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                        dataset.EnforceConstraints = false;
                        try
                        {
                            command.CommandText = $"SELECT 
  tbl_cmnform.ID as `CMNForm_ID`,
  tbl_cmnform.CMNType,
  CASE tbl_cmnform.CMNType 
  WHEN 'DMERC 01.02A' THEN 'HCFA841'
  WHEN 'DMERC 01.02B' THEN 'HCFA842'
  WHEN 'DMERC 02.03A' THEN 'HCFA843'
  WHEN 'DMERC 02.03B' THEN 'HCFA844'
  WHEN 'DMERC 03.02'  THEN 'HCFA845'
  WHEN 'DMERC 04.03B' THEN 'HCFA846'
  WHEN 'DMERC 04.03C' THEN 'HCFA847'
  WHEN 'DMERC 06.02B' THEN 'HCFA848'
  WHEN 'DMERC 07.02A' THEN 'HCFA849'
  WHEN 'DMERC 07.02B' THEN 'HCFA850'
  WHEN 'DMERC 09.02'  THEN 'HCFA851'
  WHEN 'DMERC 10.02A' THEN 'HCFA852'
  WHEN 'DMERC 10.02B' THEN 'HCFA853'
  WHEN 'DMERC 11.01'  THEN 'HCFA854'
  WHEN 'DMERC 484.2'  THEN 'HCFA484' END as `CMNForm_HCFAType`,
  tbl_cmnform.InitialDate as `CMNForm_InitialDate`,
  tbl_cmnform.RevisedDate as `CMNForm_RevisedDate`,
  tbl_cmnform.RecertificationDate as `CMNForm_RecertificationDate`,
  tbl_customer.ID            as `Customer_ID`,
  tbl_customer.FirstName     as `Customer_FirstName`,
  tbl_customer.LastName      as `Customer_LastName`,
  tbl_customer.MiddleName    as `Customer_MiddleName`,
-- null as `Customer_WholeName`
  tbl_customer.Address1      as `Customer_Address1`,
  tbl_customer.Address2      as `Customer_Address2`,
  tbl_customer.City          as `Customer_City`,
  tbl_customer.State         as `Customer_State`,
  tbl_customer.Zip           as `Customer_Zip`,
  tbl_customer.Phone         as `Customer_Phone`,
  tbl_customer.DateOfBirth   as `Customer_DOB`,
  tbl_customer.Gender        as `Customer_Gender`,
  CASE tbl_customer.Gender
  WHEN 'Male'   THEN 'M'
  WHEN 'Femail' THEN 'F' END as `Customer_Gender`,
  tbl_customer.Height        as `Customer_Height`,
  tbl_customer.Weight        as `Customer_Weight`,
  tbl_customer.SSNumber      as `Customer_SSN`,
  tbl_customer_insurance.PolicyNumber as `Customer_HIC_Number`,

  tbl_doctor.ID          as `Doctor_ID`,
  tbl_doctor.FirstName   as `Doctor_FirstName`,
  tbl_doctor.LastName    as `Doctor_LastName`,
  tbl_doctor.MiddleName  as `Doctor_MiddleName`,
-- null as `Doctor_WholeName`
  tbl_doctor.Address1    as `Doctor_Address1`,
  tbl_doctor.Address2    as `Doctor_Address2`,
  tbl_doctor.City        as `Doctor_City`,
  tbl_doctor.State       as `Doctor_State`,
  tbl_doctor.Zip         as `Doctor_Zip`,
  tbl_doctor.Phone       as `Doctor_Phone`,
  tbl_doctor.UPINNumber  as `Doctor_UPIN`,

  tbl_facility.ID        as `Facility_ID`,
  tbl_facility.Name      as `Facility_Name`,
  tbl_facility.Address1  as `Facility_Address1`,
  tbl_facility.Address2  as `Facility_Address2`,
  tbl_facility.City      as `Facility_City`,
  tbl_facility.State     as `Facility_State`,
  tbl_facility.Zip       as `Facility_Zip`,
  tbl_facility.POSTypeID as `Facility_Code`,

  tbl_company.ID       as `Company_ID`,
  tbl_company.Name     as `Company_Name`,
  tbl_company.Address1 as `Company_Address1`,
  tbl_company.Address2 as `Company_Address2`,
  tbl_company.City     as `Company_City`,
  tbl_company.State    as `Company_State`,
  tbl_company.Zip      as `Company_Zip`,
  tbl_company.Phone    as `Company_Phone`,
  tbl_provider.ProviderNumber as `Company_NSC`

FROM ((((((((tbl_cmnform
            LEFT JOIN tbl_customer ON tbl_cmnform.CustomerID = tbl_customer.ID)
           LEFT JOIN `{guid2}` as tbl_customer_insurance_id ON tbl_customer.ID = tbl_customer_insurance_id.CustomerID)
          LEFT JOIN tbl_customer_insurance ON tbl_customer_insurance_id.CustomerID = tbl_customer_insurance.CustomerID
                                          AND tbl_customer_insurance_id.CustomerInsuranceID = tbl_customer_insurance.ID)
         LEFT JOIN tbl_doctor ON tbl_cmnform.DoctorID = tbl_doctor.ID)
        LEFT JOIN tbl_facility ON tbl_cmnform.FacilityID = tbl_facility.ID)
       LEFT JOIN tbl_company ON tbl_company.ID = 1)
      LEFT JOIN tbl_location ON tbl_customer.LocationID = tbl_location.ID)
     LEFT JOIN tbl_provider ON tbl_location.ID = tbl_provider.LocationID
                           AND tbl_customer_insurance.InsuranceCompanyID = tbl_provider.InsuranceCompanyID)
WHERE (tbl_cmnform.CMNType IN ('DMERC 01.02A', 'DMERC 01.02B', 'DMERC 02.03A', 'DMERC 02.03B', 'DMERC 03.02', 'DMERC 04.03B', 'DMERC 04.03C', 'DMERC 06.02B', 'DMERC 07.02A', 'DMERC 07.02B', 'DMERC 09.02', 'DMERC 10.02A', 'DMERC 10.02B', 'DMERC 11.01', 'DMERC 484.2'))";
                            dataset.CMNForm.Clear();
                            adapter.Fill(dataset.CMNForm);
                            command.CommandText = "SELECT\r\n  tbl_cmnform_details.CMNFormID,\r\n  tbl_cmnform_details.BillingCode,\r\n  tbl_inventoryitem.Name as ItemDescription,\r\n  tbl_cmnform_details.OrderedQuantity as Quantity,\r\n  tbl_cmnform_details.OrderedUnits as Units,\r\n  tbl_cmnform_details.BillablePrice,\r\n  tbl_cmnform_details.AllowablePrice\r\nFROM tbl_cmnform_details\r\n     LEFT JOIN tbl_inventoryitem ON tbl_cmnform_details.InventoryItemID = tbl_inventoryitem.ID";
                            dataset.Details.Clear();
                            adapter.Fill(dataset.Details);
                            DatasetSecureCareExport2.CMNFormDataTable cMNForm = dataset.CMNForm;
                            int num = cMNForm.Rows.Count - 1;
                            int num2 = 0;
                            while (true)
                            {
                                if (num2 > num)
                                {
                                    dataset.AcceptChanges();
                                    break;
                                }
                                DataRow row = cMNForm.Rows[num2];
                                row[cMNForm.Customer_WholeNameColumn] = GetWholeName(row[cMNForm.Customer_FirstNameColumn], row[cMNForm.Customer_MiddleNameColumn], row[cMNForm.Customer_LastNameColumn]);
                                row[cMNForm.Doctor_WholeNameColumn] = GetWholeName(row[cMNForm.Doctor_FirstNameColumn], row[cMNForm.Doctor_MiddleNameColumn], row[cMNForm.Doctor_LastNameColumn]);
                                num2++;
                            }
                        }
                        finally
                        {
                            dataset.EnforceConstraints = true;
                        }
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        private static void PrepareCommand_0102a(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetYNDAnswer(node, "1");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetYNDAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 6, "Answer4").Value = GetYNDAnswer(node, "4");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 6, "Answer5").Value = GetYNDAnswer(node, "5");
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 6, "Answer6").Value = GetYNDAnswer(node, "6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 6, "Answer7").Value = GetYNDAnswer(node, "7");
        }

        private static void PrepareCommand_0102b(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 6, "Answer12").Value = GetYNDAnswer(node, "12");
            cmd.Parameters.Add("Answer13", MySqlType.VarChar, 6, "Answer13").Value = GetYNDAnswer(node, "13");
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 6, "Answer14").Value = GetYNDAnswer(node, "14");
            cmd.Parameters.Add("Answer15", MySqlType.VarChar, 6, "Answer15").Value = GetYNDAnswer(node, "15");
            cmd.Parameters.Add("Answer16", MySqlType.VarChar, 6, "Answer16").Value = GetYNDAnswer(node, "16");
            cmd.Parameters.Add("Answer19", MySqlType.VarChar, 6, "Answer19").Value = GetYNDAnswer(node, "19");
            cmd.Parameters.Add("Answer20", MySqlType.VarChar, 6, "Answer20").Value = GetYNDAnswer(node, "20");
            cmd.Parameters.Add("Answer21_Ulcer1_Stage", MySqlType.VarChar, 30, "Answer21_Ulcer1_Stage").Value = GetSubAnswer(node, "21", "StageUlcer1");
            cmd.Parameters.Add("Answer21_Ulcer1_MaxLength", MySqlType.Double, 8, "Answer21_Ulcer1_MaxLength").Value = DBNull.Value;
            cmd.Parameters.Add("Answer21_Ulcer1_MaxWidth", MySqlType.Double, 8, "Answer21_Ulcer1_MaxWidth").Value = DBNull.Value;
            cmd.Parameters.Add("Answer21_Ulcer2_Stage", MySqlType.VarChar, 30, "Answer21_Ulcer2_Stage").Value = GetSubAnswer(node, "21", "StageUlcer2");
            cmd.Parameters.Add("Answer21_Ulcer2_MaxLength", MySqlType.Double, 8, "Answer21_Ulcer2_MaxLength").Value = DBNull.Value;
            cmd.Parameters.Add("Answer21_Ulcer2_MaxWidth", MySqlType.Double, 8, "Answer21_Ulcer2_MaxWidth").Value = DBNull.Value;
            cmd.Parameters.Add("Answer21_Ulcer3_Stage", MySqlType.VarChar, 30, "Answer21_Ulcer3_Stage").Value = GetSubAnswer(node, "21", "StageUlcer3");
            cmd.Parameters.Add("Answer21_Ulcer3_MaxLength", MySqlType.Double, 8, "Answer21_Ulcer3_MaxLength").Value = DBNull.Value;
            cmd.Parameters.Add("Answer21_Ulcer3_MaxWidth", MySqlType.Double, 8, "Answer21_Ulcer3_MaxWidth").Value = DBNull.Value;
            cmd.Parameters.Add("Answer22", MySqlType.VarChar, 6, "Answer22").Value = GetEnumAnswer(node, "22");
            try
            {
                cmd.Parameters["Answer21_Ulcer1_MaxLength"].Value = double.Parse(GetSubAnswer(node, "21", "MaxLength1"), _eng);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer21_Ulcer2_MaxLength"].Value = double.Parse(GetSubAnswer(node, "21", "MaxLength2"), _eng);
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer21_Ulcer3_MaxLength"].Value = double.Parse(GetSubAnswer(node, "21", "MaxLength3"), _eng);
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer21_Ulcer1_MaxWidth"].Value = double.Parse(GetSubAnswer(node, "21", "MaxWidth1"), _eng);
            }
            catch (Exception exception4)
            {
                ProjectData.SetProjectError(exception4);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer21_Ulcer2_MaxWidth"].Value = double.Parse(GetSubAnswer(node, "21", "MaxWidth2"), _eng);
            }
            catch (Exception exception5)
            {
                ProjectData.SetProjectError(exception5);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer21_Ulcer3_MaxWidth"].Value = double.Parse(GetSubAnswer(node, "21", "MaxWidth3"), _eng);
            }
            catch (Exception exception6)
            {
                ProjectData.SetProjectError(exception6);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_0203a(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetYNDAnswer(node, "1");
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 6, "Answer2").Value = GetYNDAnswer(node, "2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetYNDAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 6, "Answer4").Value = GetYNDAnswer(node, "4");
            cmd.Parameters.Add("Answer5", MySqlType.Int, 0, "Answer5").Value = DBNull.Value;
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 6, "Answer6").Value = GetYNDAnswer(node, "6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 6, "Answer7").Value = GetYNDAnswer(node, "7");
            try
            {
                cmd.Parameters["Answer5"].Value = int.Parse(GetSubAnswer(node, "5"), _eng);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_0203b(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetYNDAnswer(node, "1");
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 6, "Answer2").Value = GetYNDAnswer(node, "2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetYNDAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 6, "Answer4").Value = GetYNDAnswer(node, "4");
            cmd.Parameters.Add("Answer5", MySqlType.Int, 6, "Answer5").Value = DBNull.Value;
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 6, "Answer8").Value = GetYNDAnswer(node, "8");
            cmd.Parameters.Add("Answer9", MySqlType.VarChar, 6, "Answer9").Value = GetYNDAnswer(node, "9");
            try
            {
                cmd.Parameters["Answer5"].Value = int.Parse(GetSubAnswer(node, "5"), _eng);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_0302(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer12", MySqlType.Int, 0, "Answer12").Value = DBNull.Value;
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 6, "Answer14").Value = GetYNDAnswer(node, "14");
            try
            {
                cmd.Parameters["Answer12"].Value = int.Parse(GetSubAnswer(node, "12"), _eng);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_0403b(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetYNDAnswer(node, "1");
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 6, "Answer2").Value = GetYNDAnswer(node, "2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetYNDAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 6, "Answer4").Value = GetYNDAnswer(node, "4");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 6, "Answer5").Value = GetYNDAnswer(node, "5");
        }

        private static void PrepareCommand_0403c(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer6a", MySqlType.VarChar, 6, "Answer6a").Value = GetYNDAnswer(node, "6");
            cmd.Parameters.Add("Answer6b", MySqlType.Int, 0, "Answer6b").Value = DBNull.Value;
            cmd.Parameters.Add("Answer7a", MySqlType.VarChar, 6, "Answer7a").Value = GetYNDAnswer(node, "7");
            cmd.Parameters.Add("Answer7b", MySqlType.Int, 0, "Answer7b").Value = DBNull.Value;
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 6, "Answer8").Value = GetYNDAnswer(node, "8");
            cmd.Parameters.Add("Answer9a", MySqlType.VarChar, 6, "Answer9a").Value = GetYNDAnswer(node, "9");
            cmd.Parameters.Add("Answer9b", MySqlType.Int, 0, "Answer9b").Value = DBNull.Value;
            cmd.Parameters.Add("Answer10a", MySqlType.VarChar, 6, "Answer10a").Value = GetYNDAnswer(node, "10");
            cmd.Parameters.Add("Answer10b", MySqlType.Int, 0, "Answer10b").Value = DBNull.Value;
            cmd.Parameters.Add("Answer10c", MySqlType.Int, 0, "Answer10c").Value = DBNull.Value;
            cmd.Parameters.Add("Answer11a", MySqlType.VarChar, 6, "Answer11a").Value = GetYNDAnswer(node, "11");
            cmd.Parameters.Add("Answer11b", MySqlType.Int, 0, "Answer11b").Value = DBNull.Value;
            try
            {
                cmd.Parameters["Answer6b"].Value = int.Parse(GetSubAnswer(node, "6", "B"));
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer7b"].Value = int.Parse(GetSubAnswer(node, "7", "B"));
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer9b"].Value = int.Parse(GetSubAnswer(node, "9", "B"));
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer10b"].Value = int.Parse(GetSubAnswer(node, "10", "B"));
            }
            catch (Exception exception4)
            {
                ProjectData.SetProjectError(exception4);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer10c"].Value = int.Parse(GetSubAnswer(node, "10", "C"));
            }
            catch (Exception exception5)
            {
                ProjectData.SetProjectError(exception5);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer11b"].Value = int.Parse(GetSubAnswer(node, "11", "B"));
            }
            catch (Exception exception6)
            {
                ProjectData.SetProjectError(exception6);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_0602b(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetYNDAnswer(node, "1");
            cmd.Parameters.Add("Answer2", MySqlType.Date, 0, "Answer2").Value = DBNull.Value;
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetYNDAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.Int, 0, "Answer4").Value = DBNull.Value;
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 6, "Answer5").Value = GetEnumAnswer(node, "5");
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 6, "Answer6").Value = GetYNDAnswer(node, "6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 6, "Answer7").Value = GetYNDAnswer(node, "7");
            cmd.Parameters.Add("Answer8_begun", MySqlType.Date, 0, "Answer8_begun").Value = DBNull.Value;
            cmd.Parameters.Add("Answer8_ended", MySqlType.Date, 0, "Answer8_ended").Value = DBNull.Value;
            cmd.Parameters.Add("Answer9", MySqlType.Date, 0, "Answer9").Value = DBNull.Value;
            cmd.Parameters.Add("Answer10", MySqlType.VarChar, 6, "Answer10").Value = GetEnumAnswer(node, "10");
            cmd.Parameters.Add("Answer11", MySqlType.VarChar, 6, "Answer11").Value = GetYNDAnswer(node, "11");
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 6, "Answer12").Value = GetEnumAnswer(node, "12");
            try
            {
                cmd.Parameters["Answer2"].Value = DateTime.Parse(GetSubAnswer(node, "2"));
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer4"].Value = int.Parse(GetSubAnswer(node, "4"));
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer8_begun"].Value = DateTime.Parse(GetSubAnswer(node, "8", "1"));
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer8_ended"].Value = DateTime.Parse(GetSubAnswer(node, "8", "2"));
            }
            catch (Exception exception4)
            {
                ProjectData.SetProjectError(exception4);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer9"].Value = DateTime.Parse(GetSubAnswer(node, "9"));
            }
            catch (Exception exception5)
            {
                ProjectData.SetProjectError(exception5);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_0702a(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetYNDAnswer(node, "1");
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 6, "Answer2").Value = GetYNDAnswer(node, "2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetYNDAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 6, "Answer4").Value = GetYNDAnswer(node, "4");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 6, "Answer5").Value = GetYNDAnswer(node, "5");
        }

        private static void PrepareCommand_0702b(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 6, "Answer6").Value = GetYNDAnswer(node, "6");
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 6, "Answer7").Value = GetYNDAnswer(node, "7");
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 6, "Answer8").Value = GetYNDAnswer(node, "8");
            cmd.Parameters.Add("Answer12", MySqlType.VarChar, 6, "Answer12").Value = GetYNDAnswer(node, "12");
            cmd.Parameters.Add("Answer13", MySqlType.VarChar, 6, "Answer13").Value = GetYNDAnswer(node, "13");
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 6, "Answer14").Value = GetYNDAnswer(node, "14");
        }

        private static void PrepareCommand_0902(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1", MySqlType.VarChar, 6, "Answer1").Value = GetEnumAnswer(node, "1");
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 50, "Answer2").Value = GetSubAnswer(node, "2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 50, "Answer3").Value = GetSubAnswer(node, "3");
            cmd.Parameters.Add("Answer4", MySqlType.VarChar, 6, "Answer4").Value = GetEnumAnswer(node, "4");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 6, "Answer5").Value = GetEnumAnswer(node, "5");
            cmd.Parameters.Add("Answer6", MySqlType.Int, 0, "Answer6").Value = DBNull.Value;
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 6, "Answer7").Value = GetYNDAnswer(node, "7");
            try
            {
                cmd.Parameters["Answer6"].Value = int.Parse(GetSubAnswer(node, "6"));
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_1002a(MySqlCommand cmd, XmlNode node)
        {
            throw new Exception("Under construction");
        }

        private static void PrepareCommand_1002b(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer7", MySqlType.VarChar, 6, "Answer7").Value = GetYNDAnswer(node, "7");
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 6, "Answer8").Value = GetYNDAnswer(node, "8");
            cmd.Parameters.Add("Answer10a", MySqlType.VarChar, 50, "Answer10a").Value = GetSubAnswer(node, "10", 0);
            cmd.Parameters.Add("Answer10b", MySqlType.VarChar, 50, "Answer10b").Value = GetSubAnswer(node, "10", 1);
            cmd.Parameters.Add("Answer11a", MySqlType.VarChar, 50, "Answer11a").Value = GetSubAnswer(node, "11", 0);
            cmd.Parameters.Add("Answer11b", MySqlType.VarChar, 50, "Answer11b").Value = GetSubAnswer(node, "11", 1);
            cmd.Parameters.Add("Answer12", MySqlType.Int, 0, "Answer12").Value = DBNull.Value;
            cmd.Parameters.Add("Answer13", MySqlType.VarChar, 6, "Answer13").Value = GetEnumAnswer(node, "13");
            cmd.Parameters.Add("Answer14", MySqlType.VarChar, 6, "Answer14").Value = GetYNDAnswer(node, "14");
            cmd.Parameters.Add("Answer15", MySqlType.VarChar, 50, "Answer15").Value = GetYNDAnswer(node, "15");
            try
            {
                cmd.Parameters["Answer12"].Value = int.Parse(GetSubAnswer(node, "12"), _eng);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private static void PrepareCommand_4842(MySqlCommand cmd, XmlNode node)
        {
            cmd.Parameters.Add("Answer1a", MySqlType.Int, 4, "Answer1a").Value = DBNull.Value;
            cmd.Parameters.Add("Answer1b", MySqlType.Int, 4, "Answer1b").Value = DBNull.Value;
            cmd.Parameters.Add("Answer1c", MySqlType.Date, 0, "Answer1c").Value = DBNull.Value;
            cmd.Parameters.Add("Answer2", MySqlType.VarChar, 6, "Answer2").Value = GetEnumAnswer(node, "2");
            cmd.Parameters.Add("Answer3", MySqlType.VarChar, 6, "Answer3").Value = GetEnumAnswer(node, "3");
            cmd.Parameters.Add("PhysicianAddress", MySqlType.VarChar, 50, "PhysicianAddress").Value = GetSubAnswer(node, "4", "Address");
            cmd.Parameters.Add("PhysicianCity", MySqlType.VarChar, 50, "PhysicianCity").Value = GetSubAnswer(node, "4", "City");
            cmd.Parameters.Add("PhysicianState", MySqlType.VarChar, 50, "PhysicianState").Value = GetSubAnswer(node, "4", "State");
            cmd.Parameters.Add("PhysicianZip", MySqlType.VarChar, 50, "PhysicianZip").Value = GetSubAnswer(node, "4", "Zip");
            cmd.Parameters.Add("PhysicianName", MySqlType.VarChar, 50, "PhysicianName").Value = GetSubAnswer(node, "4", "Name");
            cmd.Parameters.Add("Answer5", MySqlType.VarChar, 6, "Answer5").Value = GetYNDAnswer(node, "5");
            cmd.Parameters.Add("Answer6", MySqlType.VarChar, 10, "Answer6").Value = GetSubAnswer(node, "6");
            cmd.Parameters.Add("Answer7a", MySqlType.Int, 4, "Answer7a").Value = DBNull.Value;
            cmd.Parameters.Add("Answer7b", MySqlType.Int, 4, "Answer7b").Value = DBNull.Value;
            cmd.Parameters.Add("Answer7c", MySqlType.Date, 0, "Answer7c").Value = DBNull.Value;
            cmd.Parameters.Add("Answer8", MySqlType.VarChar, 6, "Answer8").Value = GetYNDAnswer(node, "8");
            cmd.Parameters.Add("Answer9", MySqlType.VarChar, 6, "Answer9").Value = GetYNDAnswer(node, "9");
            cmd.Parameters.Add("Answer10", MySqlType.VarChar, 6, "Answer10").Value = GetYNDAnswer(node, "10");
            try
            {
                cmd.Parameters["Answer1a"].Value = int.Parse(GetSubAnswer(node, "1", "a"), _eng);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer1b"].Value = int.Parse(GetSubAnswer(node, "1", "b"), _eng);
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer1c"].Value = DateTime.Parse(GetSubAnswer(node, "1", "c"), _eng);
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer7a"].Value = int.Parse(GetSubAnswer(node, "7", "a"), _eng);
            }
            catch (Exception exception4)
            {
                ProjectData.SetProjectError(exception4);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer7b"].Value = int.Parse(GetSubAnswer(node, "7", "b"), _eng);
            }
            catch (Exception exception5)
            {
                ProjectData.SetProjectError(exception5);
                ProjectData.ClearProjectError();
            }
            try
            {
                cmd.Parameters["Answer7c"].Value = DateTime.Parse(GetSubAnswer(node, "7", "c"), _eng);
            }
            catch (Exception exception6)
            {
                ProjectData.SetProjectError(exception6);
                ProjectData.ClearProjectError();
            }
        }

        private void tbMain_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            try
            {
                if (ReferenceEquals(e.Button, this.tbbLoad))
                {
                    this.LoadDataset();
                }
                else if (ReferenceEquals(e.Button, this.tbbDump))
                {
                    this.DumpDataset();
                }
                else if (ReferenceEquals(e.Button, this.tbbExport))
                {
                    this.ExportDataset();
                }
                else if (ReferenceEquals(e.Button, this.tbbImport))
                {
                    this.ImportData();
                }
                else if (ReferenceEquals(e.Button, this.tbbClose))
                {
                    base.Close();
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
        }

        private static string ToXmlString(object Value) => 
            !(Value is DBNull) ? (!(Value is string) ? (!(Value is DateTime) ? (!(Value is byte) ? (!(Value is short) ? (!(Value is int) ? (!(Value is long) ? (!(Value is sbyte) ? (!(Value is ushort) ? (!(Value is uint) ? (!(Value is ulong) ? (!(Value is float) ? (!(Value is double) ? (!(Value is decimal) ? "" : Conversions.ToDecimal(Value).ToString(_eng)) : Conversions.ToDouble(Value).ToString(_eng)) : Conversions.ToSingle(Value).ToString(_eng)) : Conversions.ToULong(Value).ToString(_eng)) : Conversions.ToUInteger(Value).ToString(_eng)) : Conversions.ToUShort(Value).ToString(_eng)) : Conversions.ToSByte(Value).ToString(_eng)) : Conversions.ToLong(Value).ToString(_eng)) : Conversions.ToInteger(Value).ToString(_eng)) : Conversions.ToShort(Value).ToString(_eng)) : Conversions.ToByte(Value).ToString(_eng)) : Conversions.ToDate(Value).ToString("MM/dd/yyyy", _eng)) : Conversions.ToString(Value)) : ((DBNull) Value).ToString(_eng);

        [field: AccessedThroughProperty("MySqlConnection")]
        private Devart.Data.MySql.MySqlConnection MySqlConnection { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tbMain")]
        private ToolBar tbMain { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tbbClose")]
        private ToolBarButton tbbClose { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("imglButtons")]
        private ImageList imglButtons { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("DatasetExport")]
        private DatasetSecureCareExport2 DatasetExport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tbbDump")]
        private ToolBarButton tbbDump { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tbbLoad")]
        private ToolBarButton tbbLoad { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tbbExport")]
        private ToolBarButton tbbExport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("SaveFileDialog")]
        private System.Windows.Forms.SaveFileDialog SaveFileDialog { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tbbImport")]
        private ToolBarButton tbbImport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("OpenFileDialog")]
        private System.Windows.Forms.OpenFileDialog OpenFileDialog { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

