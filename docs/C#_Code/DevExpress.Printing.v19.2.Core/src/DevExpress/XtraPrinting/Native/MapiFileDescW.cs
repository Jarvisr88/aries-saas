namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public class MapiFileDescW : IMapiFileDesc
    {
        private int m_reserved;
        private int m_flags;
        private int m_position;
        private string m_pathName;
        private string m_fileName;
        private IntPtr m_fileType;
        public int reserved
        {
            get => 
                this.m_reserved;
            set => 
                this.m_reserved = value;
        }
        public int flags
        {
            get => 
                this.m_flags;
            set => 
                this.m_flags = value;
        }
        public int position
        {
            get => 
                this.m_position;
            set => 
                this.m_position = value;
        }
        public string pathName
        {
            get => 
                this.m_pathName;
            set => 
                this.m_pathName = value;
        }
        public string fileName
        {
            get => 
                this.m_fileName;
            set => 
                this.m_fileName = value;
        }
        public IntPtr fileType
        {
            get => 
                this.m_fileType;
            set => 
                this.m_fileType = value;
        }
    }
}

