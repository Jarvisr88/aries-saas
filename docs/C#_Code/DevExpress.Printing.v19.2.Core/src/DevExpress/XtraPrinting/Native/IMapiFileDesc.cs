namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IMapiFileDesc
    {
        int reserved { get; set; }

        int flags { get; set; }

        int position { get; set; }

        string pathName { get; set; }

        string fileName { get; set; }

        IntPtr fileType { get; set; }
    }
}

