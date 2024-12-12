namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class DataBarFormatInfo
    {
        public static DataBarFormatInfo AddDataBarFormatInfo(DataBarFormatInfo info, DataBarFormat format, double zeroPosition, double value)
        {
            DataBarFormatInfo result = new DataBarFormatInfo();
            AssignDataBarFormatInfo(result, format, zeroPosition, value);
            if (info != null)
            {
                AssignIcon(result, info.Icon, info.IconVerticalAlignment);
            }
            return result;
        }

        public static DataBarFormatInfo AddIcon(DataBarFormatInfo info, ImageSource icon, VerticalAlignment verticalAlignment)
        {
            DataBarFormatInfo result = new DataBarFormatInfo();
            if (info != null)
            {
                result.Format = info.Format;
                result.ZeroPosition = info.ZeroPosition;
                result.ValuePosition = info.ValuePosition;
            }
            AssignIcon(result, icon, verticalAlignment);
            return result;
        }

        private static void AssignDataBarFormatInfo(DataBarFormatInfo result, DataBarFormat format, double zeroPosition, double value)
        {
            result.Format = (DataBarFormat) format.GetCurrentValueAsFrozen();
            result.ZeroPosition = zeroPosition;
            result.ValuePosition = value;
        }

        private static void AssignIcon(DataBarFormatInfo result, ImageSource icon, VerticalAlignment verticalAlignment)
        {
            result.Icon = icon;
            result.IconVerticalAlignment = verticalAlignment;
        }

        public override bool Equals(object obj)
        {
            DataBarFormatInfo info = obj as DataBarFormatInfo;
            return ((info != null) && (ReferenceEquals(info.Format, this.Format) && ((info.ZeroPosition == this.ZeroPosition) && ((info.ValuePosition == this.ValuePosition) && (ReferenceEquals(info.Icon, this.Icon) && (info.IconVerticalAlignment == this.IconVerticalAlignment))))));
        }

        public override int GetHashCode()
        {
            int num = (this.ZeroPosition.GetHashCode() ^ this.ValuePosition.GetHashCode()) ^ this.IconVerticalAlignment.GetHashCode();
            if (this.Format != null)
            {
                num ^= this.Format.GetHashCode();
            }
            if (this.Icon != null)
            {
                num ^= this.Icon.GetHashCode();
            }
            return num;
        }

        public static DataBarFormatInfo ModifyValuePosition(DataBarFormatInfo info, double valuePosition) => 
            (info != null) ? AddDataBarFormatInfo(info, info.Format, info.ZeroPosition, valuePosition) : null;

        public DataBarFormat Format { get; private set; }

        public double ZeroPosition { get; private set; }

        public double ValuePosition { get; private set; }

        public ImageSource Icon { get; private set; }

        public VerticalAlignment IconVerticalAlignment { get; private set; }
    }
}

