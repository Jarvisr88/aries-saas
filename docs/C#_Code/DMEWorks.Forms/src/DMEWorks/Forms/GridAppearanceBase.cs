namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    [Serializable, CLSCompliant(true)]
    public class GridAppearanceBase : Component
    {
        protected readonly GridBase searchableGrid;
        private static Hashtable hashShouldSerialize = new Hashtable();

        public event EventHandler<GridCellFormattingEventArgs> CellFormatting
        {
            add
            {
                this.searchableGrid.CellFormatting += value;
            }
            remove
            {
                this.searchableGrid.CellFormatting -= value;
            }
        }

        public event EventHandler<GridCellParsingEventArgs> CellParsing
        {
            add
            {
                this.searchableGrid.CellParsing += value;
            }
            remove
            {
                this.searchableGrid.CellParsing -= value;
            }
        }

        public event EventHandler<GridDataErrorEventArgs> DataError
        {
            add
            {
                this.searchableGrid.DataError += value;
            }
            remove
            {
                this.searchableGrid.DataError -= value;
            }
        }

        public event EventHandler<GridContextMenuNeededEventArgs> RowContextMenuNeeded
        {
            add
            {
                this.searchableGrid.RowContextMenuNeeded += value;
            }
            remove
            {
                this.searchableGrid.RowContextMenuNeeded -= value;
            }
        }

        protected GridAppearanceBase(GridBase grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }
            this.searchableGrid = grid;
            foreach (PropertyInfo info in base.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length != 0))
                {
                    DefaultValueAttribute attribute = customAttributes[0] as DefaultValueAttribute;
                    if (attribute != null)
                    {
                        try
                        {
                            info.SetValue(this, attribute.Value, new object[0]);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            this.RowTemplate.Height = 0x12;
        }

        public DataGridViewCheckBoxColumn AddBoolColumn(string PropertyName, string HeaderText, int Width) => 
            this.AddBoolColumn(PropertyName, HeaderText, Width, this.BoolStyle());

        public DataGridViewCheckBoxColumn AddBoolColumn(string PropertyName, string HeaderText, int Width, DataGridViewCellStyle CellStyle)
        {
            DataGridViewCheckBoxColumn dataGridViewColumn = new DataGridViewCheckBoxColumn {
                DataPropertyName = PropertyName,
                HeaderText = HeaderText,
                ReadOnly = true,
                Width = Width,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = DBNull.Value,
                ThreeState = false,
                DefaultCellStyle = CellStyle
            };
            this.Columns.Add(dataGridViewColumn);
            return dataGridViewColumn;
        }

        public DataGridViewTextBoxColumn AddTextColumn(string PropertyName, string HeaderText, int Width) => 
            this.AddTextColumn(PropertyName, HeaderText, Width, this.TextStyle());

        public DataGridViewTextBoxColumn AddTextColumn(string PropertyName, string HeaderText, int Width, DataGridViewCellStyle CellStyle)
        {
            DataGridViewTextBoxColumn dataGridViewColumn = new DataGridViewTextBoxColumn {
                DataPropertyName = PropertyName,
                HeaderText = HeaderText,
                ReadOnly = true,
                Width = Width,
                DefaultCellStyle = CellStyle
            };
            this.Columns.Add(dataGridViewColumn);
            return dataGridViewColumn;
        }

        public DataGridViewCellStyle BoolStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.NullValue = DBNull.Value;
            style1.Format = "";
            return style1;
        }

        public DataGridViewCellStyle DateStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.NullValue = "";
            style1.Format = "d";
            return style1;
        }

        public DataGridViewCellStyle DateTimeStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.NullValue = "";
            style1.Format = "g";
            return style1;
        }

        protected DataGridView grid() => 
            this.searchableGrid.GetGrid();

        public DataGridViewCellStyle IntegerStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleRight;
            style1.NullValue = "";
            style1.Format = "0";
            return style1;
        }

        public DataGridViewCellStyle PriceStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleRight;
            style1.NullValue = "";
            style1.Format = "0.00";
            return style1;
        }

        private bool ShouldSerialize(string name)
        {
            name = "ShouldSerialize" + name;
            if (!hashShouldSerialize.Contains(name))
            {
                MethodInfo info2 = this.grid().GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, null, new System.Type[0], null);
                if ((info2 != null) && (info2.ReturnType != typeof(bool)))
                {
                    info2 = null;
                }
                hashShouldSerialize.Add(name, info2);
            }
            MethodInfo info = hashShouldSerialize[name] as MethodInfo;
            if (info != null)
            {
                try
                {
                    return (bool) info.Invoke(this.grid(), null);
                }
                catch
                {
                }
            }
            return false;
        }

        private bool ShouldSerializeAlternatingRowsDefaultCellStyle() => 
            this.ShouldSerialize("AlternatingRowsDefaultCellStyle");

        private bool ShouldSerializeBackgroundColor() => 
            this.ShouldSerialize("BackgroundColor");

        private bool ShouldSerializeColumnHeadersDefaultCellStyle() => 
            this.ShouldSerialize("ColumnHeadersDefaultCellStyle");

        private bool ShouldSerializeDefaultCellStyle() => 
            this.ShouldSerialize("DefaultCellStyle");

        private bool ShouldSerializeGridColor() => 
            this.ShouldSerialize("GridColor");

        private bool ShouldSerializeRowHeadersDefaultCellStyle() => 
            this.ShouldSerialize("RowHeadersDefaultCellStyle");

        private bool ShouldSerializeRowsDefaultCellStyle() => 
            this.ShouldSerialize("RowsDefaultCellStyle");

        private bool ShouldSerializeRowTemplate() => 
            this.ShouldSerialize("RowTemplate");

        public DataGridViewCellStyle TextStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.NullValue = "";
            style1.Format = "";
            return style1;
        }

        public DataGridViewCellStyle TimeStyle()
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.NullValue = "";
            style1.Format = "t";
            return style1;
        }

        [Category("Appearance")]
        public DataGridViewCellStyle AlternatingRowsDefaultCellStyle
        {
            get => 
                this.grid().AlternatingRowsDefaultCellStyle;
            set => 
                this.grid().AlternatingRowsDefaultCellStyle = value;
        }

        [Category("Appearance")]
        public DataGridViewCellStyle ColumnHeadersDefaultCellStyle
        {
            get => 
                this.grid().ColumnHeadersDefaultCellStyle;
            set => 
                this.grid().ColumnHeadersDefaultCellStyle = value;
        }

        [Category("Appearance")]
        public DataGridViewCellStyle DefaultCellStyle
        {
            get => 
                this.grid().DefaultCellStyle;
            set => 
                this.grid().DefaultCellStyle = value;
        }

        [Category("Appearance")]
        public DataGridViewCellStyle RowHeadersDefaultCellStyle
        {
            get => 
                this.grid().RowHeadersDefaultCellStyle;
            set => 
                this.grid().RowHeadersDefaultCellStyle = value;
        }

        [Category("Appearance")]
        public DataGridViewCellStyle RowsDefaultCellStyle
        {
            get => 
                this.grid().RowsDefaultCellStyle;
            set => 
                this.grid().RowsDefaultCellStyle = value;
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowDelete
        {
            get => 
                this.grid().AllowUserToDeleteRows;
            set => 
                this.grid().AllowUserToDeleteRows = value;
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowEdit
        {
            get => 
                !this.grid().ReadOnly;
            set => 
                this.grid().ReadOnly = !value;
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowNew
        {
            get => 
                this.grid().AllowUserToAddRows;
            set => 
                this.grid().AllowUserToAddRows = value;
        }

        [Category("Behavior"), DefaultValue(1)]
        public DataGridViewSelectionMode SelectionMode
        {
            get => 
                this.grid().SelectionMode;
            set => 
                this.grid().SelectionMode = value;
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool MultiSelect
        {
            get => 
                this.grid().MultiSelect;
            set => 
                this.grid().MultiSelect = value;
        }

        [Category("Behavior"), DefaultValue(2)]
        public DataGridViewEditMode EditMode
        {
            get => 
                this.grid().EditMode;
            set => 
                this.grid().EditMode = value;
        }

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewRow RowTemplate
        {
            get => 
                this.grid().RowTemplate;
            set => 
                this.grid().RowTemplate = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool AutoGenerateColumns
        {
            get => 
                this.grid().AutoGenerateColumns;
            set => 
                this.grid().AutoGenerateColumns = value;
        }

        [Category("Appearance")]
        public Color BackgroundColor
        {
            get => 
                this.grid().BackgroundColor;
            set => 
                this.grid().BackgroundColor = value;
        }

        [Category("Appearance"), DefaultValue(1)]
        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get => 
                this.grid().BorderStyle;
            set => 
                this.grid().BorderStyle = value;
        }

        [Category("Appearance"), DefaultValue(1)]
        public DataGridViewCellBorderStyle CellBorderStyle
        {
            get => 
                this.grid().CellBorderStyle;
            set => 
                this.grid().CellBorderStyle = value;
        }

        [Category("Appearance"), DefaultValue(1)]
        public DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle
        {
            get => 
                this.grid().ColumnHeadersBorderStyle;
            set => 
                this.grid().ColumnHeadersBorderStyle = value;
        }

        [Category("Appearance"), DefaultValue(20)]
        public int ColumnHeadersHeight
        {
            get => 
                this.grid().ColumnHeadersHeight;
            set => 
                this.grid().ColumnHeadersHeight = value;
        }

        [Category("Appearance"), DefaultValue(1), RefreshProperties(RefreshProperties.All)]
        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            get => 
                this.grid().ColumnHeadersHeightSizeMode;
            set => 
                this.grid().ColumnHeadersHeightSizeMode = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool ColumnHeadersVisible
        {
            get => 
                this.grid().ColumnHeadersVisible;
            set => 
                this.grid().ColumnHeadersVisible = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool EnableHeadersVisualStyles
        {
            get => 
                this.grid().EnableHeadersVisualStyles;
            set => 
                this.grid().EnableHeadersVisualStyles = value;
        }

        [Category("Appearance")]
        public Color GridColor
        {
            get => 
                this.grid().GridColor;
            set => 
                this.grid().GridColor = value;
        }

        [Category("Appearance"), DefaultValue(1)]
        public DataGridViewHeaderBorderStyle RowHeadersBorderStyle
        {
            get => 
                this.grid().RowHeadersBorderStyle;
            set => 
                this.grid().RowHeadersBorderStyle = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool RowHeadersVisible
        {
            get => 
                this.grid().RowHeadersVisible;
            set => 
                this.grid().RowHeadersVisible = value;
        }

        [Category("Appearance"), DefaultValue(0x15)]
        public int RowHeadersWidth
        {
            get => 
                this.grid().RowHeadersWidth;
            set => 
                this.grid().RowHeadersWidth = value;
        }

        [Category("Appearance"), DefaultValue(1), RefreshProperties(RefreshProperties.All)]
        public DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
        {
            get => 
                this.grid().RowHeadersWidthSizeMode;
            set => 
                this.grid().RowHeadersWidthSizeMode = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool ShowCellErrors
        {
            get => 
                this.grid().ShowCellErrors;
            set => 
                this.grid().ShowCellErrors = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool ShowCellToolTips
        {
            get => 
                this.grid().ShowCellToolTips;
            set => 
                this.grid().ShowCellToolTips = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool ShowEditingIcon
        {
            get => 
                this.grid().ShowEditingIcon;
            set => 
                this.grid().ShowEditingIcon = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool ShowRowErrors
        {
            get => 
                this.grid().ShowRowErrors;
            set => 
                this.grid().ShowRowErrors = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get => 
                this.grid().ContextMenuStrip;
            set => 
                this.grid().ContextMenuStrip = value;
        }

        [Category("Layout"), DefaultValue(0), Description("DataGridView_AutoSizeRowsModeDescr")]
        public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
        {
            get => 
                this.grid().AutoSizeRowsMode;
            set => 
                this.grid().AutoSizeRowsMode = value;
        }

        public DataGridViewColumnCollection Columns =>
            this.grid().Columns;
    }
}

