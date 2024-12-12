namespace DevExpress.Utils.About
{
    using System;

    [Flags]
    public enum ProductKind : long
    {
        Default = 0L,
        DXperienceWin = 1L,
        XtraReports = 0x10L,
        DemoWin = 0x2000L,
        XPO = 0x8000L,
        DXperienceASP = 0x2000000L,
        XAF = 0x10000000L,
        DXperienceWPF = 0x4000000000L,
        DXperienceSliverlight = 0x8000000000L,
        LightSwitchReports = 0x200000000000L,
        Dashboard = 0x800000000000L,
        CodedUIWin = 0x1000000000000L,
        Snap = 0x2000000000000L,
        Docs = 0x80000000000000L,
        XtraReportsWpf = 0x200000000000000L,
        XtraReportsSL = 0x400000000000000L,
        XtraReportsWeb = 0x800000000000000L,
        XtraReportsWin = 0x1000000000000000L,
        FreeOffer = 0x4000000000000000L,
        DXperiencePro = 0x2000000000011L,
        DXperienceEnt = 0x8200c002008011L,
        DXperienceUni = 0x8280c012008011L
    }
}

