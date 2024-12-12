namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public class PdfExportException : Exception
    {
        private const string message = "The specified PDF export options are not valid. Retrieve the ValidationErrors property for more information.";
        [NonSerialized]
        private PdfExportExceptionState state;

        public PdfExportException(IList<string> validationErrors) : base("The specified PDF export options are not valid. Retrieve the ValidationErrors property for more information.")
        {
            this.state = new PdfExportExceptionState();
            this.state.ValidationErrors = validationErrors;
            this.HandleSerialization();
        }

        private void HandleSerialization()
        {
            base.SerializeObjectState += (exception, args) => args.AddSerializedState(this.state);
        }

        public IList<string> ValidationErrors =>
            this.state.ValidationErrors;

        [Serializable]
        private class PdfExportExceptionState : ISafeSerializationData
        {
            private IList<string> validationErrors;

            public void CompleteDeserialization(object deserialized)
            {
                PdfExportException exception = deserialized as PdfExportException;
                if (exception != null)
                {
                    exception.HandleSerialization();
                    exception.state = this;
                }
            }

            public IList<string> ValidationErrors
            {
                get => 
                    this.validationErrors;
                set => 
                    this.validationErrors = value;
            }
        }
    }
}

