namespace DMEWorks.Forms.Shipping.Ups
{
    using Devart.Data.MySql;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;
    using Ups.GroundShipping;

    [DesignerGenerated]
    public class FormUpsShipping : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private readonly int CustomerID;

        public FormUpsShipping(int CustomerID)
        {
            base.Load += new EventHandler(this.FormUpsShipping_Load);
            this.CustomerID = CustomerID;
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                FreightShipResponse response;
                Cursor cursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    using (FreightShipService service = new FreightShipService())
                    {
                        this.InitializeService(service);
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(FormUpsShipping.CertificateValidation);
                        response = service.ProcessShipment(this.GetRequest());
                    }
                }
                finally
                {
                    this.Cursor = cursor;
                }
                using (StringWriter writer = new StringWriter())
                {
                    FreightShipResponse response2 = response;
                    writer.WriteLine("The transaction was a " + response2.Response.ResponseStatus.Description);
                    ShipmentResultsType shipmentResults = response2.ShipmentResults;
                    writer.WriteLine("The BOLID of the shipment is: " + shipmentResults.BOLID);
                    writer.WriteLine("The Shipment number of the shipment is " + shipmentResults.ShipmentNumber);
                    TotalShipmentChargeType totalShipmentCharge = shipmentResults.TotalShipmentCharge;
                    writer.WriteLine("The Shipment total charge is " + totalShipmentCharge.MonetaryValue + " " + totalShipmentCharge.CurrencyCode);
                    totalShipmentCharge = null;
                    shipmentResults = null;
                    response2 = null;
                    MessageBox.Show(writer.ToString(), "Submission results", MessageBoxButtons.OK);
                }
            }
            catch (SoapException exception1) when ((() => // NOTE: To create compilable code, filter at IL offset 0116 was represented using lambda expression.
            {
                SoapException ex = exception1;
                ProjectData.SetProjectError(ex);
                return (ex.Detail is XmlElement);
            })())
            {
                try
                {
                    SoapException exception;
                    MessageBox.Show(GetErrorMessage((XmlElement) exception.Detail), "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                catch (Exception exception4)
                {
                    Exception ex = exception4;
                    ProjectData.SetProjectError(ex);
                    Exception exception2 = ex;
                    this.ShowException(exception2, "Submission Error");
                    ProjectData.ClearProjectError();
                }
                ProjectData.ClearProjectError();
            }
            catch (Exception exception5)
            {
                Exception ex = exception5;
                ProjectData.SetProjectError(ex);
                Exception exception3 = ex;
                this.ShowException(exception3, "Submission Error");
                ProjectData.ClearProjectError();
            }
        }

        private static bool CertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => 
            true;

        private void cmbShipFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedItem = this.cmbShipFrom.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                this.caShipFrom.txtAddress1.Text = NullableConvert.ToString(selectedItem["Address1"]);
                this.caShipFrom.txtAddress2.Text = NullableConvert.ToString(selectedItem["Address2"]);
                this.caShipFrom.txtCity.Text = NullableConvert.ToString(selectedItem["City"]);
                this.caShipFrom.txtState.Text = NullableConvert.ToString(selectedItem["State"]);
                this.caShipFrom.txtZip.Text = NullableConvert.ToString(selectedItem["Zip"]);
            }
            else
            {
                this.caShipFrom.txtAddress1.Text = "";
                this.caShipFrom.txtAddress2.Text = "";
                this.caShipFrom.txtCity.Text = "";
                this.caShipFrom.txtState.Text = "";
                this.caShipFrom.txtZip.Text = "";
            }
        }

        private void cmbShipTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedItem = this.cmbShipTo.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                this.caShipTo.txtAddress1.Text = NullableConvert.ToString(selectedItem["Address1"]);
                this.caShipTo.txtAddress2.Text = NullableConvert.ToString(selectedItem["Address2"]);
                this.caShipTo.txtCity.Text = NullableConvert.ToString(selectedItem["City"]);
                this.caShipTo.txtState.Text = NullableConvert.ToString(selectedItem["State"]);
                this.caShipTo.txtZip.Text = NullableConvert.ToString(selectedItem["Zip"]);
            }
            else
            {
                this.caShipTo.txtAddress1.Text = "";
                this.caShipTo.txtAddress2.Text = "";
                this.caShipTo.txtCity.Text = "";
                this.caShipTo.txtState.Text = "";
                this.caShipTo.txtZip.Text = "";
            }
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private static string ExtractPhone(string Phone)
        {
            StringBuilder builder;
            if (Phone != null)
            {
                builder = new StringBuilder(10);
                int num = Phone.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    char c = Phone[i];
                    if (char.IsDigit(c))
                    {
                        builder.Append(c);
                        if (builder.Capacity <= builder.Length)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                return "";
            }
            return builder.ToString();
        }

        private void FormUpsShipping_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadCompany();
                this.LoadCustomer();
                this.LoadDropdowns();
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

        private CommodityType[] GetCommodity()
        {
            List<CommodityType> list = new List<CommodityType>();
            DMEWorks.Forms.Shipping.Ups.ControlCommodities.TableCommodities commodities = this.ControlCommodities.Table();
            foreach (DataRow row in commodities.Select())
            {
                ShipCodeDescriptionType type = new ShipCodeDescriptionType {
                    Code = Conversions.ToString(row[commodities.Col_PackagingType]),
                    Description = ""
                };
                FreightShipUnitOfMeasurementType type2 = new FreightShipUnitOfMeasurementType {
                    Code = "lbs",
                    Description = "pounds"
                };
                WeightType type3 = new WeightType {
                    Value = $"{row[commodities.Col_Weight]:0.00}",
                    UnitOfMeasurement = type2
                };
                CommodityValueType type4 = new CommodityValueType {
                    CurrencyCode = "USD",
                    MonetaryValue = $"{row[commodities.Col_CommodityValue]:0.00}"
                };
                NMFCCommodityType type5 = new NMFCCommodityType {
                    PrimeCode = Conversions.ToString(row[commodities.Col_NmfcPrimeCode]),
                    SubCode = Conversions.ToString(row[commodities.Col_NmfcSubCode])
                };
                CommodityType item = new CommodityType {
                    Description = Conversions.ToString(row[commodities.Col_Description]),
                    PackagingType = type,
                    NumberOfPieces = $"{row[commodities.Col_NumberOfPieces]:0}",
                    Weight = type3,
                    CommodityValue = type4,
                    FreightClass = Conversions.ToString(row[commodities.Col_FreightClass]),
                    NMFCCommodity = type5
                };
                list.Add(item);
            }
            return list.ToArray();
        }

        protected static string GetErrorMessage(XmlElement detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new XmlNodeReader(detail).NameTable);
            nsmgr.AddNamespace("err", "http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1");
            using (StringWriter writer = new StringWriter())
            {
                IEnumerator enumerator;
                writer.WriteLine("Transaction contains some errors. Please fix them and resend.");
                writer.WriteLine();
                try
                {
                    enumerator = detail.SelectNodes("err:Errors/err:ErrorDetail", nsmgr).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        XmlElement current = (XmlElement) enumerator.Current;
                        writer.Write(GetPathText(current, "err:PrimaryErrorCode/err:Code", nsmgr));
                        writer.Write(" - ");
                        writer.Write(GetPathText(current, "err:PrimaryErrorCode/err:Description", nsmgr));
                        writer.WriteLine();
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
                return writer.ToString();
            }
        }

        protected static string GetPathText(XmlNode node, string path)
        {
            string str;
            if (node == null)
            {
                str = "";
            }
            else
            {
                XmlNode node2 = node.SelectSingleNode(path);
                str = (node2 != null) ? node2.InnerText : "";
            }
            return str;
        }

        protected static string GetPathText(XmlNode node, string path, XmlNamespaceManager manager)
        {
            string str;
            if (node == null)
            {
                str = "";
            }
            else
            {
                XmlNode node2 = node.SelectSingleNode(path, manager);
                str = (node2 != null) ? node2.InnerText : "";
            }
            return str;
        }

        private PaymentInformationType GetPaymentInformation()
        {
            List<string> list = new List<string>();
            string item = this.caPayer.txtAddress1.Text.Trim();
            if (0 < item.Length)
            {
                list.Add(item);
            }
            item = this.caPayer.txtAddress2.Text.Trim();
            if (0 < item.Length)
            {
                list.Add(item);
            }
            FreightShipAddressType type = new FreightShipAddressType {
                AddressLine = list.ToArray(),
                City = this.caPayer.txtCity.Text.Trim(),
                StateProvinceCode = this.caPayer.txtState.Text.Trim(),
                PostalCode = this.caPayer.txtZip.Text.Trim(),
                CountryCode = "US"
            };
            FreightShipPhoneType type2 = new FreightShipPhoneType {
                Number = this.txtPayerPhone.Text.Trim(),
                Extension = this.txtPayerExt.Text.Trim()
            };
            PayerType type3 = new PayerType {
                Address = type,
                AttentionName = this.txtPayerName.Text.Trim(),
                Name = this.txtPayerName.Text.Trim(),
                Phone = type2,
                EMailAddress = this.txtPayerEmail.Text.Trim(),
                ShipperNumber = ""
            };
            ShipCodeDescriptionType type4 = new ShipCodeDescriptionType {
                Code = Conversions.ToString(this.cmbBillingOption.SelectedValue),
                Description = ""
            };
            PaymentInformationType type1 = new PaymentInformationType();
            type1.Payer = type3;
            type1.ShipmentBillingOption = type4;
            return type1;
        }

        private FreightShipRequest GetRequest()
        {
            RequestType type = new RequestType();
            type.RequestOption = new string[] { "1" };
            FreightShipRequest request1 = new FreightShipRequest();
            request1.Request = type;
            request1.Shipment = this.GetShipment();
            return request1;
        }

        private ShipFromType GetShipFrom()
        {
            List<string> list = new List<string>();
            string item = this.caShipFrom.txtAddress1.Text.Trim();
            if (0 < item.Length)
            {
                list.Add(item);
            }
            item = this.caShipFrom.txtAddress2.Text.Trim();
            if (0 < item.Length)
            {
                list.Add(item);
            }
            FreightShipAddressType type = new FreightShipAddressType {
                AddressLine = list.ToArray(),
                City = this.caShipFrom.txtCity.Text.Trim(),
                StateProvinceCode = this.caShipFrom.txtState.Text.Trim(),
                PostalCode = this.caShipFrom.txtZip.Text.Trim(),
                CountryCode = "US"
            };
            FreightShipPhoneType type2 = new FreightShipPhoneType {
                Number = this.txtShipFromPhone.Text.Trim(),
                Extension = this.txtShipFromExt.Text.Trim()
            };
            ShipFromType type1 = new ShipFromType();
            type1.Address = type;
            type1.AttentionName = this.txtShipFromName.Text.Trim();
            type1.Name = this.txtShipFromName.Text.Trim();
            type1.Phone = type2;
            type1.EMailAddress = this.txtShipFromEmail.Text.Trim();
            return type1;
        }

        private ShipmentType GetShipment()
        {
            ShipCodeDescriptionType type = new ShipCodeDescriptionType {
                Code = "308",
                Description = "UPS Ground Freight"
            };
            ShipCodeDescriptionType type2 = new ShipCodeDescriptionType {
                Code = "SKD",
                Description = "SKID"
            };
            HandlingUnitType type3 = new HandlingUnitType {
                Quantity = "1",
                Type = type2
            };
            ShipmentType type1 = new ShipmentType();
            type1.ShipFrom = this.GetShipFrom();
            type1.ShipTo = this.GetShipTo();
            type1.PaymentInformation = this.GetPaymentInformation();
            type1.Service = type;
            type1.ShipperNumber = this.txtShipperNumber.Text.Trim();
            type1.HandlingUnitOne = type3;
            type1.Commodity = this.GetCommodity();
            return type1;
        }

        private ShipToType GetShipTo()
        {
            List<string> list = new List<string>();
            string item = this.caShipTo.txtAddress1.Text.Trim();
            if (0 < item.Length)
            {
                list.Add(item);
            }
            item = this.caShipTo.txtAddress2.Text.Trim();
            if (0 < item.Length)
            {
                list.Add(item);
            }
            FreightShipAddressType type = new FreightShipAddressType {
                AddressLine = list.ToArray(),
                City = this.caShipTo.txtCity.Text.Trim(),
                StateProvinceCode = this.caShipTo.txtState.Text.Trim(),
                PostalCode = this.caShipTo.txtZip.Text.Trim(),
                CountryCode = "US"
            };
            FreightShipPhoneType type2 = new FreightShipPhoneType {
                Number = this.txtShipToPhone.Text.Trim(),
                Extension = this.txtShipToExt.Text.Trim()
            };
            ShipToType type1 = new ShipToType();
            type1.Address = type;
            type1.AttentionName = this.txtShipToName.Text.Trim();
            type1.Name = this.txtShipToName.Text.Trim();
            type1.Phone = type2;
            type1.EMailAddress = this.txtShipToEmail.Text.Trim();
            return type1;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TabControl1 = new TabControl();
            this.tpShipFrom = new TabPage();
            this.txtShipperNumber = new TextBox();
            this.lblShipperNumber = new Label();
            this.lblShipFromEmail = new Label();
            this.txtShipFromEmail = new TextBox();
            this.lblShipFromExt = new Label();
            this.txtShipFromExt = new TextBox();
            this.lblShipFromPhone = new Label();
            this.txtShipFromPhone = new TextBox();
            this.lblShipFromName = new Label();
            this.txtShipFromName = new TextBox();
            this.caShipFrom = new ControlAddress();
            this.cmbShipFrom = new ComboBox();
            this.lblShipFrom = new Label();
            this.tpShipTo = new TabPage();
            this.lblShipToEmail = new Label();
            this.txtShipToEmail = new TextBox();
            this.lblShipToExt = new Label();
            this.txtShipToExt = new TextBox();
            this.lblShipToPhone = new Label();
            this.txtShipToPhone = new TextBox();
            this.lblShipToName = new Label();
            this.txtShipToName = new TextBox();
            this.cmbShipTo = new ComboBox();
            this.lblShipTo = new Label();
            this.caShipTo = new ControlAddress();
            this.tpPayment = new TabPage();
            this.cmbBillingOption = new ComboBox();
            this.lblBillingOption = new Label();
            this.lblPayerEmail = new Label();
            this.txtPayerEmail = new TextBox();
            this.lblPayerExt = new Label();
            this.txtPayerExt = new TextBox();
            this.lblPayerPhone = new Label();
            this.txtPayerPhone = new TextBox();
            this.lblPayerName = new Label();
            this.txtPayerName = new TextBox();
            this.caPayer = new ControlAddress();
            this.tpCredentials = new TabPage();
            this.txtUpsAccessLicenseNumber = new TextBox();
            this.lblUpsAccessLicenseNumber = new Label();
            this.txtUpsUsername = new TextBox();
            this.lblUpsUsername = new Label();
            this.txtUpsPassword = new TextBox();
            this.lblUpsPassword = new Label();
            this.tpCommodity = new TabPage();
            this.ControlCommodities = new DMEWorks.Forms.Shipping.Ups.ControlCommodities();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            this.ToolTip1 = new ToolTip(this.components);
            this.TabControl1.SuspendLayout();
            this.tpShipFrom.SuspendLayout();
            this.tpShipTo.SuspendLayout();
            this.tpPayment.SuspendLayout();
            this.tpCredentials.SuspendLayout();
            this.tpCommodity.SuspendLayout();
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
            base.SuspendLayout();
            this.TabControl1.Controls.Add(this.tpShipFrom);
            this.TabControl1.Controls.Add(this.tpShipTo);
            this.TabControl1.Controls.Add(this.tpPayment);
            this.TabControl1.Controls.Add(this.tpCredentials);
            this.TabControl1.Controls.Add(this.tpCommodity);
            this.TabControl1.Location = new Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x1d0, 0x150);
            this.TabControl1.TabIndex = 0;
            this.tpShipFrom.Controls.Add(this.txtShipperNumber);
            this.tpShipFrom.Controls.Add(this.lblShipperNumber);
            this.tpShipFrom.Controls.Add(this.lblShipFromEmail);
            this.tpShipFrom.Controls.Add(this.txtShipFromEmail);
            this.tpShipFrom.Controls.Add(this.lblShipFromExt);
            this.tpShipFrom.Controls.Add(this.txtShipFromExt);
            this.tpShipFrom.Controls.Add(this.lblShipFromPhone);
            this.tpShipFrom.Controls.Add(this.txtShipFromPhone);
            this.tpShipFrom.Controls.Add(this.lblShipFromName);
            this.tpShipFrom.Controls.Add(this.txtShipFromName);
            this.tpShipFrom.Controls.Add(this.caShipFrom);
            this.tpShipFrom.Controls.Add(this.cmbShipFrom);
            this.tpShipFrom.Controls.Add(this.lblShipFrom);
            this.tpShipFrom.Location = new Point(4, 0x16);
            this.tpShipFrom.Name = "tpShipFrom";
            this.tpShipFrom.Padding = new Padding(3);
            this.tpShipFrom.Size = new Size(0x1c8, 310);
            this.tpShipFrom.TabIndex = 0;
            this.tpShipFrom.Text = "Ship From";
            this.tpShipFrom.UseVisualStyleBackColor = true;
            this.txtShipperNumber.Location = new Point(0x58, 0xe0);
            this.txtShipperNumber.MaxLength = 6;
            this.txtShipperNumber.Name = "txtShipperNumber";
            this.txtShipperNumber.Size = new Size(0xb0, 20);
            this.txtShipperNumber.TabIndex = 12;
            this.lblShipperNumber.Location = new Point(0x10, 0xe0);
            this.lblShipperNumber.Name = "lblShipperNumber";
            this.lblShipperNumber.Size = new Size(0x40, 0x15);
            this.lblShipperNumber.TabIndex = 11;
            this.lblShipperNumber.Text = "Shipper #";
            this.lblShipperNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblShipFromEmail.Location = new Point(0x10, 0xc0);
            this.lblShipFromEmail.Name = "lblShipFromEmail";
            this.lblShipFromEmail.Size = new Size(0x40, 0x15);
            this.lblShipFromEmail.TabIndex = 9;
            this.lblShipFromEmail.Text = "Email";
            this.lblShipFromEmail.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipFromEmail.Location = new Point(0x58, 0xc0);
            this.txtShipFromEmail.Name = "txtShipFromEmail";
            this.txtShipFromEmail.Size = new Size(360, 20);
            this.txtShipFromEmail.TabIndex = 10;
            this.lblShipFromExt.Location = new Point(0x148, 160);
            this.lblShipFromExt.Name = "lblShipFromExt";
            this.lblShipFromExt.Size = new Size(0x18, 0x15);
            this.lblShipFromExt.TabIndex = 7;
            this.lblShipFromExt.Text = "Ext";
            this.lblShipFromExt.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipFromExt.Location = new Point(360, 160);
            this.txtShipFromExt.Name = "txtShipFromExt";
            this.txtShipFromExt.Size = new Size(0x58, 20);
            this.txtShipFromExt.TabIndex = 8;
            this.lblShipFromPhone.Location = new Point(0x10, 160);
            this.lblShipFromPhone.Name = "lblShipFromPhone";
            this.lblShipFromPhone.Size = new Size(0x40, 0x15);
            this.lblShipFromPhone.TabIndex = 5;
            this.lblShipFromPhone.Text = "Phone";
            this.lblShipFromPhone.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipFromPhone.Location = new Point(0x58, 160);
            this.txtShipFromPhone.Name = "txtShipFromPhone";
            this.txtShipFromPhone.Size = new Size(0xe8, 20);
            this.txtShipFromPhone.TabIndex = 6;
            this.lblShipFromName.Location = new Point(0x10, 0x10);
            this.lblShipFromName.Name = "lblShipFromName";
            this.lblShipFromName.Size = new Size(0x40, 0x15);
            this.lblShipFromName.TabIndex = 0;
            this.lblShipFromName.Text = "Name";
            this.lblShipFromName.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipFromName.Location = new Point(0x58, 0x10);
            this.txtShipFromName.Name = "txtShipFromName";
            this.txtShipFromName.Size = new Size(360, 20);
            this.txtShipFromName.TabIndex = 1;
            this.caShipFrom.Location = new Point(0x10, 80);
            this.caShipFrom.Name = "caShipFrom";
            this.caShipFrom.Size = new Size(0x1b0, 0x48);
            this.caShipFrom.TabIndex = 4;
            this.cmbShipFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbShipFrom.FormattingEnabled = true;
            this.cmbShipFrom.Location = new Point(0x58, 0x30);
            this.cmbShipFrom.Name = "cmbShipFrom";
            this.cmbShipFrom.Size = new Size(240, 0x15);
            this.cmbShipFrom.TabIndex = 3;
            this.lblShipFrom.Location = new Point(0x10, 0x30);
            this.lblShipFrom.Name = "lblShipFrom";
            this.lblShipFrom.Size = new Size(0x40, 0x15);
            this.lblShipFrom.TabIndex = 2;
            this.lblShipFrom.Text = "Address";
            this.lblShipFrom.TextAlign = ContentAlignment.MiddleRight;
            this.tpShipTo.Controls.Add(this.lblShipToEmail);
            this.tpShipTo.Controls.Add(this.txtShipToEmail);
            this.tpShipTo.Controls.Add(this.lblShipToExt);
            this.tpShipTo.Controls.Add(this.txtShipToExt);
            this.tpShipTo.Controls.Add(this.lblShipToPhone);
            this.tpShipTo.Controls.Add(this.txtShipToPhone);
            this.tpShipTo.Controls.Add(this.lblShipToName);
            this.tpShipTo.Controls.Add(this.txtShipToName);
            this.tpShipTo.Controls.Add(this.cmbShipTo);
            this.tpShipTo.Controls.Add(this.lblShipTo);
            this.tpShipTo.Controls.Add(this.caShipTo);
            this.tpShipTo.Location = new Point(4, 0x16);
            this.tpShipTo.Name = "tpShipTo";
            this.tpShipTo.Padding = new Padding(3);
            this.tpShipTo.Size = new Size(0x1c8, 310);
            this.tpShipTo.TabIndex = 1;
            this.tpShipTo.Text = "Ship To";
            this.tpShipTo.UseVisualStyleBackColor = true;
            this.lblShipToEmail.Location = new Point(0x10, 0xc0);
            this.lblShipToEmail.Name = "lblShipToEmail";
            this.lblShipToEmail.Size = new Size(0x40, 0x15);
            this.lblShipToEmail.TabIndex = 9;
            this.lblShipToEmail.Text = "Email";
            this.lblShipToEmail.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipToEmail.Location = new Point(0x58, 0xc0);
            this.txtShipToEmail.Name = "txtShipToEmail";
            this.txtShipToEmail.Size = new Size(360, 20);
            this.txtShipToEmail.TabIndex = 10;
            this.lblShipToExt.Location = new Point(0x148, 160);
            this.lblShipToExt.Name = "lblShipToExt";
            this.lblShipToExt.Size = new Size(0x18, 0x15);
            this.lblShipToExt.TabIndex = 7;
            this.lblShipToExt.Text = "Ext";
            this.lblShipToExt.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipToExt.Location = new Point(360, 160);
            this.txtShipToExt.Name = "txtShipToExt";
            this.txtShipToExt.Size = new Size(0x58, 20);
            this.txtShipToExt.TabIndex = 8;
            this.lblShipToPhone.Location = new Point(0x10, 160);
            this.lblShipToPhone.Name = "lblShipToPhone";
            this.lblShipToPhone.Size = new Size(0x40, 0x15);
            this.lblShipToPhone.TabIndex = 5;
            this.lblShipToPhone.Text = "Phone";
            this.lblShipToPhone.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipToPhone.Location = new Point(0x58, 160);
            this.txtShipToPhone.Name = "txtShipToPhone";
            this.txtShipToPhone.Size = new Size(0xe8, 20);
            this.txtShipToPhone.TabIndex = 6;
            this.lblShipToName.Location = new Point(0x10, 0x10);
            this.lblShipToName.Name = "lblShipToName";
            this.lblShipToName.Size = new Size(0x40, 0x15);
            this.lblShipToName.TabIndex = 0;
            this.lblShipToName.Text = "Name";
            this.lblShipToName.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipToName.Location = new Point(0x58, 0x10);
            this.txtShipToName.Name = "txtShipToName";
            this.txtShipToName.Size = new Size(360, 20);
            this.txtShipToName.TabIndex = 1;
            this.cmbShipTo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbShipTo.FormattingEnabled = true;
            this.cmbShipTo.Location = new Point(0x58, 0x30);
            this.cmbShipTo.Name = "cmbShipTo";
            this.cmbShipTo.Size = new Size(240, 0x15);
            this.cmbShipTo.TabIndex = 3;
            this.lblShipTo.Location = new Point(0x10, 0x30);
            this.lblShipTo.Name = "lblShipTo";
            this.lblShipTo.Size = new Size(0x40, 0x15);
            this.lblShipTo.TabIndex = 2;
            this.lblShipTo.Text = "Address";
            this.lblShipTo.TextAlign = ContentAlignment.MiddleRight;
            this.caShipTo.Location = new Point(0x10, 80);
            this.caShipTo.Name = "caShipTo";
            this.caShipTo.Size = new Size(0x1b0, 0x48);
            this.caShipTo.TabIndex = 4;
            this.tpPayment.Controls.Add(this.cmbBillingOption);
            this.tpPayment.Controls.Add(this.lblBillingOption);
            this.tpPayment.Controls.Add(this.lblPayerEmail);
            this.tpPayment.Controls.Add(this.txtPayerEmail);
            this.tpPayment.Controls.Add(this.lblPayerExt);
            this.tpPayment.Controls.Add(this.txtPayerExt);
            this.tpPayment.Controls.Add(this.lblPayerPhone);
            this.tpPayment.Controls.Add(this.txtPayerPhone);
            this.tpPayment.Controls.Add(this.lblPayerName);
            this.tpPayment.Controls.Add(this.txtPayerName);
            this.tpPayment.Controls.Add(this.caPayer);
            this.tpPayment.Location = new Point(4, 0x16);
            this.tpPayment.Name = "tpPayment";
            this.tpPayment.Padding = new Padding(3);
            this.tpPayment.Size = new Size(0x1c8, 310);
            this.tpPayment.TabIndex = 2;
            this.tpPayment.Text = "Payment";
            this.tpPayment.UseVisualStyleBackColor = true;
            this.cmbBillingOption.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbBillingOption.Location = new Point(0x58, 0x30);
            this.cmbBillingOption.Name = "cmbBillingOption";
            this.cmbBillingOption.Size = new Size(240, 0x15);
            this.cmbBillingOption.TabIndex = 3;
            this.lblBillingOption.Location = new Point(8, 0x30);
            this.lblBillingOption.Name = "lblBillingOption";
            this.lblBillingOption.Size = new Size(0x48, 0x15);
            this.lblBillingOption.TabIndex = 2;
            this.lblBillingOption.Text = "Billing Option";
            this.lblBillingOption.TextAlign = ContentAlignment.MiddleRight;
            this.lblPayerEmail.Location = new Point(8, 0xc0);
            this.lblPayerEmail.Name = "lblPayerEmail";
            this.lblPayerEmail.Size = new Size(0x48, 0x15);
            this.lblPayerEmail.TabIndex = 9;
            this.lblPayerEmail.Text = "Email";
            this.lblPayerEmail.TextAlign = ContentAlignment.MiddleRight;
            this.txtPayerEmail.Location = new Point(0x58, 0xc0);
            this.txtPayerEmail.Name = "txtPayerEmail";
            this.txtPayerEmail.Size = new Size(360, 20);
            this.txtPayerEmail.TabIndex = 10;
            this.lblPayerExt.Location = new Point(0x148, 160);
            this.lblPayerExt.Name = "lblPayerExt";
            this.lblPayerExt.Size = new Size(0x18, 0x15);
            this.lblPayerExt.TabIndex = 7;
            this.lblPayerExt.Text = "Ext";
            this.lblPayerExt.TextAlign = ContentAlignment.MiddleRight;
            this.txtPayerExt.Location = new Point(360, 160);
            this.txtPayerExt.Name = "txtPayerExt";
            this.txtPayerExt.Size = new Size(0x58, 20);
            this.txtPayerExt.TabIndex = 8;
            this.lblPayerPhone.Location = new Point(8, 160);
            this.lblPayerPhone.Name = "lblPayerPhone";
            this.lblPayerPhone.Size = new Size(0x48, 0x15);
            this.lblPayerPhone.TabIndex = 5;
            this.lblPayerPhone.Text = "Phone";
            this.lblPayerPhone.TextAlign = ContentAlignment.MiddleRight;
            this.txtPayerPhone.Location = new Point(0x58, 160);
            this.txtPayerPhone.Name = "txtPayerPhone";
            this.txtPayerPhone.Size = new Size(0xe8, 20);
            this.txtPayerPhone.TabIndex = 6;
            this.lblPayerName.Location = new Point(8, 0x10);
            this.lblPayerName.Name = "lblPayerName";
            this.lblPayerName.Size = new Size(0x48, 0x15);
            this.lblPayerName.TabIndex = 0;
            this.lblPayerName.Text = "Name";
            this.lblPayerName.TextAlign = ContentAlignment.MiddleRight;
            this.txtPayerName.Location = new Point(0x58, 0x10);
            this.txtPayerName.Name = "txtPayerName";
            this.txtPayerName.Size = new Size(360, 20);
            this.txtPayerName.TabIndex = 1;
            this.caPayer.Location = new Point(0x10, 80);
            this.caPayer.Name = "caPayer";
            this.caPayer.Size = new Size(0x1b0, 0x48);
            this.caPayer.TabIndex = 4;
            this.tpCredentials.Controls.Add(this.txtUpsAccessLicenseNumber);
            this.tpCredentials.Controls.Add(this.lblUpsAccessLicenseNumber);
            this.tpCredentials.Controls.Add(this.txtUpsUsername);
            this.tpCredentials.Controls.Add(this.lblUpsUsername);
            this.tpCredentials.Controls.Add(this.txtUpsPassword);
            this.tpCredentials.Controls.Add(this.lblUpsPassword);
            this.tpCredentials.Location = new Point(4, 0x16);
            this.tpCredentials.Name = "tpCredentials";
            this.tpCredentials.Padding = new Padding(3);
            this.tpCredentials.Size = new Size(0x1c8, 310);
            this.tpCredentials.TabIndex = 3;
            this.tpCredentials.Text = "Credentials";
            this.tpCredentials.UseVisualStyleBackColor = true;
            this.txtUpsAccessLicenseNumber.Location = new Point(120, 80);
            this.txtUpsAccessLicenseNumber.Name = "txtUpsAccessLicenseNumber";
            this.txtUpsAccessLicenseNumber.Size = new Size(0xb0, 20);
            this.txtUpsAccessLicenseNumber.TabIndex = 5;
            this.lblUpsAccessLicenseNumber.Location = new Point(8, 80);
            this.lblUpsAccessLicenseNumber.Name = "lblUpsAccessLicenseNumber";
            this.lblUpsAccessLicenseNumber.Size = new Size(0x68, 0x20);
            this.lblUpsAccessLicenseNumber.TabIndex = 4;
            this.lblUpsAccessLicenseNumber.Text = "Access License Number";
            this.lblUpsAccessLicenseNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtUpsUsername.Location = new Point(120, 0x10);
            this.txtUpsUsername.Name = "txtUpsUsername";
            this.txtUpsUsername.Size = new Size(0xb0, 20);
            this.txtUpsUsername.TabIndex = 1;
            this.lblUpsUsername.Location = new Point(8, 0x10);
            this.lblUpsUsername.Name = "lblUpsUsername";
            this.lblUpsUsername.Size = new Size(0x68, 0x15);
            this.lblUpsUsername.TabIndex = 0;
            this.lblUpsUsername.Text = "My Ups Username";
            this.lblUpsUsername.TextAlign = ContentAlignment.MiddleRight;
            this.txtUpsPassword.Location = new Point(120, 0x30);
            this.txtUpsPassword.Name = "txtUpsPassword";
            this.txtUpsPassword.Size = new Size(0xb0, 20);
            this.txtUpsPassword.TabIndex = 3;
            this.lblUpsPassword.Location = new Point(8, 0x30);
            this.lblUpsPassword.Name = "lblUpsPassword";
            this.lblUpsPassword.Size = new Size(0x68, 0x15);
            this.lblUpsPassword.TabIndex = 2;
            this.lblUpsPassword.Text = "My Ups Password";
            this.lblUpsPassword.TextAlign = ContentAlignment.MiddleRight;
            this.tpCommodity.Controls.Add(this.ControlCommodities);
            this.tpCommodity.Location = new Point(4, 0x16);
            this.tpCommodity.Name = "tpCommodity";
            this.tpCommodity.Padding = new Padding(3);
            this.tpCommodity.Size = new Size(0x1c8, 310);
            this.tpCommodity.TabIndex = 4;
            this.tpCommodity.Text = "Commodity";
            this.tpCommodity.UseVisualStyleBackColor = true;
            this.ControlCommodities.Dock = DockStyle.Fill;
            this.ControlCommodities.Location = new Point(3, 3);
            this.ControlCommodities.Name = "ControlCommodities";
            this.ControlCommodities.Size = new Size(450, 0x130);
            this.ControlCommodities.TabIndex = 0;
            this.btnCancel.Location = new Point(0x188, 0x160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(0x138, 0x160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.ErrorProvider1.ContainerControl = this;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(480, 0x180);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.TabControl1);
            base.Name = "FormUpsShipping";
            this.Text = "Ups Shipping";
            this.TabControl1.ResumeLayout(false);
            this.tpShipFrom.ResumeLayout(false);
            this.tpShipFrom.PerformLayout();
            this.tpShipTo.ResumeLayout(false);
            this.tpShipTo.PerformLayout();
            this.tpPayment.ResumeLayout(false);
            this.tpPayment.PerformLayout();
            this.tpCredentials.ResumeLayout(false);
            this.tpCredentials.PerformLayout();
            this.tpCommodity.ResumeLayout(false);
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            base.ResumeLayout(false);
        }

        private void InitializeService(FreightShipService service)
        {
            UPSSecurityServiceAccessToken token = new UPSSecurityServiceAccessToken {
                AccessLicenseNumber = this.txtUpsAccessLicenseNumber.Text.Trim()
            };
            UPSSecurityUsernameToken token2 = new UPSSecurityUsernameToken {
                Username = this.txtUpsUsername.Text.Trim(),
                Password = this.txtUpsPassword.Text.Trim()
            };
            UPSSecurity security = new UPSSecurity {
                ServiceAccessToken = token,
                UsernameToken = token2
            };
            service.Url = "https://wwwcie.ups.com/webservices/FreightShip";
            service.UPSSecurityValue = security;
        }

        private void LoadCompany()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT Name, Phone\r\nFROM tbl_company\r\nWHERE ID = 1";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.txtShipFromName.Text = reader["Name"] as string;
                            this.txtShipFromPhone.Text = ExtractPhone(reader["Phone"] as string);
                        }
                        else
                        {
                            this.txtShipFromName.Text = "";
                            this.txtShipFromPhone.Text = "";
                        }
                    }
                }
            }
        }

        private void LoadCustomer()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT CONCAT(FirstName, ' ', LastName) as Name, Phone\r\nFROM tbl_customer\r\nWHERE ID = :CustomerID";
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.txtShipToName.Text = reader["Name"] as string;
                            this.txtShipToPhone.Text = ExtractPhone(reader["Phone"] as string);
                        }
                        else
                        {
                            this.txtShipToName.Text = "";
                            this.txtShipToPhone.Text = "";
                        }
                    }
                }
            }
        }

        private void LoadDropdowns()
        {
            DataTable dataTable = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandText = "SELECT Display, Address1, Address2, City, State, Zip\r\nFROM (SELECT\r\n        0 as `Index`\r\n      , 'Company' as Display\r\n      , Address1, Address2, City, State, Zip\r\n      FROM tbl_company\r\n      WHERE ID = 1\r\n      UNION ALL\r\n      SELECT\r\n        1 as `Index`\r\n      , CONCAT('Location: ', Name) as Display\r\n      , Address1, Address2, City, State, Zip\r\n      FROM tbl_location\r\n      UNION ALL\r\n      SELECT\r\n        2 as `Index`\r\n      , CONCAT('Warehouse: ', Name) as Display\r\n      , Address1, Address2, City, State, Zip\r\n      FROM tbl_warehouse\r\n      UNION ALL\r\n      SELECT\r\n        3 as `Index`\r\n      , 'Custom' as Display\r\n      , '' as Address1, '' as Address2, '' as City, '' as State, '' as Zip\r\n      FROM dual\r\n) as tmp\r\nORDER BY `Index`, Display";
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.cmbShipFrom.DataSource = new DataView(dataTable);
            this.cmbShipFrom.DisplayMember = "Display";
            DataTable table2 = new DataTable();
            using (MySqlDataAdapter adapter2 = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter2.SelectCommand.CommandText = "SELECT\r\n  'Main' as Display\r\n, Address1 as Address1\r\n, Address2 as Address2\r\n, City     as City\r\n, State    as State\r\n, Zip      as Zip\r\nFROM tbl_customer\r\nWHERE ID = :CustomerID\r\nUNION ALL\r\nSELECT\r\n  'Shipping' as Display\r\n, ShipAddress1 as Address1\r\n, ShipAddress2 as Address2\r\n, ShipCity     as City\r\n, ShipState    as State\r\n, ShipZip      as Zip\r\nFROM tbl_customer\r\nWHERE ID = :CustomerID\r\n  AND ShipActive = 1\r\nUNION ALL\r\nSELECT\r\n  'Billing' as Display\r\n, BillAddress1 as Address1\r\n, BillAddress2 as Address2\r\n, BillCity     as City\r\n, BillState    as State\r\n, BillZip      as Zip\r\nFROM tbl_customer\r\nWHERE ID = :CustomerID\r\n  AND BillActive = 1\r\nUNION ALL\r\nSELECT\r\n  'Custom' as Display\r\n, '' as Address1\r\n, '' as Address2\r\n, '' as City\r\n, '' as State\r\n, '' as Zip\r\nFROM tbl_customer\r\nWHERE ID = :CustomerID";
                adapter2.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                adapter2.AcceptChangesDuringFill = true;
                adapter2.Fill(table2);
            }
            this.cmbShipTo.DataSource = new DataView(table2);
            this.cmbShipTo.DisplayMember = "Display";
            Cache.InitDropdown(this.cmbBillingOption, "tbl_upsshipping_billingoption", null);
        }

        [field: AccessedThroughProperty("lblShipFrom")]
        private Label lblShipFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbShipTo")]
        private ComboBox cmbShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipTo")]
        private Label lblShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUpsUsername")]
        private Label lblUpsUsername { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUpsPassword")]
        private Label lblUpsPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbShipFrom")]
        private ComboBox cmbShipFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpShipTo")]
        private TabPage tpShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpShipFrom")]
        private TabPage tpShipFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpCredentials")]
        private TabPage tpCredentials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caShipTo")]
        private ControlAddress caShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpCommodity")]
        private TabPage tpCommodity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipFromName")]
        private Label lblShipFromName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToName")]
        private Label lblShipToName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipToName")]
        private TextBox txtShipToName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipFromExt")]
        private Label lblShipFromExt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipFromPhone")]
        private Label lblShipFromPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipFromEmail")]
        private Label lblShipFromEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToEmail")]
        private Label lblShipToEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipToEmail")]
        private TextBox txtShipToEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToExt")]
        private Label lblShipToExt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipToExt")]
        private TextBox txtShipToExt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToPhone")]
        private Label lblShipToPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipToPhone")]
        private TextBox txtShipToPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpPayment")]
        private TabPage tpPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPayerEmail")]
        private Label lblPayerEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPayerEmail")]
        private TextBox txtPayerEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPayerExt")]
        private Label lblPayerExt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPayerExt")]
        private TextBox txtPayerExt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPayerPhone")]
        private Label lblPayerPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPayerPhone")]
        private TextBox txtPayerPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPayerName")]
        private Label lblPayerName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPayerName")]
        private TextBox txtPayerName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caPayer")]
        private ControlAddress caPayer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlCommodities")]
        private DMEWorks.Forms.Shipping.Ups.ControlCommodities ControlCommodities { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBillingOption")]
        private ComboBox cmbBillingOption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillingOption")]
        private Label lblBillingOption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUpsUsername")]
        private TextBox txtUpsUsername { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUpsPassword")]
        private TextBox txtUpsPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caShipFrom")]
        private ControlAddress caShipFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipFromName")]
        private TextBox txtShipFromName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipFromExt")]
        private TextBox txtShipFromExt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipFromPhone")]
        private TextBox txtShipFromPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipFromEmail")]
        private TextBox txtShipFromEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        private ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolTip1")]
        private ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUpsAccessLicenseNumber")]
        private TextBox txtUpsAccessLicenseNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUpsAccessLicenseNumber")]
        private Label lblUpsAccessLicenseNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipperNumber")]
        private TextBox txtShipperNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipperNumber")]
        private Label lblShipperNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

