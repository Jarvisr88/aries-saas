namespace DevExpress.Data
{
    using System;

    public class ValidateControllerRowEventArgs : ControllerRowEventArgs
    {
        private bool valid;
        private string errorText;

        public ValidateControllerRowEventArgs(int rowHandle, object row);

        public bool Valid { get; set; }

        public string ErrorText { get; set; }
    }
}

