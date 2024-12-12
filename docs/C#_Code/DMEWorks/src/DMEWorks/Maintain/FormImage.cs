namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using DMEWorks.Forms.Printing;
    using DMEWorks.Imaging;
    using DMEWorks.Printing;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormImage : FormAutoIncrementMaintain
    {
        private IContainer components;
        private DataTable F_Order = null;
        private DataTable F_Invoice = null;
        private DataTable F_CmnForm = null;
        private FileWrapper Wrapper = null;

        public FormImage()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_image", "tbl_customer", "tbl_doctor", "tbl_cmnform" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.cmbCustomer);
            base.ChangesTracker.Subscribe(this.cmbDoctor);
            base.ChangesTracker.Subscribe(this.txtCMNForm);
            base.ChangesTracker.Subscribe(this.txtDescription);
            base.ChangesTracker.Subscribe(this.txtInvoice);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtOrder);
            base.ChangesTracker.Subscribe(this.txtType);
        }

        private void btnCMNForm_Click(object sender, EventArgs e)
        {
            using (FormSelector selector = new FormSelector())
            {
                selector.DataSource = this.Table_CmnForm.ToGridSource();
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    DataRow dataRow = selector.SelectedRow.GetDataRow();
                    if (dataRow != null)
                    {
                        Functions.SetTextBoxText(this.txtCMNForm, dataRow["ID"]);
                        if (base.GetConfirmation("Would you like to assign Customer and Doctor as well?"))
                        {
                            this.cmbCustomer.SelectedValue = dataRow["CustomerID"];
                            this.cmbDoctor.SelectedValue = dataRow["DoctorID"];
                        }
                    }
                }
            }
        }

        private void btnImageChange_Click(object sender, EventArgs e)
        {
            FilterEntry[] loadFilterEntries = FileWrapper.LoadFilterEntries;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = string.Join<FilterEntry>("|", loadFilterEntries);
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Wrapper = FileWrapper.FromFile(dialog.FileName);
                        this.Thumbnail = this.Wrapper.Thumbnail;
                        base.OnObjectChanged(this);
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
            }
        }

        private void btnImageEnlarge_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnsureBlob();
                ImageWrapper wrapper = this.Wrapper as ImageWrapper;
                if (wrapper != null)
                {
                    FormImageViewer viewer1 = new FormImageViewer(wrapper.Image);
                    viewer1.MdiParent = ClassGlobalObjects.frmMain;
                    viewer1.Show();
                }
                else
                {
                    PdfWrapper wrapper2 = this.Wrapper as PdfWrapper;
                    if (wrapper2 != null)
                    {
                        FormPdfViewer viewer2 = new FormPdfViewer(wrapper2.Data);
                        viewer2.MdiParent = ClassGlobalObjects.frmMain;
                        viewer2.Show();
                    }
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

        private void btnImageExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnsureBlob();
                FileWrapper wrapper = this.Wrapper;
                if (wrapper != null)
                {
                    FilterEntry[] saveFilterEntries = wrapper.SaveFilterEntries;
                    using (SaveFileDialog dialog = new SaveFileDialog())
                    {
                        dialog.AddExtension = true;
                        dialog.Filter = string.Join<FilterEntry>("|", saveFilterEntries);
                        dialog.FilterIndex = 1;
                        dialog.DefaultExt = wrapper.DefaultExt;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            wrapper.Save(dialog.FileName);
                        }
                    }
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

        private void btnImagePrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnsureBlob();
                ImageWrapper wrapper = this.Wrapper as ImageWrapper;
                if (wrapper != null)
                {
                    new Thread(new ThreadStart(new ImagePrinter(wrapper).Execute)).Start();
                }
                else
                {
                    PdfWrapper wrapper2 = this.Wrapper as PdfWrapper;
                    if (wrapper2 != null)
                    {
                        FormPdfViewer viewer1 = new FormPdfViewer(wrapper2.Data);
                        viewer1.MdiParent = ClassGlobalObjects.frmMain;
                        viewer1.Show();
                    }
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

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            using (FormSelector selector = new FormSelector())
            {
                selector.DataSource = this.Table_Invoice.ToGridSource();
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    DataRow dataRow = selector.SelectedRow.GetDataRow();
                    if (dataRow != null)
                    {
                        Functions.SetTextBoxText(this.txtInvoice, dataRow["ID"]);
                        if (base.GetConfirmation("Would you like to assign Customer and Order as well?"))
                        {
                            this.cmbCustomer.SelectedValue = dataRow["CustomerID"];
                            Functions.SetTextBoxText(this.txtOrder, dataRow["OrderID"]);
                        }
                    }
                }
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            using (FormSelector selector = new FormSelector())
            {
                selector.DataSource = this.Table_Order.ToGridSource();
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    DataRow dataRow = selector.SelectedRow.GetDataRow();
                    if (dataRow != null)
                    {
                        Functions.SetTextBoxText(this.txtOrder, dataRow["ID"]);
                        if (base.GetConfirmation("Would you like to assign Customer as well?"))
                        {
                            this.cmbCustomer.SelectedValue = dataRow["CustomerID"];
                        }
                    }
                }
            }
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.txtType, DBNull.Value);
            Functions.SetTextBoxText(this.txtDescription, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbDoctor, DBNull.Value);
            Functions.SetTextBoxText(this.txtOrder, DBNull.Value);
            Functions.SetTextBoxText(this.txtInvoice, DBNull.Value);
            Functions.SetTextBoxText(this.txtCMNForm, DBNull.Value);
            this.Thumbnail = null;
            this.Wrapper = null;
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "DELETE FROM tbl_image WHERE (ID = :ID)";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteNonQuery())
                    {
                        throw new ObjectIsNotFoundException();
                    }
                    try
                    {
                        new ImagingHelper(Globals.CompanyImagingUri).DelImage(Globals.CompanyDatabase, ID);
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        TraceHelper.TraceException(ex);
                        ProjectData.ClearProjectError();
                    }
                }
            }
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

        private void EnsureBlob()
        {
            if ((this.Wrapper == null) && (Globals.CompanyImagingUri != null))
            {
                int? nullable = NullableConvert.ToInt32(this.ObjectID);
                if (nullable != null)
                {
                    byte[] image = new ImagingHelper(Globals.CompanyImagingUri).GetImage(Globals.CompanyDatabase, nullable.Value);
                    this.Wrapper = FileWrapper.FromData(image);
                }
            }
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete image #{this.ObjectID}?";
            messages.DeletedSuccessfully = $"Image #{this.ObjectID} was successfully deleted.";
            return messages;
        }

        public static Image ImageFromBlob(object value, Image Default = null)
        {
            Image image;
            try
            {
                image = Image.FromStream(new MemoryStream((byte[]) value));
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                image = Default;
                ProjectData.ClearProjectError();
            }
            return image;
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbDoctor, "tbl_doctor", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormImage));
            this.cmbCustomer = new Combobox();
            this.lblCustomer = new Label();
            this.lblCMNForm = new Label();
            this.pbLogo = new PictureBox();
            this.gbRelations = new GroupBox();
            this.btnCMNForm = new Button();
            this.btnInvoice = new Button();
            this.btnOrder = new Button();
            this.txtCMNForm = new TextBox();
            this.txtInvoice = new TextBox();
            this.txtOrder = new TextBox();
            this.lblInvoice = new Label();
            this.lblOrder = new Label();
            this.cmbDoctor = new Combobox();
            this.lblDoctor = new Label();
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.btnImageChange = new Button();
            this.lblID = new Label();
            this.gbImage = new GroupBox();
            this.btnImagePrint = new Button();
            this.lblType = new Label();
            this.txtType = new TextBox();
            this.btnImageExport = new Button();
            this.btnImageEnlarge = new Button();
            this.mnuGotoImages = new MenuItem();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.pbLogo).BeginInit();
            this.gbRelations.SuspendLayout();
            this.gbImage.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.gbImage);
            base.tpWorkArea.Controls.Add(this.gbRelations);
            base.tpWorkArea.Size = new Size(0x220, 330);
            MenuItem[] items = new MenuItem[] { this.mnuGotoImages };
            base.cmnuGoto.MenuItems.AddRange(items);
            this.cmbCustomer.Location = new Point(0x58, 0x10);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0xd8, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.lblCustomer.Location = new Point(8, 0x10);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(0x48, 20);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.lblCMNForm.Location = new Point(8, 0x70);
            this.lblCMNForm.Name = "lblCMNForm";
            this.lblCMNForm.Size = new Size(0x48, 20);
            this.lblCMNForm.TabIndex = 10;
            this.lblCMNForm.Text = "CMN #";
            this.lblCMNForm.TextAlign = ContentAlignment.MiddleRight;
            this.pbLogo.BackColor = Color.FromArgb(0xff, 240, 0xff);
            this.pbLogo.BorderStyle = BorderStyle.FixedSingle;
            this.pbLogo.Location = new Point(400, 0x10);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new Size(0x7a, 0x5c);
            this.pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0x4b;
            this.pbLogo.TabStop = false;
            this.gbRelations.Controls.Add(this.btnCMNForm);
            this.gbRelations.Controls.Add(this.btnInvoice);
            this.gbRelations.Controls.Add(this.btnOrder);
            this.gbRelations.Controls.Add(this.txtCMNForm);
            this.gbRelations.Controls.Add(this.txtInvoice);
            this.gbRelations.Controls.Add(this.txtOrder);
            this.gbRelations.Controls.Add(this.lblInvoice);
            this.gbRelations.Controls.Add(this.lblOrder);
            this.gbRelations.Controls.Add(this.cmbDoctor);
            this.gbRelations.Controls.Add(this.lblDoctor);
            this.gbRelations.Controls.Add(this.cmbCustomer);
            this.gbRelations.Controls.Add(this.lblCustomer);
            this.gbRelations.Controls.Add(this.lblCMNForm);
            this.gbRelations.Location = new Point(8, 0xb0);
            this.gbRelations.Name = "gbRelations";
            this.gbRelations.Size = new Size(0x210, 0x90);
            this.gbRelations.TabIndex = 1;
            this.gbRelations.TabStop = false;
            this.gbRelations.Text = "Relations";
            this.btnCMNForm.FlatStyle = FlatStyle.Flat;
            this.btnCMNForm.Image = (Image) manager.GetObject("btnCMNForm.Image");
            this.btnCMNForm.Location = new Point(0xe0, 0x70);
            this.btnCMNForm.Name = "btnCMNForm";
            this.btnCMNForm.Size = new Size(0x15, 0x15);
            this.btnCMNForm.TabIndex = 12;
            this.btnInvoice.FlatStyle = FlatStyle.Flat;
            this.btnInvoice.Image = (Image) manager.GetObject("btnInvoice.Image");
            this.btnInvoice.Location = new Point(0xe0, 0x58);
            this.btnInvoice.Margin = new Padding(0);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new Size(0x15, 0x15);
            this.btnInvoice.TabIndex = 9;
            this.btnOrder.FlatStyle = FlatStyle.Flat;
            this.btnOrder.Image = My.Resources.Resources.ImageSpyglass;
            this.btnOrder.Location = new Point(0xe0, 0x40);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new Size(0x15, 0x15);
            this.btnOrder.TabIndex = 6;
            this.txtCMNForm.Location = new Point(0x58, 0x70);
            this.txtCMNForm.Name = "txtCMNForm";
            this.txtCMNForm.Size = new Size(0x80, 20);
            this.txtCMNForm.TabIndex = 11;
            this.txtInvoice.Location = new Point(0x58, 0x58);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new Size(0x80, 20);
            this.txtInvoice.TabIndex = 8;
            this.txtOrder.Location = new Point(0x58, 0x40);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new Size(0x80, 20);
            this.txtOrder.TabIndex = 5;
            this.lblInvoice.Location = new Point(8, 0x58);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new Size(0x48, 20);
            this.lblInvoice.TabIndex = 7;
            this.lblInvoice.Text = "Invoice #";
            this.lblInvoice.TextAlign = ContentAlignment.MiddleRight;
            this.lblOrder.Location = new Point(8, 0x40);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new Size(0x48, 20);
            this.lblOrder.TabIndex = 4;
            this.lblOrder.Text = "Order #";
            this.lblOrder.TextAlign = ContentAlignment.MiddleRight;
            this.cmbDoctor.Location = new Point(0x58, 40);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new Size(0xd8, 0x15);
            this.cmbDoctor.TabIndex = 3;
            this.lblDoctor.Location = new Point(8, 40);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new Size(0x48, 20);
            this.lblDoctor.TabIndex = 2;
            this.lblDoctor.Text = "Doctor";
            this.lblDoctor.TextAlign = ContentAlignment.MiddleRight;
            this.lblName.Location = new Point(8, 0x10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x40, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.txtName.BorderStyle = BorderStyle.FixedSingle;
            this.txtName.Location = new Point(80, 0x10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xe8, 20);
            this.txtName.TabIndex = 1;
            this.lblDescription.Location = new Point(8, 0x40);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x40, 20);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = ContentAlignment.MiddleRight;
            this.txtDescription.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescription.Location = new Point(80, 0x40);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x138, 0x58);
            this.txtDescription.TabIndex = 6;
            this.btnImageChange.FlatStyle = FlatStyle.Flat;
            this.btnImageChange.Image = My.Resources.Resources.ImageOpen;
            this.btnImageChange.Location = new Point(400, 0x70);
            this.btnImageChange.Name = "btnImageChange";
            this.btnImageChange.Size = new Size(0x15, 0x15);
            this.btnImageChange.TabIndex = 7;
            base.ToolTip1.SetToolTip(this.btnImageChange, "Change Image");
            this.btnImageChange.UseVisualStyleBackColor = true;
            this.lblID.BorderStyle = BorderStyle.FixedSingle;
            this.lblID.Location = new Point(320, 0x10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new Size(0x48, 20);
            this.lblID.TabIndex = 2;
            this.lblID.Text = "Image #";
            this.lblID.TextAlign = ContentAlignment.MiddleCenter;
            this.gbImage.Controls.Add(this.btnImagePrint);
            this.gbImage.Controls.Add(this.lblType);
            this.gbImage.Controls.Add(this.txtType);
            this.gbImage.Controls.Add(this.btnImageExport);
            this.gbImage.Controls.Add(this.btnImageEnlarge);
            this.gbImage.Controls.Add(this.lblName);
            this.gbImage.Controls.Add(this.lblID);
            this.gbImage.Controls.Add(this.pbLogo);
            this.gbImage.Controls.Add(this.btnImageChange);
            this.gbImage.Controls.Add(this.txtName);
            this.gbImage.Controls.Add(this.txtDescription);
            this.gbImage.Controls.Add(this.lblDescription);
            this.gbImage.Location = new Point(8, 8);
            this.gbImage.Name = "gbImage";
            this.gbImage.Size = new Size(0x210, 160);
            this.gbImage.TabIndex = 0;
            this.gbImage.TabStop = false;
            this.gbImage.Text = "Image";
            this.btnImagePrint.FlatStyle = FlatStyle.Flat;
            this.btnImagePrint.Image = My.Resources.Resources.ImagePrint;
            this.btnImagePrint.Location = new Point(0x1d8, 0x70);
            this.btnImagePrint.Name = "btnImagePrint";
            this.btnImagePrint.Size = new Size(0x15, 0x15);
            this.btnImagePrint.TabIndex = 10;
            base.ToolTip1.SetToolTip(this.btnImagePrint, "Print");
            this.btnImagePrint.UseVisualStyleBackColor = true;
            this.lblType.Location = new Point(8, 40);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(0x40, 20);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = ContentAlignment.MiddleRight;
            this.txtType.BorderStyle = BorderStyle.FixedSingle;
            this.txtType.Location = new Point(80, 40);
            this.txtType.Name = "txtType";
            this.txtType.Size = new Size(0xe8, 20);
            this.txtType.TabIndex = 4;
            this.btnImageExport.FlatStyle = FlatStyle.Flat;
            this.btnImageExport.Image = My.Resources.Resources.ImageSave;
            this.btnImageExport.Location = new Point(0x1c0, 0x70);
            this.btnImageExport.Name = "btnImageExport";
            this.btnImageExport.Size = new Size(0x15, 0x15);
            this.btnImageExport.TabIndex = 9;
            base.ToolTip1.SetToolTip(this.btnImageExport, "Export");
            this.btnImageExport.UseVisualStyleBackColor = true;
            this.btnImageEnlarge.FlatStyle = FlatStyle.Flat;
            this.btnImageEnlarge.Image = My.Resources.Resources.ImageEnlarge;
            this.btnImageEnlarge.Location = new Point(0x1a8, 0x70);
            this.btnImageEnlarge.Name = "btnImageEnlarge";
            this.btnImageEnlarge.Size = new Size(0x15, 0x15);
            this.btnImageEnlarge.TabIndex = 8;
            base.ToolTip1.SetToolTip(this.btnImageEnlarge, "Show fullsize");
            this.btnImageEnlarge.UseVisualStyleBackColor = true;
            this.mnuGotoImages.Index = 0;
            this.mnuGotoImages.Text = "Search Images";
            base.ClientSize = new Size(0x228, 0x18d);
            base.Name = "FormImage";
            this.Text = "Maintain Images";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) this.pbLogo).EndInit();
            this.gbRelations.ResumeLayout(false);
            this.gbRelations.PerformLayout();
            this.gbImage.ResumeLayout(false);
            this.gbImage.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [HandleDatabaseChanged("tbl_cmnform")]
        private void Invalidate_Table_CmnForm()
        {
            this.Table_CmnForm = null;
        }

        [HandleDatabaseChanged("tbl_invoice")]
        private void Invalidate_Table_Invoice()
        {
            this.Table_Invoice = null;
        }

        [HandleDatabaseChanged("tbl_order")]
        private void Invalidate_Table_Order()
        {
            this.Table_Order = null;
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  ID\r\n, Name\r\n, Type\r\n, Description\r\n, CustomerID\r\n, OrderID\r\n, InvoiceID\r\n, DoctorID\r\n, CMNFormID\r\n, Thumbnail\r\nFROM tbl_image\r\nWHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.txtType, reader["Type"]);
                            Functions.SetTextBoxText(this.txtDescription, reader["Description"]);
                            Functions.SetComboBoxValue(this.cmbCustomer, reader["CustomerID"]);
                            Functions.SetComboBoxValue(this.cmbDoctor, reader["DoctorID"]);
                            Functions.SetTextBoxText(this.txtOrder, reader["OrderID"]);
                            Functions.SetTextBoxText(this.txtInvoice, reader["InvoiceID"]);
                            Functions.SetTextBoxText(this.txtCMNForm, reader["CMNFormID"]);
                            this.Thumbnail = ImageFromBlob(reader["Thumbnail"], null);
                            this.Wrapper = null;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void mnuGotoImages_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["InvoiceID"] = this.txtInvoice.Text,
                ["OrderID"] = this.txtOrder.Text,
                ["CMNFormID"] = this.txtCMNForm.Text,
                ["CustomerID"] = this.cmbCustomer.SelectedValue,
                ["DoctorID"] = this.cmbDoctor.SelectedValue
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImageSearch(), @params);
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_image" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected void ProcessParameter_Other(FormParameters Params)
        {
            if (Params != null)
            {
                if (Params.ContainsKey("ID"))
                {
                    base.OpenObject(Params["ID"]);
                }
                else
                {
                    Functions.SetTextBoxText(this.txtInvoice, NullableConvert.ToDb(NullableConvert.ToInt32(Params["InvoiceID"])));
                    Functions.SetTextBoxText(this.txtOrder, NullableConvert.ToDb(NullableConvert.ToInt32(Params["OrderID"])));
                    Functions.SetTextBoxText(this.txtCMNForm, NullableConvert.ToDb(NullableConvert.ToInt32(Params["CMNFormID"])));
                    Functions.SetComboBoxValue(this.cmbCustomer, NullableConvert.ToDb(NullableConvert.ToInt32(Params["CustomerID"])));
                    Functions.SetComboBoxValue(this.cmbDoctor, NullableConvert.ToDb(NullableConvert.ToInt32(Params["DoctorID"])));
                }
            }
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Name", MySqlType.VarChar).Value = this.txtName.Text;
                    command.Parameters.Add("Type", MySqlType.VarChar).Value = this.txtType.Text;
                    command.Parameters.Add("Description", MySqlType.Text).Value = this.txtDescription.Text;
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = NullableConvert.ToDb(NullableConvert.ToInt32(this.cmbCustomer.SelectedValue));
                    command.Parameters.Add("OrderID", MySqlType.Int).Value = NullableConvert.ToDb(NullableConvert.ToInt32(this.txtOrder.Text));
                    command.Parameters.Add("InvoiceID", MySqlType.Int).Value = NullableConvert.ToDb(NullableConvert.ToInt32(this.txtInvoice.Text));
                    command.Parameters.Add("DoctorID", MySqlType.Int).Value = NullableConvert.ToDb(NullableConvert.ToInt32(this.cmbDoctor.SelectedValue));
                    command.Parameters.Add("CMNFormID", MySqlType.Int).Value = NullableConvert.ToDb(NullableConvert.ToInt32(this.txtCMNForm.Text));
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_image", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_image"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_image");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                }
                if (Globals.CompanyImagingUri != null)
                {
                    FileWrapper wrapper = this.Wrapper;
                    if ((wrapper != null) && ReferenceEquals(wrapper.Thumbnail, this.Thumbnail))
                    {
                        new ImagingHelper(Globals.CompanyImagingUri).PutImage(Globals.CompanyDatabase, ID, wrapper.Data);
                    }
                    using (MySqlCommand command2 = new MySqlCommand("", connection))
                    {
                        command2.CommandText = "SELECT Thumbnail\r\nFROM tbl_image\r\nWHERE ID = :ID";
                        command2.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        using (MySqlDataReader reader = command2.ExecuteReader())
                        {
                            this.Thumbnail = !reader.Read() ? null : ImageFromBlob(reader["Thumbnail"], null);
                        }
                    }
                }
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n   tbl_image.ID\r\n  ,tbl_image.Name\r\n  ,tbl_image.Type\r\n  ,tbl_cmnform.CMNType as CmnType\r\n  ,CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName\r\n  ,CONCAT(tbl_doctor.LastName, ', ', tbl_doctor.FirstName) as DoctorName\r\nFROM tbl_image\r\n     LEFT JOIN tbl_customer ON tbl_image.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_doctor   ON tbl_image.DoctorID   = tbl_doctor  .ID\r\n     LEFT JOIN tbl_cmnform  ON tbl_image.CMNFormID  = tbl_cmnform .ID\r\nORDER BY tbl_image.ID DESC", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "#", 40);
            appearance.AddTextColumn("Name", "Name", 80);
            appearance.AddTextColumn("Type", "Type", 60);
            appearance.AddTextColumn("DoctorName", "Doctor", 100);
            appearance.AddTextColumn("CustomerName", "Customer", 100);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__127-0 e$__- = new _Closure$__127-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_Other(Params);
        }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCMNForm")]
        private Label lblCMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pbLogo")]
        private PictureBox pbLogo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbRelations")]
        private GroupBox gbRelations { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDescription")]
        private Label lblDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnImageChange")]
        private Button btnImageChange { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescription")]
        private TextBox txtDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbImage")]
        private GroupBox gbImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblID")]
        private Label lblID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor")]
        private Combobox cmbDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctor")]
        private Label lblDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrder")]
        private TextBox txtOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoice")]
        private Label lblInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrder")]
        private Label lblOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInvoice")]
        private TextBox txtInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCMNForm")]
        private Button btnCMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnInvoice")]
        private Button btnInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOrder")]
        private Button btnOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCMNForm")]
        private TextBox txtCMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnImageEnlarge")]
        private Button btnImageEnlarge { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnImageExport")]
        private Button btnImageExport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblType")]
        private Label lblType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtType")]
        private TextBox txtType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoImages")]
        private MenuItem mnuGotoImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnImagePrint")]
        private Button btnImagePrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        protected override object ObjectID
        {
            get => 
                base.ObjectID;
            set
            {
                base.ObjectID = value;
                try
                {
                    this.lblID.Text = Convert.ToInt32(value).ToString();
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException exception = ex;
                    this.lblID.Text = "Image #";
                    ProjectData.ClearProjectError();
                }
            }
        }

        protected override bool IsNew
        {
            get => 
                base.IsNew;
            set => 
                base.IsNew = value;
        }

        private DataTable Table_Order
        {
            get
            {
                if (this.F_Order == null)
                {
                    this.F_Order = new DataTable("tbl_order");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(FormOrder.GetQuery(), ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.F_Order);
                    }
                }
                return this.F_Order;
            }
            set => 
                this.F_Order = null;
        }

        private DataTable Table_Invoice
        {
            get
            {
                if (this.F_Invoice == null)
                {
                    this.F_Invoice = new DataTable("tbl_invoice");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(FormInvoice.GetQuery(), ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.F_Invoice);
                    }
                }
                return this.F_Invoice;
            }
            set => 
                this.F_Invoice = null;
        }

        private DataTable Table_CmnForm
        {
            get
            {
                if (this.F_CmnForm == null)
                {
                    this.F_CmnForm = new DataTable("tbl_cmnform");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(FormCMNRX.GetQuery(), ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.F_CmnForm);
                    }
                }
                return this.F_CmnForm;
            }
            set => 
                this.F_CmnForm = null;
        }

        private Image Thumbnail
        {
            get => 
                this.pbLogo.Image;
            set => 
                this.pbLogo.Image = value;
        }

        [CompilerGenerated]
        internal sealed class _Closure$__127-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }

        private abstract class FileWrapper
        {
            private static readonly byte[] PdfHeader10 = Encoding.ASCII.GetBytes("%PDF-1.0");
            private static readonly byte[] PdfHeader11 = Encoding.ASCII.GetBytes("%PDF-1.1");
            private static readonly byte[] PdfHeader12 = Encoding.ASCII.GetBytes("%PDF-1.2");
            private static readonly byte[] PdfHeader13 = Encoding.ASCII.GetBytes("%PDF-1.3");
            private static readonly byte[] PdfHeader14 = Encoding.ASCII.GetBytes("%PDF-1.4");
            private static readonly byte[] PdfHeader15 = Encoding.ASCII.GetBytes("%PDF-1.5");
            private static readonly byte[] PdfHeader16 = Encoding.ASCII.GetBytes("%PDF-1.6");
            private static readonly byte[] PdfHeader17 = Encoding.ASCII.GetBytes("%PDF-1.7");
            public static readonly FormImage.FilterEntry[] LoadFilterEntries;

            static FileWrapper()
            {
                List<FormImage.FilterEntry> list = new List<FormImage.FilterEntry>();
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                for (int i = 0; i < imageEncoders.Length; i++)
                {
                    ImageCodecInfo info1 = imageEncoders[i];
                    string wildcard = info1.FilenameExtension.ToLowerInvariant();
                    string description = info1.FormatDescription + " (" + wildcard + ")";
                    list.Add(new FormImage.FilterEntry(description, wildcard));
                }
                FormImage.FilterEntry entry = new FormImage.FilterEntry("All files", "*.*");
                FormImage.FilterEntry entry2 = FormImage.FilterEntry.Union("All picture files", list);
                list.Add(new FormImage.FilterEntry("PDF (*.pdf)", "*.pdf"));
                FormImage.FilterEntry entry3 = FormImage.FilterEntry.Union("All supported files", list);
                list.Sort(new Comparison<FormImage.FilterEntry>(_Closure$__.$I._Lambda$__23-0));
                FormImage.FilterEntry[] collection = new FormImage.FilterEntry[] { entry3, entry2, entry };
                list.InsertRange(0, collection);
                LoadFilterEntries = list.ToArray();
            }

            protected FileWrapper()
            {
            }

            public static FormImage.FileWrapper FromData(byte[] data) => 
                !IsPdf(data) ? ((FormImage.FileWrapper) new FormImage.ImageWrapper(data)) : ((FormImage.FileWrapper) new FormImage.PdfWrapper(data));

            public static FormImage.FileWrapper FromFile(string filname) => 
                FromData(File.ReadAllBytes(filname));

            public static bool IsPdf(byte[] data) => 
                StartsWith(data, PdfHeader17) || (StartsWith(data, PdfHeader16) || (StartsWith(data, PdfHeader15) || (StartsWith(data, PdfHeader14) || (StartsWith(data, PdfHeader13) || (StartsWith(data, PdfHeader12) || (StartsWith(data, PdfHeader11) || StartsWith(data, PdfHeader10)))))));

            public void Save(string filename)
            {
                byte[] data = this.Data;
                using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            public static bool StartsWith(byte[] image, byte[] test)
            {
                bool flag;
                if ((image == null) || ((test == null) || (image.Length < test.Length)))
                {
                    flag = false;
                }
                else
                {
                    int num = test.Length - 1;
                    int index = 0;
                    while (true)
                    {
                        if (index > num)
                        {
                            flag = true;
                        }
                        else
                        {
                            if (test[index] == image[index])
                            {
                                index++;
                                continue;
                            }
                            flag = false;
                        }
                        break;
                    }
                }
                return flag;
            }

            public abstract byte[] Data { get; }

            public abstract string DefaultExt { get; }

            public abstract Image Thumbnail { get; }

            public abstract FormImage.FilterEntry[] SaveFilterEntries { get; }

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormImage.FileWrapper._Closure$__ $I = new FormImage.FileWrapper._Closure$__();

                internal int _Lambda$__23-0(FormImage.FilterEntry x, FormImage.FilterEntry y) => 
                    string.Compare(x.Description, y.Description, StringComparison.OrdinalIgnoreCase);
            }
        }

        private class FilterEntry
        {
            public readonly string Description;
            public readonly string Wildcard;

            public FilterEntry(string description, string wildcard)
            {
                this.Description = description;
                this.Wildcard = wildcard;
            }

            public override string ToString() => 
                this.Description + "|" + this.Wildcard;

            public static FormImage.FilterEntry Union(string description, IEnumerable<FormImage.FilterEntry> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                IEnumerable<string> values = list.SelectMany<FormImage.FilterEntry, string>(((_Closure$__.$I4-0 == null) ? (_Closure$__.$I4-0 = new Func<FormImage.FilterEntry, IEnumerable<string>>(_Closure$__.$I._Lambda$__4-0)) : _Closure$__.$I4-0)).Where<string>(((_Closure$__.$I4-1 == null) ? (_Closure$__.$I4-1 = new Func<string, bool>(_Closure$__.$I._Lambda$__4-1)) : _Closure$__.$I4-1)).OrderBy<string, string>(((_Closure$__.$I4-2 == null) ? (_Closure$__.$I4-2 = new Func<string, string>(_Closure$__.$I._Lambda$__4-2)) : _Closure$__.$I4-2), StringComparer.Ordinal).ToArray<string>();
                return new FormImage.FilterEntry(description, string.Join(";", values));
            }

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormImage.FilterEntry._Closure$__ $I = new FormImage.FilterEntry._Closure$__();
                public static Func<FormImage.FilterEntry, IEnumerable<string>> $I4-0;
                public static Func<string, bool> $I4-1;
                public static Func<string, string> $I4-2;

                internal IEnumerable<string> _Lambda$__4-0(FormImage.FilterEntry fi)
                {
                    char[] separator = new char[] { ';' };
                    return fi.Wildcard.Split(separator);
                }

                internal bool _Lambda$__4-1(string s) => 
                    !string.IsNullOrWhiteSpace(s);

                internal string _Lambda$__4-2(string s) => 
                    s;
            }
        }

        private class ImagePrinter
        {
            private readonly FormImage.ImageWrapper wrapper;

            public ImagePrinter(FormImage.ImageWrapper wrapper)
            {
                if (wrapper == null)
                {
                    throw new ArgumentNullException("wrapper");
                }
                this.wrapper = wrapper;
            }

            public void Execute()
            {
                using (ImagePrintDocument document = ImagePrintDocument.FromArray(this.wrapper.Data))
                {
                    using (FormPrintPreview preview = new FormPrintPreview())
                    {
                        preview.Document = document;
                        Application.Run(preview);
                    }
                }
            }
        }

        private class ImageWrapper : FormImage.FileWrapper
        {
            private readonly MemoryStream sourceStream;

            public ImageWrapper(byte[] data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                this.sourceStream = new MemoryStream(data, false);
                this._Data = data;
                this._Image = System.Drawing.Image.FromStream(this.sourceStream);
                this._PageCount = this.Image.GetFrameCount(FrameDimension.Page);
                ImageFormat rawFormat = this.Image.RawFormat;
                this._DefaultExt = FindExtension(rawFormat);
                this._SaveFilterEntries = GetSaveFilterEntries(rawFormat);
            }

            public static ImageCodecInfo FindCodec(ImageCodecInfo[] codecs, ImageFormat format)
            {
                ImageCodecInfo[] infoArray = codecs;
                int index = 0;
                while (true)
                {
                    ImageCodecInfo info;
                    if (index >= infoArray.Length)
                    {
                        info = null;
                    }
                    else
                    {
                        ImageCodecInfo info2 = infoArray[index];
                        if (!(info2.FormatID == format.Guid))
                        {
                            index++;
                            continue;
                        }
                        info = info2;
                    }
                    return info;
                }
            }

            public static ImageCodecInfo FindEncoder(ImageFormat format) => 
                FindCodec(ImageCodecInfo.GetImageEncoders(), format);

            public static string FindExtension(ImageFormat format)
            {
                ImageCodecInfo info = FindEncoder(format);
                return ((info != null) ? ("." + info.FormatDescription) : null);
            }

            public System.Drawing.Image GetPage(int index)
            {
                System.Drawing.Image image;
                if ((index < 0) || (this.PageCount <= index))
                {
                    image = null;
                }
                else
                {
                    using (System.Drawing.Image image2 = (System.Drawing.Image) this.Image.Clone())
                    {
                        image2.SelectActiveFrame(FrameDimension.Page, index);
                        image = new Bitmap(image2);
                    }
                }
                return image;
            }

            public static FormImage.FilterEntry[] GetSaveFilterEntries(ImageFormat format)
            {
                List<FormImage.FilterEntry> list = new List<FormImage.FilterEntry>();
                foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
                {
                    if (info.FormatID == format.Guid)
                    {
                        string wildcard = info.FilenameExtension.ToLowerInvariant();
                        list.Add(new FormImage.FilterEntry(info.FormatDescription + " (" + wildcard + ")", wildcard));
                    }
                }
                list.Add(new FormImage.FilterEntry("All files", "*.*"));
                return list.ToArray();
            }

            public override byte[] Data { get; }

            public override string DefaultExt { get; }

            public override FormImage.FilterEntry[] SaveFilterEntries { get; }

            public override System.Drawing.Image Thumbnail =>
                this.Image;

            public int PageCount { get; }

            public System.Drawing.Image Image { get; }
        }

        private class PdfWrapper : FormImage.FileWrapper
        {
            public PdfWrapper(byte[] data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                this._Data = data;
                this._DefaultExt = ".pdf";
                this._SaveFilterEntries = GetSaveFilterEntries();
                this._Thumbnail = My.Resources.Resources.ImagePdfThumbnail;
            }

            public static FormImage.FilterEntry[] GetSaveFilterEntries()
            {
                List<FormImage.FilterEntry> list1 = new List<FormImage.FilterEntry>();
                list1.Add(new FormImage.FilterEntry("PDF (*.pdf)", "*.pdf"));
                list1.Add(new FormImage.FilterEntry("All files", "*.*"));
                return list1.ToArray();
            }

            public override byte[] Data { get; }

            public override string DefaultExt { get; }

            public override FormImage.FilterEntry[] SaveFilterEntries { get; }

            public override Image Thumbnail { get; }
        }
    }
}

