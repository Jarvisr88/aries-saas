namespace DevExpress.Xpf.Printing.Parameters.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ValidateParameterEventArgs : EventArgs
    {
        public ValidateParameterEventArgs(DevExpress.Xpf.Printing.Parameters.Models.ParameterModel model)
        {
            this.ParameterModel = model;
        }

        public DevExpress.Xpf.Printing.Parameters.Models.ParameterModel ParameterModel { get; private set; }

        public string Error { get; set; }
    }
}

