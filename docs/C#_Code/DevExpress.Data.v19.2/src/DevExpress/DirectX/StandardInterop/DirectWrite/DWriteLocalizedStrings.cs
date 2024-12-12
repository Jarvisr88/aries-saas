namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Runtime.InteropServices;

    public class DWriteLocalizedStrings : ComObject<IDWriteLocalizedStrings>
    {
        internal DWriteLocalizedStrings(IDWriteLocalizedStrings nativeObject) : base(nativeObject)
        {
        }

        public void FindLocaleName(string localeName, out int index, out bool exisit)
        {
            base.WrappedObject.FindLocaleName(localeName, out index, out exisit);
        }

        public string GetLocaleName(int index)
        {
            int num;
            base.WrappedObject.GetLocaleNameLength(index, out num);
            string str = new string('\0', num);
            base.WrappedObject.GetLocaleName(index, str, num + 1);
            return str;
        }

        public string GetString(int index)
        {
            int num;
            base.WrappedObject.GetStringLength(index, out num);
            string str = new string('\0', num);
            base.WrappedObject.GetString(index, str, num + 1);
            return str;
        }
    }
}

