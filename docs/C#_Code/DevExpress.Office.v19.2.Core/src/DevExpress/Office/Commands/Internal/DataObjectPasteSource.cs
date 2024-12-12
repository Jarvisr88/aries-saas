namespace DevExpress.Office.Commands.Internal
{
    using System;
    using System.Security;
    using System.Windows.Forms;

    public class DataObjectPasteSource : PasteSource
    {
        private IDataObject dataObject;

        public DataObjectPasteSource(IDataObject dataObject)
        {
            this.dataObject = dataObject;
        }

        public override bool ContainsData(string format, bool autoConvert)
        {
            try
            {
                return ((this.dataObject != null) && this.dataObject.GetDataPresent(format, autoConvert));
            }
            catch (SecurityException)
            {
                return false;
            }
        }

        public override object GetData(string format, bool autoConvert)
        {
            try
            {
                return this.dataObject?.GetData(format, autoConvert);
            }
            catch (SecurityException)
            {
                return null;
            }
        }

        public IDataObject DataObject
        {
            get => 
                this.dataObject;
            set => 
                this.dataObject = value;
        }
    }
}

