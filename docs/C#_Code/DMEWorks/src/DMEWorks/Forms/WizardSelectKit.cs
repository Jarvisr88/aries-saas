namespace DMEWorks.Forms
{
    using ActiproSoftware.Wizard;
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Core.Extensions;
    using DMEWorks.Data;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class WizardSelectKit : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private StageBase stWelcome;
        private StageBase stKit;
        private StageBase stPriceCode;
        private StageBase stMissing;
        private StageBase stSummary;
        private WizardData data;

        public WizardSelectKit()
        {
            this.InitializeComponent();
            this.cmbKit.EditButton = false;
            this.cmbKit.NewButton = false;
            this.cmbPriceCode.EditButton = false;
            this.cmbPriceCode.NewButton = false;
            this.stWelcome = new StageWelcome(this);
            this.stKit = new StageKit(this);
            this.stPriceCode = new StagePriceCode(this);
            this.stMissing = new StageMissing(this);
            this.stSummary = new StageSummary(this);
            this.InitializeGrid(this.gridSummary.Appearance);
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

        private StageBase GetNextStage(StageBase stage) => 
            !ReferenceEquals(stage, this.stWelcome) ? (!ReferenceEquals(stage, this.stKit) ? (!ReferenceEquals(stage, this.stPriceCode) ? (!ReferenceEquals(stage, this.stMissing) ? null : this.stSummary) : this.stMissing) : this.stPriceCode) : this.stKit;

        private StageBase GetPrevStage(StageBase stage) => 
            !ReferenceEquals(stage, this.stKit) ? (!ReferenceEquals(stage, this.stPriceCode) ? (!ReferenceEquals(stage, this.stMissing) ? (!ReferenceEquals(stage, this.stSummary) ? null : this.stMissing) : this.stPriceCode) : this.stKit) : this.stWelcome;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            WindowsClassicWizardRenderer renderer = new WindowsClassicWizardRenderer();
            this.wizard = new ActiproSoftware.Wizard.Wizard();
            this.wpWelcome = new WizardWelcomePage();
            this.Label3 = new Label();
            this.Label2 = new Label();
            this.Label1 = new Label();
            this.wpKit = new WizardPage();
            this.cmbKit = new Combobox();
            this.lblKit = new Label();
            this.wpPriceCode = new WizardPage();
            this.cmbPriceCode = new Combobox();
            this.lblPriceCode = new Label();
            this.wpMissing = new WizardPage();
            this.pnlMissing = new Panel();
            this.wpSummary = new WizardPage();
            this.gridSummary = new FilteredGrid();
            ((ISupportInitialize) this.wizard).BeginInit();
            this.wpWelcome.SuspendLayout();
            this.wpKit.SuspendLayout();
            this.wpPriceCode.SuspendLayout();
            this.wpMissing.SuspendLayout();
            this.wpSummary.SuspendLayout();
            base.SuspendLayout();
            this.wizard.Dock = DockStyle.Fill;
            this.wizard.Location = new Point(0, 0);
            this.wizard.Name = "wizard";
            WizardPage[] pages = new WizardPage[] { this.wpWelcome, this.wpKit, this.wpPriceCode, this.wpMissing, this.wpSummary };
            this.wizard.Pages.AddRange(pages);
            this.wizard.Renderer = renderer;
            this.wizard.Size = new Size(0x1fd, 0x169);
            this.wizard.TabIndex = 0;
            this.wpWelcome.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpWelcome.BackColor = SystemColors.Window;
            this.wpWelcome.Controls.Add(this.Label3);
            this.wpWelcome.Controls.Add(this.Label2);
            this.wpWelcome.Controls.Add(this.Label1);
            this.wpWelcome.HelpButtonVisible = false;
            this.wpWelcome.IsInteriorPage = false;
            this.wpWelcome.Location = new Point(0, 0);
            this.wpWelcome.Name = "wpWelcome";
            this.wpWelcome.PageCaption = "";
            this.wpWelcome.PageDescription = "";
            this.wpWelcome.Size = new Size(0x1fd, 0x142);
            this.wpWelcome.TabIndex = 2;
            this.Label3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.Label3.AutoSize = true;
            this.Label3.Location = new Point(0xb2, 0x126);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(120, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "To continue, click Next.";
            this.Label2.Location = new Point(0xb2, 0x45);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x13c, 0xf4);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Enter a brief description of the wizard here.";
            this.Label1.Font = new Font("Verdana", 12f, FontStyle.Bold);
            this.Label1.Location = new Point(0xb0, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x135, 0x37);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Welcome to the select kit wizard";
            this.wpKit.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpKit.Controls.Add(this.cmbKit);
            this.wpKit.Controls.Add(this.lblKit);
            this.wpKit.HelpButtonVisible = false;
            this.wpKit.Location = new Point(0x10, 0x4c);
            this.wpKit.Name = "wpKit";
            this.wpKit.PageCaption = "Select kit";
            this.wpKit.PageDescription = "Select kit that you want to add";
            this.wpKit.PageTitleBarText = "Select kit";
            this.wpKit.Size = new Size(0x1dd, 230);
            this.wpKit.TabIndex = 0;
            this.cmbKit.Location = new Point(0x10, 0x20);
            this.cmbKit.Name = "cmbKit";
            this.cmbKit.Size = new Size(0x158, 0x15);
            this.cmbKit.TabIndex = 1;
            this.lblKit.Location = new Point(0, 0);
            this.lblKit.Name = "lblKit";
            this.lblKit.Size = new Size(100, 0x15);
            this.lblKit.TabIndex = 0;
            this.lblKit.Text = "Kit :";
            this.wpPriceCode.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpPriceCode.Controls.Add(this.cmbPriceCode);
            this.wpPriceCode.Controls.Add(this.lblPriceCode);
            this.wpPriceCode.HelpButtonVisible = false;
            this.wpPriceCode.Location = new Point(0x10, 0x4c);
            this.wpPriceCode.Name = "wpPriceCode";
            this.wpPriceCode.PageCaption = "Select price code";
            this.wpPriceCode.PageDescription = "Select price code to be used for items that do not have one assigned";
            this.wpPriceCode.PageTitleBarText = "Select price code";
            this.wpPriceCode.Size = new Size(0x1dd, 230);
            this.wpPriceCode.TabIndex = 5;
            this.cmbPriceCode.Location = new Point(0x10, 0x20);
            this.cmbPriceCode.Name = "cmbPriceCode";
            this.cmbPriceCode.Size = new Size(0x158, 0x15);
            this.cmbPriceCode.TabIndex = 3;
            this.lblPriceCode.Location = new Point(0, 0);
            this.lblPriceCode.Name = "lblPriceCode";
            this.lblPriceCode.Size = new Size(480, 0x15);
            this.lblPriceCode.TabIndex = 2;
            this.lblPriceCode.Text = "Price Code :";
            this.wpMissing.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpMissing.Controls.Add(this.pnlMissing);
            this.wpMissing.HelpButtonVisible = false;
            this.wpMissing.Location = new Point(0x10, 0x4c);
            this.wpMissing.Name = "wpMissing";
            this.wpMissing.PageCaption = "Missing pricing";
            this.wpMissing.PageDescription = "Select price code for items that have wrong price code";
            this.wpMissing.PageTitleBarText = "Missing pricing";
            this.wpMissing.Size = new Size(0x1dd, 230);
            this.wpMissing.TabIndex = 7;
            this.pnlMissing.Dock = DockStyle.Fill;
            this.pnlMissing.Location = new Point(0, 0);
            this.pnlMissing.Name = "pnlMissing";
            this.pnlMissing.Size = new Size(0x1dd, 230);
            this.pnlMissing.TabIndex = 0;
            this.wpSummary.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpSummary.Controls.Add(this.gridSummary);
            this.wpSummary.HelpButtonVisible = false;
            this.wpSummary.Location = new Point(0x10, 0x4c);
            this.wpSummary.Name = "wpSummary";
            this.wpSummary.PageCaption = "Summary";
            this.wpSummary.PageDescription = "Summary of inventory items to be added";
            this.wpSummary.PageTitleBarText = "Summary";
            this.wpSummary.Size = new Size(0x37f, 0x160);
            this.wpSummary.TabIndex = 1;
            this.wpSummary.Text = "Summary";
            this.gridSummary.Dock = DockStyle.Fill;
            this.gridSummary.Location = new Point(0, 0);
            this.gridSummary.Name = "gridSummary";
            this.gridSummary.Size = new Size(0x37f, 0x160);
            this.gridSummary.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1fd, 0x169);
            base.Controls.Add(this.wizard);
            base.Name = "WizardSelectKit";
            this.Text = "Select Kit";
            ((ISupportInitialize) this.wizard).EndInit();
            this.wpWelcome.ResumeLayout(false);
            this.wpWelcome.PerformLayout();
            this.wpKit.ResumeLayout(false);
            this.wpPriceCode.ResumeLayout(false);
            this.wpMissing.ResumeLayout(false);
            this.wpSummary.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("InventoryItem", "InventoryItem", 180);
            Appearance.AddTextColumn("PriceCode", "PriceCode", 80);
            Appearance.AddTextColumn("Warehouse", "Warehouse", 100);
            Appearance.AddTextColumn("Quantity", "Quantity", 60, Appearance.IntegerStyle());
        }

        private void wizard_BackButtonClick(object sender, WizardPageCancelEventArgs e)
        {
            e.Cancel = true;
            while (true)
            {
                StageBase currentStage = this.CurrentStage;
                if (currentStage != null)
                {
                    StageBase prevStage = this.GetPrevStage(currentStage);
                    if (prevStage != null)
                    {
                        this.CurrentStage = prevStage;
                        if (prevStage.IsHidden)
                        {
                            continue;
                        }
                    }
                }
                return;
            }
        }

        private void wizard_CancelButtonClick(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void wizard_FinishButtonClick(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void wizard_NextButtonClick(object sender, WizardPageCancelEventArgs e)
        {
            e.Cancel = true;
            while (true)
            {
                try
                {
                    while (true)
                    {
                        StageBase currentStage = this.CurrentStage;
                        if (currentStage != null)
                        {
                            if (!currentStage.IsHidden)
                            {
                                currentStage.Commit();
                            }
                            StageBase nextStage = this.GetNextStage(currentStage);
                            if (nextStage != null)
                            {
                                this.CurrentStage = nextStage;
                                nextStage.Begin();
                                nextStage.UpdateButtons();
                                if (nextStage.IsHidden)
                                {
                                    break;
                                }
                            }
                        }
                        return;
                    }
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception);
                    ProjectData.ClearProjectError();
                    return;
                }
            }
        }

        [field: AccessedThroughProperty("wizard")]
        private ActiproSoftware.Wizard.Wizard wizard { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpWelcome")]
        private WizardWelcomePage wpWelcome { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpKit")]
        private WizardPage wpKit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpPriceCode")]
        private WizardPage wpPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpMissing")]
        private WizardPage wpMissing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpSummary")]
        private WizardPage wpSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbKit")]
        private Combobox cmbKit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblKit")]
        private Label lblKit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gridSummary")]
        private FilteredGrid gridSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPriceCode")]
        private Combobox cmbPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPriceCode")]
        private Label lblPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlMissing")]
        private Panel pnlMissing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private StageBase CurrentStage
        {
            get => 
                !ReferenceEquals(this.wizard.SelectedPage, this.stWelcome.Page) ? (!ReferenceEquals(this.wizard.SelectedPage, this.stKit.Page) ? (!ReferenceEquals(this.wizard.SelectedPage, this.stPriceCode.Page) ? (!ReferenceEquals(this.wizard.SelectedPage, this.stMissing.Page) ? (!ReferenceEquals(this.wizard.SelectedPage, this.stSummary.Page) ? null : this.stSummary) : this.stMissing) : this.stPriceCode) : this.stKit) : this.stWelcome;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.wizard.SelectedPage = value.Page;
                this.wizard.SelectedPage.HelpButtonVisible = false;
            }
        }

        public IEnumerable<ItemToAdd> Items =>
            ((this.data == null) || (this.data.KitItems == null)) ? ((IEnumerable<ItemToAdd>) new ItemToAdd[0]) : this.data.KitItems.Select<KitItem, ItemToAdd>(((_Closure$__.$I94-0 == null) ? (_Closure$__.$I94-0 = new Func<KitItem, ItemToAdd>(_Closure$__.$I._Lambda$__94-0)) : _Closure$__.$I94-0));

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly WizardSelectKit._Closure$__ $I = new WizardSelectKit._Closure$__();
            public static Func<WizardSelectKit.KitItem, WizardSelectKit.ItemToAdd> $I94-0;

            internal WizardSelectKit.ItemToAdd _Lambda$__94-0(WizardSelectKit.KitItem i) => 
                new WizardSelectKit.ItemToAdd(i.InventoryItem.Id, i.UpdatedPriceCode.Id, new int?((i.Warehouse == null) ? 0 : i.Warehouse.Id), i.Quantity);
        }

        private class Entity
        {
            public readonly int Id;
            public readonly string Name;

            public Entity(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public override string ToString() => 
                this.Name;

            public class Factory
            {
                private readonly Dictionary<int, WizardSelectKit.Entity> hash = new Dictionary<int, WizardSelectKit.Entity>();

                public WizardSelectKit.Entity Create(int? id, string name)
                {
                    WizardSelectKit.Entity entity = null;
                    if ((id != null) && !this.hash.TryGetValue(id.Value, out entity))
                    {
                        entity = new WizardSelectKit.Entity(id.Value, name);
                        this.hash.Add(id.Value, entity);
                    }
                    return entity;
                }

                public WizardSelectKit.Entity Create(IDataRecord record, string nameId, string nameName) => 
                    this.Create(record.GetInt32(nameId), record.GetString(nameName));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ItemToAdd
        {
            public readonly int InventoryItemID;
            public readonly int PriceCodeID;
            public readonly int? WarehouseID;
            public readonly int Quantity;
            public ItemToAdd(int InventoryItemID, int PriceCodeID, int? WarehouseID, int Quantity)
            {
                this = new WizardSelectKit.ItemToAdd();
                this.InventoryItemID = InventoryItemID;
                this.PriceCodeID = PriceCodeID;
                this.WarehouseID = WarehouseID;
                this.Quantity = Quantity;
            }
        }

        private class KitItem
        {
            public readonly int Id;
            public readonly WizardSelectKit.Entity InventoryItem;
            public readonly WizardSelectKit.Entity PriceCode;
            public readonly WizardSelectKit.Entity Warehouse;
            public readonly int Quantity;

            public KitItem(int Id, WizardSelectKit.Entity InventoryItem, WizardSelectKit.Entity PriceCode, WizardSelectKit.Entity Warehouse, int Quantity)
            {
                if (InventoryItem == null)
                {
                    throw new ArgumentNullException("InventoryItem");
                }
                this.Id = Id;
                this.InventoryItem = InventoryItem;
                this.PriceCode = PriceCode;
                this.Warehouse = Warehouse;
                this.Quantity = Quantity;
                this.UpdatedPriceCode = PriceCode;
            }

            public WizardSelectKit.Entity UpdatedPriceCode { get; set; }
        }

        public class PriceCodeDropdownHelper : DropdownHelperBase
        {
            private readonly WizardSelectKit wizard;

            public PriceCodeDropdownHelper(WizardSelectKit wizard)
            {
                this.wizard = wizard;
            }

            public override void ClickEdit(object source, EventArgs e)
            {
                throw new NotSupportedException();
            }

            public override void ClickNew(object source, EventArgs e)
            {
                throw new NotSupportedException();
            }

            public override DataTable GetTable()
            {
                if (this.wizard.data == null)
                {
                    throw new Exception("Internal Error");
                }
                DataTable table = new DataTable("tbl_serial");
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                row.AcceptChanges();
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Entity", typeof(object));
                foreach (WizardSelectKit.Entity entity in this.wizard.data.PriceCodes)
                {
                    DataRow row2 = table.NewRow();
                    row2["ID"] = entity.Id;
                    row2["Name"] = entity.Name;
                    row2["Entity"] = entity;
                    table.Rows.Add(row2);
                    row2.AcceptChanges();
                }
                return table;
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Price Code";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("ID", "#", 50);
                e.Appearance.AddTextColumn("Name", "Name", 240);
            }
        }

        private class Pricing
        {
            public readonly WizardSelectKit.Entity InventoryItem;
            public readonly WizardSelectKit.Entity PriceCode;

            public Pricing(WizardSelectKit.Entity InventoryItem, WizardSelectKit.Entity PriceCode)
            {
                if (InventoryItem == null)
                {
                    throw new ArgumentNullException("InventoryItem");
                }
                if (PriceCode == null)
                {
                    throw new ArgumentNullException("PriceCode");
                }
                this.InventoryItem = InventoryItem;
                this.PriceCode = PriceCode;
            }

            public override string ToString() => 
                this.PriceCode.Name;
        }

        private abstract class StageBase
        {
            public readonly WizardSelectKit wizard;

            protected StageBase(WizardSelectKit wizard)
            {
                if (wizard == null)
                {
                    throw new ArgumentNullException("wizard");
                }
                this.wizard = wizard;
            }

            public virtual void Begin()
            {
            }

            public virtual void Commit()
            {
            }

            public virtual void UpdateButtons()
            {
            }

            public virtual WizardPage Page
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public virtual bool IsHidden =>
                false;
        }

        private class StageKit : WizardSelectKit.StageBase
        {
            public StageKit(WizardSelectKit wizard) : base(wizard)
            {
                wizard.cmbKit.SelectedIndexChanged += new EventHandler(this._Lambda$__0-0);
            }

            [CompilerGenerated]
            private void _Lambda$__0-0(object e, EventArgs args)
            {
                this.UpdateButtons();
            }

            public override void Begin()
            {
                Cache.InitDropdown(base.wizard.cmbKit, "tbl_kit", null);
            }

            public override void Commit()
            {
                int? kitID = this.GetKitID();
                if (kitID == null)
                {
                    throw new UserNotifyException("You must select kit to prooceed");
                }
                WizardSelectKit.WizardData data1 = new WizardSelectKit.WizardData();
                data1.KitId = kitID.Value;
                WizardSelectKit.WizardData data = data1;
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        _Closure$__5-0 e$__- = new _Closure$__5-0();
                        command.CommandText = "SELECT\r\n  kd.ID\r\n, kd.Quantity\r\n, ii.ID   as InventoryItemID\r\n, ii.Name as InventoryItemName\r\n, pc.ID   as PriceCodeID\r\n, pc.Name as PriceCodeName\r\n, wh.ID   as WarehouseID\r\n, wh.Name as WarehouseName\r\nFROM tbl_kitdetails as kd\r\n     INNER JOIN tbl_inventoryitem as ii ON kd.InventoryItemID = ii.ID\r\n     LEFT JOIN tbl_pricecode as pc ON kd.PriceCodeID = pc.ID\r\n     LEFT JOIN tbl_warehouse as wh ON kd.WarehouseID = wh.ID\r\nWHERE kd.KitID = :kitID\r\nORDER BY kd.ID;\r\nSELECT\r\n  ID\r\n, Name\r\nFROM tbl_pricecode\r\nORDER BY Name;\r\nSELECT\r\n  ii.ID   as InventoryItemID\r\n, ii.Name as InventoryItemName\r\n, pc.ID   as PriceCodeID\r\n, pc.Name as PriceCodeName\r\nFROM tbl_pricecode_item as pr\r\n     INNER JOIN tbl_kitdetails as kd ON pr.InventoryItemID = kd.InventoryItemID\r\n     INNER JOIN tbl_inventoryitem as ii ON pr.InventoryItemID = ii.ID\r\n     INNER JOIN tbl_pricecode as pc ON pr.PriceCodeID = pc.ID\r\nWHERE kd.KitID = :kitID\r\nORDER BY pr.InventoryItemID, pr.PriceCodeID;";
                        MySqlParameter parameter1 = new MySqlParameter("kitID", MySqlType.Int);
                        parameter1.Value = kitID.Value;
                        command.Parameters.Add(parameter1);
                        e$__-.$VB$Local_factInventoryItem = new WizardSelectKit.Entity.Factory();
                        e$__-.$VB$Local_factPriceCode = new WizardSelectKit.Entity.Factory();
                        e$__-.$VB$Local_factWarehouse = new WizardSelectKit.Entity.Factory();
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            data.KitItems = reader.ToEnumerable().Select<IDataRecord, WizardSelectKit.KitItem>(new Func<IDataRecord, WizardSelectKit.KitItem>(e$__-._Lambda$__0)).ToArray<WizardSelectKit.KitItem>();
                            reader.NextResult();
                            data.PriceCodes = reader.ToEnumerable().Select<IDataRecord, WizardSelectKit.Entity>(new Func<IDataRecord, WizardSelectKit.Entity>(e$__-._Lambda$__1)).ToArray<WizardSelectKit.Entity>();
                            reader.NextResult();
                            data.Pricings = reader.ToEnumerable().Select<IDataRecord, WizardSelectKit.Pricing>(new Func<IDataRecord, WizardSelectKit.Pricing>(e$__-._Lambda$__2)).ToArray<WizardSelectKit.Pricing>();
                        }
                    }
                }
                base.wizard.data = data;
            }

            private int? GetKitID() => 
                NullableConvert.ToInt32(base.wizard.cmbKit.SelectedValue);

            public override void UpdateButtons()
            {
                if (this.GetKitID() != null)
                {
                    this.Page.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
                }
                else
                {
                    this.Page.NextButtonEnabled = WizardButtonEnabledDefault.False;
                }
            }

            public override WizardPage Page =>
                base.wizard.wpKit;

            [CompilerGenerated]
            internal sealed class _Closure$__5-0
            {
                public WizardSelectKit.Entity.Factory $VB$Local_factInventoryItem;
                public WizardSelectKit.Entity.Factory $VB$Local_factPriceCode;
                public WizardSelectKit.Entity.Factory $VB$Local_factWarehouse;

                internal WizardSelectKit.KitItem _Lambda$__0(IDataRecord r) => 
                    new WizardSelectKit.KitItem(r.GetInt32("ID").Value, this.$VB$Local_factInventoryItem.Create(r, "InventoryItemId", "InventoryItemName"), this.$VB$Local_factPriceCode.Create(r, "PriceCodeId", "PriceCodeName"), this.$VB$Local_factWarehouse.Create(r, "WarehouseId", "WarehouseName"), r.GetInt32("Quantity").Value);

                internal WizardSelectKit.Entity _Lambda$__1(IDataRecord r) => 
                    this.$VB$Local_factPriceCode.Create(r, "Id", "Name");

                internal WizardSelectKit.Pricing _Lambda$__2(IDataRecord r) => 
                    new WizardSelectKit.Pricing(this.$VB$Local_factInventoryItem.Create(r, "InventoryItemId", "InventoryItemName"), this.$VB$Local_factPriceCode.Create(r, "PriceCodeId", "PriceCodeName"));
            }
        }

        private class StageMissing : WizardSelectKit.StageBase
        {
            public StageMissing(WizardSelectKit wizard) : base(wizard)
            {
            }

            public override void Begin()
            {
                IEnumerator<WizardSelectKit.KitItem> enumerator;
                WizardSelectKit.WizardData data = base.wizard.data;
                if (data == null)
                {
                    throw new Exception("Internal Error");
                }
                base.wizard.pnlMissing.Controls.Clear();
                try
                {
                    enumerator = data.KitItems.OrderBy<WizardSelectKit.KitItem, string>(((_Closure$__.$I5-0 == null) ? (_Closure$__.$I5-0 = new Func<WizardSelectKit.KitItem, string>(_Closure$__.$I._Lambda$__5-0)) : _Closure$__.$I5-0), StringComparer.OrdinalIgnoreCase).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        _Closure$__5-0 e$__-;
                        e$__- = new _Closure$__5-0(e$__-) {
                            $VB$Local_item = enumerator.Current
                        };
                        if (!data.PricingExists(e$__-.$VB$Local_item.InventoryItem, e$__-.$VB$Local_item.PriceCode))
                        {
                            ArrayList list = new ArrayList {
                                DBNull.Value
                            };
                            list.AddRange(data.Pricings.Where<WizardSelectKit.Pricing>(new Func<WizardSelectKit.Pricing, bool>(e$__-._Lambda$__1)).Select<WizardSelectKit.Pricing, WizardSelectKit.Entity>(((_Closure$__.$I5-2 == null) ? (_Closure$__.$I5-2 = new Func<WizardSelectKit.Pricing, WizardSelectKit.Entity>(_Closure$__.$I._Lambda$__5-2)) : _Closure$__.$I5-2)).OrderBy<WizardSelectKit.Entity, string>(((_Closure$__.$I5-3 == null) ? (_Closure$__.$I5-3 = new Func<WizardSelectKit.Entity, string>(_Closure$__.$I._Lambda$__5-3)) : _Closure$__.$I5-3)).ToArray<WizardSelectKit.Entity>());
                            ControlMissingPricing pricing = new ControlMissingPricing(e$__-.$VB$Local_item.Id) {
                                Source = e$__-.$VB$Local_item.InventoryItem.Name,
                                Dropdown = { 
                                    DropDownStyle = ComboBoxStyle.DropDownList,
                                    DataSource = list
                                }
                            };
                            pricing.Dropdown.SelectedIndexChanged += new EventHandler(this.Dropdown_SelectedIndexChanged);
                            pricing.Location = new Point(0, 0x3e8);
                            pricing.Dock = DockStyle.Top;
                            base.wizard.pnlMissing.Controls.Add(pricing);
                        }
                    }
                }
                finally
                {
                    if (enumerator != null)
                    {
                        enumerator.Dispose();
                    }
                }
            }

            public override void Commit()
            {
                WizardSelectKit.WizardData data = base.wizard.data;
                if (data == null)
                {
                    throw new Exception("Internal Error");
                }
                int num = 0;
                ControlMissingPricing[] editors = this.GetEditors();
                for (int i = 0; i < editors.Length; i++)
                {
                    _Closure$__7-0 e$__-;
                    e$__- = new _Closure$__7-0(e$__-) {
                        $VB$Local_editor = editors[i]
                    };
                    WizardSelectKit.Entity selectedItem = e$__-.$VB$Local_editor.Dropdown.SelectedItem as WizardSelectKit.Entity;
                    if (selectedItem == null)
                    {
                        num++;
                        e$__-.$VB$Local_editor.Highlight(true);
                    }
                    else
                    {
                        WizardSelectKit.KitItem item = data.KitItems.FirstOrDefault<WizardSelectKit.KitItem>(new Func<WizardSelectKit.KitItem, bool>(e$__-._Lambda$__0));
                        if (item != null)
                        {
                            item.UpdatedPriceCode = selectedItem;
                        }
                    }
                }
            }

            private void Dropdown_SelectedIndexChanged(object sender, EventArgs args)
            {
                ComboBox box = sender as ComboBox;
                if (box != null)
                {
                    ControlMissingPricing parent = box.Parent as ControlMissingPricing;
                    if (parent != null)
                    {
                        parent.Highlight(false);
                    }
                }
                this.UpdateButtons();
            }

            private ControlMissingPricing[] GetEditors()
            {
                IEnumerator enumerator;
                List<ControlMissingPricing> list = new List<ControlMissingPricing>();
                try
                {
                    enumerator = base.wizard.pnlMissing.Controls.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ControlMissingPricing current = ((Control) enumerator.Current) as ControlMissingPricing;
                        if (current != null)
                        {
                            list.Add(current);
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
                return list.ToArray();
            }

            public override void UpdateButtons()
            {
                int num = 0;
                ControlMissingPricing[] editors = this.GetEditors();
                for (int i = 0; i < editors.Length; i++)
                {
                    if (!(editors[i].Dropdown.SelectedItem is WizardSelectKit.Entity))
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    this.Page.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
                }
                else
                {
                    this.Page.NextButtonEnabled = WizardButtonEnabledDefault.False;
                }
            }

            public override WizardPage Page =>
                base.wizard.wpMissing;

            public override bool IsHidden
            {
                get
                {
                    _Closure$__4-0 e$__- = new _Closure$__4-0 {
                        $VB$Local_data = base.wizard.data
                    };
                    return ((e$__-.$VB$Local_data != null) ? e$__-.$VB$Local_data.KitItems.All<WizardSelectKit.KitItem>(new Func<WizardSelectKit.KitItem, bool>(e$__-._Lambda$__0)) : true);
                }
            }

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly WizardSelectKit.StageMissing._Closure$__ $I = new WizardSelectKit.StageMissing._Closure$__();
                public static Func<WizardSelectKit.KitItem, string> $I5-0;
                public static Func<WizardSelectKit.Pricing, WizardSelectKit.Entity> $I5-2;
                public static Func<WizardSelectKit.Entity, string> $I5-3;

                internal string _Lambda$__5-0(WizardSelectKit.KitItem ki) => 
                    ki.InventoryItem.Name;

                internal WizardSelectKit.Entity _Lambda$__5-2(WizardSelectKit.Pricing pr) => 
                    pr.PriceCode;

                internal string _Lambda$__5-3(WizardSelectKit.Entity en) => 
                    en.Name;
            }

            [CompilerGenerated]
            internal sealed class _Closure$__4-0
            {
                public WizardSelectKit.WizardData $VB$Local_data;

                internal bool _Lambda$__0(WizardSelectKit.KitItem ki) => 
                    this.$VB$Local_data.PricingExists(ki.InventoryItem, ki.PriceCode);
            }

            [CompilerGenerated]
            internal sealed class _Closure$__5-0
            {
                public WizardSelectKit.KitItem $VB$Local_item;

                public _Closure$__5-0(WizardSelectKit.StageMissing._Closure$__5-0 arg0)
                {
                    if (arg0 != null)
                    {
                        this.$VB$Local_item = arg0.$VB$Local_item;
                    }
                }

                internal bool _Lambda$__1(WizardSelectKit.Pricing pr) => 
                    ReferenceEquals(pr.InventoryItem, this.$VB$Local_item.InventoryItem);
            }

            [CompilerGenerated]
            internal sealed class _Closure$__7-0
            {
                public ControlMissingPricing $VB$Local_editor;

                public _Closure$__7-0(WizardSelectKit.StageMissing._Closure$__7-0 arg0)
                {
                    if (arg0 != null)
                    {
                        this.$VB$Local_editor = arg0.$VB$Local_editor;
                    }
                }

                internal bool _Lambda$__0(WizardSelectKit.KitItem ki) => 
                    ki.Id == this.$VB$Local_editor.ID;
            }
        }

        private class StagePriceCode : WizardSelectKit.StageBase
        {
            public StagePriceCode(WizardSelectKit wizard) : base(wizard)
            {
                wizard.cmbPriceCode.SelectedIndexChanged += new EventHandler(this._Lambda$__0-0);
            }

            [CompilerGenerated]
            private void _Lambda$__0-0(object e, EventArgs args)
            {
                this.UpdateButtons();
            }

            public override void Begin()
            {
                new WizardSelectKit.PriceCodeDropdownHelper(base.wizard).InitDropdown(base.wizard.cmbPriceCode, null);
            }

            public override void Commit()
            {
                WizardSelectKit.Entity priceCode = this.GetPriceCode();
                if (priceCode == null)
                {
                    throw new UserNotifyException("You must select price code to prooceed");
                }
                if (base.wizard.data == null)
                {
                    throw new Exception("Internal Error");
                }
                WizardSelectKit.WizardData data = base.wizard.data;
                data.PriceCode = priceCode;
                foreach (WizardSelectKit.KitItem item in data.KitItems)
                {
                    if (item.PriceCode == null)
                    {
                        item.UpdatedPriceCode = priceCode;
                    }
                }
            }

            private WizardSelectKit.Entity GetPriceCode()
            {
                DataRowView selectedRowView = base.wizard.cmbPriceCode.SelectedRowView;
                return ((selectedRowView != null) ? (selectedRowView["Entity"] as WizardSelectKit.Entity) : null);
            }

            public override void UpdateButtons()
            {
                if (this.GetPriceCode() != null)
                {
                    this.Page.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
                }
                else
                {
                    this.Page.NextButtonEnabled = WizardButtonEnabledDefault.False;
                }
            }

            public override WizardPage Page =>
                base.wizard.wpPriceCode;

            public override bool IsHidden
            {
                get
                {
                    WizardSelectKit.WizardData data = base.wizard.data;
                    return ((data != null) ? data.KitItems.All<WizardSelectKit.KitItem>(((_Closure$__.$I4-0 == null) ? (_Closure$__.$I4-0 = new Func<WizardSelectKit.KitItem, bool>(_Closure$__.$I._Lambda$__4-0)) : _Closure$__.$I4-0)) : true);
                }
            }

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly WizardSelectKit.StagePriceCode._Closure$__ $I = new WizardSelectKit.StagePriceCode._Closure$__();
                public static Func<WizardSelectKit.KitItem, bool> $I4-0;

                internal bool _Lambda$__4-0(WizardSelectKit.KitItem ki) => 
                    ki.PriceCode != null;
            }
        }

        private class StageSummary : WizardSelectKit.StageBase
        {
            public StageSummary(WizardSelectKit wizard) : base(wizard)
            {
            }

            public override void Begin()
            {
                WizardSelectKit.WizardData data = base.wizard.data;
                if (data == null)
                {
                    throw new Exception("Internal Error");
                }
                DataTable table = new DataTable();
                DataColumn[] columns = new DataColumn[] { new DataColumn("InventoryItem", typeof(string)), new DataColumn("PriceCode", typeof(string)), new DataColumn("OrigPriceCode", typeof(string)), new DataColumn("Warehouse", typeof(string)), new DataColumn("Quantity", typeof(int)) };
                table.Columns.AddRange(columns);
                foreach (WizardSelectKit.KitItem item in data.KitItems)
                {
                    DataRow row = table.NewRow();
                    row["InventoryItem"] = item.InventoryItem.ToString();
                    row["PriceCode"] = (item.UpdatedPriceCode == null) ? "" : item.UpdatedPriceCode.ToString();
                    row["OrigPriceCode"] = (item.PriceCode == null) ? "" : item.PriceCode.ToString();
                    row["Warehouse"] = (item.Warehouse == null) ? "" : item.Warehouse.ToString();
                    row["Quantity"] = item.Quantity;
                    table.Rows.Add(row);
                    row.AcceptChanges();
                }
                base.wizard.gridSummary.GridSource = table.ToGridSource();
            }

            public override void Commit()
            {
            }

            public override WizardPage Page =>
                base.wizard.wpSummary;
        }

        private class StageWelcome : WizardSelectKit.StageBase
        {
            public StageWelcome(WizardSelectKit wizard) : base(wizard)
            {
            }

            public override WizardPage Page =>
                base.wizard.wpWelcome;
        }

        private class WizardData
        {
            public bool PricingExists(WizardSelectKit.Entity InventoryItem, WizardSelectKit.Entity PriceCode)
            {
                bool flag;
                _Closure$__21-0 e$__- = new _Closure$__21-0 {
                    $VB$Local_InventoryItem = InventoryItem,
                    $VB$Local_PriceCode = PriceCode
                };
                if (e$__-.$VB$Local_InventoryItem == null)
                {
                    flag = false;
                }
                else
                {
                    e$__-.$VB$Local_PriceCode ??= this.PriceCode;
                    flag = ((e$__-.$VB$Local_InventoryItem != null) && (e$__-.$VB$Local_PriceCode != null)) && this.Pricings.Any<WizardSelectKit.Pricing>(new Func<WizardSelectKit.Pricing, bool>(e$__-._Lambda$__0));
                }
                return flag;
            }

            public int KitId { get; set; }

            public WizardSelectKit.Entity PriceCode { get; set; }

            public WizardSelectKit.Entity[] PriceCodes { get; set; }

            public WizardSelectKit.KitItem[] KitItems { get; set; }

            public WizardSelectKit.Pricing[] Pricings { get; set; }

            [CompilerGenerated]
            internal sealed class _Closure$__21-0
            {
                public WizardSelectKit.Entity $VB$Local_InventoryItem;
                public WizardSelectKit.Entity $VB$Local_PriceCode;

                internal bool _Lambda$__0(WizardSelectKit.Pricing pr) => 
                    ReferenceEquals(pr.InventoryItem, this.$VB$Local_InventoryItem) && ReferenceEquals(pr.PriceCode, this.$VB$Local_PriceCode);
            }
        }
    }
}

