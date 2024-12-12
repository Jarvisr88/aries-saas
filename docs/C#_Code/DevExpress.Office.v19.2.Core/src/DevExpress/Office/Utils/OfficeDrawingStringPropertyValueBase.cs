namespace DevExpress.Office.Utils
{
    using System;
    using System.Text;

    public abstract class OfficeDrawingStringPropertyValueBase : OfficeDrawingIntPropertyBase
    {
        private string data;

        protected OfficeDrawingStringPropertyValueBase()
        {
        }

        private string GetStringData()
        {
            if (string.IsNullOrEmpty(this.data))
            {
                this.data = Encoding.Unicode.GetString(base.ComplexData, 0, base.ComplexData.Length);
            }
            return this.data;
        }

        private void SetStringData(string value)
        {
            if (this.data != value)
            {
                this.data = value;
                base.SetComplexData(Encoding.Unicode.GetBytes(value));
                base.Value = base.ComplexData.Length;
            }
        }

        public override bool Complex =>
            true;

        public string Data
        {
            get => 
                this.GetStringData();
            set => 
                this.SetStringData(value);
        }
    }
}

