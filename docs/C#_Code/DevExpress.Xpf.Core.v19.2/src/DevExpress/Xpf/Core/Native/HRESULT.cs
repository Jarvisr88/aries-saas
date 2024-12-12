namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Explicit)]
    internal struct HRESULT
    {
        [FieldOffset(0)]
        private readonly uint _value;
        public static readonly HRESULT S_OK;
        public static readonly HRESULT S_FALSE;
        public static readonly HRESULT E_PENDING;
        public static readonly HRESULT E_NOTIMPL;
        public static readonly HRESULT E_NOINTERFACE;
        public static readonly HRESULT E_POINTER;
        public static readonly HRESULT E_ABORT;
        public static readonly HRESULT E_FAIL;
        public static readonly HRESULT E_UNEXPECTED;
        public static readonly HRESULT STG_E_INVALIDFUNCTION;
        public static readonly HRESULT REGDB_E_CLASSNOTREG;
        public static readonly HRESULT DESTS_E_NO_MATCHING_ASSOC_HANDLER;
        public static readonly HRESULT DESTS_E_NORECDOCS;
        public static readonly HRESULT DESTS_E_NOTALLCLEARED;
        public static readonly HRESULT E_ACCESSDENIED;
        public static readonly HRESULT E_OUTOFMEMORY;
        public static readonly HRESULT E_INVALIDARG;
        public static readonly HRESULT INTSAFE_E_ARITHMETIC_OVERFLOW;
        public static readonly HRESULT COR_E_OBJECTDISPOSED;
        public static readonly HRESULT WC_E_GREATERTHAN;
        public static readonly HRESULT WC_E_SYNTAX;

        static HRESULT();
        public HRESULT(uint i);
        [SecuritySafeCritical]
        private static Exception CreateWin32Exception(int code, string message);
        public override bool Equals(object obj);
        public static int GetCode(int error);
        public static DevExpress.Xpf.Core.Native.Facility GetFacility(int errorCode);
        public override int GetHashCode();
        public static HRESULT Make(bool severe, DevExpress.Xpf.Core.Native.Facility facility, int code);
        public static bool operator ==(HRESULT hrLeft, HRESULT hrRight);
        public static bool operator !=(HRESULT hrLeft, HRESULT hrRight);
        public void ThrowIfFailed();
        public void ThrowIfFailed(string message);
        [SecurityCritical]
        public static void ThrowLastError();
        public override string ToString();

        public DevExpress.Xpf.Core.Native.Facility Facility { get; }

        public int Code { get; }

        public bool Succeeded { get; }

        public bool Failed { get; }
    }
}

