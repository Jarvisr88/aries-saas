namespace DevExpress.Printing.Utils.DocumentStoring
{
    using System;

    public abstract class StorableObjectBase : IStorableObject
    {
        private StoredID storedID = StoredID.Undefined;

        protected StorableObjectBase()
        {
        }

        StoredID IStorableObject.StoredID
        {
            get => 
                this.storedID;
            set => 
                this.storedID = value;
        }
    }
}

