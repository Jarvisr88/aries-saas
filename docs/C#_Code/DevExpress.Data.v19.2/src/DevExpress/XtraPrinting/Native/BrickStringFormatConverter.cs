namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Text;

    public class BrickStringFormatConverter : StructStringConverter
    {
        public static readonly BrickStringFormatConverter Instance = new BrickStringFormatConverter();

        private BrickStringFormatConverter()
        {
        }

        protected override object CreateObject(string[] values) => 
            new BrickStringFormat((StringAlignment) Enum.Parse(typeof(StringAlignment), values[0], true), (StringAlignment) Enum.Parse(typeof(StringAlignment), values[1], true), (StringFormatFlags) Enum.Parse(typeof(StringFormatFlags), values[2], true), (HotkeyPrefix) Enum.Parse(typeof(HotkeyPrefix), values[3], true), (StringTrimming) Enum.Parse(typeof(StringTrimming), values[4], true)) { PrototypeKind = (BrickStringFormatPrototypeKind) Enum.Parse(typeof(BrickStringFormatPrototypeKind), values[5], true) };

        protected override string[] GetValues(object obj)
        {
            BrickStringFormat format = (BrickStringFormat) obj;
            return new string[] { format.Alignment.ToString(), format.LineAlignment.ToString(), format.FormatFlags.ToString(), format.HotkeyPrefix.ToString(), format.Trimming.ToString(), format.PrototypeKind.ToString() };
        }

        public override System.Type Type =>
            typeof(BrickStringFormat);

        protected override char Delimiter =>
            ';';
    }
}

