namespace DevExpress.Office.PInvoke
{
    using System;
    using System.Runtime.InteropServices;

    public class SimpleSecurityInformation : ISecurityInformation
    {
        private Win32.SI_OBJECT_INFO objectInfo;
        private Win32.SI_ACCESS[] accessList = new Win32.SI_ACCESS[0];
        private Win32.GENERIC_MAPPING mapping;

        public virtual void GetAccessRight(IntPtr guidObject, int dwFlags, out Win32.SI_ACCESS[] access, ref int accessCount, ref int defaultAccess)
        {
            access = this.accessList;
            accessCount = this.accessList.Length;
            defaultAccess = 0;
        }

        public virtual void GetInheritTypes(ref Win32.SI_INHERIT_TYPE inheritType, IntPtr inheritTypesCount)
        {
        }

        public virtual void GetObjectInformation(ref Win32.SI_OBJECT_INFO objectInfo)
        {
            objectInfo = this.objectInfo;
        }

        public virtual void GetSecurity(int requestInformation, IntPtr ppSecurityDescriptor, bool fDefault)
        {
        }

        public virtual void MapGeneric(IntPtr guidObjectType, IntPtr aceFlags, IntPtr mask)
        {
            Win32.MapGenericMask(mask, ref this.mapping);
        }

        public virtual void PropertySheetPageCallback(IntPtr hwnd, int uMsg, int uPage)
        {
        }

        public virtual void SetSecurity(int requestInformation, IntPtr securityDescriptor)
        {
        }

        public Win32.SI_OBJECT_INFO ObjectInfo
        {
            get => 
                this.objectInfo;
            set => 
                this.objectInfo = value;
        }

        public Win32.SI_ACCESS[] AccessList
        {
            get => 
                this.accessList;
            set => 
                this.accessList = value;
        }

        public Win32.GENERIC_MAPPING GenericMapping
        {
            get => 
                this.mapping;
            set => 
                this.mapping = value;
        }

        public string ObjectName
        {
            get => 
                this.objectInfo.szObjectName;
            set => 
                this.objectInfo.szObjectName = value;
        }
    }
}

