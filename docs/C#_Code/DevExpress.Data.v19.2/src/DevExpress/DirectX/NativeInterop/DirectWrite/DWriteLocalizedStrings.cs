namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    public class DWriteLocalizedStrings : ComObject
    {
        internal DWriteLocalizedStrings(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public bool FindLocaleName(string localeName, out int index)
        {
            int num;
            using (StringMarshaler marshaler = new StringMarshaler(localeName))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, out index, out num, 4));
            }
            return (num != 0);
        }

        public int GetCount() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 3);

        private int GetLength(int index, int methodIndex)
        {
            int num;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, out num, methodIndex));
            return num;
        }

        public string GetLocaleName(int index) => 
            this.GetString(index, this.GetLocaleNameLength(index), 6);

        public int GetLocaleNameLength(int index) => 
            this.GetLength(index, 5);

        public string GetString(int index) => 
            this.GetString(index, this.GetStringLength(index), 8);

        private string GetString(int index, int bufferSize, int methodIndex)
        {
            char[] chArray = new char[bufferSize + 1];
            using (ArrayMarshaler marshaler = new ArrayMarshaler(chArray))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, marshaler.Pointer, chArray.Length, methodIndex));
            }
            return new string(chArray, 0, chArray.Length - 1);
        }

        public int GetStringLength(int index) => 
            this.GetLength(index, 7);
    }
}

