namespace DevExpress.Xpo.Logger
{
    using System;

    [Serializable]
    public class LogMessageParameter
    {
        private string name;
        private object parameterValue;

        public LogMessageParameter()
        {
        }

        public LogMessageParameter(string name, object value)
        {
            this.name = name;
            this.parameterValue = value;
        }

        public override string ToString() => 
            $"Name: {this.name} Value: {this.parameterValue}";

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public object Value
        {
            get => 
                this.parameterValue;
            set => 
                this.parameterValue = value;
        }
    }
}

