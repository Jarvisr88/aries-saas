namespace DevExpress.Office.Drawing
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class AdjustableCoordinate
    {
        public AdjustableCoordinate()
        {
        }

        public AdjustableCoordinate(double value)
        {
            this.ValueEMU = value;
        }

        public AdjustableCoordinate(double value, string guideName)
        {
            this.ValueEMU = value;
            this.GuideName = guideName;
        }

        public override bool Equals(object obj)
        {
            AdjustableCoordinate coordinate = obj as AdjustableCoordinate;
            return ((coordinate != null) ? ((this.ValueEMU == coordinate.ValueEMU) && string.Equals(this.GuideName, coordinate.GuideName, StringComparison.OrdinalIgnoreCase)) : false);
        }

        public double Evaluate(IShapeGuideCalculator calculator) => 
            this.Constant ? this.ValueEMU : calculator.GetGuideValue(this.GuideName);

        public static AdjustableCoordinate FromString(string value)
        {
            long num;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            AdjustableCoordinate coordinate = new AdjustableCoordinate();
            if (long.TryParse(value, out num))
            {
                coordinate.ValueEMU = num;
            }
            else
            {
                coordinate.GuideName = value;
            }
            return coordinate;
        }

        public override int GetHashCode()
        {
            int hashCode = this.ValueEMU.GetHashCode();
            if (this.GuideName != null)
            {
                hashCode ^= this.GuideName.GetHashCode();
            }
            return hashCode;
        }

        public override string ToString() => 
            string.IsNullOrEmpty(this.GuideName) ? this.ValueEMU.ToString(CultureInfo.InvariantCulture) : this.GuideName;

        public double ValueEMU { get; private set; }

        public string GuideName { get; private set; }

        public bool Constant =>
            string.IsNullOrEmpty(this.GuideName);
    }
}

