﻿namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    internal static class PropVariantNativeMethods
    {
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromBooleanVector([In, MarshalAs(UnmanagedType.LPArray)] bool[] prgf, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromDoubleVector([In, Out] double[] prgn, uint cElems, [Out] PropVariant propvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME pftIn, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromFileTimeVector([In, Out] System.Runtime.InteropServices.ComTypes.FILETIME[] prgft, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromInt16Vector([In, Out] short[] prgn, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromInt32Vector([In, Out] int[] prgn, uint cElems, [Out] PropVariant propVar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromInt64Vector([In, Out] long[] prgn, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromPropVariantVectorElem([In] PropVariant propvarIn, uint iElem, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromStringVector([In, Out] string[] prgsz, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromUInt16Vector([In, Out] ushort[] prgn, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromUInt32Vector([In, Out] uint[] prgn, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void InitPropVariantFromUInt64Vector([In, Out] ulong[] prgn, uint cElems, [Out] PropVariant ppropvar);
        [DllImport("Ole32.dll", PreserveSig=false)]
        internal static extern void PropVariantClear([In, Out] PropVariant pvar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetBooleanElem([In] PropVariant propVar, [In] uint iElem, [MarshalAs(UnmanagedType.Bool)] out bool pfVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetDoubleElem([In] PropVariant propVar, [In] uint iElem, out double pnVal);
        [return: MarshalAs(UnmanagedType.I4)]
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        internal static extern int PropVariantGetElementCount([In] PropVariant propVar);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetFileTimeElem([In] PropVariant propVar, [In] uint iElem, [MarshalAs(UnmanagedType.Struct)] out System.Runtime.InteropServices.ComTypes.FILETIME pftVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetInt16Elem([In] PropVariant propVar, [In] uint iElem, out short pnVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetInt32Elem([In] PropVariant propVar, [In] uint iElem, out int pnVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetInt64Elem([In] PropVariant propVar, [In] uint iElem, out long pnVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetStringElem([In] PropVariant propVar, [In] uint iElem, [MarshalAs(UnmanagedType.LPWStr)] ref string ppszVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetUInt16Elem([In] PropVariant propVar, [In] uint iElem, out ushort pnVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetUInt32Elem([In] PropVariant propVar, [In] uint iElem, out uint pnVal);
        [DllImport("propsys.dll", CharSet=CharSet.Unicode, SetLastError=true, PreserveSig=false)]
        internal static extern void PropVariantGetUInt64Elem([In] PropVariant propVar, [In] uint iElem, out ulong pnVal);
        [DllImport("OleAut32.dll", PreserveSig=false)]
        internal static extern IntPtr SafeArrayAccessData(IntPtr psa);
        [DllImport("OleAut32.dll")]
        internal static extern IntPtr SafeArrayCreateVector(ushort vt, int lowerBound, uint cElems);
        [DllImport("OleAut32.dll")]
        internal static extern uint SafeArrayGetDim(IntPtr psa);
        [return: MarshalAs(UnmanagedType.IUnknown)]
        [DllImport("OleAut32.dll", PreserveSig=false)]
        internal static extern object SafeArrayGetElement(IntPtr psa, ref int rgIndices);
        [DllImport("OleAut32.dll", PreserveSig=false)]
        internal static extern int SafeArrayGetLBound(IntPtr psa, uint nDim);
        [DllImport("OleAut32.dll", PreserveSig=false)]
        internal static extern int SafeArrayGetUBound(IntPtr psa, uint nDim);
        [DllImport("OleAut32.dll", PreserveSig=false)]
        internal static extern void SafeArrayUnaccessData(IntPtr psa);
    }
}

