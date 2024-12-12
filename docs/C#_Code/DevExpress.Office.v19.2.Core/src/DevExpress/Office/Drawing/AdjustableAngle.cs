namespace DevExpress.Office.Drawing
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class AdjustableAngle
    {
        public AdjustableAngle()
        {
        }

        public AdjustableAngle(double value)
        {
            this.Value = value;
        }

        public AdjustableAngle(double value, string guideName)
        {
            this.Value = value;
            this.GuideName = guideName;
        }

        public override bool Equals(object obj)
        {
            AdjustableAngle angle = obj as AdjustableAngle;
            return ((angle != null) ? ((this.Value == angle.Value) && string.Equals(this.GuideName, angle.GuideName, StringComparison.OrdinalIgnoreCase)) : false);
        }

        public double Evaluate(IShapeGuideCalculator calculator) => 
            this.Constant ? this.Value : calculator.GetGuideValue(this.GuideName);

        public static AdjustableAngle FromString(string value)
        {
            int num;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            AdjustableAngle angle = new AdjustableAngle();
            if (int.TryParse(value, out num))
            {
                angle.Value = num;
            }
            else
            {
                angle.GuideName = value;
            }
            return angle;
        }

        public override int GetHashCode()
        {
            int hashCode = this.Value.GetHashCode();
            if (this.GuideName != null)
            {
                hashCode ^= this.GuideName.GetHashCode();
            }
            return hashCode;
        }

        public override string ToString() => 
            string.IsNullOrEmpty(this.GuideName) ? this.Value.ToString(CultureInfo.InvariantCulture) : this.GuideName;

        public double Value { get; private set; }

        public string GuideName { get; private set; }

        public bool Constant =>
            string.IsNullOrEmpty(this.GuideName);
    }
}

