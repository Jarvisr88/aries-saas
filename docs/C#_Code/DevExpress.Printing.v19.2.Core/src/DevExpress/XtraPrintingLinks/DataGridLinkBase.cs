namespace DevExpress.XtraPrintingLinks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    [DefaultProperty("DataGrid")]
    public class DataGridLinkBase : LinkBase
    {
        private System.Windows.Forms.DataGrid dataGrid;
        private bool autoHeight;
        private DataGridTableStyle tableStyle;
        private DataGridPrintStyle printStyle;
        private DataGridPrintStyle activePrintStyle;
        private DataTable dummyTable;
        private bool useDataGridView;
        private object oldDataSource;
        private Point offset;
        private Rectangle[] rects;

        public DataGridLinkBase()
        {
            this.autoHeight = true;
        }

        public DataGridLinkBase(PrintingSystemBase ps) : base(ps)
        {
            this.autoHeight = true;
        }

        public DataGridLinkBase(IContainer container) : base(container)
        {
            this.autoHeight = true;
        }

        public override void AddSubreport(PointF offset)
        {
            if (this.dataGrid != null)
            {
                base.AddSubreport(offset);
            }
        }

        protected override void AfterCreate()
        {
            base.AfterCreate();
            try
            {
                if (this.dummyTable != null)
                {
                    this.dataGrid.DataSource = this.oldDataSource;
                }
            }
            catch
            {
            }
            this.dummyTable = null;
        }

        protected override void BeforeCreate()
        {
            if (this.DataGrid == null)
            {
                throw new NullReferenceException("The DataGrid property value must not be null");
            }
            base.BeforeCreate();
            base.ps.Graph.PageUnit = GraphicsUnit.Pixel;
            this.tableStyle = this.GetTableStyle();
            this.activePrintStyle = null;
            this.dummyTable = null;
            try
            {
                if ((this.dataGrid.Site != null) && this.IsEmpty)
                {
                    this.dummyTable = MakeDataTable();
                    this.oldDataSource = this.dataGrid.DataSource;
                    this.dataGrid.DataSource = this.dummyTable;
                }
            }
            catch
            {
            }
            this.FillRects();
            this.offset = this.IsEmpty ? Point.Empty : new Point(0, this.dataGrid.GetCellBounds(0, 0).Y);
            PrintingSystemCommand[] commands = new PrintingSystemCommand[] { PrintingSystemCommand.ExportXls, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportRtf, PrintingSystemCommand.ExportHtm, PrintingSystemCommand.ExportMht, PrintingSystemCommand.SendXls, PrintingSystemCommand.SendXlsx, PrintingSystemCommand.SendTxt, PrintingSystemCommand.SendCsv, PrintingSystemCommand.SendRtf, PrintingSystemCommand.SendMht };
            this.PrintingSystemBase.SetCommandVisibility(commands, CommandVisibility.All, Priority.Low);
        }

        private VisualBrick CreateBrick(object obj, ref Rectangle rect, DataGridColumnStyle style)
        {
            VisualBrick brick = null;
            if (obj is byte[])
            {
                System.Drawing.Image image = PSConvert.ImageFromArray((byte[]) obj);
                if (image != null)
                {
                    if (this.autoHeight)
                    {
                        rect.Height = (int) ((((float) image.Size.Height) / ((float) image.Size.Width)) * rect.Width);
                    }
                    brick = new ImageBrick();
                    ((ImageBrick) brick).Image = image;
                    brick.BackColor = Color.Transparent;
                }
            }
            else if (obj as bool)
            {
                brick = new CheckBoxBrick();
                ((CheckBoxBrick) brick).Checked = Convert.ToBoolean(obj);
            }
            else
            {
                brick = new TextBrick();
                DataGridTextBoxColumn textBoxStyle = style as DataGridTextBoxColumn;
                ((TextBrick) brick).Text = (textBoxStyle == null) ? Convert.ToString(obj) : GetDisplayText(textBoxStyle, obj);
            }
            if (brick != null)
            {
                brick.Sides = BorderSide.All;
            }
            return brick;
        }

        protected override void CreateDetail(BrickGraphics gr)
        {
            if (this.IsPrintable)
            {
                gr.Font = this.dataGrid.Font;
                gr.BackColor = this.ActivePrintStyle.AlternatingBackColor;
                gr.ForeColor = this.ActivePrintStyle.ForeColor;
                gr.BorderColor = (this.ActivePrintStyle.GridLineStyle == DataGridLineStyle.Solid) ? this.ActivePrintStyle.GridLineColor : Color.Empty;
                gr.BackColor = gr.BackColor.Equals(this.ActivePrintStyle.BackColor) ? this.ActivePrintStyle.AlternatingBackColor : this.ActivePrintStyle.BackColor;
                gr.StringFormat = new BrickStringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap);
                GridColumnStylesCollection gridColumnStyles = this.tableStyle.GridColumnStyles;
                Brick[] brickArray = new Brick[gridColumnStyles.Count];
                Rectangle[] rectangleArray = new Rectangle[gridColumnStyles.Count];
                int num = 0;
                int row = 0;
                while (true)
                {
                    int num3 = 0;
                    int col = 0;
                    while (true)
                    {
                        if (col < gridColumnStyles.Count)
                        {
                            try
                            {
                                Rectangle rect = this.GetCellBounds(gr, row, col);
                                rect.Y = num;
                                brickArray[col] = this.CreateBrick(this.dataGrid[row, col], ref rect, gridColumnStyles[col]);
                                rectangleArray[col] = rect;
                                num3 = Math.Max(num3, rect.Height);
                            }
                            catch
                            {
                                break;
                            }
                            col++;
                            continue;
                        }
                        else
                        {
                            num += num3;
                            int index = 0;
                            while (true)
                            {
                                if (index >= brickArray.Length)
                                {
                                    row++;
                                    break;
                                }
                                rectangleArray[index].Height = num3;
                                gr.DrawBrick(brickArray[index], rectangleArray[index]);
                                index++;
                            }
                        }
                        break;
                    }
                }
            }
        }

        protected override void CreateDetailHeader(BrickGraphics gr)
        {
            if (this.IsPrintable && this.dataGrid.ColumnHeadersVisible)
            {
                gr.Font = this.dataGrid.HeaderFont;
                gr.BackColor = this.ActivePrintStyle.HeaderBackColor;
                gr.ForeColor = this.ActivePrintStyle.HeaderForeColor;
                gr.BorderColor = this.ActivePrintStyle.FlatMode ? this.ActivePrintStyle.HeaderBackColor : Color.White;
                gr.StringFormat = new BrickStringFormat(StringFormatFlags.NoWrap, StringAlignment.Near, StringAlignment.Center);
                int num = this.dataGrid.HeaderFont.Height + 4;
                GridColumnStylesCollection gridColumnStyles = this.tableStyle.GridColumnStyles;
                for (int i = 0; i < gridColumnStyles.Count; i++)
                {
                    Rectangle rect = this.GetCellBounds(gr, 0, i);
                    rect.Height = num;
                    this.PrintObj(gr, gridColumnStyles[i].HeaderText, ref rect);
                }
            }
        }

        protected override void CreateReportHeader(BrickGraphics gr)
        {
            if (this.IsPrintable && this.dataGrid.CaptionVisible)
            {
                gr.Font = this.dataGrid.CaptionFont;
                gr.BackColor = this.ActivePrintStyle.CaptionBackColor;
                gr.ForeColor = this.ActivePrintStyle.CaptionForeColor;
                gr.BorderColor = this.ActivePrintStyle.CaptionBackColor;
                gr.StringFormat = new BrickStringFormat(StringFormatFlags.NoWrap, StringAlignment.Near, StringAlignment.Center);
                Rectangle rect = this.GetCellBounds(gr, 0, 0);
                Rectangle rectangle2 = this.GetCellBounds(gr, 0, this.tableStyle.GridColumnStyles.Count - 1);
                rect.Height = this.dataGrid.CaptionFont.Height + 4;
                rect.Width = rectangle2.Right - rect.Left;
                string captionText = this.dataGrid.CaptionText;
                if ((captionText.Length == 0) && (this.dummyTable != null))
                {
                    captionText = "Caption";
                }
                this.PrintObj(gr, captionText, ref rect).SeparableHorz = true;
            }
        }

        private void FillRects()
        {
            this.rects = new Rectangle[this.ColumnCount];
            for (int i = 0; i < this.rects.Length; i++)
            {
                this.rects[i] = this.dataGrid.GetCellBounds(0, i);
            }
            if (this.rects.Length != 0)
            {
                this.rects[0].X = 0;
                for (int j = 1; j < this.rects.Length; j++)
                {
                    this.rects[j].X = this.rects[j - 1].Right + 1;
                }
            }
        }

        private unsafe Rectangle GetCellBounds(BrickGraphics gr, int row, int col)
        {
            Rectangle cellBounds = this.dataGrid.GetCellBounds(row, col);
            cellBounds.X = this.rects[col].X;
            Rectangle* rectanglePtr1 = &cellBounds;
            rectanglePtr1.Width += (int) gr.BorderWidth;
            Rectangle* rectanglePtr2 = &cellBounds;
            rectanglePtr2.Height += (int) gr.BorderWidth;
            cellBounds.Offset(-this.offset.X, -this.offset.Y);
            return cellBounds;
        }

        private static string GetDisplayText(DataGridTextBoxColumn textBoxStyle, object obj)
        {
            MethodInfo method = textBoxStyle.GetType().GetMethod("GetText", BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                return Convert.ToString(obj);
            }
            object[] parameters = new object[] { obj };
            return (string) method.Invoke(textBoxStyle, parameters);
        }

        private DataGridTableStyle GetTableStyle()
        {
            try
            {
                if (this.dataGrid.TableStyles.Count > 0)
                {
                    return this.dataGrid.TableStyles[0];
                }
            }
            catch
            {
            }
            try
            {
                return (DataGridTableStyle) this.dataGrid.GetType().InvokeMember("defaultTableStyle", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, this.dataGrid, null);
            }
            catch
            {
                return null;
            }
        }

        private static DataTable MakeDataTable()
        {
            DataTable table = new DataTable();
            for (int i = 0; i < 3; i++)
            {
                DataColumn column = new DataColumn {
                    DataType = System.Type.GetType("System.String"),
                    ColumnName = $"Column{i}"
                };
                table.Columns.Add(column);
            }
            for (int j = 0; j < 5; j++)
            {
                DataRow row = table.NewRow();
                foreach (DataColumn column2 in table.Columns)
                {
                    row[column2] = "abc";
                }
                table.Rows.Add(row);
            }
            table.AcceptChanges();
            return table;
        }

        private VisualBrick PrintObj(BrickGraphics gr, object obj, ref Rectangle rect)
        {
            VisualBrick brick = this.CreateBrick(obj, ref rect, null);
            if (brick != null)
            {
                gr.DrawBrick(brick, rect);
            }
            return brick;
        }

        public override void SetDataObject(object data)
        {
            if (data is System.Windows.Forms.DataGrid)
            {
                this.dataGrid = data as System.Windows.Forms.DataGrid;
            }
        }

        private bool ShouldSerializePrintStyle() => 
            this.PrintStyle.ShouldSerialize();

        public override System.Type PrintableObjectType =>
            typeof(System.Windows.Forms.DataGrid);

        [Description("Gets or sets a DataGrid object to be printed via the current link."), Category("Printing"), DefaultValue((string) null)]
        public System.Windows.Forms.DataGrid DataGrid
        {
            get => 
                this.dataGrid;
            set => 
                this.dataGrid = value;
        }

        [Description("Gets or sets a value indicating whether the height of the DataGrid to be printed should be calculated automatically."), Category("Print Options"), DefaultValue(true)]
        public bool AutoHeight
        {
            get => 
                this.autoHeight;
            set => 
                this.autoHeight = value;
        }

        [Description("Gets or sets the data grid's printing style."), Category("Print Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridPrintStyle PrintStyle
        {
            get
            {
                this.printStyle ??= new DataGridPrintStyle();
                return this.printStyle;
            }
            set => 
                this.printStyle = value;
        }

        [Description("Gets or sets a value indicating whether this DataGridLinkBase class descendant should use the visual style of the DataGrid it prints."), Category("Print Options"), DefaultValue(false)]
        public bool UseDataGridView
        {
            get => 
                this.useDataGridView;
            set => 
                this.useDataGridView = value;
        }

        protected bool IsEmpty
        {
            get
            {
                try
                {
                    return (((this.dataGrid.DataSource == null) || (this.dataGrid.VisibleRowCount == 0)) || (this.dataGrid[0, 0] == null));
                }
                catch
                {
                    return true;
                }
            }
        }

        protected bool IsPrintable =>
            !this.IsEmpty && (this.tableStyle != null);

        protected DataGridPrintStyle ActivePrintStyle
        {
            get
            {
                this.activePrintStyle ??= ((!this.UseDataGridView || (this.dataGrid == null)) ? new DataGridPrintStyle(this.printStyle) : new DataGridPrintStyle(this.dataGrid));
                return this.activePrintStyle;
            }
        }

        protected int ColumnCount =>
            this.tableStyle.GridColumnStyles.Count;
    }
}

