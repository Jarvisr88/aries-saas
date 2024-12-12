namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public class MapiRecipDescA : IMapiRecipDesc
    {
        private int m_reserved;
        private int m_recipientClass;
        private string m_name;
        private string m_address;
        private int m_eIDSize;
        private IntPtr m_entryID;
        public int reserved
        {
            get => 
                this.m_reserved;
            set => 
                this.m_reserved = value;
        }
        public int recipientClass
        {
            get => 
                this.m_recipientClass;
            set => 
                this.m_recipientClass = value;
        }
        public string name
        {
            get => 
                this.m_name;
            set => 
                this.m_name = value;
        }
        public string address
        {
            get => 
                this.m_address;
            set => 
                this.m_address = value;
        }
        public int eIDSize
        {
            get => 
                this.m_eIDSize;
            set => 
                this.m_eIDSize = value;
        }
        public IntPtr entryID
        {
            get => 
                this.m_entryID;
            set => 
                this.m_entryID = value;
        }
    }
}

