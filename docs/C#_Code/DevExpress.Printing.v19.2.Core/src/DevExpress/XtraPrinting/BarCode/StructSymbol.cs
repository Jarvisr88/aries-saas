namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct StructSymbol
    {
        public int MinValue;
        public int MaxValue;
        public int OddModules;
        public int EvenModules;
        public int OddWidestModule;
        public int EvenWidestModules;
        public int OddNumberCombinations;
        public int EvenNumberCombinations;
        public StructSymbol(int minValue, int maxValue, int oddModules, int evenModules, int oddWidestModule, int evenWidestModules, int oddNumberCombinations, int evenNumberCombinations);
    }
}

