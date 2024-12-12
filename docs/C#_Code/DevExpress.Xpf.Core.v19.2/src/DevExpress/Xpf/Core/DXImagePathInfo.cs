namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Design;
    using System;

    public class DXImagePathInfo : DXImageInfo
    {
        private const string startName = "pack://application:,,,/";
        private const string endName = ";component/";

        public DXImagePathInfo(IDXImageInfo info) : base(info)
        {
        }

        public static string GetFullName(string shortName) => 
            "pack://application:,,,/DevExpress.Images.v19.2;component/" + shortName;

        public static string GetShortName(string fullName) => 
            fullName.Replace("pack://application:,,,/DevExpress.Images.v19.2;component/", "");

        public override string ToString() => 
            GetShortName(base.info.MakeUri());
    }
}

