namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public class MapiMessageA : IMapiMessage
    {
        private int m_reserved;
        private string m_subject;
        private string m_noteText;
        private string m_messageType;
        private string m_dateReceived;
        private string m_conversationID;
        private int m_flags;
        private IntPtr m_originator;
        private int m_recipientCount;
        private IntPtr m_recipients;
        private int m_fileCount;
        private IntPtr m_files;
        public int reserved
        {
            get => 
                this.m_reserved;
            set => 
                this.m_reserved = value;
        }
        public string subject
        {
            get => 
                this.m_subject;
            set => 
                this.m_subject = value;
        }
        public string noteText
        {
            get => 
                this.m_noteText;
            set => 
                this.m_noteText = value;
        }
        public string messageType
        {
            get => 
                this.m_messageType;
            set => 
                this.m_messageType = value;
        }
        public string dateReceived
        {
            get => 
                this.m_dateReceived;
            set => 
                this.m_dateReceived = value;
        }
        public string conversationID
        {
            get => 
                this.m_conversationID;
            set => 
                this.m_conversationID = value;
        }
        public int flags
        {
            get => 
                this.m_flags;
            set => 
                this.m_flags = value;
        }
        public IntPtr originator
        {
            get => 
                this.m_originator;
            set => 
                this.m_originator = value;
        }
        public int recipientCount
        {
            get => 
                this.m_recipientCount;
            set => 
                this.m_recipientCount = value;
        }
        public IntPtr recipients
        {
            get => 
                this.m_recipients;
            set => 
                this.m_recipients = value;
        }
        public int fileCount
        {
            get => 
                this.m_fileCount;
            set => 
                this.m_fileCount = value;
        }
        public IntPtr files
        {
            get => 
                this.m_files;
            set => 
                this.m_files = value;
        }
    }
}

