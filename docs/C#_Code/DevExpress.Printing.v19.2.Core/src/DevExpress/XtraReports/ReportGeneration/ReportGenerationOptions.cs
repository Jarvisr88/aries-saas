namespace DevExpress.XtraReports.ReportGeneration
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public class ReportGenerationOptions
    {
        private DefaultBoolean printColumnHeaders;
        private DefaultBoolean printTotalSummaryFooter;
        private DefaultBoolean printGroupSummaryFooter;
        private DefaultBoolean printGroupRows;
        private DefaultBoolean printHorizontalLines;
        private DefaultBoolean printVerticalLines;
        private DefaultBoolean usePrintAppearances;
        private DefaultBoolean enablePrintAppearanceEvenRow;
        private DefaultBoolean enablePrintAppearanceOddRow;
        private DefaultBoolean autoFitToPageWidth;
        private DefaultBoolean printBandHeaders;

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintColumnHeaders
        {
            get => 
                this.printColumnHeaders;
            set => 
                this.printColumnHeaders = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintBandHeaders
        {
            get => 
                this.printBandHeaders;
            set => 
                this.printBandHeaders = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintGroupRows
        {
            get => 
                this.printGroupRows;
            set => 
                this.printGroupRows = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintTotalSummaryFooter
        {
            get => 
                this.printTotalSummaryFooter;
            set => 
                this.printTotalSummaryFooter = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintGroupSummaryFooter
        {
            get => 
                this.printGroupSummaryFooter;
            set => 
                this.printGroupSummaryFooter = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintHorizontalLines
        {
            get => 
                this.printHorizontalLines;
            set => 
                this.printHorizontalLines = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean PrintVerticalLines
        {
            get => 
                this.printVerticalLines;
            set => 
                this.printVerticalLines = value;
        }

        [XtraSerializableProperty, DefaultValue(1), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean UsePrintAppearances
        {
            get => 
                this.usePrintAppearances;
            set => 
                this.usePrintAppearances = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean EnablePrintAppearanceEvenRow
        {
            get => 
                this.enablePrintAppearanceEvenRow;
            set => 
                this.enablePrintAppearanceEvenRow = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean EnablePrintAppearanceOddRow
        {
            get => 
                this.enablePrintAppearanceOddRow;
            set => 
                this.enablePrintAppearanceOddRow = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AutoFitToPageWidth
        {
            get => 
                this.autoFitToPageWidth;
            set => 
                this.autoFitToPageWidth = value;
        }
    }
}

