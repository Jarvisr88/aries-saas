namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IMapiMessage
    {
        int reserved { get; set; }

        string subject { get; set; }

        string noteText { get; set; }

        string messageType { get; set; }

        string dateReceived { get; set; }

        string conversationID { get; set; }

        int flags { get; set; }

        IntPtr originator { get; set; }

        int recipientCount { get; set; }

        IntPtr recipients { get; set; }

        int fileCount { get; set; }

        IntPtr files { get; set; }
    }
}

