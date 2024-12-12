namespace DevExpress.Data.Camera
{
    using DevExpress.Data.Camera.Interfaces;
    using System;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security;

    [SecuritySafeCritical]
    public class CameraDeviceInfo : IComparable
    {
        public readonly string Name;
        public readonly string MonikerString;

        internal CameraDeviceInfo(IMoniker moniker);
        public CameraDeviceInfo(string monikerString);
        public CameraDeviceInfo(string monikerString, string name);
        public int CompareTo(object value);
        internal static IBaseFilter CreateFilter(string filterMoniker);
        internal static IMoniker GetMoniker(string filterMoniker);
        private string GetMonikerString(IMoniker moniker);
        private string GetName(IMoniker moniker);
        private string GetName(string monikerString);
        public override string ToString();
    }
}

