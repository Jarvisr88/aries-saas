namespace DevExpress.XtraPrintingLinks
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DataGridPrintStyle
    {
        private Color captionBackColor;
        private Color captionForeColor;
        private Color headerBackColor;
        private Color headerForeColor;
        private Color alternatingBackColor;
        private Color backColor;
        private Color foreColor;
        private Color gridLineColor;
        private bool flatMode;
        private DataGridLineStyle gridLineStyle;

        public DataGridPrintStyle()
        {
            this.captionBackColor = SystemColors.ActiveCaption;
            this.captionForeColor = SystemColors.ActiveCaptionText;
            this.headerBackColor = SystemColors.Control;
            this.headerForeColor = SystemColors.ControlText;
            this.alternatingBackColor = SystemColors.Window;
            this.backColor = SystemColors.Window;
            this.foreColor = SystemColors.WindowText;
            this.gridLineColor = SystemColors.Control;
            this.gridLineStyle = DataGridLineStyle.Solid;
        }

        public DataGridPrintStyle(DataGridPrintStyle printStyle)
        {
            this.captionBackColor = SystemColors.ActiveCaption;
            this.captionForeColor = SystemColors.ActiveCaptionText;
            this.headerBackColor = SystemColors.Control;
            this.headerForeColor = SystemColors.ControlText;
            this.alternatingBackColor = SystemColors.Window;
            this.backColor = SystemColors.Window;
            this.foreColor = SystemColors.WindowText;
            this.gridLineColor = SystemColors.Control;
            this.gridLineStyle = DataGridLineStyle.Solid;
            this.CopyFrom(printStyle);
        }

        public DataGridPrintStyle(DataGrid dataGrid)
        {
            this.captionBackColor = SystemColors.ActiveCaption;
            this.captionForeColor = SystemColors.ActiveCaptionText;
            this.headerBackColor = SystemColors.Control;
            this.headerForeColor = SystemColors.ControlText;
            this.alternatingBackColor = SystemColors.Window;
            this.backColor = SystemColors.Window;
            this.foreColor = SystemColors.WindowText;
            this.gridLineColor = SystemColors.Control;
            this.gridLineStyle = DataGridLineStyle.Solid;
            this.CopyFrom(dataGrid);
        }

        public void CopyFrom(DataGridPrintStyle printStyle)
        {
            if (printStyle != null)
            {
                try
                {
                    this.captionBackColor = printStyle.CaptionBackColor;
                    this.captionForeColor = printStyle.CaptionForeColor;
                    this.headerBackColor = printStyle.HeaderBackColor;
                    this.headerForeColor = printStyle.HeaderForeColor;
                    this.alternatingBackColor = printStyle.AlternatingBackColor;
                    this.backColor = printStyle.BackColor;
                    this.foreColor = printStyle.ForeColor;
                    this.gridLineColor = printStyle.GridLineColor;
                    this.flatMode = printStyle.FlatMode;
                    this.gridLineStyle = printStyle.GridLineStyle;
                }
                catch
                {
                }
            }
        }

        public void CopyFrom(DataGrid dataGrid)
        {
            try
            {
                this.captionBackColor = dataGrid.CaptionBackColor;
                this.captionForeColor = dataGrid.CaptionForeColor;
                this.headerBackColor = dataGrid.HeaderBackColor;
                this.headerForeColor = dataGrid.HeaderForeColor;
                this.alternatingBackColor = dataGrid.AlternatingBackColor;
                this.backColor = dataGrid.BackColor;
                this.foreColor = dataGrid.ForeColor;
                this.gridLineColor = dataGrid.GridLineColor;
                this.flatMode = dataGrid.FlatMode;
                this.gridLineStyle = dataGrid.GridLineStyle;
            }
            catch
            {
            }
        }

        internal bool ShouldSerialize() => 
            this.ShouldSerializeCaptionBackColor() || (this.ShouldSerializeCaptionForeColor() || (this.ShouldSerializeHeaderBackColor() || (this.ShouldSerializeHeaderForeColor() || (this.ShouldSerializeAlternatingBackColor() || (this.ShouldSerializeBackColor() || (this.ShouldSerializeForeColor() || (this.ShouldSerializeGridLineColor() || (this.FlatMode || (this.GridLineStyle != DataGridLineStyle.Solid)))))))));

        protected bool ShouldSerializeAlternatingBackColor() => 
            this.alternatingBackColor != SystemColors.Window;

        protected bool ShouldSerializeBackColor() => 
            this.backColor != SystemColors.Window;

        protected bool ShouldSerializeCaptionBackColor() => 
            this.captionBackColor != SystemColors.ActiveCaption;

        protected bool ShouldSerializeCaptionForeColor() => 
            this.captionForeColor != SystemColors.ActiveCaptionText;

        protected bool ShouldSerializeForeColor() => 
            this.foreColor != SystemColors.WindowText;

        protected bool ShouldSerializeGridLineColor() => 
            this.gridLineColor != SystemColors.Control;

        protected bool ShouldSerializeHeaderBackColor() => 
            this.headerBackColor != SystemColors.Control;

        protected bool ShouldSerializeHeaderForeColor() => 
            this.headerForeColor != SystemColors.ControlText;

        public override string ToString() => 
            base.GetType().Name;

        [NotifyParentProperty(true), Description("Gets or sets the background color of the caption area.")]
        public Color CaptionBackColor
        {
            get => 
                this.captionBackColor;
            set => 
                this.captionBackColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the foreground color of the caption area.")]
        public Color CaptionForeColor
        {
            get => 
                this.captionForeColor;
            set => 
                this.captionForeColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the background color of all row and column headers.")]
        public Color HeaderBackColor
        {
            get => 
                this.headerBackColor;
            set => 
                this.headerBackColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the foreground color of headers.")]
        public Color HeaderForeColor
        {
            get => 
                this.headerForeColor;
            set => 
                this.headerForeColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the background color of odd-numbered rows of the grid.")]
        public Color AlternatingBackColor
        {
            get => 
                this.alternatingBackColor;
            set => 
                this.alternatingBackColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the background color of even-numbered rows of the grid.")]
        public Color BackColor
        {
            get => 
                this.backColor;
            set => 
                this.backColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the foreground color (typically the color of the text) of the data grid.")]
        public Color ForeColor
        {
            get => 
                this.foreColor;
            set => 
                this.foreColor = value;
        }

        [NotifyParentProperty(true), Description("Gets or sets the color of the grid lines.")]
        public Color GridLineColor
        {
            get => 
                this.gridLineColor;
            set => 
                this.gridLineColor = value;
        }

        [DefaultValue(false), NotifyParentProperty(true), Description("Gets or sets a value indicating whether the grid is printed in flat mode.")]
        public bool FlatMode
        {
            get => 
                this.flatMode;
            set => 
                this.flatMode = value;
        }

        [DefaultValue(1), NotifyParentProperty(true), Description("Gets or sets the line style of the grid.")]
        public DataGridLineStyle GridLineStyle
        {
            get => 
                this.gridLineStyle;
            set => 
                this.gridLineStyle = value;
        }
    }
}

