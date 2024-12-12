namespace DevExpress.Data.Helpers
{
    using System;

    public class ServerModeEtcValue
    {
        private object value;
        private bool isDesc;

        public ServerModeEtcValue(object value, bool isDesc);
        public override string ToString();

        public object Value { get; }

        public bool IsDesc { get; }
    }
}

