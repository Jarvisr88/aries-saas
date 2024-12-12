namespace DevExpress.Office.Utils
{
    using System;

    public class ValueInfo
    {
        private static readonly ValueInfo empty = new ValueInfo();
        private string unit;
        private float value;
        private bool isValidNumber;

        public ValueInfo()
        {
            this.unit = string.Empty;
        }

        public ValueInfo(string unit)
        {
            this.isValidNumber = false;
            this.unit = unit;
        }

        public ValueInfo(float value, string unit)
        {
            this.isValidNumber = true;
            this.unit = unit;
            this.value = value;
        }

        public static ValueInfo Empty =>
            empty;

        public string Unit =>
            this.unit;

        public float Value =>
            this.value;

        public bool IsValidNumber =>
            this.isValidNumber;

        public bool IsEmpty =>
            ReferenceEquals(this, Empty);
    }
}

