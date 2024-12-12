namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IMapiRecipDesc
    {
        int reserved { get; set; }

        int recipientClass { get; set; }

        string name { get; set; }

        string address { get; set; }

        int eIDSize { get; set; }

        IntPtr entryID { get; set; }
    }
}

